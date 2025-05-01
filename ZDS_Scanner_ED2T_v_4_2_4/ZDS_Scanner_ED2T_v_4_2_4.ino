//=================================================================================================
//Ревлизация пульта ЭД2т на базе arduino mega 2560 для ZDSimulator 54.006
//alexval2007(Александр Бондаренко), Uletaeff (Тёма Улетаев)
//множитель для пневматики в сканере 10
// Добавлена работа звонка при включении стеклообогрева в хвостовой кабине
//ПРОВЕРИТЬ ЛАМПЫ ТЯГА, ЭДТ. СОТХ
//НАСТРОИТЬ КИЛОВОЛЬТМЕТР
//ДОБАВИТЬ ЧАСТЬ ПЕТРА
//ПРОПИСАТЬ ЗАПРЕТ ТЯГИ
//ПРОПИСАТЬ В СЕРИАЛ ТУМБЛЕРА
//НЕ ТУХНУТ ПР ВЦ СОТ БВ РБ ПОЖАР ДВЕРИ СТЕКЛО ОТ ВУ ВЫКЛ
//ВПИСАТЬ ТАЙМЕР НА ПРРБ (ОТБИВАНИЕ БВ И РАЗБОР СХЕМЫ) НА 5 СЕК
//ДОБАВИТЬ САВПЭ
//
//=================================================================================================
#include "SwitecX12.h"            //Билиотека Использование шаговых двигателей X27.168
//=================================================================================================
// Пины микроконтроллера для ШД
#define stepper1_STEP_pin  32     //f(scx)A driver  pin 28
#define stepper1_DIR_pin   34     //CW/CCW A driver pin 27
#define RESET_Pin          36     //RESET driver    pin 26
#define stepper2_STEP_pin  38     //f(scx)A driver  pin 3
#define stepper2_DIR_pin   40     //CW/CCW A driver pin 2
#define stepper3_STEP_pin  42     //f(scx)A driver  pin 14
#define stepper3_DIR_pin   44     //CW/CCW A driver pin 13
#define stepper4_STEP_pin  46     //f(scx)A driver  pin 17
#define stepper4_DIR_pin   48     //CW/CCW A driver pin 16

//стандартный диапазон X12.168  315 градусов с шагом 1/3 градуса
//макс шаги
#define STEP_1              3200
#define STEP_2              3200
#define STEP_3              3200
#define STEP_4              3200



// Пины микроконтроллера лампы и светофор
//#define als_bel_Pin                      22  //лампа белый огонь «Б» (на путевом светофоре белый огонь) показания путевых светофоров на локомотив не передаются.
#define als_k_Pin                        52  //лампа красный огонь «К» (на путевом светофоре красный огонь) запрещающий движение сигнал.
#define als_kj_Pin                       26  //лампа желтый огонь с красным «КЖ» (на путевом светофоре красно желтый огонь).
#define als_jelt_Pin                     28  //лампа желтый огонь «Ж» (на путевом светофоре желтый огонь).
#define als_zelen_Pin                    30  //лампа зеленый огонь «3» (на путевом светофоре, к которому приближается поезд, горит зеленый огонь).

#define PSS_Lamp_Pin                     23   //Лампа ПСС Бдительность
#define EPT_O_Lamp_Pin                   33   //Лампа ЭПТ-О
#define EPT_kontrol_Lamp_Pin             37   //Лампа ЭПТ-контроль.
#define EPT_T_Lamp_Pin                   31   //Лампа ЭПТ-Т
#define SOT_Lamp_Pin                     35   //Лампа СОТ (наполнение ТЦ).
#define vspom_cepi_Lamp_Pin              45   //Лампа вспомогательные цепи
#define preobrazovatel_Lamp_Pin          43   //Лампа преобразователь
#define RB_Lamp_Pin                      41   //Лампа РБ Боксование
#define LK_Lamp_Pin                      47   //Лампа ЛКиТ
#define BV_Lamp_Pin                      29   //Лампа БВ
#define Fire_Lamp_Pin                    27   //Лампа Пожароопасность 
#define RN_Lamp_Pin                      25   //Лампа РН реле контроля напряжения горит при отсуствии высокого напряжения.
#define SOTx_Lamp_Pin                    52   //Лампа СОТx (наполнение ТЦ). НЕ ПРОВЕРЯЛАСЬ
#define Tiaga_Lamp_Pin                   53   //Лампа тяга(горит при контроллере от м-4).НЕ ПРОВЕРЯЛСЯ
#define EDT_Lamp_Pin                     55   //Лампа ЭДТ (горит при т1-т5). НЕ ПРОВЕРЯЛАСЬ
#define Dveri_cont_Lamp_Pin              51   //Лампа Контроль дверей
#define Steclo_Lamp_Pin                  39   //Лампа Стеклообогрева 39

// Пины микроконтроллера приборы ШИМ
#define KS_Voltage_Pin                    2   //Вывод для прибора ШИМ Напряжение КС

//Кнопки и тумблеры
#define Zvonok_pin                        22   //Звонок, для имитации работы помощника машиниста
#define PPT_Switch_pin                    3   //pin пакетника ППТ в положении головной
#define CSO_Button_pin                    4   //pin кнопка проверки РБ, РН
#define steclo_Switch_pin                 5   //pin ТУМБЛЕР Стеклообогрева
#define BVin_Switch_pin                   6   //pin ТУМБЛЕР ВОЗВРАТ БВ
#define VUin_Switch_pin                   A7   //pin тумблер ВУ
#define EDTin_Switch_pin                  8   //pin тумблер ЭДТ
#define zapret_switch_pin                 9   //тумблер запрета тяги при НЕ закрытых дверях
#define RVT_pin                           24
#define EPK_pin                           11
//#define provod_22u_pin                   10  //pin положений т1-т5 для отклоючения лампы К
//#define provod_22sh_pin    
//#define provod_2_pin    
//#define provod_41_pin

                
//Задержки
#define PR_LAMP_DELAY                  5000   // Задержка для лампы ПР (5 секунд)
#define VC_LAMP_DELAY                  5500   // Задержка для лампы ВЦ (6.5 секунд)
#define RN_LAMP_ON_DELAY               500    // Задержка для лампы РН вкл (1 секунда)
#define RN_LAMP_OFF_DELAY              4000   // Задержка для лампы РН выкл (4 секунды)
#define ZVONOK_DELAY                  1000   // задержка для работы звонка (1 секунда)
//=================================================================================================
uint8_t rk_pos_prev = 0;                     //Предыдущее положение контроллера
unsigned long TimerProshlo_LK = 0;           //Таймер для задержки лампы ЛК
unsigned long LK_Lamp_Delay = 0;             //Случайная задержка для лампы ЛК (в миллисекундах)
bool LK_Lamp_Timer_Started = false;          //Флаг, указывающий, что таймер запущен
bool LK_Lamp_Extinguished = false;           //Флаг, указывающий, что лампа ЛК погасла

int skor_dop = 0;                            //допустимая скорость
int currentSpeed = 0;                        //текущая скорость с учетом множителя для шаговых двигаетелей
int currentSpeed2 = 0;                       //аналогично первой, только без учета    
int rk_pos = 0;                              //Позиции контроллера
int poziciya_rk = 0;                          //симуляция реостатного контроллера

//Шим приборы
int KS_Voltage = 0;                           //Напряжение контактной сети
int KS_Voltage1 = 0;                          //переменная напряжения с которой работаем в коде
int ks_vivod = 0;                             //переменная напряжения которую в последствии отправим на ШИМ
int ks_minus = 0;                             //переменная отклонения напряжения -
int ks_plus = 0;                              //переменная отклонения напряжения +
int ks_gisterezis = 20;                        //отклонение стрелки в каждую сторону от напряжения симулятора

//Давление
int NM_Pressure = 0;                          //Давление НМ
int TM_Pressure = 0;                          //Давление ТМ
int UR_Pressure = 0;                          //Давление УР
int TC_Pressure = 0;                          //Давление ТЦ

//Состояние сигналов для ламп
uint8_t BV_State = 0;                         //Состояние БВ
uint8_t RB_State = 0;                         //Состояние РБ Боксование
uint8_t LK_State = 0;                         //Состояние лк
uint8_t RN_State = 0;                         //Состояние РН реле контроля напряжения горит при отсуствии высокого напряжения.
uint8_t Dveri_cont_State = 0;                 //Состояние Контроля дверей
uint8_t Tiaga_State = 0;                      //Состояние тяги(горит при контроллере от м-4).
uint8_t EDT_State = 0;                        //Состояние ЭДТ (горит при т1-т5).
uint8_t VU_State = 0;                         //Состояние ВУ
uint8_t PR_State = 0;                         //Состояние преобразователя
uint8_t K_State = 0;                          //Состояние k эпт
uint8_t O_State = 0;                          //Состояние O эпт
uint8_t T_State = 0;                          //Состояние t эпт
uint8_t SOTx_State = 0;
uint8_t SOT_State = 0;
uint8_t otoplenie_State = 0;                  // Cостояние тумблера отопление салонов
uint8_t EDTin_State = 0;                      // Состояние тумблера ЭДТ
uint8_t steclo_State = 0;                     // Состояние тумблера стеклообогрева
uint8_t zapret_State = 0;                     // Состояние тумблера запрета тяги
uint8_t PPT_State = 0;                        // Состояние пакетника ППТ
uint8_t Zvonok_State = 0;                     // Cостояние звонка
uint8_t PRMN_State = 0;                       // Состояние ПРМН для отключения рекуперации
uint8_t RVT_State = 0;                        // Состояние выходного пина РВТ
uint8_t RVT2_State = 0;                       // Состояние РВТ2 (замещение)
uint8_t RVT3_State = 0;                       // Состояние РВТ3 (дотормаживание)
uint8_t KBX_State = 0;
uint8_t KBT_State = 0;                        // Cостояние КВT
uint8_t PRRB_State = 0;
uint8_t EPK_State = 0;
uint8_t provod_22u_State = 0;
uint8_t provod_22sh_State = 0;
uint8_t provod_2_State = 0;
uint8_t provod_41_State = 0;


//Лампы и светофор
uint8_t ALS_State = 0;                        //Светофор.
uint8_t PSS_Lamp = 0;                         //Лампа ПСС Бдительность
uint8_t EPT_O_Lamp = 0;                       //Лампа ЭПТ-О
uint8_t EPT_kontrol_Lamp = 0;                 //Лампа ЭПТ-контроль.
uint8_t EPT_T_Lamp = 0;                       //Лампа ЭПТ-Т
uint8_t SOT_Lamp = 0;                         //Лампа СОТ (наполнение ТЦ).
uint8_t vspom_cepi_Lamp = 0;                  //Лампа вспомогательные цепи
uint8_t preobrazovatel_Lamp = 0;              //Лампа преобразователь
uint8_t RB_Lamp = 0;                          //Лампа РБ Боксование
uint8_t LK_Lamp = 0;                          //Лампа ЛКиТ
uint8_t BV_Lamp = 0;                          //Лампа БВ
uint8_t Fire_Lamp  = 0;                       //Лампа Пожароопасность
uint8_t RN_Lamp = 0;                          //Лампа РН реле контроля напряжения горит при отсуствии высокого напряжения.
uint8_t SOTx_Lamp = 0;                        //Лампа СОТx (наполнение ТЦ).ОТКЛЮЧЕН
uint8_t Tiaga_Lamp_P = 0;                     //Лампа тяга(горит при контроллере от м-4).ОТКЛЮЧЕН
uint8_t EDT_Lamp = 0;                         //Лампа ЭДТ (горит при т1-т5).ОТКЛЮЧЕН
uint8_t Dveri_cont_Lamp = 0;                  //Лампа Контроль дверей
uint8_t Steclo_Lamp = 0;                      //Лампа Стеклообогрева

//Время
uint8_t chas = 0;                             //игровые часы: часы
uint8_t minuta = 0;                           //игровые часы: минуты
uint8_t sekunda = 0;                          //игровые часы: секунды

//Флаги
uint8_t preobrazovatel_flag = 0;              //флаг преобразователь.
uint8_t vspom_cepi_flag = 0;                  //флаг вспомогательные цепи.
uint8_t RN_State_flag = 0;                    //флаг состояния РН (реле напряжения).
uint8_t SOT_flag = 0;
uint8_t SOTx_flag = 0;                        //флаг СОТx. ОТКЛЮЧЕН
uint8_t Dveri_cont_flag = 0;                  //флаг контроль дверей.
uint8_t BV_flag = 0;                          //флаг лампы БВ
uint8_t CSO_Button_flag = 0;                  //флаг кнопки CSO проверка ламп
uint8_t VU_Button_flag = 0;                   //флаг тумблера ВУ
uint8_t lk_flag = 0;                          //Флаг лампы ЛКиТ
uint8_t K_flag = 0;                           //флаг лампы к эпт
uint8_t EDTin_flag = 0;                       //флаг тумблера эдт
uint8_t zapret_flag = 0;                      //флаг тублера запрета тяги
uint8_t PPT_flag = 0;                         //флаг пакетника ППТ
uint8_t Zvonok_flag = 0;                    
uint8_t dir = 1;                              //направление отклонения стрелки киломольтметра
uint8_t RVT3_flag = 0;
uint8_t RVT2_flag =0;

uint8_t sz = 0;
uint8_t buffer[256];                          //Приемный буфер из сканера

//Для светофора
//значение АЛС. 0 - выкл эпк, 1 - код белый, 2 - код красный, 3 - код красно-желтый(подход к запрещающему), 4- код желтый, 5 - код зеленый
const uint8_t ALSNvalues[] = {0x07, 0x00, 0x01, 0x02, 0x03, 0x04};
//=================================================================================================
SwitecX12 stepper1(STEP_1, stepper1_STEP_pin, stepper1_DIR_pin); 
SwitecX12 stepper2(STEP_2, stepper2_STEP_pin, stepper2_DIR_pin);
SwitecX12 stepper3(STEP_3, stepper3_STEP_pin, stepper3_DIR_pin);
SwitecX12 stepper4(STEP_4, stepper4_STEP_pin, stepper4_DIR_pin); 
//=================================================================================================
unsigned long TimerOld = 0;       //Предыдущее время Millis для цикла 0,4с
unsigned long TimerOld_bdit = 0;  //Предыдущее время Millis для мигания лампы бдительность.
unsigned long TimerTek = 0;       //Текущее время в Millis
unsigned long TimerProshlo = 0;   //Прошедшее с предыдущего время в Millis

//Таймеры
unsigned long TimerOld_display = 0;             //Предыдущее время display
unsigned long TimerProshlo_display = 0;         //Прошедшее с предыдущего display время.

unsigned long TimerOld_lampShow = 0;             //Предыдущее время lampShow
unsigned long TimerProshlo_lampShow = 0;         //Прошедшее с предыдущего lampShow время.

unsigned long TimerProshlo_bdit = 0;            //Прошедшее с предыдущего bdit время.
unsigned long TimerProshlo_pwr_cont = 0;        //Прошедшее с предыдущего pwr_cont время.
unsigned long TimerProshlo_SOTx = 0;            //Прошедшее с предыдущего SOTx время.
unsigned long TimerProshlo_dveri_cont = 0;      //Прошедшее с предыдущего dveri_cont время.
unsigned long TimerProshlo_preobrazovatel = 0;  //Прошедшее с предыдущего preobrazovatel время.
unsigned long TimerProshlo_vspom_cepi = 0;      //Прошедшее с предыдущего vspom_cepi время.
unsigned long TimerProshlo_zvonok = 0;   
unsigned long TimerProshlo_otklonenie_kiloamp = 0;  //Прошедшее с предыдущего otklonenie_kiloamp время.
unsigned long TimerProshlo_perekluchenie_rk = 0;    //Прошедшее с предыдущего perekluchenie_rk время.
unsigned long TimerProshlo_Tokopriemnik = 0;
unsigned long TimerProshlo_PRRB = 0;
unsigned long TimerProshlo_RVT = 0;
unsigned long zaderzka_ot_rk = 0;
//=================================================================================================
//Получаем данные из сима
//=================================================================================================
int8_t processSerial() 
{
  uint8_t estimated = 0;
  if (sz > 255) sz = 0;
  
  estimated = Serial.available();
  
  if(estimated) 
  {
    Serial.readBytes(&buffer[sz], estimated);
    sz += estimated;
    
    if (buffer[sz] == 0xD0 && buffer[sz - 1] == 0xC0 && buffer[sz - 2] == 0xB0 && buffer[sz - 3] == 0xA0)  
    {
      if (sz >= 63) return sz - 63;
    }
    
  }
  return -1;
}
//===============================================================
//Состояние стрелочных приборов управляемых ШИМ
//===============================================================
void showGauges_pwm(int _KS_Voltage) 
{
  
  if(_KS_Voltage > 0){
    if(TimerProshlo_Tokopriemnik < TimerTek){
  _KS_Voltage = _KS_Voltage * 4;//костыль для вставки блока ниже
  
    if(rk_pos == 1 && (KBX_State == 1 && poziciya_rk == 0)){
      //выполняется, когда позиция м
      zaderzka_ot_rk = millis() + 12000;
      if(currentSpeed2 <= 20){ //если скорость меньше 20 
     KS_Voltage1 = _KS_Voltage - (200 - (currentSpeed2*10));//максимальная просадка 200 вольт из нее вычитаем скорость помноженную на 10
     poziciya_rk = 0; //обнуляем РК
      }
      else{
        KS_Voltage1 = _KS_Voltage;
        }
    }
   else if(((rk_pos == 2  || ((rk_pos == 3 || rk_pos == 4 || rk_pos == 5) && poziciya_rk < 4) && KBX_State == 1) || ((rk_pos == 3 || rk_pos == 4 || rk_pos == 5)) && KBX_State == 1 && currentSpeed2 < 1)){//выполняется, когда позиция 1
  // zaderzka_ot_rk = millis() + 8000;
     if(poziciya_rk == 0){//эмулятор рк начальное положение нужно для перехода с М без непредвиденных бросков стрелки
        if(currentSpeed2 <= 20){//если скорость меньше 20
        KS_Voltage1 = _KS_Voltage - (200 - (currentSpeed2*10));//максимальная просадка 200 вольт из нее вычитаем скорость помноженную на 10
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
      }
      
      if((poziciya_rk == 0 || poziciya_rk == 1 || poziciya_rk == 2 || poziciya_rk == 3) && TimerTek - TimerProshlo_perekluchenie_rk >= 700){//условие для перещелкивания РК по таймеру каздые 700 мс
        TimerProshlo_perekluchenie_rk = millis();//для хранения времени предыдущего переключния РК
        
        poziciya_rk = poziciya_rk + 1;//перещёлкиваем РК
       
       if(poziciya_rk == 1){//эмулятор рк первое положение
        if(currentSpeed2 <= 25){//если скорость меньше 25
        KS_Voltage1 = _KS_Voltage - (250 - (currentSpeed2*10));//максимальная просадка 200 вольт из нее вычитаем скорость помноженную на 10
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
      }
        if(poziciya_rk == 2){//эмулятор рк второе положение
        if(currentSpeed2 <= 30){//если скорость меньше 30
        KS_Voltage1 = _KS_Voltage - (300 - (currentSpeed2*10));//максимальная просадка 300 вольт из нее вычитаем скорость помноженную на 10
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
      }
        if(poziciya_rk == 3){//эмулятор рк третье положение
        if(currentSpeed2 <= 40){//если скорость меньше 40
        KS_Voltage1 = _KS_Voltage - (360 - (currentSpeed2*9));//максимальная просадка 360 вольт из нее вычитаем скорость помноженную на 9
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
      } 
      }
      else if(poziciya_rk == 4){//эмулятор рк четвертое положение
        if(currentSpeed2 <= 50){//если скорость меньше 50
        KS_Voltage1 = _KS_Voltage - (400 - (currentSpeed2*8));//максимальная просадка 400 вольт из нее вычитаем скорость помноженную на 8
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
      }
      if(poziciya_rk > 4 && rk_pos == 2){
        poziciya_rk = 4;
        }
   }
      else if((rk_pos == 3 || ((rk_pos == 4 || rk_pos == 5) && poziciya_rk < 6 && poziciya_rk > 3)) && KBX_State == 1 && currentSpeed2 > 1){
      //выполняется, когда позиция 2
      zaderzka_ot_rk = millis() + 6000;
if(poziciya_rk == 4){//эмулятор рк начальное положение нужно для перехода с М без непредвиденных бросков стрелки
        if(currentSpeed2 <= 50){//если скорость меньше 50
        KS_Voltage1 = _KS_Voltage - (400 - (currentSpeed2*8));//максимальная просадка 400 вольт из нее вычитаем скорость помноженную на 8
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
}
if((poziciya_rk == 4 || poziciya_rk == 5) && TimerTek - TimerProshlo_perekluchenie_rk >= 700){//условие для перещелкивания РК по таймеру каздые 700 мс
        TimerProshlo_perekluchenie_rk = millis();//для хранения времени предыдущего переключния РК
        
        poziciya_rk = poziciya_rk + 1;//перещёлкиваем РК
       
       if(poziciya_rk == 5){//эмулятор рк первое положение
        if(currentSpeed2 <= 70){//если скорость меньше 25
        KS_Voltage1 = _KS_Voltage - (420 - (currentSpeed2*6));//максимальная просадка 200 вольт из нее вычитаем скорость помноженную на 10
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
      }
    }
      else if(poziciya_rk == 6){//эмулятор рк четвертое положение
        if(currentSpeed2 <= 80){//если скорость меньше 50
        KS_Voltage1 = _KS_Voltage - (480 - (currentSpeed2*6));//максимальная просадка 400 вольт из нее вычитаем скорость помноженную на 8
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
      }
     


      }
      
            else if((rk_pos == 4 || ( rk_pos == 5 && poziciya_rk < 8 && poziciya_rk > 5)) && KBX_State == 1 && currentSpeed2 > 1){
      //выполняется, когда позиция 3
      zaderzka_ot_rk = millis() + 4000;
      if(poziciya_rk == 6){//эмулятор рк начальное положение нужно для перехода с М без непредвиденных бросков стрелки
        if(currentSpeed2 <= 80){//если скорость меньше 50
        KS_Voltage1 = _KS_Voltage - (480 - (currentSpeed2*6));//максимальная просадка 400 вольт из нее вычитаем скорость помноженную на 8
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
}
if((poziciya_rk == 6 || poziciya_rk == 7) && TimerTek - TimerProshlo_perekluchenie_rk >= 700){//условие для перещелкивания РК по таймеру каздые 700 мс
        TimerProshlo_perekluchenie_rk = millis();//для хранения времени предыдущего переключния РК
        
        poziciya_rk = poziciya_rk + 1;//перещёлкиваем РК
       
       if(poziciya_rk == 7){//эмулятор рк первое положение
        if(currentSpeed2 <= 90){//если скорость меньше 25
        KS_Voltage1 = _KS_Voltage - (540 - (currentSpeed2*6));//максимальная просадка 200 вольт из нее вычитаем скорость помноженную на 10
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
      }
    }
      else if(poziciya_rk == 8){//эмулятор рк четвертое положение
        if(currentSpeed2 <= 100){//если скорость меньше 50
        KS_Voltage1 = _KS_Voltage - (600 - (currentSpeed2*6));//максимальная просадка 400 вольт из нее вычитаем скорость помноженную на 8
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
      }
      
}
            else if(rk_pos == 5 && KBX_State == 1 && currentSpeed2 > 1){
      //выполняется, когда позиция 4
      zaderzka_ot_rk = millis() + 2000;
     if(poziciya_rk == 8){//эмулятор рк начальное положение нужно для перехода с М без непредвиденных бросков стрелки
        if(currentSpeed2 <= 100){//если скорость меньше 50
        KS_Voltage1 = _KS_Voltage - (600 - (currentSpeed2*6));//максимальная просадка 400 вольт из нее вычитаем скорость помноженную на 8
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
}
if((poziciya_rk == 8 || poziciya_rk == 9) && TimerTek - TimerProshlo_perekluchenie_rk >= 700){//условие для перещелкивания РК по таймеру каздые 700 мс
        TimerProshlo_perekluchenie_rk = millis();//для хранения времени предыдущего переключния РК
        
        poziciya_rk = poziciya_rk + 1;//перещёлкиваем РК
       
       if(poziciya_rk == 9){//эмулятор рк первое положение
        if(currentSpeed2 <= 110){//если скорость меньше 25
        KS_Voltage1 = _KS_Voltage - (660 - (currentSpeed2*6));//максимальная просадка 200 вольт из нее вычитаем скорость помноженную на 10
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
      }
    }
      else if(poziciya_rk == 10){//эмулятор рк четвертое положение
        if(currentSpeed2 <= 120){//если скорость меньше 50
        KS_Voltage1 = _KS_Voltage - (720 - (currentSpeed2*6));//максимальная просадка 400 вольт из нее вычитаем скорость помноженную на 8
      }
       else{
        KS_Voltage1 = _KS_Voltage;
        }
      }
      if(poziciya_rk > 10){
        poziciya_rk = 10;
        }
}
      else if((rk_pos == 255 || rk_pos == 254 || rk_pos == 253) && currentSpeed2 >= 50 && TC_Pressure < 20 && KBT_State == 1){//условие работы рекуперации: позиция т1,т2 или т3 при этом скорость выше 50 и давление тц меньше 2
        if(rk_pos == 255  && KBT_State == 1){ //выполняется, когда позиция т1
      if(currentSpeed2 <= 55){//если скорость меньше 55
        KS_Voltage1 = _KS_Voltage + (int(map(currentSpeed2, 50, 55, 0, 55))*2);      //максимальный подьём напряжения КС 110 вольт прибавляем к КС скорость переведённую по мап помноженную на 2
      }
      else{
        KS_Voltage1 = _KS_Voltage + 110;//если скорость больше 55 прибавляем к КС 110 вольт 
        }
      }
            else if(rk_pos == 254 && KBT_State == 1){ //выполняется, когда позиция т2
      if(currentSpeed2 <= 55){//если скорость меньше 55
        KS_Voltage1 = _KS_Voltage + (int(map(currentSpeed2, 50, 55, 0, 55))*3);    //максимальный подьём напряжения КС 165 вольт прибавляем к КС скорость переведённую по мап помноженную на 3  
      }
      else{
        KS_Voltage1 = _KS_Voltage + 190;//если скорость больше 55 прибавляем к КС 165 вольт 
        }
      }
                   else if(rk_pos == 253 && KBT_State == 1){ //выполняется, когда позиция т3
      if(currentSpeed2 <= 55){//если скорость меньше 55
        KS_Voltage1 = _KS_Voltage + (int(map(currentSpeed2, 50, 55, 0, 55))*5);  //максимальный подьём напряжения КС 275 вольт прибавляем к КС скорость переведённую по мап помноженную на 5    
      }
      else{
        KS_Voltage1 = _KS_Voltage + 275;//если скорость больше 55 прибавляем к КС 275 вольт 
        }
      }
      }
            
     else if((rk_pos == 0) || (rk_pos >= 1 && rk_pos <= 5 && KBX_State == 0) || (rk_pos >= 251 && rk_pos <= 255 && KBT_State == 0)){
      poziciya_rk = 0; //обнуляем РК
      KS_Voltage1 = _KS_Voltage; //выводим напряжение без изменений если не одно из условий выше не верно
     }

   unsigned long obmovlenie_kiloommetra = 50;//объявляем переменную частоты обновления в режиме "плавания" стрелки
   if(ks_vivod < (KS_Voltage1 - (ks_gisterezis*2)) || ks_vivod > (KS_Voltage1 + (ks_gisterezis*2))){//если разница переменной из сима z и переменной для шима z1 более +-100в то убираем задержку обновления 
   ks_vivod = KS_Voltage1;
   }
  
   
    unsigned long obmovlenie_kiloommetra_tekuschee = millis();//присваиваем значение текущих милисекунд
    if(obmovlenie_kiloommetra_tekuschee - obmovlenie_kiloommetra > TimerProshlo_otklonenie_kiloamp) {//таймер на вход в обновление данных шим
    TimerProshlo_otklonenie_kiloamp = millis();//если вошли то присваиваем переменной текуще значение миллис
    if (dir == 1){ 
      ks_vivod++; // увеличиваем напряжение на киловольтметре
    }
    else{
      ks_vivod--;     // уменьшаем напряжение на лиловольтметре
    }
    if (ks_vivod > (KS_Voltage1 + ks_plus)){//если достигли верхнего предела отклонения напряжения 
      dir = 0; // разворачиваем направление
    ks_minus =int(random(1,ks_gisterezis));//рандомно выбираем отклонение напряжения в минус от реального из сима
    }
    else if (ks_vivod < (KS_Voltage1 - ks_minus)){//если достигли нижнего предела отклонения напряжения
      dir = 1;// разворачиваем направление
      ks_plus = int(random(1,ks_gisterezis));//рандомно выбираем отклонение напряжения в плюс от реального из сима
 }
    }
  }
  }
     else{
ks_vivod = 0;//если в КС 0 или каким то чудом меньше то выводим 0
TimerProshlo_Tokopriemnik = millis() + 3000;
    }
    if(ks_vivod > 4200){//для предотвращения выхода значения за пределы из функции МАП
      ks_vivod = 4200;
    }
    else if(ks_vivod < 0){//для предотвращения выхода значения за пределы из функции МАП
      ks_vivod = 0;
    }
  
    analogWrite(KS_Voltage_Pin, map(ks_vivod, 0, 4200, 0, 255)); //закомментировать для настройки

 //   analogWrite(KS_Voltage_Pin, map(_KS_Voltage, 0, 1000, 0, 255));//раскоментировать для настройки показание должно соответствовать симу

  // analogWrite(KS_Voltage_Pin,255);//раскоментировать для настройки показание должно соответствовать 4000в
 
} 
//=================================================================================================
//=================================================================================================
//Обновим состояние ламп и светофора
//=================================================================================================
void showLampState()
{
    //Управление Локомотивным светофором
//    digitalWrite(als_bel_Pin, LOW);   //лампа белый огонь «Б» (на путевом светофоре белый огонь) показания путевых светофоров на локомотив не передаются.
    digitalWrite(als_k_Pin, LOW);     //лампа красный огонь «К» (на путевом светофоре красный огонь) запрещающий движение сигнал.
    digitalWrite(als_kj_Pin, LOW);    //лампа желтый огонь с красным «КЖ» (на путевом светофоре красно желтый огонь).
    digitalWrite(als_jelt_Pin, LOW);  //лампа желтый огонь «Ж» (на путевом светофоре желтый огонь).
    digitalWrite(als_zelen_Pin, LOW); //лампа зеленый огонь «3» (на путевом светофоре, к которому приближается поезд, горит зеленый огонь).
    
    switch (ALS_State) 
    {
//      case 1: digitalWrite(als_bel_Pin, HIGH); break;
      case 2: digitalWrite(als_k_Pin, HIGH); break;
      case 3: digitalWrite(als_kj_Pin, HIGH); break;
      case 4: digitalWrite(als_jelt_Pin, HIGH); break;
      case 5: digitalWrite(als_zelen_Pin, HIGH); break;
    }
     //------------------------------------------------------------------------ проверить
    //Зажигаем лампы от пакетника ППТ в положении головной
     if (digitalRead(VUin_Switch_pin) == LOW) //Тумблер ВУ включен
     {
        VU_State = 1;  // Включаем ВУ
     }
     else                                     //Тумблер ВУ выключен
     {
       VU_State = 0;  //ВУ выкл
     }
    //------------------------------------------------------------------------ проверить
    //Зажигаем лампы от пакетника ППТ
     if (digitalRead(PPT_Switch_pin) == HIGH) //Пакетник ППТ в положении головной
     {
        PPT_State = 1;  // Включаем ППТ
     }
     else                                     //Пакетник не в положении головной
     {
        PPT_State = 0;  //ППТ выкл
     }
     //----------------------------------------------------------------------- работает
     // Тумблер ЭДТ
    if (digitalRead(EDTin_Switch_pin) == HIGH)
    {
        EDTin_State = 1;
    }
    else
    {
        EDTin_State = 0;
    }
    //----------------------------------------------------------------------- работает
    // Тумблер Стеклообогрева
    if (digitalRead(steclo_Switch_pin) == LOW)
    {
        steclo_State = 1;
    }
    else
    {
        steclo_State = 0;
    }
    //----------------------------------------------------------------------- не ключен в сххему
    // Тумблер запрета тяги
    if (digitalRead(zapret_switch_pin) == LOW)
    {
        zapret_State = 1;
    }
    else
    {
        zapret_State = 0;
    }
    //==========================================================================
    // ПИН М положения штурвала
/*    if(digitalRead(provod_22u_pin) == LOW)//хардкор
    {
        provod_22u_State = 1;
    }
    else
    {
        provod_22u_State = 0;
    }
    // ПИН 1T положения штурвала
    if(digitalRead(provod_22sh_pin) == LOW)//хардкор
    {
        provod_22sh_State = 1;
    }
    else
    {
       provod_22sh_State = 0;
    }
    */
    //пин положения ключа ЭПК
    if(digitalRead(EPK_pin) == HIGH)
    {
     EPK_State = 1;
    }
     if(digitalRead(EPK_pin) == LOW)
    {
     EPK_State = 0;
    }
    //-------------------------------------------------------------------------=проверить
     if (ks_vivod > 3950) // Устанавливаем функцию ПРМН
  {
    PRMN_State = 1;
  }
  else if (ks_vivod < 3950)
  {
    PRMN_State = 0;
  }
  else
  {
    PRMN_State = 0;
  }
  //=============================================================================
   if((rk_pos == 254 || rk_pos == 253) && currentSpeed2 == 20)// условие сбора дотормаживания
     {
     RVT3_State = 1;
     }
     if ((rk_pos == 254 || rk_pos == 253) && KBT_State == 0 && EDTin_State == 1)// условие сбора замещения
     {
     RVT2_State = 1;
     }
    if(rk_pos == 0)
    {
      RVT2_State = 0;
      RVT3_State = 0;
    }
          //----------------------------------------------------------------------- не тухнут при выключенном ВУ
      //РАБОТА ЛАМП ПРЕОБРАЗОВАТЕЛЬ И ВЦ. ПРИ ВКЛЮЧЕНИИ ВУ ГАСНУТ
      if(VU_State == 0) //Состояние ВУ
      {
       //ПР лампа (преобразователь) выкл.
        TimerProshlo_preobrazovatel = millis() + 6000;
        preobrazovatel_Lamp = 0;//Лампа преобразователь Выкл.
        preobrazovatel_flag = 0;
        PR_State = 0;

       //ВЦ лампа (вспомогательные цепи) выкл.
        TimerProshlo_vspom_cepi = millis() + 7500;
        vspom_cepi_Lamp = 0; //Лампа вспомогательные цепи Вкл.
        vspom_cepi_flag = 0;
       }
       if (VU_State == 1 && RN_State == 1)
      {
  //ПР лампа (преобразователь) вкл.
        TimerProshlo_preobrazovatel = millis() + 6000;
        preobrazovatel_Lamp = 1;//Лампа преобразователь Вкл.
        preobrazovatel_flag = 0;
        PR_State = 0;

       //ВЦ лампа (вспомогательные цепи) выкл.
        TimerProshlo_vspom_cepi = millis() + 7500;
        vspom_cepi_Lamp = 1; //Лампа вспомогательные цепи Вкл.
        vspom_cepi_flag = 0;
       }
 else if (VU_State == 1 && RN_State == 0)
      {
//ПР лампа (преобразователь) вкл.
       if(preobrazovatel_flag == 0 && TimerProshlo_preobrazovatel > TimerTek)
       {
       preobrazovatel_Lamp = 1;//Лампа преобразователь
       preobrazovatel_flag = 0;
       PR_State = 0;
       }//конец if(preobrazovatel_flag == 0 && TimerProshlo_preobrazovatel > TimerTek)
       else{
          preobrazovatel_Lamp = 0;//Лампа преобразователь
       preobrazovatel_flag = 1;
       PR_State = 1;
       }

        if(vspom_cepi_flag == 0 && TimerProshlo_vspom_cepi > TimerTek)
       {
       vspom_cepi_Lamp = 1; //Лампа вспомогательные цепи вкл
       vspom_cepi_flag = 0;
       }//конец if(vspom_cepi_flag == 0 && TimerProshlo_vspom_cepi > TimerTek)
       else{
        vspom_cepi_Lamp = 0; //Лампа вспомогательные цепи выкл
       vspom_cepi_flag = 1;
        }
}
    //-------------------------------------------------------------------------- исравна
    if(VU_State == 1 && BV_State == 1 || RN_State == 1 || PR_State == 1) //Состояние БВ
    {
       BV_Lamp = 1;//лампа БВ Вкл.
       BV_flag = 1;
    }
    
    if (digitalRead(BVin_Switch_pin) == HIGH) // КНОПКА ВОЗВРАТ БВ
    {
       BV_Lamp = 0;//лампа БВ Выкл.
       BV_flag = 0;
       PRRB_State = 0;
    }
    
    //--------------------------------------------------------------------------------
 //Логика работы лампы ЛКиТ
    // Определяем, перешел ли контроллер из нулевого положения в "м" или "т1"
    if (rk_pos_prev == 0 && (rk_pos == 1 || rk_pos == 255))
    //if (rk_pos_prev == 0 && (provod_22u_State == 1 || provod_22sh_State == 1)) //хардкор версия
    {
        LK_Lamp = 1;
        LK_Lamp_Delay = random(100, 1000);
        TimerProshlo_LK = millis();
        LK_Lamp_Timer_Started = true;
        LK_Lamp_Extinguished = false;
    }

    rk_pos_prev = rk_pos;

    if (LK_Lamp_Timer_Started && !LK_Lamp_Extinguished)
    {
        if ((millis() - TimerProshlo_LK >= LK_Lamp_Delay) && zaderzka_ot_rk < TimerTek                                                                         )
        {
            if (BV_Lamp == 0)
            {
                if ((rk_pos >= 1 && rk_pos <= 6) && EPK_State == 1)// алгоритм в тягу
                //if (provod_22u_State == 1 && EPK_State == 1)// алгоритм в тягу, хардкор
                {
                        LK_Lamp = 0;
                        LK_Lamp_Extinguished = true;
                        LK_Lamp_Timer_Started = false;
                        KBX_State = 1;
                        provod_2_State = 1;
                }
                else
                {
                    LK_Lamp = 0;
                    LK_Lamp_Extinguished = true;
                    LK_Lamp_Timer_Started = false;
                    KBX_State = 0;
                    provod_2_State = 0;
                }
                 if (rk_pos <= 255 && rk_pos >= 250 && TC_Pressure == 0)// алгоритм в эдт
                 //if (provod_22sh_State == 1 && TC_Pressure == 0)// алгоритм в эдт, хардкор
                {
                        LK_Lamp = 0;
                        LK_Lamp_Extinguished = true;
                        LK_Lamp_Timer_Started = false;
                        KBT_State = 1;
                        provod_41_State = 1;
                }
                else
                {
                    LK_Lamp = 0;
                    LK_Lamp_Extinguished = true;
                    LK_Lamp_Timer_Started = false;
                    KBT_State = 0;
                    provod_41_State = 0;
                }
            }
        }
    }

    if (rk_pos == 0)
    {
        LK_Lamp = 0;
        LK_Lamp_Timer_Started = false;
        LK_Lamp_Extinguished = false;
        KBX_State = 0;
        KBT_State = 0;
    }

    if (((BV_Lamp == 1 || preobrazovatel_Lamp == 1 || RN_Lamp == 1 || PRRB_State == 1) && rk_pos >= 1) || ((BV_Lamp == 1 || preobrazovatel_Lamp == 1 || RN_Lamp == 1 || EDTin_State == 0 || PRRB_State == 1) && rk_pos >= 6))
    {
        LK_Lamp = 1;
        LK_Lamp_Timer_Started = false;
        LK_Lamp_Extinguished = false;
    }
    //--------------------------------------------------------------------------------
     if (RB_State)// Работа лампы РБ, а также БВ при срабатывании ПРРБ.
     {
      RB_Lamp = 1;  //зажигаем лампу РБ
      TimerProshlo_PRRB = millis() + 3000;
    //Так как мы не можем разобрать тягу через самоподхват ПРРБ (разносное боксование) отключим БВ на секции.
      PRRB_State = 1; //отключаем тягу на секции. при тяге/эдт
     }
     if(RB_State == 0 && TimerProshlo_PRRB < TimerTek)
     //if(RB_State == 0)
     {
      RB_Lamp = 0;  // отключаем РБ, оставив отбитый БВ
     }
     
    //--------------------------------------------------------------------------исправна
      if (VU_State == 1 && PPT_State == 1)// Работа лампы K Проверить рнаботоспособность
      //if (K_State)// Работа лампы K// 
     {
      EPT_kontrol_Lamp = 1;  //зажигаем лампу К
      }    
     if(PPT_State == 0 || rk_pos >= 6)
     {
      EPT_kontrol_Lamp = 0;  // отключаем K
     }
    //--------------------------------------------------------------------------исправна
      if (O_State || T_State)// Работа лампы o 
     {
      EPT_O_Lamp = 1;  //зажигаем лампу o
      O_State = 1;
     }
     else
     {
      EPT_O_Lamp = 0;  // отключаем o
      O_State = 0;
     }
   //--------------------------------------------------------------------------исправна
    if(rk_pos != 252) // отключаем лампу Т в 4Т положении
    {
     if (T_State)// Работа лампы t 
     {
     EPT_T_Lamp = 1;  //зажигаем лампу t
     T_State = 1;
     }
     else
     {
     EPT_T_Lamp = 0;  // отключаем t
     T_State = 0;
     }
    }
    //--------------------------------------------------------------------------исправна
       //Лампа двери горит при полностью закрытых дверях на всем электропоезде. 
    if(Dveri_cont_State != Dveri_cont_flag)     //Состояние лампы контроля дверей
    {
        Dveri_cont_flag = Dveri_cont_State;
        TimerProshlo_dveri_cont = millis();
    }
     
     //таймер на включение
     if(VU_State == 1 && PPT_State == 1 && Dveri_cont_flag == 1 && millis() - TimerProshlo_dveri_cont > 1500)
     //if( Dveri_cont_flag == 1 && millis() - TimerProshlo_dveri_cont > 1500)
     {
       Dveri_cont_Lamp = 1;  //Лампа дверей вкл.
       
     }
     if( Dveri_cont_flag == 1 && millis() - TimerProshlo_dveri_cont > 2500)
     {
       Zvonok_State = 1;  //Звонок вкл.
     }
          if( Dveri_cont_flag == 1 && millis() - TimerProshlo_dveri_cont > 3000)
     {
       Zvonok_State = 0;  //Звонок выкл.
     }
     //таймер на выключение
     if(PPT_State == 0 || VU_State == 0  || Dveri_cont_flag == 0 && millis() - TimerProshlo_dveri_cont > 150)
     //if(Dveri_cont_flag == 0 && millis() - TimerProshlo_dveri_cont > 150)
     {
       Dveri_cont_Lamp = 0;  //Лампа дверей выкл.
     } 
     //----------------------------------------------------------------исправна
  if(ks_vivod < 2700){
        RN_State = 1;
        }
        else {
          RN_State = 0;
          }
       
       if(RN_State != RN_State_flag)     //Состояние РН реле контроля напряжения
    {
        RN_State_flag = RN_State;
        TimerProshlo_pwr_cont = millis();
    }
    //Лампа РН реле контроля напряжения горит при отсуствии высокого напряжения. 
    if(RN_State != RN_State_flag)     //Состояние РН реле контроля напряжения
    {
        RN_State_flag = RN_State;
        TimerProshlo_pwr_cont = millis();
   }

     RN_Lamp = RN_State;
     //таймер на включение
  //   if(RN_State_flag == 1 && millis() - TimerProshlo_pwr_cont > 1000)
  //   {
   //    RN_Lamp = 1;  //Лампа РН реле контроля напряжения вкл.
  //  }

     //таймер на выключение
     if(RN_State_flag == 0 && millis() - TimerProshlo_pwr_cont > 4000)
  //   {
  //     RN_Lamp = 0;  //Лампа РН реле контроля напряжения выкл.
  //   } 
     //--------------------------------------------------------------------------------исправна
     //Зажигаем лампы от кнопки CSO_Button 
     if (VU_State == 1 && digitalRead(CSO_Button_pin) == LOW) //КНОПКА ПРОВЕРКИ ЛАМП РБ И РН
     {
        RB_Lamp = 1;  //Лампа РБ Боксование вкл
        RN_Lamp = 1;  //лампа РН вкл.
        CSO_Button_flag = 1;
     }
     else if (digitalRead(CSO_Button_pin) == HIGH && CSO_Button_flag == 1)
     {
       RB_Lamp = 0;  //Лампа РБ Боксование выкл
       RN_Lamp = 0;  //лампа РН            выкл.
       CSO_Button_flag = 0;
     }
     //------------------------------------------------------------------------исправен
     if (VU_State == 1 && steclo_State == 1) // ТУМБЛЕР СТЕКЛООБОГРЕВА ВКЛЮЧЕН 
     {
       Steclo_Lamp = 1; //лампа стеклообогрев вкл
     }
     if(VU_State == 1 && steclo_State == 1 && PPT_State == 0)
     {
      Steclo_Lamp = 1; //лампа стеклообогрев вкл
      Zvonok_State = 1;
     }
     if(steclo_State == 0)
     {
       Steclo_Lamp = 0; //лампа стеклообогрев выкл
     }
    //------------------------------------------------------------------------проверить
    //Состояние тяги(горит при контроллере от м-4).
    if (KBX_State == 1)//Позиции контроллера
    {
       Tiaga_State = 1;                      //Тяга Вкл.
    }
    else if (KBX_State == 0)
    {
       Tiaga_State = 0;                      //Тяга Выкл.
    } 
    else 
    {
       Tiaga_State = 0;                      //Тяга Выкл.
    } 
    //------------------------------------------------------------------------проверить
    //Состояние ЭДТ (горит при т1-т5).
    if (EDTin_State == 1 && KBT_State == 1)//Позиции контроллера
    {
       EDT_State = 1;                        //ЭДТ Вкл
    }
    else if (rk_pos = 0)
    {
      EDT_State = 0;
    }
    else
    {
       EDT_State = 0;                        //ЭДТ Выкл
    } 
     //--------------------------------------------------------------------------исправна
      if(RVT3_State != RVT3_flag)     //Состояние провода РВТ3
    {
        RVT3_flag = RVT3_State;
        TimerProshlo_RVT = millis();
    }
    if(RVT3_flag == 1 && millis() - TimerProshlo_RVT > 100)// включение дотормаживания
     {
       RVT_State = 1;  //рвт3 вкл.
     }
          if( RVT3_flag == 1 && millis() - TimerProshlo_RVT > 1100)// выдержка 1 сек, при учете доп задержки в 50мс
     {
       RVT_State = 0;  //рвт3 выкл.
     }
     if(RVT2_State != RVT2_flag)     //Состояние провода РВТ2
    {
        RVT2_flag = RVT2_State;
        TimerProshlo_RVT = millis();
    }
    if(RVT2_flag == 1 && millis() - TimerProshlo_RVT > 50)// включение замещения
     {
       RVT_State = 1;  //рвт2 вкл.
     }
          if(RVT2_flag == 1 && millis() - TimerProshlo_RVT > 2600)// выдержка 2,5 сек
     {
       RVT_State = 0;  //рвт2 выкл.
     }
    //--------------------------------------------------------------------------исправна
    
//-----------------------------------------------  проверить лампу СОТх
  if(SOT_Lamp == 1) //Работа сигнальной лампы СОТ-х
  {
      SOT_State = 1; //лампа СОТ (наполнение ТЦ).
       SOT_Lamp = 1;    //лампа СОТx (наполнение ТЦ).
        TimerProshlo_SOTx = millis() + 2000;
        SOTx_Lamp = 1;    //лампа СОТx (наполнение ТЦ)
        SOTx_flag = 0;
  }
  if(SOT_Lamp == 0)
   {
    SOT_State = 0;
   }
   if(SOT_State == 0 && TimerProshlo_SOTx < TimerTek)
         {
           SOTx_Lamp = 0;
           SOTx_flag = 1;
         }
    //------------------------------------------------------------------------
    digitalWrite(RN_Lamp_Pin, RN_Lamp);                         //Лампа РН реле контроля напряжения горит при отсуствии высокого напряжения.
    digitalWrite(BV_Lamp_Pin, BV_Lamp);                         //Лампа БВ
    digitalWrite(RB_Lamp_Pin, RB_Lamp);                         //Лампа РБ Боксование
    digitalWrite(preobrazovatel_Lamp_Pin, preobrazovatel_Lamp); //Лампа преобразователь
    digitalWrite(vspom_cepi_Lamp_Pin, vspom_cepi_Lamp);         //Лампа вспомогательные цепи
    digitalWrite(LK_Lamp_Pin, LK_Lamp);                         //Лампа ЛКиТ
    digitalWrite(Fire_Lamp_Pin, RVT_State);                     //Лампа Пожароопасность
    digitalWrite(Tiaga_Lamp_Pin, Tiaga_Lamp_P);                 //Лампа тяга(горит при контроллере от м-4).ОТКЛЮЧЕН
    digitalWrite(EDT_Lamp_Pin, EDT_Lamp);                       //Лампа ЭДТ (горит при т1-т5).ОТКЛЮЧЕН
    digitalWrite(Dveri_cont_Lamp_Pin, Dveri_cont_Lamp);         //Лампа Контроль дверей
    digitalWrite(Steclo_Lamp_Pin, Steclo_Lamp);                 //Лампа Стеклообогрева
    digitalWrite(PSS_Lamp_Pin, PSS_Lamp);                       //Лампа ПСС Бдительность
    digitalWrite(EPT_O_Lamp_Pin, EPT_O_Lamp);                   //Лампа ЭПТ-О
    digitalWrite(EPT_kontrol_Lamp_Pin, EPT_kontrol_Lamp);       //Лампа ЭПТ-контроль.
    digitalWrite(EPT_T_Lamp_Pin, EPT_T_Lamp);                   //Лампа ЭПТ-Т
    digitalWrite(SOT_Lamp_Pin, SOT_Lamp);                       //Лампа СОТ (наполнение ТЦ).
    digitalWrite(SOTx_Lamp_Pin, SOTx_Lamp);                     //Лампа СОТx (наполнение ТЦ).
    digitalWrite(Zvonok_pin, Zvonok_State);                     //Пин реле Звонка
    digitalWrite(RVT_pin, RVT_State);                           //Пин дотормаживания(8А на контроллер параллельно)
//    digitalWrite(provod_2_pin, provod_2_State);                 //Пин тяги (22У снимаем, вместо него ставим 2), 22у пускаем на вход меги
//    digitalWrite(provod_41_pin, provod_41_State);               //ПИН ЭДТ (22Ш снимаем с контроллера, ставим 41), 22ш пускаем на вход меги  
}
//=================================================================================================
void setup()
{
  Serial.begin(57600);//Инициализация UART
    
  //Лампы и светофор
//  pinMode(als_bel_Pin, OUTPUT);   //лампа белый огонь «Б» (на путевом светофоре белый огонь) показания путевых светофоров на локомотив не передаются.
  pinMode(als_k_Pin, OUTPUT);     //лампа красный огонь «К» (на путевом светофоре красный огонь) запрещающий движение сигнал.
  pinMode(als_kj_Pin, OUTPUT);    //лампа желтый огонь с красным «КЖ» (на путевом светофоре красно желтый огонь).
  pinMode(als_jelt_Pin, OUTPUT);  //лампа желтый огонь «Ж» (на путевом светофоре желтый огонь).
  pinMode(als_zelen_Pin, OUTPUT); //лампа зеленый огонь «3» (на путевом светофоре, к которому приближается поезд, горит зеленый огонь).
  pinMode(Zvonok_pin, OUTPUT);    //Пин реле Звонка
  pinMode(RVT_pin, OUTPUT);       //Пин 8А провода
//  pinMode(provod_2_pin, OUTPUT); //хардкор
//  pinMode(provod_41_pin, OUTPUT);//хардкор
  
  
  pinMode(PSS_Lamp_Pin, OUTPUT);                //Лампа ПСС Бдительность
  pinMode(EPT_O_Lamp_Pin, OUTPUT);              //Лампа ЭПТ-О
  pinMode(EPT_kontrol_Lamp_Pin, OUTPUT);        //Лампа ЭПТ-контроль.
  pinMode(EPT_T_Lamp_Pin, OUTPUT);              //Лампа ЭПТ-Т
  pinMode(SOT_Lamp_Pin, OUTPUT);                //Лампа СОТ (наполнение ТЦ).
  pinMode(vspom_cepi_Lamp_Pin, OUTPUT);         //Лампа вспомогательные цепи
  pinMode(preobrazovatel_Lamp_Pin, OUTPUT);     //Лампа преобразователь
  pinMode(RB_Lamp_Pin, OUTPUT);                 //Лампа РБ Боксование
  pinMode(LK_Lamp_Pin, OUTPUT);                 //Лампа ЛКиТ
  pinMode(BV_Lamp_Pin, OUTPUT);                 //Лампа БВ
  pinMode(Fire_Lamp_Pin, OUTPUT);               //Лампа Пожароопасность
  pinMode(RN_Lamp_Pin, OUTPUT);                 //Лампа РН реле контроля напряжения горит при отсуствии высокого напряжения.
  pinMode(SOTx_Lamp_Pin, OUTPUT);               //Лампа СОТx (наполнение ТЦ).ОТКЛЮЧЕН
  pinMode(Tiaga_Lamp_Pin, OUTPUT);              //Лампа тяга(горит при контроллере от м-4).ОТКЛЮЧЕН
  pinMode(EDT_Lamp_Pin, OUTPUT);                //Лампа ЭДТ (горит при т1-т5).ОТКЛЮЧЕН
  pinMode(Dveri_cont_Lamp_Pin, OUTPUT);         //Лампа Контроль дверей
  pinMode(Steclo_Lamp_Pin, OUTPUT);             //Лампа Стеклообогрева
  pinMode(RESET_Pin, OUTPUT);                   //Сброс для ШД


  //Кнопки
  pinMode(CSO_Button_pin, INPUT_PULLUP);        //кнопка ЦСО (подтянутый к +5V вход)
  pinMode(steclo_Switch_pin, INPUT_PULLUP);     //кнопка стеклообогрева (подтянутый к +5V вход)
  pinMode(BVin_Switch_pin, INPUT_PULLUP);       //ТУМБЛЕР ВОЗВРАТ БВ (подтянутый к +5V вход)
  pinMode(VUin_Switch_pin, INPUT_PULLUP);       //Тумблер ВУ (подтянутый к +5V вход)
  pinMode(EDTin_Switch_pin, INPUT_PULLUP);      //Тумблер ЭДТ (подтянутый к +5V вход)
  pinMode(zapret_switch_pin, INPUT_PULLUP);     //Тумблер запрет тяги (подтянутый к +5V вход)
  pinMode(PPT_Switch_pin, INPUT_PULLUP);        //Пакетник ППТ в положении головной (подтянутый к +5V вход)
  pinMode(EPK_pin, INPUT_PULLUP);               //тумблер ЭПК (подтянутый к +5V вход)
//  pinMode(provod_22u_pin, INPUT_PULLUP);        //провод 22У с контроллера машиниста (подтянутый к +5V вход)
//  pinMode(provod_22sh_pin, INPUT_PULLUP);       //провод 22Ш с контроллера машиниста (подтянутый к +5V вход)

  
  //Настройка шаговых двигателей
  digitalWrite(RESET_Pin, LOW);
  delay(100);
  digitalWrite(RESET_Pin, HIGH);
  
  stepper1.zero();
  stepper2.zero();
  stepper3.zero();
  stepper4.zero();

  stepper1.setPosition(STEP_1);
  stepper2.setPosition(STEP_2);
  stepper3.setPosition(STEP_3);
  stepper4.setPosition(STEP_4);
}
//=================================================================================================
void loop()
{
  int index = processSerial();
  
  //обновление показаний измерителя выполняется в фоновом режиме
  stepper1.update(); //Обновить показания 
  stepper2.update(); //Обновить показания  
  stepper3.update(); //Обновить показания   
  stepper4.update(); //Обновить показания 
  
  TimerTek = millis();//Получаем текущее время из таймера
  
  //Fire_Lamp = 0;                   //Лампа Пожароопасность

  if (index != -1)//Обработка полученных данных из сканера 
  {
    sz = 0;
    
    memcpy(&skor_dop, &buffer[index + 0], 2);                     //Допустимая скорость
    memcpy(&currentSpeed, &buffer[index + 2], 2);                 //Текущая скорость с учетом множителя для шаговых двигаетелей
    memcpy(&currentSpeed2, &buffer[index + 6], 2);                //Текущая скорость без множителя для шаговых двигаетелей
  
    //Время
    memcpy(&chas, &buffer[index + 10], 1);                        //Игровые часы: часы
    memcpy(&minuta, &buffer[index + 11], 1);                      //Игровые часы: минуты
    memcpy(&sekunda, &buffer[index + 12], 1);                     //Игровые часы: секунды
    memcpy(&rk_pos, &buffer[index + 15], 1);                      //Позиции контроллера

    //Стрелочные приборы (ШИМ) получаем данные.
    memcpy(&KS_Voltage, &buffer[index + 13], 2);                  //Напряжение Контактной сети

    //Давление получаем данные.
    memcpy(&NM_Pressure, &buffer[index + 29], 2);                 //Давление НМ
    memcpy(&TM_Pressure, &buffer[index + 31], 2);                 //Давление ТМ
    memcpy(&UR_Pressure, &buffer[index + 33], 2);                 //Давление УР
    memcpy(&TC_Pressure, &buffer[index + 35], 2);                 //Давление ТЦ
  
    //Лампы и светофор
    memcpy(&ALS_State, &buffer[index + 4], 1);                    //Состояние Светофора АЛС
    memcpy(&PSS_Lamp, &buffer[index + 5], 1);                     //Лампа ПСС Бдительность
    memcpy(&SOT_Lamp, &buffer[index + 37], 1);                    //Лампа СОТ (наполнение ТЦ).
    memcpy(&K_State, &buffer[index + 38], 1);            //Лампа ЭПТ-контроль
    memcpy(&O_State, &buffer[index + 39], 1);                  //Лампа ЭПТ-О
    memcpy(&T_State, &buffer[index + 40], 1);                  //Лампа ЭПТ-T
    memcpy(&LK_State, &buffer[index + 45], 1);                     //Лампа ЛКиТ
    memcpy(&Dveri_cont_State, &buffer[index + 46], 1);            //Состояние Контроля дверей
    memcpy(&RN_State, &buffer[index + 50], 1);                    //Состояние РН реле контроля напряжения
    memcpy(&BV_State, &buffer[index + 51], 1);                    //Состояние БВ
    memcpy(&RB_State, &buffer[index + 54], 1);                    //Состояние РБ Боксование
    //memcpy(&SOTx_Lamp, &buffer[index + 45], 1);                   //Лампа СОТx (наполнение ТЦ).
    
    //Обновить показания давления (манометры на ШД).
    stepper1.setPosition(map(NM_Pressure, 0, 100, 0, STEP_1));
    stepper2.setPosition(map(TM_Pressure, 0, 100, 0, STEP_2));   
    stepper3.setPosition(map(UR_Pressure, 0, 100, 0, STEP_3));  
    stepper4.setPosition(map(TC_Pressure, 0, 100, 0, STEP_4));   

    //Обновить показания стрелочных приборов (ШИМ).
    showGauges_pwm(KS_Voltage);    
//    showSpeed(currentSpeed);
  }

  TimerProshlo_lampShow = TimerTek - TimerOld_lampShow;
  if (TimerProshlo_lampShow >= 400)//Состояние ламп меняем с циклом 400ms
  {
      //Обновим состояние ламп и светофора
      showLampState();   
      TimerOld_lampShow = millis(); //Сохраним предыдущее время lampShow
  }

  //Обновить показания давления (манометры на ШД).
  //обновление показаний измерителя выполняется в фоновом режиме
  stepper1.update(); //Обновить показания 
  stepper2.update(); //Обновить показания  
  stepper3.update(); //Обновить показания   
  stepper4.update(); //Обновить показания
}
//=================================================================================================
