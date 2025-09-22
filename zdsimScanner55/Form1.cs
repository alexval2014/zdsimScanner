using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using Microsoft.DirectX.DirectInput;
using System.Configuration;
using System.Text;
using zdsimScanner.NativeApi;
using zdsimScanner.Core;
using zdsimScanner.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace zdsimScanner
{
    public partial class Form1 : Form
    {
        //Инициализируем переменные кнопок
        public static void LocoButtonsStructInit0()
        {
            //постоянные 28
            LocoButtons.svistok = 0;
            LocoButtons.tifon = 0;
            LocoButtons.kran395_0 = 0; //от 0 до 6
            LocoButtons.kran395_1 = 0;
            LocoButtons.kran395_2 = 0;
            LocoButtons.kran395_3 = 0;
            LocoButtons.kran395_4 = 0;
            LocoButtons.kran395_5 = 0;
            LocoButtons.kran395_6 = 0;
            LocoButtons.kran254_0 = 0; //float от от -1, 0-4
            LocoButtons.kran254_1 = 0;
            LocoButtons.kran254_2 = 0;
            LocoButtons.kran254_3 = 0;
            LocoButtons.kran254_4 = 0;
            LocoButtons.kran254_5 = 0;
            LocoButtons.vid_vlevo = 0;
            LocoButtons.vid_vpravo = 0;//double от -0,5 до +0,5
            LocoButtons.vid_vverh = 0;
            LocoButtons.vid_vniz = 0;//double от -0,5 до +0,5
            LocoButtons.vid_zoom_in = 0;
            LocoButtons.vid_zoom_out = 0;//float от 3.5_3.21875_2.5
            LocoButtons.vid_outside = 0; //0-каб, далее по кол-ву ваг
            LocoButtons.vid_vpered = 0;
            LocoButtons.vid_nazad = 0;//float от 6.2_6.619999886_8
            LocoButtons.protyazhka_lenty = 0;//int по 3000
            LocoButtons.bdit_Z = 0;
            LocoButtons.bdit_M = 0;
            LocoButtons.pesok = 0;
            LocoButtons.dvorniki_0 = 0;
            LocoButtons.dvorniki_1 = 0;
            LocoButtons.dvorniki_2 = 0;
            LocoButtons.dvorniki_3 = 0;
            LocoButtons.dvorniki_4 = 0;
            LocoButtons.dvorniki_5 = 0;

            //нештатки 100
            for (int i = 0; i < 100; i++)
            {
                LocoButtons.b_neshtatki[i] = 0;
            }

            //2es5k 109
            LocoButtons.es5k_kontr_0 = 0;
            LocoButtons.es5k_kontr_h4 = 0;
            LocoButtons.es5k_kontr_h5 = 0;
            LocoButtons.es5k_kontr_h6 = 0;
            LocoButtons.es5k_kontr_h7 = 0;
            LocoButtons.es5k_kontr_h8 = 0;
            LocoButtons.es5k_kontr_h9 = 0;
            LocoButtons.es5k_kontr_h10 = 0;
            LocoButtons.es5k_kontr_h11 = 0;
            LocoButtons.es5k_kontr_h12 = 0;
            LocoButtons.es5k_kontr_h13 = 0;
            LocoButtons.es5k_kontr_h14 = 0;
            LocoButtons.es5k_kontr_h15 = 0;
            LocoButtons.es5k_kontr_h16 = 0;
            LocoButtons.es5k_kontr_h17 = 0;
            LocoButtons.es5k_kontr_h18 = 0;
            LocoButtons.es5k_kontr_h19 = 0;
            LocoButtons.es5k_kontr_h20 = 0;
            LocoButtons.es5k_kontr_h21 = 0;
            LocoButtons.es5k_kontr_h22 = 0;
            LocoButtons.es5k_kontr_h23 = 0;
            LocoButtons.es5k_kontr_h24 = 0;
            LocoButtons.es5k_kontr_h25 = 0;
            LocoButtons.es5k_kontr_h26 = 0;
            LocoButtons.es5k_kontr_h27 = 0;
            LocoButtons.es5k_kontr_h28 = 0;
            LocoButtons.es5k_kontr_h29 = 0;
            LocoButtons.es5k_kontr_h30 = 0;
            LocoButtons.es5k_kontr_h31 = 0;
            LocoButtons.es5k_kontr_h32 = 0;
            LocoButtons.es5k_kontr_h33 = 0;
            LocoButtons.es5k_kontr_h34 = 0;
            LocoButtons.es5k_kontr_h35 = 0;
            LocoButtons.es5k_kontr_h36 = 0;

            LocoButtons.es5k_kontr_t4 = 0;
            LocoButtons.es5k_kontr_t5 = 0;
            LocoButtons.es5k_kontr_t6 = 0;
            LocoButtons.es5k_kontr_t7 = 0;
            LocoButtons.es5k_kontr_t8 = 0;
            LocoButtons.es5k_kontr_t9 = 0;
            LocoButtons.es5k_kontr_t10 = 0;
            LocoButtons.es5k_kontr_t11 = 0;
            LocoButtons.es5k_kontr_t12 = 0;
            LocoButtons.es5k_kontr_t13 = 0;
            LocoButtons.es5k_kontr_t14 = 0;
            LocoButtons.es5k_kontr_t15 = 0;
            LocoButtons.es5k_kontr_t16 = 0;
            LocoButtons.es5k_kontr_t17 = 0;
            LocoButtons.es5k_kontr_t18 = 0;
            LocoButtons.es5k_kontr_t19 = 0;
            LocoButtons.es5k_kontr_t20 = 0;
            LocoButtons.es5k_kontr_t21 = 0;
            LocoButtons.es5k_kontr_t22 = 0;
            LocoButtons.es5k_kontr_t23 = 0;
            LocoButtons.es5k_kontr_t24 = 0;
            LocoButtons.es5k_kontr_t25 = 0;
            LocoButtons.es5k_kontr_t26 = 0;
            LocoButtons.es5k_kontr_t27 = 0;
            LocoButtons.es5k_kontr_t28 = 0;
            LocoButtons.es5k_kontr_t29 = 0;
            LocoButtons.es5k_kontr_t30 = 0;
            LocoButtons.es5k_kontr_t31 = 0;
            LocoButtons.es5k_kontr_t32 = 0;
            LocoButtons.es5k_kontr_t33 = 0;
            LocoButtons.es5k_kontr_t34 = 0;
            LocoButtons.es5k_kontr_t35 = 0;
            LocoButtons.es5k_kontr_t36 = 0;//float -36 -4 0 4 36

            LocoButtons.es5k_rev_0 = 0;//int вп-1 0-0 наз-FFFF
            LocoButtons.es5k_rev_vpered = 0;
            LocoButtons.es5k_rev_nazad = 0;

            LocoButtons.es5k_reg_skor_140 = 0;//float 0-140
            LocoButtons.es5k_reg_skor_plus = 0;//по 5км
            LocoButtons.es5k_reg_skor_minus = 0;
            LocoButtons.es5k_kranTM_0 = 0;
            LocoButtons.es5k_kranTM_1 = 0;
            LocoButtons.es5k_bv_0 = 0;
            LocoButtons.es5k_bv_1 = 0;
            LocoButtons.es5k_vozvrat_bv = 0;
            LocoButtons.es5k_tokopr_per_0 = 0;
            LocoButtons.es5k_tokopr_per_1 = 0;
            LocoButtons.es5k_tokopr_zad_0 = 0;
            LocoButtons.es5k_tokopr_zad_1 = 0;
            LocoButtons.es5k_upravlenie_0 = 0;
            LocoButtons.es5k_upravlenie_1 = 0;
            LocoButtons.es5k_komp_0 = 0;
            LocoButtons.es5k_komp_1 = 0;
            LocoButtons.es5k_vent1_0 = 0;
            LocoButtons.es5k_vent1_1 = 0;
            LocoButtons.es5k_vent2_0 = 0;
            LocoButtons.es5k_vent2_1 = 0;
            LocoButtons.es5k_MSUD_0 = 0;
            LocoButtons.es5k_MSUD_1 = 0;
            LocoButtons.es5k_vspom_mash_0 = 0;
            LocoButtons.es5k_vspom_mash_1 = 0;
            LocoButtons.es5k_svet_cab_0 = 0;//0,1
            LocoButtons.es5k_svet_cab_1 = 0;
            LocoButtons.es5k_EPK_0 = 0;
            LocoButtons.es5k_EPK_1 = 0;
            LocoButtons.es5k_sign_0 = 0;
            LocoButtons.es5k_sign_1 = 0;
            LocoButtons.es5k_signC1_0 = 0;
            LocoButtons.es5k_signC1_1 = 0;
            LocoButtons.es5k_signC2_0 = 0;
            LocoButtons.es5k_signC2_1 = 0;
            LocoButtons.es5k_prozh_0 = 0;//0,1,2
            LocoButtons.es5k_prozh_1 = 0;
            LocoButtons.es5k_prozh_2 = 0;
            LocoButtons.es5k_avtoreg_0 = 0;
            LocoButtons.es5k_avtoreg_1 = 0;

            //ep1m 111
            LocoButtons.ep1m_kontr_0 = 0;
            LocoButtons.ep1m_kontr_h4 = 0;
            LocoButtons.ep1m_kontr_h5 = 0;
            LocoButtons.ep1m_kontr_h6 = 0;
            LocoButtons.ep1m_kontr_h7 = 0;
            LocoButtons.ep1m_kontr_h8 = 0;
            LocoButtons.ep1m_kontr_h9 = 0;
            LocoButtons.ep1m_kontr_h10 = 0;
            LocoButtons.ep1m_kontr_h11 = 0;
            LocoButtons.ep1m_kontr_h12 = 0;
            LocoButtons.ep1m_kontr_h13 = 0;
            LocoButtons.ep1m_kontr_h14 = 0;
            LocoButtons.ep1m_kontr_h15 = 0;
            LocoButtons.ep1m_kontr_h16 = 0;
            LocoButtons.ep1m_kontr_h17 = 0;
            LocoButtons.ep1m_kontr_h18 = 0;
            LocoButtons.ep1m_kontr_h19 = 0;
            LocoButtons.ep1m_kontr_h20 = 0;
            LocoButtons.ep1m_kontr_h21 = 0;
            LocoButtons.ep1m_kontr_h22 = 0;
            LocoButtons.ep1m_kontr_h23 = 0;
            LocoButtons.ep1m_kontr_h24 = 0;
            LocoButtons.ep1m_kontr_h25 = 0;
            LocoButtons.ep1m_kontr_h26 = 0;
            LocoButtons.ep1m_kontr_h27 = 0;
            LocoButtons.ep1m_kontr_h28 = 0;
            LocoButtons.ep1m_kontr_h29 = 0;
            LocoButtons.ep1m_kontr_h30 = 0;
            LocoButtons.ep1m_kontr_h31 = 0;
            LocoButtons.ep1m_kontr_h32 = 0;
            LocoButtons.ep1m_kontr_h33 = 0;
            LocoButtons.ep1m_kontr_h34 = 0;
            LocoButtons.ep1m_kontr_h35 = 0;
            LocoButtons.ep1m_kontr_h36 = 0;

            LocoButtons.ep1m_kontr_t4 = 0;
            LocoButtons.ep1m_kontr_t5 = 0;
            LocoButtons.ep1m_kontr_t6 = 0;
            LocoButtons.ep1m_kontr_t7 = 0;
            LocoButtons.ep1m_kontr_t8 = 0;
            LocoButtons.ep1m_kontr_t9 = 0;
            LocoButtons.ep1m_kontr_t10 = 0;
            LocoButtons.ep1m_kontr_t11 = 0;
            LocoButtons.ep1m_kontr_t12 = 0;
            LocoButtons.ep1m_kontr_t13 = 0;
            LocoButtons.ep1m_kontr_t14 = 0;
            LocoButtons.ep1m_kontr_t15 = 0;
            LocoButtons.ep1m_kontr_t16 = 0;
            LocoButtons.ep1m_kontr_t17 = 0;
            LocoButtons.ep1m_kontr_t18 = 0;
            LocoButtons.ep1m_kontr_t19 = 0;
            LocoButtons.ep1m_kontr_t20 = 0;
            LocoButtons.ep1m_kontr_t21 = 0;
            LocoButtons.ep1m_kontr_t22 = 0;
            LocoButtons.ep1m_kontr_t23 = 0;
            LocoButtons.ep1m_kontr_t24 = 0;
            LocoButtons.ep1m_kontr_t25 = 0;
            LocoButtons.ep1m_kontr_t26 = 0;
            LocoButtons.ep1m_kontr_t27 = 0;
            LocoButtons.ep1m_kontr_t28 = 0;
            LocoButtons.ep1m_kontr_t29 = 0;
            LocoButtons.ep1m_kontr_t30 = 0;
            LocoButtons.ep1m_kontr_t31 = 0;
            LocoButtons.ep1m_kontr_t32 = 0;
            LocoButtons.ep1m_kontr_t33 = 0;
            LocoButtons.ep1m_kontr_t34 = 0;
            LocoButtons.ep1m_kontr_t35 = 0;
            LocoButtons.ep1m_kontr_t36 = 0;//float -36 -4 0 4 36

            LocoButtons.ep1m_rev_0 = 0;//int вп-1 0-0 наз-FFFF
            LocoButtons.ep1m_rev_vpered = 0;
            LocoButtons.ep1m_rev_nazad = 0;

            LocoButtons.ep1m_reg_skor_160 = 0;//float 0-160
            LocoButtons.ep1m_reg_skor_plus = 0;//по 5км
            LocoButtons.ep1m_reg_skor_minus = 0;
            LocoButtons.ep1m_kranTM_0 = 0;
            LocoButtons.ep1m_kranTM_1 = 0;
            LocoButtons.ep1m_bv_0 = 0;
            LocoButtons.ep1m_bv_1 = 0;
            LocoButtons.ep1m_vozvrat_zaschity = 0;
            LocoButtons.ep1m_blok_vvk_0 = 0;
            LocoButtons.ep1m_blok_vvk_1 = 0;
            LocoButtons.ep1m_tokopr_per_0 = 0;
            LocoButtons.ep1m_tokopr_per_1 = 0;
            LocoButtons.ep1m_tokopr_zad_0 = 0;
            LocoButtons.ep1m_tokopr_zad_1 = 0;
            LocoButtons.ep1m_upravlenie = 0;
            LocoButtons.ep1m_komp_0 = 0;
            LocoButtons.ep1m_komp_1 = 0;
            LocoButtons.ep1m_vent1_0 = 0;
            LocoButtons.ep1m_vent1_1 = 0;
            LocoButtons.ep1m_vent2_0 = 0;
            LocoButtons.ep1m_vent2_1 = 0;
            LocoButtons.ep1m_vent3_0 = 0;
            LocoButtons.ep1m_vent3_1 = 0;
            LocoButtons.ep1m_MSUD_0 = 0;
            LocoButtons.ep1m_MSUD_1 = 0;
            LocoButtons.ep1m_vspom_mash_0 = 0;
            LocoButtons.ep1m_vspom_mash_1 = 0;
            LocoButtons.ep1m_svet_cab_0 = 0;//0,1,2
            LocoButtons.ep1m_svet_cab_1 = 0;
            LocoButtons.ep1m_svet_cab_2 = 0;
            LocoButtons.ep1m_EPK_0 = 0;
            LocoButtons.ep1m_EPK_1 = 0;
            LocoButtons.ep1m_EPT_0 = 0;
            LocoButtons.ep1m_EPT_1 = 0;
            LocoButtons.ep1m_sign_0 = 0;
            LocoButtons.ep1m_sign_1 = 0;
            LocoButtons.ep1m_prozh_0 = 0;//0,1,2
            LocoButtons.ep1m_prozh_1 = 0;
            LocoButtons.ep1m_prozh_2 = 0;
            LocoButtons.ep1m_avtoreg_0 = 0;
            LocoButtons.ep1m_avtoreg_1 = 0;

            //chs2k 31
            LocoButtons.chs2k_rev_0 = 0;//int вп-1 0-0 наз-FFFF
            LocoButtons.chs2k_rev_vpered = 0;
            LocoButtons.chs2k_rev_nazad = 0;

            LocoButtons.chs2k_kontr_0 = 0;
            LocoButtons.chs2k_kontr_plus = 0;
            LocoButtons.chs2k_kontr_minus = 0;
            LocoButtons.chs2k_kontr_plus1 = 0;
            LocoButtons.chs2k_kontr_minus1 = 0;
            LocoButtons.chs2k_kranTM_0 = 0;
            LocoButtons.chs2k_kranTM_1 = 0;
            LocoButtons.chs2k_bv_0 = 0; //через P
            LocoButtons.chs2k_bv_1 = 0;//через Shift P
            LocoButtons.chs2k_tokopr_per_0 = 0;
            LocoButtons.chs2k_tokopr_per_1 = 0;
            LocoButtons.chs2k_tokopr_zad_0 = 0;
            LocoButtons.chs2k_tokopr_zad_1 = 0;
            LocoButtons.chs2k_komp1_0 = 0;
            LocoButtons.chs2k_komp1_1 = 0;
            LocoButtons.chs2k_komp2_0 = 0;
            LocoButtons.chs2k_komp2_1 = 0;
            LocoButtons.chs2k_vent_0 = 0;
            LocoButtons.chs2k_vent_1 = 0;
            LocoButtons.chs2k_svet_cab_0 = 0;//0,1,2
            LocoButtons.chs2k_svet_cab_1 = 0;
            LocoButtons.chs2k_svet_cab_2 = 0;
            LocoButtons.chs2k_EPK_0 = 0;
            LocoButtons.chs2k_EPK_1 = 0;
            LocoButtons.chs2k_EPT_0 = 0;
            LocoButtons.chs2k_EPT_1 = 0;
            LocoButtons.chs2k_prozh_0 = 0;//float 0-1.75
            LocoButtons.chs2k_prozh_1 = 0;
            LocoButtons.chs2k_prozh_2 = 0;

            //chs4 55
            LocoButtons.chs4_rev_0 = 0;//вп-0 0-1 наз-2
            LocoButtons.chs4_rev_vpered = 0;
            LocoButtons.chs4_rev_nazad = 0;

            LocoButtons.chs4_kontr_0 = 0; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
            LocoButtons.chs4_kontr_plus = 0;
            LocoButtons.chs4_kontr_minus = 0;
            LocoButtons.chs4_kontr_plus1 = 0;
            LocoButtons.chs4_kontr_minus1 = 0;
            LocoButtons.chs4_kontr_shunt1 = 0;
            LocoButtons.chs4_kontr_shunt2 = 0;
            LocoButtons.chs4_kontr_shunt3 = 0;
            LocoButtons.chs4_kontr_shunt4 = 0;
            LocoButtons.chs4_kontr_shunt5 = 0;
            LocoButtons.chs4_kranTM_0 = 0;
            LocoButtons.chs4_kranTM_1 = 0;
            LocoButtons.chs4_tokopr_per_0 = 0;
            LocoButtons.chs4_tokopr_per_1 = 0;
            LocoButtons.chs4_tokopr_zad_0 = 0;
            LocoButtons.chs4_tokopr_zad_1 = 0;
            LocoButtons.chs4_bv_0 = 0; //0-0 1-1 восст-2
            LocoButtons.chs4_bv_1 = 0;
            LocoButtons.chs4_bv_2 = 0;
            LocoButtons.chs4_komp1_0 = 0;//0-2
            LocoButtons.chs4_komp1_1 = 0;
            LocoButtons.chs4_komp1_2 = 0;
            LocoButtons.chs4_komp2_0 = 0;
            LocoButtons.chs4_komp2_1 = 0;
            LocoButtons.chs4_komp2_2 = 0;
            LocoButtons.chs4_vent_0 = 0; //0-7
            LocoButtons.chs4_vent_1 = 0;
            LocoButtons.chs4_vent_2 = 0;
            LocoButtons.chs4_vent_3 = 0;
            LocoButtons.chs4_vent_4 = 0;
            LocoButtons.chs4_vent_5 = 0;
            LocoButtons.chs4_vent_6 = 0;
            LocoButtons.chs4_vent_7 = 0;
            LocoButtons.chs4_vspom_komp_0 = 0;//0,0-1,песок-2,Авто-3
            LocoButtons.chs4_vspom_komp_1 = 0;
            LocoButtons.chs4_vspom_komp_2 = 0;
            LocoButtons.chs4_vspom_komp_3 = 0;
            LocoButtons.chs4_svet_cab_0 = 0;//зел-0,приб-1,0-2,общ-3
            LocoButtons.chs4_svet_cab_1 = 0;
            LocoButtons.chs4_svet_cab_2 = 0;
            LocoButtons.chs4_svet_cab_3 = 0;
            LocoButtons.chs4_EPK_0 = 0;
            LocoButtons.chs4_EPK_1 = 0;
            LocoButtons.chs4_EPT_0 = 0;
            LocoButtons.chs4_EPT_1 = 0;
            LocoButtons.chs4_avar_nabor_0 = 0;
            LocoButtons.chs4_avar_nabor_1 = 0;
            LocoButtons.chs4_avar_nabor_2 = 0;
            LocoButtons.chs4_avar_nabor_3 = 0;
            LocoButtons.chs4_prozh_0 = 0; //0,1,2
            LocoButtons.chs4_prozh_1 = 0;
            LocoButtons.chs4_prozh_2 = 0;

            //chs4kvr 55
            LocoButtons.chs4kvr_rev_0 = 0;//вп-0 0-1 наз-2
            LocoButtons.chs4kvr_rev_vpered = 0;
            LocoButtons.chs4kvr_rev_nazad = 0;

            LocoButtons.chs4kvr_kontr_0 = 0; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
            LocoButtons.chs4kvr_kontr_plus = 0;
            LocoButtons.chs4kvr_kontr_minus = 0;
            LocoButtons.chs4kvr_kontr_plus1 = 0;
            LocoButtons.chs4kvr_kontr_minus1 = 0;
            LocoButtons.chs4kvr_kontr_shunt1 = 0;
            LocoButtons.chs4kvr_kontr_shunt2 = 0;
            LocoButtons.chs4kvr_kontr_shunt3 = 0;
            LocoButtons.chs4kvr_kontr_shunt4 = 0;
            LocoButtons.chs4kvr_kontr_shunt5 = 0;
            LocoButtons.chs4kvr_kranTM_0 = 0;
            LocoButtons.chs4kvr_kranTM_1 = 0;
            LocoButtons.chs4kvr_tokopr_per_0 = 0;
            LocoButtons.chs4kvr_tokopr_per_1 = 0;
            LocoButtons.chs4kvr_tokopr_zad_0 = 0;
            LocoButtons.chs4kvr_tokopr_zad_1 = 0;
            LocoButtons.chs4kvr_bv_0 = 0; //0-0 1-1 восст-2
            LocoButtons.chs4kvr_bv_1 = 0;
            LocoButtons.chs4kvr_bv_2 = 0;
            LocoButtons.chs4kvr_komp1_0 = 0;//0Т-0,0-1,А-2,Р-3
            LocoButtons.chs4kvr_komp1_1 = 0;
            LocoButtons.chs4kvr_komp1_2 = 0;
            LocoButtons.chs4kvr_komp2_0 = 0;
            LocoButtons.chs4kvr_komp2_1 = 0;
            LocoButtons.chs4kvr_komp2_2 = 0;
            LocoButtons.chs4kvr_vent_0 = 0; //0-7
            LocoButtons.chs4kvr_vent_1 = 0; //0-7
            LocoButtons.chs4kvr_vent_2 = 0; //0-7
            LocoButtons.chs4kvr_vent_3 = 0; //0-7
            LocoButtons.chs4kvr_vent_4 = 0; //0-7
            LocoButtons.chs4kvr_vent_5 = 0; //0-7
            LocoButtons.chs4kvr_vent_6 = 0; //0-7
            LocoButtons.chs4kvr_vent_7 = 0; //0-7
            LocoButtons.chs4kvr_vspom_komp_0 = 0;//0,0-1,песок-2,Авто-3
            LocoButtons.chs4kvr_vspom_komp_1 = 0;
            LocoButtons.chs4kvr_vspom_komp_2 = 0;
            LocoButtons.chs4kvr_vspom_komp_3 = 0;
            LocoButtons.chs4kvr_svet_cab_0 = 0;//зел-0,приб-1,0-2,общ-3
            LocoButtons.chs4kvr_svet_cab_1 = 0;
            LocoButtons.chs4kvr_svet_cab_2 = 0;
            LocoButtons.chs4kvr_svet_cab_3 = 0;
            LocoButtons.chs4kvr_EPK_0 = 0;
            LocoButtons.chs4kvr_EPK_1 = 0;
            LocoButtons.chs4kvr_EPT_0 = 0;
            LocoButtons.chs4kvr_EPT_1 = 0;
            LocoButtons.chs4kvr_avar_nabor_0 = 0;
            LocoButtons.chs4kvr_avar_nabor_1 = 0;
            LocoButtons.chs4kvr_avar_nabor_2 = 0;
            LocoButtons.chs4kvr_avar_nabor_3 = 0;
            LocoButtons.chs4kvr_prozh_0 = 0; //0,1,2
            LocoButtons.chs4kvr_prozh_1 = 0;
            LocoButtons.chs4kvr_prozh_2 = 0;

            //chs4t 52
            LocoButtons.chs4t_rev_0 = 0;//вп-0 0-1 наз-2
            LocoButtons.chs4t_rev_vpered = 0;
            LocoButtons.chs4t_rev_nazad = 0;

            LocoButtons.chs4t_kontr_0 = 0; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
            LocoButtons.chs4t_kontr_plus = 0;
            LocoButtons.chs4t_kontr_minus = 0;
            LocoButtons.chs4t_kontr_plus1 = 0;
            LocoButtons.chs4t_kontr_minus1 = 0;
            LocoButtons.chs4t_kontr_shunt1 = 0;
            LocoButtons.chs4t_kontr_shunt2 = 0;
            LocoButtons.chs4t_kontr_shunt3 = 0;
            LocoButtons.chs4t_kontr_shunt4 = 0;
            LocoButtons.chs4t_kontr_shunt5 = 0;
            LocoButtons.chs4t_kranTM_0 = 0;
            LocoButtons.chs4t_kranTM_1 = 0;
            LocoButtons.chs4t_tokopr_per_0 = 0;
            LocoButtons.chs4t_tokopr_per_1 = 0;
            LocoButtons.chs4t_tokopr_zad_0 = 0;
            LocoButtons.chs4t_tokopr_zad_1 = 0;
            LocoButtons.chs4t_bv_0 = 0; //0-0 1-1 восст-2
            LocoButtons.chs4t_bv_1 = 0;
            LocoButtons.chs4t_bv_2 = 0;
            LocoButtons.chs4t_komp1_0 = 0;//0Т-0,0-1,А-2,Р-3
            LocoButtons.chs4t_komp1_1 = 0;
            LocoButtons.chs4t_komp1_2 = 0;
            LocoButtons.chs4t_komp2_0 = 0;
            LocoButtons.chs4t_komp2_1 = 0;
            LocoButtons.chs4t_komp2_2 = 0;
            LocoButtons.chs4t_vent_0 = 0; //1,2раб,0-выкл
            LocoButtons.chs4t_vent_1 = 0;
            LocoButtons.chs4t_vent_2 = 0;
            LocoButtons.chs4t_vspom_komp_0 = 0;//0,0-1,песок-2,Авто-3
            LocoButtons.chs4t_vspom_komp_1 = 0;
            LocoButtons.chs4t_vspom_komp_2 = 0;
            LocoButtons.chs4t_vspom_komp_3 = 0;
            LocoButtons.chs4t_svet_cab_0 = 0;//зел-0,приб-1,0-2,общ-3
            LocoButtons.chs4t_svet_cab_1 = 0;
            LocoButtons.chs4t_svet_cab_2 = 0;
            LocoButtons.chs4t_svet_cab_3 = 0;
            LocoButtons.chs4t_EPK_0 = 0;
            LocoButtons.chs4t_EPK_1 = 0;
            LocoButtons.chs4t_EPT_0 = 0;
            LocoButtons.chs4t_EPT_1 = 0;
            LocoButtons.chs4t_avar_nabor_0 = 0;
            LocoButtons.chs4t_avar_nabor_1 = 0;
            LocoButtons.chs4t_avar_nabor_2 = 0;
            LocoButtons.chs4t_avar_nabor_3 = 0;
            LocoButtons.chs4t_prozh_0 = 0; //0,1,2
            LocoButtons.chs4t_prozh_1 = 0;
            LocoButtons.chs4t_prozh_2 = 0;
            LocoButtons.chs4t_zhalyzi_0 = 0;
            LocoButtons.chs4t_zhalyzi_1 = 0;

            //chs7 45
            LocoButtons.chs7_rev_0 = 0;//вп1 0-0 нз255
            LocoButtons.chs7_rev_vpered = 0;
            LocoButtons.chs7_rev_nazad = 0;

            LocoButtons.chs7_kontr_0 = 0; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
            LocoButtons.chs7_kontr_plus = 0;
            LocoButtons.chs7_kontr_minus = 0;
            LocoButtons.chs7_kontr_plus1 = 0;
            LocoButtons.chs7_kontr_minus1 = 0;
            LocoButtons.chs7_kontr_shunt1 = 0;
            LocoButtons.chs7_kontr_shunt2 = 0;
            LocoButtons.chs7_kontr_shunt3 = 0;
            LocoButtons.chs7_kontr_shunt4 = 0;
            LocoButtons.chs7_kontr_shunt5 = 0;
            LocoButtons.chs7_kranTM_0 = 0;
            LocoButtons.chs7_kranTM_1 = 0;
            LocoButtons.chs7_tokopr_per_0 = 0;//0-2 2через shift I,O
            LocoButtons.chs7_tokopr_per_1 = 0;
            LocoButtons.chs7_tokopr_per_2 = 0;
            LocoButtons.chs7_tokopr_zad_0 = 0;
            LocoButtons.chs7_tokopr_zad_1 = 0;
            LocoButtons.chs7_tokopr_zad_2 = 0;
            LocoButtons.chs7_bv_0 = 0; //0-0 1-1 восст-2 через shift P
            LocoButtons.chs7_bv_1 = 0;
            LocoButtons.chs7_bv_2 = 0;
            LocoButtons.chs7_komp1_0 = 0;//0-0,1А,2Р
            LocoButtons.chs7_komp1_1 = 0;
            LocoButtons.chs7_komp1_2 = 0;
            LocoButtons.chs7_komp2_0 = 0;
            LocoButtons.chs7_komp2_1 = 0;
            LocoButtons.chs7_komp2_2 = 0;
            LocoButtons.chs7_vent_0 = 0; //0выкл 1вкл_пр 255вкл_лев
            LocoButtons.chs7_vent_1 = 0;
            LocoButtons.chs7_vent_2 = 0;
            LocoButtons.chs7_sbros_SP = 0;
            LocoButtons.chs7_svet_cab_0 = 0;//0выкл,приб-1,2общ
            LocoButtons.chs7_svet_cab_1 = 0;
            LocoButtons.chs7_svet_cab_2 = 0;
            LocoButtons.chs7_EPK_0 = 0;
            LocoButtons.chs7_EPK_1 = 0;
            LocoButtons.chs7_EPT_0 = 0;
            LocoButtons.chs7_EPT_1 = 0;
            LocoButtons.chs7_prozh_0 = 0;//float 0-1,75
            LocoButtons.chs7_prozh_1 = 0;
            LocoButtons.chs7_prozh_2 = 0;
            LocoButtons.chs7_zhalyzi1_0 = 0;
            LocoButtons.chs7_zhalyzi1_1 = 0;

            //chs8 63
            LocoButtons.chs8_rev_0 = 0;//вп0 0-1 нз2
            LocoButtons.chs8_rev_vpered = 0;
            LocoButtons.chs8_rev_nazad = 0;

            LocoButtons.chs8_kontr_0 = 0; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
            LocoButtons.chs8_kontr_plus = 0;
            LocoButtons.chs8_kontr_minus = 0;
            LocoButtons.chs8_kontr_plus1 = 0;
            LocoButtons.chs8_kontr_minus1 = 0;
            LocoButtons.chs8_kontr_shunt1 = 0;
            LocoButtons.chs8_kontr_shunt2 = 0;
            LocoButtons.chs8_kontr_shunt3 = 0;
            LocoButtons.chs8_kontr_shunt4 = 0;
            LocoButtons.chs8_kontr_shunt5 = 0;
            LocoButtons.chs8_kranTM_0 = 0;
            LocoButtons.chs8_kranTM_1 = 0;
            LocoButtons.chs8_tokopr_per_0 = 0;//0-1
            LocoButtons.chs8_tokopr_per_1 = 0;
            LocoButtons.chs8_tokopr_zad_0 = 0;
            LocoButtons.chs8_tokopr_zad_1 = 0;
            LocoButtons.chs8_bv_0 = 0; //вкл БВ 0-0 1-1 
            LocoButtons.chs8_bv_1 = 0;
            LocoButtons.chs8_vosst_bv = 0;//через K
            LocoButtons.chs8_komp1_0 = 0;//0выкл,1А,2Р
            LocoButtons.chs8_komp1_1 = 0;
            LocoButtons.chs8_komp1_2 = 0;
            LocoButtons.chs8_komp2_0 = 0;
            LocoButtons.chs8_komp2_1 = 0;
            LocoButtons.chs8_komp2_2 = 0;
            LocoButtons.chs8_vent1_0 = 0; //0выкл,1авто,2-4раб
            LocoButtons.chs8_vent1_1 = 0;
            LocoButtons.chs8_vent1_2 = 0;
            LocoButtons.chs8_vent1_3 = 0;
            LocoButtons.chs8_vent1_4 = 0;
            LocoButtons.chs8_vent2_0 = 0; //0выкл,1авто,2-4раб
            LocoButtons.chs8_vent2_1 = 0;
            LocoButtons.chs8_vent2_2 = 0;
            LocoButtons.chs8_vent2_3 = 0;
            LocoButtons.chs8_vent2_4 = 0;
            LocoButtons.chs8_vspom_komp_0 = 0;//0выкл,1песок,2авто,3комп
            LocoButtons.chs8_vspom_komp_1 = 0;
            LocoButtons.chs8_vspom_komp_2 = 0;
            LocoButtons.chs8_vspom_komp_3 = 0;
            LocoButtons.chs8_svet_cab_0 = 0;//0зел,1приб,2выкл,3общ,4приб,5зел
            LocoButtons.chs8_svet_cab_1 = 0;
            LocoButtons.chs8_svet_cab_2 = 0;
            LocoButtons.chs8_svet_cab_3 = 0;
            LocoButtons.chs8_svet_cab_4 = 0;
            LocoButtons.chs8_svet_cab_5 = 0;
            LocoButtons.chs8_EPK_0 = 0;
            LocoButtons.chs8_EPK_1 = 0;
            LocoButtons.chs8_EPT_0 = 0;
            LocoButtons.chs8_EPT_1 = 0;
            LocoButtons.chs8_avar_nabor_0 = 0;
            LocoButtons.chs8_avar_nabor_1 = 0;
            LocoButtons.chs8_avar_nabor_2 = 0;
            LocoButtons.chs8_avar_nabor_3 = 0;
            LocoButtons.chs8_prozh_0 = 0; //0,1,2
            LocoButtons.chs8_prozh_1 = 0;
            LocoButtons.chs8_prozh_2 = 0;
            LocoButtons.chs8_reost_torm_proverka = 0;//0выкл,1проверка через backspace
            LocoButtons.chs8_reost_torm_0 = 0;//0выкл,1середина,2торм через >
            LocoButtons.chs8_reost_torm_1 = 0;
            LocoButtons.chs8_reost_torm_2 = 0;

            //vl11 82
            LocoButtons.vl11_rev_0 = 0;//вп1 0-0 нз255
            LocoButtons.vl11_rev_vpered = 0;
            LocoButtons.vl11_rev_nazad = 0;

            LocoButtons.vl11_kontr_0 = 0;
            LocoButtons.vl11_kontr_1 = 0;
            LocoButtons.vl11_kontr_2 = 0;
            LocoButtons.vl11_kontr_3 = 0;
            LocoButtons.vl11_kontr_4 = 0;
            LocoButtons.vl11_kontr_5 = 0;
            LocoButtons.vl11_kontr_6 = 0;
            LocoButtons.vl11_kontr_7 = 0;
            LocoButtons.vl11_kontr_8 = 0;
            LocoButtons.vl11_kontr_9 = 0;
            LocoButtons.vl11_kontr_10 = 0;
            LocoButtons.vl11_kontr_11 = 0;
            LocoButtons.vl11_kontr_12 = 0;
            LocoButtons.vl11_kontr_13 = 0;
            LocoButtons.vl11_kontr_14 = 0;
            LocoButtons.vl11_kontr_15 = 0;
            LocoButtons.vl11_kontr_16 = 0;
            LocoButtons.vl11_kontr_17 = 0;
            LocoButtons.vl11_kontr_18 = 0;
            LocoButtons.vl11_kontr_19 = 0;
            LocoButtons.vl11_kontr_20 = 0;
            LocoButtons.vl11_kontr_21 = 0;
            LocoButtons.vl11_kontr_22 = 0;
            LocoButtons.vl11_kontr_23 = 0;
            LocoButtons.vl11_kontr_24 = 0;
            LocoButtons.vl11_kontr_25 = 0;
            LocoButtons.vl11_kontr_26 = 0;
            LocoButtons.vl11_kontr_27 = 0;
            LocoButtons.vl11_kontr_28 = 0;
            LocoButtons.vl11_kontr_29 = 0;
            LocoButtons.vl11_kontr_30 = 0;
            LocoButtons.vl11_kontr_31 = 0;
            LocoButtons.vl11_kontr_32 = 0;
            LocoButtons.vl11_kontr_33 = 0;
            LocoButtons.vl11_kontr_34 = 0;
            LocoButtons.vl11_kontr_35 = 0;
            LocoButtons.vl11_kontr_36 = 0;
            LocoButtons.vl11_kontr_37 = 0;
            LocoButtons.vl11_kontr_38 = 0;
            LocoButtons.vl11_kontr_39 = 0;
            LocoButtons.vl11_kontr_40 = 0;
            LocoButtons.vl11_kontr_41 = 0;
            LocoButtons.vl11_kontr_42 = 0;
            LocoButtons.vl11_kontr_43 = 0;
            LocoButtons.vl11_kontr_44 = 0;
            LocoButtons.vl11_kontr_45 = 0;
            LocoButtons.vl11_kontr_46 = 0;
            LocoButtons.vl11_kontr_47 = 0;
            LocoButtons.vl11_kontr_48 = 0;

            LocoButtons.vl11_kontr_shunt_0 = 0;//0выкл 255-252
            LocoButtons.vl11_kontr_shunt_1 = 0;
            LocoButtons.vl11_kontr_shunt_2 = 0;
            LocoButtons.vl11_kontr_shunt_3 = 0;
            LocoButtons.vl11_kontr_shunt_4 = 0;

            LocoButtons.vl11_kranTM_0 = 0;
            LocoButtons.vl11_kranTM_1 = 0;
            LocoButtons.vl11_tokopr_obshiy_0 = 0;
            LocoButtons.vl11_tokopr_obshiy_1 = 0;
            LocoButtons.vl11_tokopr_per_0 = 0;
            LocoButtons.vl11_tokopr_per_1 = 0;
            LocoButtons.vl11_tokopr_zad_0 = 0;
            LocoButtons.vl11_tokopr_zad_1 = 0;
            LocoButtons.vl11_bv_0 = 0; //БВ 0-0 1-1
            LocoButtons.vl11_bv_1 = 0;
            LocoButtons.vl11_vosst_bv = 0;//через K
            LocoButtons.vl11_komp_0 = 0;//0выкл,1вкл
            LocoButtons.vl11_komp_1 = 0;
            LocoButtons.vl11_vent_0 = 0; //0выкл,1низ,2выс
            LocoButtons.vl11_vent_1 = 0;
            LocoButtons.vl11_vent_2 = 0;
            LocoButtons.vl11_svet_cab_0 = 0;//0выкл,1приб,2общ
            LocoButtons.vl11_svet_cab_1 = 0;
            LocoButtons.vl11_svet_cab_2 = 0;
            LocoButtons.vl11_EPK_0 = 0;
            LocoButtons.vl11_EPK_1 = 0;
            LocoButtons.vl11_prozh_0 = 0; //float 0-1,875
            LocoButtons.vl11_prozh_1 = 0;
            LocoButtons.vl11_prozh_2 = 0;
            LocoButtons.vl11_sign_0 = 0;
            LocoButtons.vl11_sign_1 = 0;

            //vl82 83
            LocoButtons.vl82_rev_0 = 0;//0нз,1-0,2вп
            LocoButtons.vl82_rev_vpered = 0;
            LocoButtons.vl82_rev_nazad = 0;

            LocoButtons.vl82_kontr_bv = 0;//0-38 БВ_255 ,клавD для откл БВ
            LocoButtons.vl82_kontr_0 = 0;
            LocoButtons.vl82_kontr_1 = 0;
            LocoButtons.vl82_kontr_2 = 0;
            LocoButtons.vl82_kontr_3 = 0;
            LocoButtons.vl82_kontr_4 = 0;
            LocoButtons.vl82_kontr_5 = 0;
            LocoButtons.vl82_kontr_6 = 0;
            LocoButtons.vl82_kontr_7 = 0;
            LocoButtons.vl82_kontr_8 = 0;
            LocoButtons.vl82_kontr_9 = 0;
            LocoButtons.vl82_kontr_10 = 0;
            LocoButtons.vl82_kontr_11 = 0;
            LocoButtons.vl82_kontr_12 = 0;
            LocoButtons.vl82_kontr_13 = 0;
            LocoButtons.vl82_kontr_14 = 0;
            LocoButtons.vl82_kontr_15 = 0;
            LocoButtons.vl82_kontr_16 = 0;
            LocoButtons.vl82_kontr_17 = 0;
            LocoButtons.vl82_kontr_18 = 0;
            LocoButtons.vl82_kontr_19 = 0;
            LocoButtons.vl82_kontr_20 = 0;
            LocoButtons.vl82_kontr_21 = 0;
            LocoButtons.vl82_kontr_22 = 0;
            LocoButtons.vl82_kontr_23 = 0;
            LocoButtons.vl82_kontr_24 = 0;
            LocoButtons.vl82_kontr_25 = 0;
            LocoButtons.vl82_kontr_26 = 0;
            LocoButtons.vl82_kontr_27 = 0;
            LocoButtons.vl82_kontr_28 = 0;
            LocoButtons.vl82_kontr_29 = 0;
            LocoButtons.vl82_kontr_30 = 0;
            LocoButtons.vl82_kontr_31 = 0;
            LocoButtons.vl82_kontr_32 = 0;
            LocoButtons.vl82_kontr_33 = 0;
            LocoButtons.vl82_kontr_34 = 0;
            LocoButtons.vl82_kontr_35 = 0;
            LocoButtons.vl82_kontr_36 = 0;
            LocoButtons.vl82_kontr_37 = 0;
            LocoButtons.vl82_kontr_38 = 0;

            LocoButtons.vl82_kontr_shunt_0 = 0;//0выкл,1-4шунты,255реостат
            LocoButtons.vl82_kontr_shunt_1 = 0;
            LocoButtons.vl82_kontr_shunt_2 = 0;
            LocoButtons.vl82_kontr_shunt_3 = 0;
            LocoButtons.vl82_kontr_shunt_4 = 0;
            LocoButtons.vl82_kontr_shunt_reostat = 0;

            LocoButtons.vl82_kranTM_0 = 0;
            LocoButtons.vl82_kranTM_1 = 0;
            LocoButtons.vl82_tokopr_obshiy_0 = 0;
            LocoButtons.vl82_tokopr_obshiy_1 = 0;
            LocoButtons.vl82_tokopr_per_0 = 0;
            LocoButtons.vl82_tokopr_per_1 = 0;
            LocoButtons.vl82_tokopr_zad_0 = 0;
            LocoButtons.vl82_tokopr_zad_1 = 0;
            LocoButtons.vl82_gv_0 = 0; //ГВ 0-0 1-1
            LocoButtons.vl82_gv_1 = 0;
            LocoButtons.vl82_bv_0 = 0;
            LocoButtons.vl82_bv_1 = 0;
            LocoButtons.vl82_vosst_gv = 0;//через K
            LocoButtons.vl82_komp_0 = 0;//0выкл,1вкл
            LocoButtons.vl82_komp_1 = 0;
            LocoButtons.vl82_vent1_0 = 0;
            LocoButtons.vl82_vent1_1 = 0;
            LocoButtons.vl82_vent2_0 = 0;
            LocoButtons.vl82_vent2_1 = 0;
            LocoButtons.vl82_kvc_0 = 0;
            LocoButtons.vl82_kvc_1 = 0;
            LocoButtons.vl82_vozvr_kvc = 0;//через Y
            LocoButtons.vl82_upravlenie_0 = 0;
            LocoButtons.vl82_upravlenie_1 = 0;
            LocoButtons.vl82_svet_cab_0 = 0;//0выкл,1приб,2общ
            LocoButtons.vl82_svet_cab_1 = 0;
            LocoButtons.vl82_svet_cab_2 = 0;
            LocoButtons.vl82_EPK_0 = 0;
            LocoButtons.vl82_EPK_1 = 0;
            LocoButtons.vl82_prozh_0 = 0;//0,1,2
            LocoButtons.vl82_prozh_1 = 0;
            LocoButtons.vl82_prozh_2 = 0;
            LocoButtons.vl82_sign_0 = 0;
            LocoButtons.vl82_sign_1 = 0;

            //vl80t 51
            LocoButtons.vl80t_rev_0 = 0;//255нз,0-0,1вп,2-4шунты
            LocoButtons.vl80t_rev_vpered = 0;
            LocoButtons.vl80t_rev_nazad = 0;
            LocoButtons.vl80t_rev_shunt1 = 0;
            LocoButtons.vl80t_rev_shunt2 = 0;
            LocoButtons.vl80t_rev_shunt3 = 0;

            LocoButtons.vl80t_kontr_bv = 0;//255выкл_бв,0-0,1ав,2рв,3фв,4фп,5рп,6ап ,клавD для откл ГВ
            LocoButtons.vl80t_kontr_0 = 0;
            LocoButtons.vl80t_kontr_1 = 0;
            LocoButtons.vl80t_kontr_2 = 0;
            LocoButtons.vl80t_kontr_3 = 0;
            LocoButtons.vl80t_kontr_4 = 0;
            LocoButtons.vl80t_kontr_5 = 0;
            LocoButtons.vl80t_kontr_6 = 0;//клавA для 6 полож

            LocoButtons.vl80t_kranTM_0 = 0;
            LocoButtons.vl80t_kranTM_1 = 0;
            LocoButtons.vl80t_tokopr_obshiy_0 = 0;
            LocoButtons.vl80t_tokopr_obshiy_1 = 0;
            LocoButtons.vl80t_tokopr_per_0 = 0;
            LocoButtons.vl80t_tokopr_per_1 = 0;
            LocoButtons.vl80t_tokopr_zad_0 = 0;
            LocoButtons.vl80t_tokopr_zad_1 = 0;
            LocoButtons.vl80t_gv_0 = 0; //ГВ 0-0 1-1
            LocoButtons.vl80t_gv_1 = 0;
            LocoButtons.vl80t_vosst_gv = 0;//через K
            LocoButtons.vl80t_komp_0 = 0;//0выкл,1вкл
            LocoButtons.vl80t_komp_1 = 0;
            LocoButtons.vl80t_vent1_0 = 0;
            LocoButtons.vl80t_vent1_1 = 0;
            LocoButtons.vl80t_vent2_0 = 0;
            LocoButtons.vl80t_vent2_1 = 0;
            LocoButtons.vl80t_vent3_0 = 0;
            LocoButtons.vl80t_vent3_1 = 0;
            LocoButtons.vl80t_vent4_0 = 0;
            LocoButtons.vl80t_vent4_1 = 0;
            LocoButtons.vl80t_fz_0 = 0;
            LocoButtons.vl80t_fz_1 = 0;
            LocoButtons.vl80t_upravlenie_0 = 0;
            LocoButtons.vl80t_upravlenie_1 = 0;
            LocoButtons.vl80t_svet_cab_0 = 0;//0выкл,1приб,2общ
            LocoButtons.vl80t_svet_cab_1 = 0;
            LocoButtons.vl80t_svet_cab_2 = 0;
            LocoButtons.vl80t_EPK_0 = 0;
            LocoButtons.vl80t_EPK_1 = 0;
            LocoButtons.vl80t_prozh_0 = 0;//0,1,2
            LocoButtons.vl80t_prozh_1 = 0;
            LocoButtons.vl80t_prozh_2 = 0;
            LocoButtons.vl80t_sign_0 = 0;
            LocoButtons.vl80t_sign_1 = 0;

            //vl85 80
            LocoButtons.vl85_rev_0 = 0;//255нз,0-0,1вп,2-4шунты
            LocoButtons.vl85_rev_vpered = 0;
            LocoButtons.vl85_rev_nazad = 0;
            LocoButtons.vl85_rev_shunt1 = 0;
            LocoButtons.vl85_rev_shunt2 = 0;
            LocoButtons.vl85_rev_shunt3 = 0;

            LocoButtons.vl85_kontr_bv = 0;//0выкл,255откл.БВ,1-32поз, клав D для откл БВ
            LocoButtons.vl85_kontr_0 = 0;
            LocoButtons.vl85_kontr_1 = 0;
            LocoButtons.vl85_kontr_2 = 0;
            LocoButtons.vl85_kontr_3 = 0;
            LocoButtons.vl85_kontr_4 = 0;
            LocoButtons.vl85_kontr_5 = 0;
            LocoButtons.vl85_kontr_6 = 0;
            LocoButtons.vl85_kontr_7 = 0;
            LocoButtons.vl85_kontr_8 = 0;
            LocoButtons.vl85_kontr_9 = 0;
            LocoButtons.vl85_kontr_10 = 0;
            LocoButtons.vl85_kontr_11 = 0;
            LocoButtons.vl85_kontr_12 = 0;
            LocoButtons.vl85_kontr_13 = 0;
            LocoButtons.vl85_kontr_14 = 0;
            LocoButtons.vl85_kontr_15 = 0;
            LocoButtons.vl85_kontr_16 = 0;
            LocoButtons.vl85_kontr_17 = 0;
            LocoButtons.vl85_kontr_18 = 0;
            LocoButtons.vl85_kontr_19 = 0;
            LocoButtons.vl85_kontr_20 = 0;
            LocoButtons.vl85_kontr_21 = 0;
            LocoButtons.vl85_kontr_22 = 0;
            LocoButtons.vl85_kontr_23 = 0;
            LocoButtons.vl85_kontr_24 = 0;
            LocoButtons.vl85_kontr_25 = 0;
            LocoButtons.vl85_kontr_26 = 0;
            LocoButtons.vl85_kontr_27 = 0;
            LocoButtons.vl85_kontr_28 = 0;
            LocoButtons.vl85_kontr_29 = 0;
            LocoButtons.vl85_kontr_30 = 0;
            LocoButtons.vl85_kontr_31 = 0;
            LocoButtons.vl85_kontr_32 = 0;

            LocoButtons.vl85_kranTM_0 = 0;
            LocoButtons.vl85_kranTM_1 = 0;
            LocoButtons.vl85_tokopr_obshiy_0 = 0;
            LocoButtons.vl85_tokopr_obshiy_1 = 0;
            LocoButtons.vl85_tokopr_per_0 = 0;
            LocoButtons.vl85_tokopr_per_1 = 0;
            LocoButtons.vl85_tokopr_zad_0 = 0;
            LocoButtons.vl85_tokopr_zad_1 = 0;
            LocoButtons.vl85_gv_0 = 0; //ГВ 0-0 1-1
            LocoButtons.vl85_gv_1 = 0;
            LocoButtons.vl85_vosst_gv = 0;//через K
            LocoButtons.vl85_avtoreg_140 = 0;//LocoButtons.0-140
            LocoButtons.vl85_avtoreg_plus = 0;
            LocoButtons.vl85_avtoreg_minus = 0;
            LocoButtons.vl85_komp_0 = 0;//0выкл,1вкл
            LocoButtons.vl85_komp_1 = 0;
            LocoButtons.vl85_vent1_0 = 0;
            LocoButtons.vl85_vent1_1 = 0;
            LocoButtons.vl85_vent2_0 = 0;
            LocoButtons.vl85_vent2_1 = 0;
            LocoButtons.vl85_vent3_0 = 0;
            LocoButtons.vl85_vent3_2 = 0;
            LocoButtons.vl85_vent4_0 = 0;
            LocoButtons.vl85_vent4_1 = 0;
            LocoButtons.vl85_fz_0 = 0;
            LocoButtons.vl85_fz_1 = 0;
            LocoButtons.vl85_svet_cab_0 = 0;//0выкл,1приб,2общ
            LocoButtons.vl85_svet_cab_1 = 0;
            LocoButtons.vl85_svet_cab_2 = 0;
            LocoButtons.vl85_EPK_0 = 0;
            LocoButtons.vl85_EPK_1 = 0;
            LocoButtons.vl85_prozh_0 = 0;//0,1,2
            LocoButtons.vl85_prozh_1 = 0;
            LocoButtons.vl85_prozh_2 = 0;
            LocoButtons.vl85_sign_0 = 0;
            LocoButtons.vl85_sign_1 = 0;
            LocoButtons.vl85_sign1_0 = 0;
            LocoButtons.vl85_sign1_1 = 0;
            LocoButtons.vl85_sign2_0 = 0;
            LocoButtons.vl85_sign2_1 = 0;

            //tep70 35
            LocoButtons.tep70_rev_0 = 0;//255нз,0-0,1вп
            LocoButtons.tep70_rev_vpered = 0;
            LocoButtons.tep70_rev_nazad = 0;

            LocoButtons.tep70_kontr_0 = 0;//0-15
            LocoButtons.tep70_kontr_1 = 0;
            LocoButtons.tep70_kontr_2 = 0;
            LocoButtons.tep70_kontr_3 = 0;
            LocoButtons.tep70_kontr_4 = 0;
            LocoButtons.tep70_kontr_5 = 0;
            LocoButtons.tep70_kontr_6 = 0;
            LocoButtons.tep70_kontr_7 = 0;
            LocoButtons.tep70_kontr_8 = 0;
            LocoButtons.tep70_kontr_9 = 0;
            LocoButtons.tep70_kontr_10 = 0;
            LocoButtons.tep70_kontr_11 = 0;
            LocoButtons.tep70_kontr_12 = 0;
            LocoButtons.tep70_kontr_13 = 0;
            LocoButtons.tep70_kontr_14 = 0;
            LocoButtons.tep70_kontr_15 = 0;

            LocoButtons.tep70_kranTM_0 = 0;
            LocoButtons.tep70_kranTM_1 = 0;
            LocoButtons.tep70_nasos_0 = 0;
            LocoButtons.tep70_nasos_1 = 0;
            LocoButtons.tep70_pusk = 0;//через K
            LocoButtons.tep70_upravlenie_0 = 0;
            LocoButtons.tep70_upravlenie_1 = 0;
            LocoButtons.tep70_svet_cab_0 = 0;//0выкл,1приб,2общ
            LocoButtons.tep70_svet_cab_1 = 0;
            LocoButtons.tep70_svet_cab_2 = 0;
            LocoButtons.tep70_EPK_0 = 0;
            LocoButtons.tep70_EPK_1 = 0;
            LocoButtons.tep70_EPT_0 = 0;
            LocoButtons.tep70_EPT_1 = 0;
            LocoButtons.tep70_prozh_0 = 0;//float 0-1.75
            LocoButtons.tep70_prozh_1 = 0;
            LocoButtons.tep70_prozh_2 = 0;

            //te10u 46
            LocoButtons.te10u_rev_0 = 0;//255нз,0-0,1вп
            LocoButtons.te10u_rev_vpered = 0;
            LocoButtons.te10u_rev_nazad = 0;

            LocoButtons.te10u_kontr_0 = 0;//0-15
            LocoButtons.te10u_kontr_1 = 0;
            LocoButtons.te10u_kontr_2 = 0;
            LocoButtons.te10u_kontr_3 = 0;
            LocoButtons.te10u_kontr_4 = 0;
            LocoButtons.te10u_kontr_5 = 0;
            LocoButtons.te10u_kontr_6 = 0;
            LocoButtons.te10u_kontr_7 = 0;
            LocoButtons.te10u_kontr_8 = 0;
            LocoButtons.te10u_kontr_9 = 0;
            LocoButtons.te10u_kontr_10 = 0;
            LocoButtons.te10u_kontr_11 = 0;
            LocoButtons.te10u_kontr_12 = 0;
            LocoButtons.te10u_kontr_13 = 0;
            LocoButtons.te10u_kontr_14 = 0;
            LocoButtons.te10u_kontr_15 = 0;

            LocoButtons.te10u_kranTM_0 = 0;
            LocoButtons.te10u_kranTM_1 = 0;
            LocoButtons.te10u_nasos1_0 = 0;
            LocoButtons.te10u_nasos1_1 = 0;
            LocoButtons.te10u_nasos2_0 = 0;
            LocoButtons.te10u_nasos2_1 = 0;
            LocoButtons.te10u_pusk1 = 0;//через J
            LocoButtons.te10u_pusk2 = 0;//через K
            LocoButtons.te10u_upravlenie_0 = 0;
            LocoButtons.te10u_upravlenie_1 = 0;
            LocoButtons.te10u_dvizhenie_0 = 0;
            LocoButtons.te10u_dvizhenie_1 = 0;
            LocoButtons.te10u_perehody_0 = 0;
            LocoButtons.te10u_perehody_1 = 0;
            LocoButtons.te10u_holost1_0 = 0;
            LocoButtons.te10u_holost1_1 = 0;
            LocoButtons.te10u_holost2_0 = 0;
            LocoButtons.te10u_holost2_1 = 0;
            LocoButtons.te10u_svet_cab_0 = 0;//0выкл,1приб,2общ
            LocoButtons.te10u_svet_cab_1 = 0;
            LocoButtons.te10u_svet_cab_2 = 0;
            LocoButtons.te10u_EPK_0 = 0;
            LocoButtons.te10u_EPK_1 = 0;
            LocoButtons.te10u_EPT_0 = 0;
            LocoButtons.te10u_EPT_1 = 0;
            LocoButtons.te10u_prozh_0 = 0;//float 0-1.75
            LocoButtons.te10u_prozh_1 = 0;
            LocoButtons.te10u_prozh_2 = 0;

            //m62 35
            LocoButtons.m62_rev_0 = 0;//255нз,0-0,1вп
            LocoButtons.m62_rev_vpered = 0;
            LocoButtons.m62_rev_nazad = 0;

            LocoButtons.m62_kontr_0 = 0;//0-15
            LocoButtons.m62_kontr_1 = 0;
            LocoButtons.m62_kontr_2 = 0;
            LocoButtons.m62_kontr_3 = 0;
            LocoButtons.m62_kontr_4 = 0;
            LocoButtons.m62_kontr_5 = 0;
            LocoButtons.m62_kontr_6 = 0;
            LocoButtons.m62_kontr_7 = 0;
            LocoButtons.m62_kontr_8 = 0;
            LocoButtons.m62_kontr_9 = 0;
            LocoButtons.m62_kontr_10 = 0;
            LocoButtons.m62_kontr_11 = 0;
            LocoButtons.m62_kontr_12 = 0;
            LocoButtons.m62_kontr_13 = 0;
            LocoButtons.m62_kontr_14 = 0;
            LocoButtons.m62_kontr_15 = 0;

            LocoButtons.m62_kranTM_0 = 0;
            LocoButtons.m62_kranTM_1 = 0;
            LocoButtons.m62_nasos_0 = 0;
            LocoButtons.m62_nasos_1 = 0;
            LocoButtons.m62_pusk = 0;//через K
            LocoButtons.m62_upravlenie_0 = 0;
            LocoButtons.m62_upravlenie_1 = 0;
            LocoButtons.m62_perehody_0 = 0;
            LocoButtons.m62_perehody_1 = 0;
            LocoButtons.m62_svet_cab_0 = 0;//0выкл,1приб,2общ
            LocoButtons.m62_svet_cab_1 = 0;
            LocoButtons.m62_svet_cab_2 = 0;
            LocoButtons.m62_EPK_0 = 0;
            LocoButtons.m62_EPK_1 = 0;
            LocoButtons.m62_prozh_0 = 0;//float 0-1.75
            LocoButtons.m62_prozh_1 = 0;
            LocoButtons.m62_prozh_2 = 0;

            //ed4m 29
            LocoButtons.ed4m_rev_0 = 0;//0-0,1вп,255нз
            LocoButtons.ed4m_rev_vpered = 0;
            LocoButtons.ed4m_rev_nazad = 0;

            LocoButtons.ed4m_kontr_0 = 0; //0-0,1-2ход,255-251тормоз
            LocoButtons.ed4m_kontr_h1 = 0;
            LocoButtons.ed4m_kontr_h2 = 0;
            LocoButtons.ed4m_kontr_h3 = 0;
            LocoButtons.ed4m_kontr_h4 = 0;
            LocoButtons.ed4m_kontr_h5 = 0;
            LocoButtons.ed4m_kontr_t1 = 0;
            LocoButtons.ed4m_kontr_t2 = 0;
            LocoButtons.ed4m_kontr_t3 = 0;
            LocoButtons.ed4m_kontr_t4 = 0;
            LocoButtons.ed4m_kontr_t5 = 0;
            LocoButtons.ed4m_kranTM_0 = 0;
            LocoButtons.ed4m_kranTM_1 = 0;
            LocoButtons.ed4m_tokopr_0 = 0;
            LocoButtons.ed4m_tokopr_1 = 0;
            LocoButtons.ed4m_bv_0 = 0; //0-0 1-1
            LocoButtons.ed4m_bv_1 = 0;
            LocoButtons.ed4m_svet_cab_0 = 0;//0-1
            LocoButtons.ed4m_svet_cab_1 = 0;
            LocoButtons.ed4m_EPK_0 = 0;
            LocoButtons.ed4m_EPK_1 = 0;
            LocoButtons.ed4m_EPT_0 = 0;
            LocoButtons.ed4m_EPT_1 = 0;
            LocoButtons.ed4m_dvery_lev_0 = 0;
            LocoButtons.ed4m_dvery_lev_1 = 0;
            LocoButtons.ed4m_dvery_pr_0 = 0;
            LocoButtons.ed4m_dvery_pr_1 = 0;
            LocoButtons.ed4m_prozh_0 = 0; //float 0-1,625
            LocoButtons.ed4m_prozh_1 = 0;
            LocoButtons.ed4m_prozh_2 = 0;

            //ed9m 23
            LocoButtons.ed9m_rev_0 = 0;//0-0,1вп,255нз
            LocoButtons.ed9m_rev_vpered = 0;
            LocoButtons.ed9m_rev_nazad = 0;

            LocoButtons.ed9m_kontr_0 = 0; //0-0,1-5ход,255-251тормоз
            LocoButtons.ed9m_kontr_h1 = 0;
            LocoButtons.ed9m_kontr_h2 = 0;
            LocoButtons.ed9m_kontr_t1 = 0;
            LocoButtons.ed9m_kontr_t2 = 0;
            LocoButtons.ed9m_kontr_t3 = 0;
            LocoButtons.ed9m_kontr_t4 = 0;
            LocoButtons.ed9m_kontr_t5 = 0;
            LocoButtons.ed9m_kranTM_0 = 0;
            LocoButtons.ed9m_kranTM_1 = 0;
            LocoButtons.ed9m_tokopr_0 = 0;
            LocoButtons.ed9m_tokopr_1 = 0;
            LocoButtons.ed9m_bv_0 = 0; //0-0 1-1
            LocoButtons.ed9m_bv_1 = 0;
            LocoButtons.ed9m_svet_cab_0 = 0;//0-1
            LocoButtons.ed9m_svet_cab_1 = 0;
            LocoButtons.ed9m_EPK_0 = 0;
            LocoButtons.ed9m_EPK_1 = 0;
            LocoButtons.ed9m_EPT_0 = 0;
            LocoButtons.ed9m_EPT_1 = 0;
            LocoButtons.ed9m_dvery_lev_0 = 0;
            LocoButtons.ed9m_dvery_lev_1 = 0;
            LocoButtons.ed9m_dvery_pr_0 = 0;
            LocoButtons.ed9m_dvery_pr_1 = 0;
            LocoButtons.ed9m_prozh_0 = 0; //float 0-1,625
            LocoButtons.ed9m_prozh_1 = 0;
            LocoButtons.ed9m_prozh_2 = 0;


            //tem18 31
            LocoButtons.tem18_rev_0 = 0;//255нз,0-0,1вп
            LocoButtons.tem18_rev_vpered = 0;
            LocoButtons.tem18_rev_nazad = 0;

            LocoButtons.tem18_kontr_0 = 0;//0-15
            LocoButtons.tem18_kontr_1 = 0;
            LocoButtons.tem18_kontr_2 = 0;
            LocoButtons.tem18_kontr_3 = 0;
            LocoButtons.tem18_kontr_4 = 0;
            LocoButtons.tem18_kontr_5 = 0;
            LocoButtons.tem18_kontr_6 = 0;
            LocoButtons.tem18_kontr_7 = 0;
            LocoButtons.tem18_kontr_8 = 0;

            LocoButtons.tem18_kranTM_0 = 0;
            LocoButtons.tem18_kranTM_1 = 0;
            LocoButtons.tem18_nasos_maslo0 = 0;
            LocoButtons.tem18_nasos_maslo1 = 0;
            LocoButtons.tem18_nasos_toplivo0 = 0;
            LocoButtons.tem18_nasos_toplivo1 = 0;
            LocoButtons.tem18_pusk = 0;//через K
            LocoButtons.tem18_upravlenie_0 = 0;
            LocoButtons.tem18_upravlenie_1 = 0;
            LocoButtons.tem18_perehody_0 = 0;
            LocoButtons.tem18_perehody_1 = 0;
            LocoButtons.tem18_svet_cab_0 = 0;//0выкл,1вкл
            LocoButtons.tem18_svet_cab_1 = 0;
            LocoButtons.tem18_svet_prib_0 = 0;//0выкл,1вкл
            LocoButtons.tem18_svet_prib_1 = 0;
            LocoButtons.tem18_EPK_0 = 0;
            LocoButtons.tem18_EPK_1 = 0;
            LocoButtons.tem18_prozh_0 = 0;//float 0-1.75
            LocoButtons.tem18_prozh_1 = 0;
            LocoButtons.tem18_prozh_2 = 0;
        }

        //Инициализируем массивы кнопок осей и нештаток
        public static void LocoButtonsStructInit()
        {
            //постоянные 28
            if (Controls_axis_buffer[0, 0] == 0 && Controls_key_buffer[0] == 0) LocoButtons.svistok = 255;
            if (Controls_axis_buffer[1, 0] == 0 && Controls_key_buffer[1] == 0) LocoButtons.tifon = 255;
            if (Controls_axis_buffer[2, 0] == 0 && Controls_key_buffer[2] == 0) LocoButtons.kran395_0 = 255; //от 0 до 6
            if (Controls_axis_buffer[3, 0] == 0 && Controls_key_buffer[3] == 0) LocoButtons.kran395_1 = 255;
            if (Controls_axis_buffer[4, 0] == 0 && Controls_key_buffer[4] == 0) LocoButtons.kran395_2 = 255;
            if (Controls_axis_buffer[5, 0] == 0 && Controls_key_buffer[5] == 0) LocoButtons.kran395_3 = 255;
            if (Controls_axis_buffer[6, 0] == 0 && Controls_key_buffer[6] == 0) LocoButtons.kran395_4 = 255;
            if (Controls_axis_buffer[7, 0] == 0 && Controls_key_buffer[7] == 0) LocoButtons.kran395_5 = 255;
            if (Controls_axis_buffer[8, 0] == 0 && Controls_key_buffer[8] == 0) LocoButtons.kran395_6 = 255;
            if (Controls_axis_buffer[9, 0] == 0 && Controls_key_buffer[9] == 0) LocoButtons.kran254_0 = 255; //float от от -1, 0-4
            if (Controls_axis_buffer[10, 0] == 0 && Controls_key_buffer[10] == 0) LocoButtons.kran254_1 = 255;
            if (Controls_axis_buffer[11, 0] == 0 && Controls_key_buffer[11] == 0) LocoButtons.kran254_2 = 255;
            if (Controls_axis_buffer[12, 0] == 0 && Controls_key_buffer[12] == 0) LocoButtons.kran254_3 = 255;
            if (Controls_axis_buffer[13, 0] == 0 && Controls_key_buffer[13] == 0) LocoButtons.kran254_4 = 255;
            if (Controls_axis_buffer[14, 0] == 0 && Controls_key_buffer[14] == 0) LocoButtons.kran254_5 = 255;
            if (Controls_axis_buffer[15, 0] == 0 && Controls_key_buffer[15] == 0) LocoButtons.vid_vlevo = 255;
            if (Controls_axis_buffer[16, 0] == 0 && Controls_key_buffer[16] == 0) LocoButtons.vid_vpravo = 255;//double от -0,5 до +0,5
            if (Controls_axis_buffer[17, 0] == 0 && Controls_key_buffer[17] == 0) LocoButtons.vid_vverh = 255;
            if (Controls_axis_buffer[18, 0] == 0 && Controls_key_buffer[18] == 0) LocoButtons.vid_vniz = 255;//double от -0,5 до +0,5
            if (Controls_axis_buffer[19, 0] == 0 && Controls_key_buffer[19] == 0) LocoButtons.vid_zoom_in = 255;
            if (Controls_axis_buffer[20, 0] == 0 && Controls_key_buffer[20] == 0) LocoButtons.vid_zoom_out = 255;//float от 3.5_3.21875_2.5
            if (Controls_axis_buffer[21, 0] == 0 && Controls_key_buffer[21] == 0) LocoButtons.vid_outside = 255; //0-каб, далее по кол-ву ваг
            if (Controls_axis_buffer[22, 0] == 0 && Controls_key_buffer[22] == 0) LocoButtons.vid_vpered = 255;
            if (Controls_axis_buffer[23, 0] == 0 && Controls_key_buffer[23] == 0) LocoButtons.vid_nazad = 255;//float от 6.2_6.619999886_8
            if (Controls_axis_buffer[24, 0] == 0 && Controls_key_buffer[24] == 0) LocoButtons.protyazhka_lenty = 255;//int по 3000
            if (Controls_axis_buffer[25, 0] == 0 && Controls_key_buffer[25] == 0) LocoButtons.bdit_Z = 255;
            if (Controls_axis_buffer[26, 0] == 0 && Controls_key_buffer[26] == 0) LocoButtons.bdit_M = 255;
            if (Controls_axis_buffer[27, 0] == 0 && Controls_key_buffer[27] == 0) LocoButtons.pesok = 255;
            if (Controls_axis_buffer[28, 0] == 0 && Controls_key_buffer[28] == 0) LocoButtons.dvorniki_0 = 255;
            if (Controls_axis_buffer[29, 0] == 0 && Controls_key_buffer[29] == 0) LocoButtons.dvorniki_1 = 255;
            if (Controls_axis_buffer[30, 0] == 0 && Controls_key_buffer[30] == 0) LocoButtons.dvorniki_2 = 255;
            if (Controls_axis_buffer[31, 0] == 0 && Controls_key_buffer[31] == 0) LocoButtons.dvorniki_3 = 255;
            if (Controls_axis_buffer[32, 0] == 0 && Controls_key_buffer[32] == 0) LocoButtons.dvorniki_4 = 255;
            if (Controls_axis_buffer[33, 0] == 0 && Controls_key_buffer[33] == 0) LocoButtons.dvorniki_5 = 255;

            //нештатки 100
            for (int i = 0; i < 100; i++)
            {
                if (Neshtatki_axis_buffer[i, 0] == 0 && Neshtatki_key_buffer[i] == 0) LocoButtons.b_neshtatki[i] = 255;
            }


            //2es5k 109
            if (ES5K_axis_buffer[0, 0] == 0 && ES5K_key_buffer[0] == 0) LocoButtons.es5k_kontr_0 = 255;
            if (ES5K_axis_buffer[1, 0] == 0 && ES5K_key_buffer[1] == 0) LocoButtons.es5k_kontr_h4 = 255;
            if (ES5K_axis_buffer[2, 0] == 0 && ES5K_key_buffer[2] == 0) LocoButtons.es5k_kontr_h5 = 255;
            if (ES5K_axis_buffer[3, 0] == 0 && ES5K_key_buffer[3] == 0) LocoButtons.es5k_kontr_h6 = 255;
            if (ES5K_axis_buffer[4, 0] == 0 && ES5K_key_buffer[4] == 0) LocoButtons.es5k_kontr_h7 = 255;
            if (ES5K_axis_buffer[5, 0] == 0 && ES5K_key_buffer[5] == 0) LocoButtons.es5k_kontr_h8 = 255;
            if (ES5K_axis_buffer[6, 0] == 0 && ES5K_key_buffer[6] == 0) LocoButtons.es5k_kontr_h9 = 255;
            if (ES5K_axis_buffer[7, 0] == 0 && ES5K_key_buffer[7] == 0) LocoButtons.es5k_kontr_h10 = 255;
            if (ES5K_axis_buffer[8, 0] == 0 && ES5K_key_buffer[8] == 0) LocoButtons.es5k_kontr_h11 = 255;
            if (ES5K_axis_buffer[9, 0] == 0 && ES5K_key_buffer[9] == 0) LocoButtons.es5k_kontr_h12 = 255;
            if (ES5K_axis_buffer[10, 0] == 0 && ES5K_key_buffer[10] == 0) LocoButtons.es5k_kontr_h13 = 255;
            if (ES5K_axis_buffer[11, 0] == 0 && ES5K_key_buffer[11] == 0) LocoButtons.es5k_kontr_h14 = 255;
            if (ES5K_axis_buffer[12, 0] == 0 && ES5K_key_buffer[12] == 0) LocoButtons.es5k_kontr_h15 = 255;
            if (ES5K_axis_buffer[13, 0] == 0 && ES5K_key_buffer[13] == 0) LocoButtons.es5k_kontr_h16 = 255;
            if (ES5K_axis_buffer[14, 0] == 0 && ES5K_key_buffer[14] == 0) LocoButtons.es5k_kontr_h17 = 255;
            if (ES5K_axis_buffer[15, 0] == 0 && ES5K_key_buffer[15] == 0) LocoButtons.es5k_kontr_h18 = 255;
            if (ES5K_axis_buffer[16, 0] == 0 && ES5K_key_buffer[16] == 0) LocoButtons.es5k_kontr_h19 = 255;
            if (ES5K_axis_buffer[17, 0] == 0 && ES5K_key_buffer[17] == 0) LocoButtons.es5k_kontr_h20 = 255;
            if (ES5K_axis_buffer[18, 0] == 0 && ES5K_key_buffer[18] == 0) LocoButtons.es5k_kontr_h21 = 255;
            if (ES5K_axis_buffer[19, 0] == 0 && ES5K_key_buffer[19] == 0) LocoButtons.es5k_kontr_h22 = 255;
            if (ES5K_axis_buffer[20, 0] == 0 && ES5K_key_buffer[20] == 0) LocoButtons.es5k_kontr_h23 = 255;
            if (ES5K_axis_buffer[21, 0] == 0 && ES5K_key_buffer[21] == 0) LocoButtons.es5k_kontr_h24 = 255;
            if (ES5K_axis_buffer[22, 0] == 0 && ES5K_key_buffer[22] == 0) LocoButtons.es5k_kontr_h25 = 255;
            if (ES5K_axis_buffer[23, 0] == 0 && ES5K_key_buffer[23] == 0) LocoButtons.es5k_kontr_h26 = 255;
            if (ES5K_axis_buffer[24, 0] == 0 && ES5K_key_buffer[24] == 0) LocoButtons.es5k_kontr_h27 = 255;
            if (ES5K_axis_buffer[25, 0] == 0 && ES5K_key_buffer[25] == 0) LocoButtons.es5k_kontr_h28 = 255;
            if (ES5K_axis_buffer[26, 0] == 0 && ES5K_key_buffer[26] == 0) LocoButtons.es5k_kontr_h29 = 255;
            if (ES5K_axis_buffer[27, 0] == 0 && ES5K_key_buffer[27] == 0) LocoButtons.es5k_kontr_h30 = 255;
            if (ES5K_axis_buffer[28, 0] == 0 && ES5K_key_buffer[28] == 0) LocoButtons.es5k_kontr_h31 = 255;
            if (ES5K_axis_buffer[29, 0] == 0 && ES5K_key_buffer[29] == 0) LocoButtons.es5k_kontr_h32 = 255;
            if (ES5K_axis_buffer[30, 0] == 0 && ES5K_key_buffer[30] == 0) LocoButtons.es5k_kontr_h33 = 255;
            if (ES5K_axis_buffer[31, 0] == 0 && ES5K_key_buffer[31] == 0) LocoButtons.es5k_kontr_h34 = 255;
            if (ES5K_axis_buffer[32, 0] == 0 && ES5K_key_buffer[32] == 0) LocoButtons.es5k_kontr_h35 = 255;
            if (ES5K_axis_buffer[33, 0] == 0 && ES5K_key_buffer[33] == 0) LocoButtons.es5k_kontr_h36 = 255;

            if (ES5K_axis_buffer[34, 0] == 0 && ES5K_key_buffer[34] == 0) LocoButtons.es5k_kontr_t4 = 255;
            if (ES5K_axis_buffer[35, 0] == 0 && ES5K_key_buffer[35] == 0) LocoButtons.es5k_kontr_t5 = 255;
            if (ES5K_axis_buffer[36, 0] == 0 && ES5K_key_buffer[36] == 0) LocoButtons.es5k_kontr_t6 = 255;
            if (ES5K_axis_buffer[37, 0] == 0 && ES5K_key_buffer[37] == 0) LocoButtons.es5k_kontr_t7 = 255;
            if (ES5K_axis_buffer[38, 0] == 0 && ES5K_key_buffer[38] == 0) LocoButtons.es5k_kontr_t8 = 255;
            if (ES5K_axis_buffer[39, 0] == 0 && ES5K_key_buffer[39] == 0) LocoButtons.es5k_kontr_t9 = 255;
            if (ES5K_axis_buffer[40, 0] == 0 && ES5K_key_buffer[40] == 0) LocoButtons.es5k_kontr_t10 = 255;
            if (ES5K_axis_buffer[41, 0] == 0 && ES5K_key_buffer[41] == 0) LocoButtons.es5k_kontr_t11 = 255;
            if (ES5K_axis_buffer[42, 0] == 0 && ES5K_key_buffer[42] == 0) LocoButtons.es5k_kontr_t12 = 255;
            if (ES5K_axis_buffer[43, 0] == 0 && ES5K_key_buffer[43] == 0) LocoButtons.es5k_kontr_t13 = 255;
            if (ES5K_axis_buffer[44, 0] == 0 && ES5K_key_buffer[44] == 0) LocoButtons.es5k_kontr_t14 = 255;
            if (ES5K_axis_buffer[45, 0] == 0 && ES5K_key_buffer[45] == 0) LocoButtons.es5k_kontr_t15 = 255;
            if (ES5K_axis_buffer[46, 0] == 0 && ES5K_key_buffer[46] == 0) LocoButtons.es5k_kontr_t16 = 255;
            if (ES5K_axis_buffer[47, 0] == 0 && ES5K_key_buffer[47] == 0) LocoButtons.es5k_kontr_t17 = 255;
            if (ES5K_axis_buffer[48, 0] == 0 && ES5K_key_buffer[48] == 0) LocoButtons.es5k_kontr_t18 = 255;
            if (ES5K_axis_buffer[49, 0] == 0 && ES5K_key_buffer[49] == 0) LocoButtons.es5k_kontr_t19 = 255;
            if (ES5K_axis_buffer[50, 0] == 0 && ES5K_key_buffer[50] == 0) LocoButtons.es5k_kontr_t20 = 255;
            if (ES5K_axis_buffer[51, 0] == 0 && ES5K_key_buffer[51] == 0) LocoButtons.es5k_kontr_t21 = 255;
            if (ES5K_axis_buffer[52, 0] == 0 && ES5K_key_buffer[52] == 0) LocoButtons.es5k_kontr_t22 = 255;
            if (ES5K_axis_buffer[53, 0] == 0 && ES5K_key_buffer[53] == 0) LocoButtons.es5k_kontr_t23 = 255;
            if (ES5K_axis_buffer[54, 0] == 0 && ES5K_key_buffer[54] == 0) LocoButtons.es5k_kontr_t24 = 255;
            if (ES5K_axis_buffer[55, 0] == 0 && ES5K_key_buffer[55] == 0) LocoButtons.es5k_kontr_t25 = 255;
            if (ES5K_axis_buffer[56, 0] == 0 && ES5K_key_buffer[56] == 0) LocoButtons.es5k_kontr_t26 = 255;
            if (ES5K_axis_buffer[57, 0] == 0 && ES5K_key_buffer[57] == 0) LocoButtons.es5k_kontr_t27 = 255;
            if (ES5K_axis_buffer[58, 0] == 0 && ES5K_key_buffer[58] == 0) LocoButtons.es5k_kontr_t28 = 255;
            if (ES5K_axis_buffer[59, 0] == 0 && ES5K_key_buffer[59] == 0) LocoButtons.es5k_kontr_t29 = 255;
            if (ES5K_axis_buffer[60, 0] == 0 && ES5K_key_buffer[60] == 0) LocoButtons.es5k_kontr_t30 = 255;
            if (ES5K_axis_buffer[61, 0] == 0 && ES5K_key_buffer[61] == 0) LocoButtons.es5k_kontr_t31 = 255;
            if (ES5K_axis_buffer[62, 0] == 0 && ES5K_key_buffer[62] == 0) LocoButtons.es5k_kontr_t32 = 255;
            if (ES5K_axis_buffer[63, 0] == 0 && ES5K_key_buffer[63] == 0) LocoButtons.es5k_kontr_t33 = 255;
            if (ES5K_axis_buffer[64, 0] == 0 && ES5K_key_buffer[64] == 0) LocoButtons.es5k_kontr_t34 = 255;
            if (ES5K_axis_buffer[65, 0] == 0 && ES5K_key_buffer[65] == 0) LocoButtons.es5k_kontr_t35 = 255;
            if (ES5K_axis_buffer[66, 0] == 0 && ES5K_key_buffer[66] == 0) LocoButtons.es5k_kontr_t36 = 255;//float -36 -4 0 4 36

            if (ES5K_axis_buffer[67, 0] == 0 && ES5K_key_buffer[67] == 0) LocoButtons.es5k_rev_0 = 255;//int вп-1 0-0 наз-FFFF
            if (ES5K_axis_buffer[68, 0] == 0 && ES5K_key_buffer[68] == 0) LocoButtons.es5k_rev_vpered = 255;
            if (ES5K_axis_buffer[69, 0] == 0 && ES5K_key_buffer[69] == 0) LocoButtons.es5k_rev_nazad = 255;

            if (ES5K_axis_buffer[70, 0] == 0 && ES5K_key_buffer[70] == 0) LocoButtons.es5k_reg_skor_140 = 255;//float 0-140
            if (ES5K_axis_buffer[71, 0] == 0 && ES5K_key_buffer[71] == 0) LocoButtons.es5k_reg_skor_plus = 255;//по 5км
            if (ES5K_axis_buffer[72, 0] == 0 && ES5K_key_buffer[72] == 0) LocoButtons.es5k_reg_skor_minus = 255;
            if (ES5K_axis_buffer[73, 0] == 0 && ES5K_key_buffer[73] == 0) LocoButtons.es5k_kranTM_0 = 255;
            if (ES5K_axis_buffer[74, 0] == 0 && ES5K_key_buffer[74] == 0) LocoButtons.es5k_kranTM_1 = 255;
            if (ES5K_axis_buffer[75, 0] == 0 && ES5K_key_buffer[75] == 0) LocoButtons.es5k_bv_0 = 255;
            if (ES5K_axis_buffer[76, 0] == 0 && ES5K_key_buffer[76] == 0) LocoButtons.es5k_bv_1 = 255;
            if (ES5K_axis_buffer[77, 0] == 0 && ES5K_key_buffer[77] == 0) LocoButtons.es5k_vozvrat_bv = 255;
            if (ES5K_axis_buffer[78, 0] == 0 && ES5K_key_buffer[78] == 0) LocoButtons.es5k_tokopr_per_0 = 255;
            if (ES5K_axis_buffer[79, 0] == 0 && ES5K_key_buffer[79] == 0) LocoButtons.es5k_tokopr_per_1 = 255;
            if (ES5K_axis_buffer[80, 0] == 0 && ES5K_key_buffer[80] == 0) LocoButtons.es5k_tokopr_zad_0 = 255;
            if (ES5K_axis_buffer[81, 0] == 0 && ES5K_key_buffer[81] == 0) LocoButtons.es5k_tokopr_zad_1 = 255;
            if (ES5K_axis_buffer[82, 0] == 0 && ES5K_key_buffer[82] == 0) LocoButtons.es5k_upravlenie_0 = 255;
            if (ES5K_axis_buffer[83, 0] == 0 && ES5K_key_buffer[83] == 0) LocoButtons.es5k_upravlenie_1 = 255;
            if (ES5K_axis_buffer[84, 0] == 0 && ES5K_key_buffer[84] == 0) LocoButtons.es5k_komp_0 = 255;
            if (ES5K_axis_buffer[85, 0] == 0 && ES5K_key_buffer[85] == 0) LocoButtons.es5k_komp_1 = 255;
            if (ES5K_axis_buffer[86, 0] == 0 && ES5K_key_buffer[86] == 0) LocoButtons.es5k_vent1_0 = 255;
            if (ES5K_axis_buffer[87, 0] == 0 && ES5K_key_buffer[87] == 0) LocoButtons.es5k_vent1_1 = 255;
            if (ES5K_axis_buffer[88, 0] == 0 && ES5K_key_buffer[88] == 0) LocoButtons.es5k_vent2_0 = 255;
            if (ES5K_axis_buffer[89, 0] == 0 && ES5K_key_buffer[89] == 0) LocoButtons.es5k_vent2_1 = 255;
            if (ES5K_axis_buffer[90, 0] == 0 && ES5K_key_buffer[90] == 0) LocoButtons.es5k_MSUD_0 = 255;
            if (ES5K_axis_buffer[91, 0] == 0 && ES5K_key_buffer[91] == 0) LocoButtons.es5k_MSUD_1 = 255;
            if (ES5K_axis_buffer[92, 0] == 0 && ES5K_key_buffer[92] == 0) LocoButtons.es5k_vspom_mash_0 = 255;
            if (ES5K_axis_buffer[93, 0] == 0 && ES5K_key_buffer[93] == 0) LocoButtons.es5k_vspom_mash_1 = 255;
            if (ES5K_axis_buffer[94, 0] == 0 && ES5K_key_buffer[94] == 0) LocoButtons.es5k_svet_cab_0 = 255;//0,1
            if (ES5K_axis_buffer[95, 0] == 0 && ES5K_key_buffer[95] == 0) LocoButtons.es5k_svet_cab_1 = 255;
            if (ES5K_axis_buffer[96, 0] == 0 && ES5K_key_buffer[96] == 0) LocoButtons.es5k_EPK_0 = 255;
            if (ES5K_axis_buffer[97, 0] == 0 && ES5K_key_buffer[97] == 0) LocoButtons.es5k_EPK_1 = 255;
            if (ES5K_axis_buffer[98, 0] == 0 && ES5K_key_buffer[98] == 0) LocoButtons.es5k_sign_0 = 255;
            if (ES5K_axis_buffer[99, 0] == 0 && ES5K_key_buffer[99] == 0) LocoButtons.es5k_sign_1 = 255;
            if (ES5K_axis_buffer[100, 0] == 0 && ES5K_key_buffer[100] == 0) LocoButtons.es5k_signC1_0 = 255;
            if (ES5K_axis_buffer[101, 0] == 0 && ES5K_key_buffer[101] == 0) LocoButtons.es5k_signC1_1 = 255;
            if (ES5K_axis_buffer[102, 0] == 0 && ES5K_key_buffer[102] == 0) LocoButtons.es5k_signC2_0 = 255;
            if (ES5K_axis_buffer[103, 0] == 0 && ES5K_key_buffer[103] == 0) LocoButtons.es5k_signC2_1 = 255;
            if (ES5K_axis_buffer[104, 0] == 0 && ES5K_key_buffer[104] == 0) LocoButtons.es5k_prozh_0 = 255;//0,1,2
            if (ES5K_axis_buffer[105, 0] == 0 && ES5K_key_buffer[105] == 0) LocoButtons.es5k_prozh_1 = 255;
            if (ES5K_axis_buffer[106, 0] == 0 && ES5K_key_buffer[106] == 0) LocoButtons.es5k_prozh_2 = 255;
            if (ES5K_axis_buffer[107, 0] == 0 && ES5K_key_buffer[107] == 0) LocoButtons.es5k_avtoreg_0 = 255;
            if (ES5K_axis_buffer[108, 0] == 0 && ES5K_key_buffer[108] == 0) LocoButtons.es5k_avtoreg_1 = 255;

            //ep1m 111
            if (EP1M_axis_buffer[0, 0] == 0 && EP1M_key_buffer[0] == 0) LocoButtons.ep1m_kontr_0 = 255;
            if (EP1M_axis_buffer[1, 0] == 0 && EP1M_key_buffer[1] == 0) LocoButtons.ep1m_kontr_h4 = 255;
            if (EP1M_axis_buffer[2, 0] == 0 && EP1M_key_buffer[2] == 0) LocoButtons.ep1m_kontr_h5 = 255;
            if (EP1M_axis_buffer[3, 0] == 0 && EP1M_key_buffer[3] == 0) LocoButtons.ep1m_kontr_h6 = 255;
            if (EP1M_axis_buffer[4, 0] == 0 && EP1M_key_buffer[4] == 0) LocoButtons.ep1m_kontr_h7 = 255;
            if (EP1M_axis_buffer[5, 0] == 0 && EP1M_key_buffer[5] == 0) LocoButtons.ep1m_kontr_h8 = 255;
            if (EP1M_axis_buffer[6, 0] == 0 && EP1M_key_buffer[6] == 0) LocoButtons.ep1m_kontr_h9 = 255;
            if (EP1M_axis_buffer[7, 0] == 0 && EP1M_key_buffer[7] == 0) LocoButtons.ep1m_kontr_h10 = 255;
            if (EP1M_axis_buffer[8, 0] == 0 && EP1M_key_buffer[8] == 0) LocoButtons.ep1m_kontr_h11 = 255;
            if (EP1M_axis_buffer[9, 0] == 0 && EP1M_key_buffer[9] == 0) LocoButtons.ep1m_kontr_h12 = 255;
            if (EP1M_axis_buffer[10, 0] == 0 && EP1M_key_buffer[10] == 0) LocoButtons.ep1m_kontr_h13 = 255;
            if (EP1M_axis_buffer[11, 0] == 0 && EP1M_key_buffer[11] == 0) LocoButtons.ep1m_kontr_h14 = 255;
            if (EP1M_axis_buffer[12, 0] == 0 && EP1M_key_buffer[12] == 0) LocoButtons.ep1m_kontr_h15 = 255;
            if (EP1M_axis_buffer[13, 0] == 0 && EP1M_key_buffer[13] == 0) LocoButtons.ep1m_kontr_h16 = 255;
            if (EP1M_axis_buffer[14, 0] == 0 && EP1M_key_buffer[14] == 0) LocoButtons.ep1m_kontr_h17 = 255;
            if (EP1M_axis_buffer[15, 0] == 0 && EP1M_key_buffer[15] == 0) LocoButtons.ep1m_kontr_h18 = 255;
            if (EP1M_axis_buffer[16, 0] == 0 && EP1M_key_buffer[16] == 0) LocoButtons.ep1m_kontr_h19 = 255;
            if (EP1M_axis_buffer[17, 0] == 0 && EP1M_key_buffer[17] == 0) LocoButtons.ep1m_kontr_h20 = 255;
            if (EP1M_axis_buffer[18, 0] == 0 && EP1M_key_buffer[18] == 0) LocoButtons.ep1m_kontr_h21 = 255;
            if (EP1M_axis_buffer[19, 0] == 0 && EP1M_key_buffer[19] == 0) LocoButtons.ep1m_kontr_h22 = 255;
            if (EP1M_axis_buffer[20, 0] == 0 && EP1M_key_buffer[20] == 0) LocoButtons.ep1m_kontr_h23 = 255;
            if (EP1M_axis_buffer[21, 0] == 0 && EP1M_key_buffer[21] == 0) LocoButtons.ep1m_kontr_h24 = 255;
            if (EP1M_axis_buffer[22, 0] == 0 && EP1M_key_buffer[22] == 0) LocoButtons.ep1m_kontr_h25 = 255;
            if (EP1M_axis_buffer[23, 0] == 0 && EP1M_key_buffer[23] == 0) LocoButtons.ep1m_kontr_h26 = 255;
            if (EP1M_axis_buffer[24, 0] == 0 && EP1M_key_buffer[24] == 0) LocoButtons.ep1m_kontr_h27 = 255;
            if (EP1M_axis_buffer[25, 0] == 0 && EP1M_key_buffer[25] == 0) LocoButtons.ep1m_kontr_h28 = 255;
            if (EP1M_axis_buffer[26, 0] == 0 && EP1M_key_buffer[26] == 0) LocoButtons.ep1m_kontr_h29 = 255;
            if (EP1M_axis_buffer[27, 0] == 0 && EP1M_key_buffer[27] == 0) LocoButtons.ep1m_kontr_h30 = 255;
            if (EP1M_axis_buffer[28, 0] == 0 && EP1M_key_buffer[28] == 0) LocoButtons.ep1m_kontr_h31 = 255;
            if (EP1M_axis_buffer[29, 0] == 0 && EP1M_key_buffer[29] == 0) LocoButtons.ep1m_kontr_h32 = 255;
            if (EP1M_axis_buffer[30, 0] == 0 && EP1M_key_buffer[30] == 0) LocoButtons.ep1m_kontr_h33 = 255;
            if (EP1M_axis_buffer[31, 0] == 0 && EP1M_key_buffer[31] == 0) LocoButtons.ep1m_kontr_h34 = 255;
            if (EP1M_axis_buffer[32, 0] == 0 && EP1M_key_buffer[32] == 0) LocoButtons.ep1m_kontr_h35 = 255;
            if (EP1M_axis_buffer[33, 0] == 0 && EP1M_key_buffer[33] == 0) LocoButtons.ep1m_kontr_h36 = 255;

            if (EP1M_axis_buffer[34, 0] == 0 && EP1M_key_buffer[34] == 0) LocoButtons.ep1m_kontr_t4 = 255;
            if (EP1M_axis_buffer[35, 0] == 0 && EP1M_key_buffer[35] == 0) LocoButtons.ep1m_kontr_t5 = 255;
            if (EP1M_axis_buffer[36, 0] == 0 && EP1M_key_buffer[36] == 0) LocoButtons.ep1m_kontr_t6 = 255;
            if (EP1M_axis_buffer[37, 0] == 0 && EP1M_key_buffer[37] == 0) LocoButtons.ep1m_kontr_t7 = 255;
            if (EP1M_axis_buffer[38, 0] == 0 && EP1M_key_buffer[38] == 0) LocoButtons.ep1m_kontr_t8 = 255;
            if (EP1M_axis_buffer[39, 0] == 0 && EP1M_key_buffer[39] == 0) LocoButtons.ep1m_kontr_t9 = 255;
            if (EP1M_axis_buffer[40, 0] == 0 && EP1M_key_buffer[40] == 0) LocoButtons.ep1m_kontr_t10 = 255;
            if (EP1M_axis_buffer[41, 0] == 0 && EP1M_key_buffer[41] == 0) LocoButtons.ep1m_kontr_t11 = 255;
            if (EP1M_axis_buffer[42, 0] == 0 && EP1M_key_buffer[42] == 0) LocoButtons.ep1m_kontr_t12 = 255;
            if (EP1M_axis_buffer[43, 0] == 0 && EP1M_key_buffer[43] == 0) LocoButtons.ep1m_kontr_t13 = 255;
            if (EP1M_axis_buffer[44, 0] == 0 && EP1M_key_buffer[44] == 0) LocoButtons.ep1m_kontr_t14 = 255;
            if (EP1M_axis_buffer[45, 0] == 0 && EP1M_key_buffer[45] == 0) LocoButtons.ep1m_kontr_t15 = 255;
            if (EP1M_axis_buffer[46, 0] == 0 && EP1M_key_buffer[46] == 0) LocoButtons.ep1m_kontr_t16 = 255;
            if (EP1M_axis_buffer[47, 0] == 0 && EP1M_key_buffer[47] == 0) LocoButtons.ep1m_kontr_t17 = 255;
            if (EP1M_axis_buffer[48, 0] == 0 && EP1M_key_buffer[48] == 0) LocoButtons.ep1m_kontr_t18 = 255;
            if (EP1M_axis_buffer[49, 0] == 0 && EP1M_key_buffer[49] == 0) LocoButtons.ep1m_kontr_t19 = 255;
            if (EP1M_axis_buffer[50, 0] == 0 && EP1M_key_buffer[50] == 0) LocoButtons.ep1m_kontr_t20 = 255;
            if (EP1M_axis_buffer[51, 0] == 0 && EP1M_key_buffer[51] == 0) LocoButtons.ep1m_kontr_t21 = 255;
            if (EP1M_axis_buffer[52, 0] == 0 && EP1M_key_buffer[52] == 0) LocoButtons.ep1m_kontr_t22 = 255;
            if (EP1M_axis_buffer[53, 0] == 0 && EP1M_key_buffer[53] == 0) LocoButtons.ep1m_kontr_t23 = 255;
            if (EP1M_axis_buffer[54, 0] == 0 && EP1M_key_buffer[54] == 0) LocoButtons.ep1m_kontr_t24 = 255;
            if (EP1M_axis_buffer[55, 0] == 0 && EP1M_key_buffer[55] == 0) LocoButtons.ep1m_kontr_t25 = 255;
            if (EP1M_axis_buffer[56, 0] == 0 && EP1M_key_buffer[56] == 0) LocoButtons.ep1m_kontr_t26 = 255;
            if (EP1M_axis_buffer[57, 0] == 0 && EP1M_key_buffer[57] == 0) LocoButtons.ep1m_kontr_t27 = 255;
            if (EP1M_axis_buffer[58, 0] == 0 && EP1M_key_buffer[58] == 0) LocoButtons.ep1m_kontr_t28 = 255;
            if (EP1M_axis_buffer[59, 0] == 0 && EP1M_key_buffer[59] == 0) LocoButtons.ep1m_kontr_t29 = 255;
            if (EP1M_axis_buffer[60, 0] == 0 && EP1M_key_buffer[60] == 0) LocoButtons.ep1m_kontr_t30 = 255;
            if (EP1M_axis_buffer[61, 0] == 0 && EP1M_key_buffer[61] == 0) LocoButtons.ep1m_kontr_t31 = 255;
            if (EP1M_axis_buffer[62, 0] == 0 && EP1M_key_buffer[62] == 0) LocoButtons.ep1m_kontr_t32 = 255;
            if (EP1M_axis_buffer[63, 0] == 0 && EP1M_key_buffer[63] == 0) LocoButtons.ep1m_kontr_t33 = 255;
            if (EP1M_axis_buffer[64, 0] == 0 && EP1M_key_buffer[64] == 0) LocoButtons.ep1m_kontr_t34 = 255;
            if (EP1M_axis_buffer[65, 0] == 0 && EP1M_key_buffer[65] == 0) LocoButtons.ep1m_kontr_t35 = 255;
            if (EP1M_axis_buffer[66, 0] == 0 && EP1M_key_buffer[66] == 0) LocoButtons.ep1m_kontr_t36 = 255;//float -36 -4 0 4 36

            if (EP1M_axis_buffer[67, 0] == 0 && EP1M_key_buffer[67] == 0) LocoButtons.ep1m_rev_0 = 255;//int вп-1 0-0 наз-FFFF
            if (EP1M_axis_buffer[68, 0] == 0 && EP1M_key_buffer[68] == 0) LocoButtons.ep1m_rev_vpered = 255;
            if (EP1M_axis_buffer[69, 0] == 0 && EP1M_key_buffer[69] == 0) LocoButtons.ep1m_rev_nazad = 255;

            if (EP1M_axis_buffer[70, 0] == 0 && EP1M_key_buffer[70] == 0) LocoButtons.ep1m_reg_skor_160 = 255;//float 0-160
            if (EP1M_axis_buffer[71, 0] == 0 && EP1M_key_buffer[71] == 0) LocoButtons.ep1m_reg_skor_plus = 255;//по 5км
            if (EP1M_axis_buffer[72, 0] == 0 && EP1M_key_buffer[72] == 0) LocoButtons.ep1m_reg_skor_minus = 255;
            if (EP1M_axis_buffer[73, 0] == 0 && EP1M_key_buffer[73] == 0) LocoButtons.ep1m_kranTM_0 = 255;
            if (EP1M_axis_buffer[74, 0] == 0 && EP1M_key_buffer[74] == 0) LocoButtons.ep1m_kranTM_1 = 255;
            if (EP1M_axis_buffer[75, 0] == 0 && EP1M_key_buffer[75] == 0) LocoButtons.ep1m_bv_0 = 255;
            if (EP1M_axis_buffer[76, 0] == 0 && EP1M_key_buffer[76] == 0) LocoButtons.ep1m_bv_1 = 255;
            if (EP1M_axis_buffer[77, 0] == 0 && EP1M_key_buffer[77] == 0) LocoButtons.ep1m_vozvrat_zaschity = 255;
            if (EP1M_axis_buffer[78, 0] == 0 && EP1M_key_buffer[78] == 0) LocoButtons.ep1m_blok_vvk_0 = 255;
            if (EP1M_axis_buffer[79, 0] == 0 && EP1M_key_buffer[79] == 0) LocoButtons.ep1m_blok_vvk_1 = 255;
            if (EP1M_axis_buffer[80, 0] == 0 && EP1M_key_buffer[80] == 0) LocoButtons.ep1m_tokopr_per_0 = 255;
            if (EP1M_axis_buffer[81, 0] == 0 && EP1M_key_buffer[81] == 0) LocoButtons.ep1m_tokopr_per_1 = 255;
            if (EP1M_axis_buffer[82, 0] == 0 && EP1M_key_buffer[82] == 0) LocoButtons.ep1m_tokopr_zad_0 = 255;
            if (EP1M_axis_buffer[83, 0] == 0 && EP1M_key_buffer[83] == 0) LocoButtons.ep1m_tokopr_zad_1 = 255;
            if (EP1M_axis_buffer[84, 0] == 0 && EP1M_key_buffer[84] == 0) LocoButtons.ep1m_upravlenie = 255;
            if (EP1M_axis_buffer[85, 0] == 0 && EP1M_key_buffer[85] == 0) LocoButtons.ep1m_komp_0 = 255;
            if (EP1M_axis_buffer[86, 0] == 0 && EP1M_key_buffer[86] == 0) LocoButtons.ep1m_komp_1 = 255;
            if (EP1M_axis_buffer[87, 0] == 0 && EP1M_key_buffer[87] == 0) LocoButtons.ep1m_vent1_0 = 255;
            if (EP1M_axis_buffer[88, 0] == 0 && EP1M_key_buffer[88] == 0) LocoButtons.ep1m_vent1_1 = 255;
            if (EP1M_axis_buffer[89, 0] == 0 && EP1M_key_buffer[89] == 0) LocoButtons.ep1m_vent2_0 = 255;
            if (EP1M_axis_buffer[90, 0] == 0 && EP1M_key_buffer[90] == 0) LocoButtons.ep1m_vent2_1 = 255;
            if (EP1M_axis_buffer[91, 0] == 0 && EP1M_key_buffer[91] == 0) LocoButtons.ep1m_vent3_0 = 255;
            if (EP1M_axis_buffer[92, 0] == 0 && EP1M_key_buffer[92] == 0) LocoButtons.ep1m_vent3_1 = 255;
            if (EP1M_axis_buffer[93, 0] == 0 && EP1M_key_buffer[93] == 0) LocoButtons.ep1m_MSUD_0 = 255;
            if (EP1M_axis_buffer[94, 0] == 0 && EP1M_key_buffer[94] == 0) LocoButtons.ep1m_MSUD_1 = 255;
            if (EP1M_axis_buffer[95, 0] == 0 && EP1M_key_buffer[95] == 0) LocoButtons.ep1m_vspom_mash_0 = 255;
            if (EP1M_axis_buffer[96, 0] == 0 && EP1M_key_buffer[96] == 0) LocoButtons.ep1m_vspom_mash_1 = 255;
            if (EP1M_axis_buffer[97, 0] == 0 && EP1M_key_buffer[97] == 0) LocoButtons.ep1m_svet_cab_0 = 255;//0,1,2
            if (EP1M_axis_buffer[98, 0] == 0 && EP1M_key_buffer[98] == 0) LocoButtons.ep1m_svet_cab_1 = 255;
            if (EP1M_axis_buffer[99, 0] == 0 && EP1M_key_buffer[99] == 0) LocoButtons.ep1m_svet_cab_2 = 255;
            if (EP1M_axis_buffer[100, 0] == 0 && EP1M_key_buffer[100] == 0) LocoButtons.ep1m_EPK_0 = 255;
            if (EP1M_axis_buffer[101, 0] == 0 && EP1M_key_buffer[101] == 0) LocoButtons.ep1m_EPK_1 = 255;
            if (EP1M_axis_buffer[102, 0] == 0 && EP1M_key_buffer[102] == 0) LocoButtons.ep1m_EPT_0 = 255;
            if (EP1M_axis_buffer[103, 0] == 0 && EP1M_key_buffer[103] == 0) LocoButtons.ep1m_EPT_1 = 255;
            if (EP1M_axis_buffer[104, 0] == 0 && EP1M_key_buffer[104] == 0) LocoButtons.ep1m_sign_0 = 255;
            if (EP1M_axis_buffer[105, 0] == 0 && EP1M_key_buffer[105] == 0) LocoButtons.ep1m_sign_1 = 255;
            if (EP1M_axis_buffer[106, 0] == 0 && EP1M_key_buffer[106] == 0) LocoButtons.ep1m_prozh_0 = 255;//0,1,2
            if (EP1M_axis_buffer[107, 0] == 0 && EP1M_key_buffer[107] == 0) LocoButtons.ep1m_prozh_1 = 255;
            if (EP1M_axis_buffer[108, 0] == 0 && EP1M_key_buffer[108] == 0) LocoButtons.ep1m_prozh_2 = 255;
            if (EP1M_axis_buffer[109, 0] == 0 && EP1M_key_buffer[109] == 0) LocoButtons.ep1m_avtoreg_0 = 255;
            if (EP1M_axis_buffer[110, 0] == 0 && EP1M_key_buffer[110] == 0) LocoButtons.ep1m_avtoreg_1 = 255;

            //chs2k 31
            if (CHS2K_axis_buffer[0, 0] == 0 && CHS2K_key_buffer[0] == 0) LocoButtons.chs2k_rev_0 = 255;//int вп-1 0-0 наз-FFFF
            if (CHS2K_axis_buffer[1, 0] == 0 && CHS2K_key_buffer[1] == 0) LocoButtons.chs2k_rev_vpered = 255;
            if (CHS2K_axis_buffer[2, 0] == 0 && CHS2K_key_buffer[2] == 0) LocoButtons.chs2k_rev_nazad = 255;

            if (CHS2K_axis_buffer[3, 0] == 0 && CHS2K_key_buffer[3] == 0) LocoButtons.chs2k_kontr_0 = 255;
            if (CHS2K_axis_buffer[4, 0] == 0 && CHS2K_key_buffer[4] == 0) LocoButtons.chs2k_kontr_plus = 255;
            if (CHS2K_axis_buffer[5, 0] == 0 && CHS2K_key_buffer[5] == 0) LocoButtons.chs2k_kontr_minus = 255;
            if (CHS2K_axis_buffer[6, 0] == 0 && CHS2K_key_buffer[6] == 0) LocoButtons.chs2k_kontr_plus1 = 255;
            if (CHS2K_axis_buffer[7, 0] == 0 && CHS2K_key_buffer[7] == 0) LocoButtons.chs2k_kontr_minus1 = 255;
            if (CHS2K_axis_buffer[8, 0] == 0 && CHS2K_key_buffer[8] == 0) LocoButtons.chs2k_kranTM_0 = 255;
            if (CHS2K_axis_buffer[9, 0] == 0 && CHS2K_key_buffer[9] == 0) LocoButtons.chs2k_kranTM_1 = 255;
            if (CHS2K_axis_buffer[10, 0] == 0 && CHS2K_key_buffer[10] == 0) LocoButtons.chs2k_bv_0 = 255; //через P
            if (CHS2K_axis_buffer[11, 0] == 0 && CHS2K_key_buffer[11] == 0) LocoButtons.chs2k_bv_1 = 255;//через Shift P
            if (CHS2K_axis_buffer[12, 0] == 0 && CHS2K_key_buffer[12] == 0) LocoButtons.chs2k_tokopr_per_0 = 255;
            if (CHS2K_axis_buffer[13, 0] == 0 && CHS2K_key_buffer[13] == 0) LocoButtons.chs2k_tokopr_per_1 = 255;
            if (CHS2K_axis_buffer[14, 0] == 0 && CHS2K_key_buffer[14] == 0) LocoButtons.chs2k_tokopr_zad_0 = 255;
            if (CHS2K_axis_buffer[15, 0] == 0 && CHS2K_key_buffer[15] == 0) LocoButtons.chs2k_tokopr_zad_1 = 255;
            if (CHS2K_axis_buffer[16, 0] == 0 && CHS2K_key_buffer[16] == 0) LocoButtons.chs2k_komp1_0 = 255;
            if (CHS2K_axis_buffer[17, 0] == 0 && CHS2K_key_buffer[17] == 0) LocoButtons.chs2k_komp1_1 = 255;
            if (CHS2K_axis_buffer[18, 0] == 0 && CHS2K_key_buffer[18] == 0) LocoButtons.chs2k_komp2_0 = 255;
            if (CHS2K_axis_buffer[19, 0] == 0 && CHS2K_key_buffer[19] == 0) LocoButtons.chs2k_komp2_1 = 255;
            if (CHS2K_axis_buffer[20, 0] == 0 && CHS2K_key_buffer[20] == 0) LocoButtons.chs2k_vent_0 = 255;
            if (CHS2K_axis_buffer[21, 0] == 0 && CHS2K_key_buffer[21] == 0) LocoButtons.chs2k_vent_1 = 255;
            if (CHS2K_axis_buffer[22, 0] == 0 && CHS2K_key_buffer[22] == 0) LocoButtons.chs2k_svet_cab_0 = 255;//0,1,2
            if (CHS2K_axis_buffer[23, 0] == 0 && CHS2K_key_buffer[23] == 0) LocoButtons.chs2k_svet_cab_1 = 255;
            if (CHS2K_axis_buffer[24, 0] == 0 && CHS2K_key_buffer[24] == 0) LocoButtons.chs2k_svet_cab_2 = 255;
            if (CHS2K_axis_buffer[25, 0] == 0 && CHS2K_key_buffer[25] == 0) LocoButtons.chs2k_EPK_0 = 255;
            if (CHS2K_axis_buffer[26, 0] == 0 && CHS2K_key_buffer[26] == 0) LocoButtons.chs2k_EPK_1 = 255;
            if (CHS2K_axis_buffer[27, 0] == 0 && CHS2K_key_buffer[27] == 0) LocoButtons.chs2k_EPT_0 = 255;
            if (CHS2K_axis_buffer[28, 0] == 0 && CHS2K_key_buffer[28] == 0) LocoButtons.chs2k_EPT_1 = 255;
            if (CHS2K_axis_buffer[29, 0] == 0 && CHS2K_key_buffer[29] == 0) LocoButtons.chs2k_prozh_0 = 255;//float 0-1.75
            if (CHS2K_axis_buffer[30, 0] == 0 && CHS2K_key_buffer[30] == 0) LocoButtons.chs2k_prozh_1 = 255;
            if (CHS2K_axis_buffer[31, 0] == 0 && CHS2K_key_buffer[31] == 0) LocoButtons.chs2k_prozh_2 = 255;

            //chs4 55
            if (CHS4_axis_buffer[0, 0] == 0 && CHS4_key_buffer[0] == 0) LocoButtons.chs4_rev_0 = 255;//вп-0 0-1 наз-2
            if (CHS4_axis_buffer[1, 0] == 0 && CHS4_key_buffer[1] == 0) LocoButtons.chs4_rev_vpered = 255;
            if (CHS4_axis_buffer[2, 0] == 0 && CHS4_key_buffer[2] == 0) LocoButtons.chs4_rev_nazad = 255;

            if (CHS4_axis_buffer[3, 0] == 0 && CHS4_key_buffer[3] == 0) LocoButtons.chs4_kontr_0 = 255; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
            if (CHS4_axis_buffer[4, 0] == 0 && CHS4_key_buffer[4] == 0) LocoButtons.chs4_kontr_plus = 255;
            if (CHS4_axis_buffer[5, 0] == 0 && CHS4_key_buffer[5] == 0) LocoButtons.chs4_kontr_minus = 255;
            if (CHS4_axis_buffer[6, 0] == 0 && CHS4_key_buffer[6] == 0) LocoButtons.chs4_kontr_plus1 = 255;
            if (CHS4_axis_buffer[7, 0] == 0 && CHS4_key_buffer[7] == 0) LocoButtons.chs4_kontr_minus1 = 255;
            if (CHS4_axis_buffer[8, 0] == 0 && CHS4_key_buffer[8] == 0) LocoButtons.chs4_kontr_shunt1 = 255;
            if (CHS4_axis_buffer[9, 0] == 0 && CHS4_key_buffer[9] == 0) LocoButtons.chs4_kontr_shunt2 = 255;
            if (CHS4_axis_buffer[10, 0] == 0 && CHS4_key_buffer[10] == 0) LocoButtons.chs4_kontr_shunt3 = 255;
            if (CHS4_axis_buffer[11, 0] == 0 && CHS4_key_buffer[11] == 0) LocoButtons.chs4_kontr_shunt4 = 255;
            if (CHS4_axis_buffer[12, 0] == 0 && CHS4_key_buffer[12] == 0) LocoButtons.chs4_kontr_shunt5 = 255;
            if (CHS4_axis_buffer[13, 0] == 0 && CHS4_key_buffer[13] == 0) LocoButtons.chs4_kranTM_0 = 255;
            if (CHS4_axis_buffer[14, 0] == 0 && CHS4_key_buffer[14] == 0) LocoButtons.chs4_kranTM_1 = 255;
            if (CHS4_axis_buffer[15, 0] == 0 && CHS4_key_buffer[15] == 0) LocoButtons.chs4_tokopr_per_0 = 255;
            if (CHS4_axis_buffer[16, 0] == 0 && CHS4_key_buffer[16] == 0) LocoButtons.chs4_tokopr_per_1 = 255;
            if (CHS4_axis_buffer[17, 0] == 0 && CHS4_key_buffer[17] == 0) LocoButtons.chs4_tokopr_zad_0 = 255;
            if (CHS4_axis_buffer[18, 0] == 0 && CHS4_key_buffer[18] == 0) LocoButtons.chs4_tokopr_zad_1 = 255;
            if (CHS4_axis_buffer[19, 0] == 0 && CHS4_key_buffer[19] == 0) LocoButtons.chs4_bv_0 = 255; //0-0 1-1 восст-2
            if (CHS4_axis_buffer[20, 0] == 0 && CHS4_key_buffer[20] == 0) LocoButtons.chs4_bv_1 = 255;
            if (CHS4_axis_buffer[21, 0] == 0 && CHS4_key_buffer[21] == 0) LocoButtons.chs4_bv_2 = 255;
            if (CHS4_axis_buffer[22, 0] == 0 && CHS4_key_buffer[22] == 0) LocoButtons.chs4_komp1_0 = 255;//0-2
            if (CHS4_axis_buffer[23, 0] == 0 && CHS4_key_buffer[23] == 0) LocoButtons.chs4_komp1_1 = 255;
            if (CHS4_axis_buffer[24, 0] == 0 && CHS4_key_buffer[24] == 0) LocoButtons.chs4_komp1_2 = 255;
            if (CHS4_axis_buffer[25, 0] == 0 && CHS4_key_buffer[25] == 0) LocoButtons.chs4_komp2_0 = 255;
            if (CHS4_axis_buffer[26, 0] == 0 && CHS4_key_buffer[26] == 0) LocoButtons.chs4_komp2_1 = 255;
            if (CHS4_axis_buffer[27, 0] == 0 && CHS4_key_buffer[27] == 0) LocoButtons.chs4_komp2_2 = 255;
            if (CHS4_axis_buffer[28, 0] == 0 && CHS4_key_buffer[28] == 0) LocoButtons.chs4_vent_0 = 255; //0-7
            if (CHS4_axis_buffer[29, 0] == 0 && CHS4_key_buffer[29] == 0) LocoButtons.chs4_vent_1 = 255;
            if (CHS4_axis_buffer[30, 0] == 0 && CHS4_key_buffer[30] == 0) LocoButtons.chs4_vent_2 = 255;
            if (CHS4_axis_buffer[31, 0] == 0 && CHS4_key_buffer[31] == 0) LocoButtons.chs4_vent_3 = 255;
            if (CHS4_axis_buffer[32, 0] == 0 && CHS4_key_buffer[32] == 0) LocoButtons.chs4_vent_4 = 255;
            if (CHS4_axis_buffer[33, 0] == 0 && CHS4_key_buffer[33] == 0) LocoButtons.chs4_vent_5 = 255;
            if (CHS4_axis_buffer[34, 0] == 0 && CHS4_key_buffer[34] == 0) LocoButtons.chs4_vent_6 = 255;
            if (CHS4_axis_buffer[35, 0] == 0 && CHS4_key_buffer[35] == 0) LocoButtons.chs4_vent_7 = 255;
            if (CHS4_axis_buffer[36, 0] == 0 && CHS4_key_buffer[36] == 0) LocoButtons.chs4_vspom_komp_0 = 255;//0,0-1,песок-2,Авто-3
            if (CHS4_axis_buffer[37, 0] == 0 && CHS4_key_buffer[37] == 0) LocoButtons.chs4_vspom_komp_1 = 255;
            if (CHS4_axis_buffer[38, 0] == 0 && CHS4_key_buffer[38] == 0) LocoButtons.chs4_vspom_komp_2 = 255;
            if (CHS4_axis_buffer[39, 0] == 0 && CHS4_key_buffer[39] == 0) LocoButtons.chs4_vspom_komp_3 = 255;
            if (CHS4_axis_buffer[40, 0] == 0 && CHS4_key_buffer[40] == 0) LocoButtons.chs4_svet_cab_0 = 255;//зел-0,приб-1,0-2,общ-3
            if (CHS4_axis_buffer[41, 0] == 0 && CHS4_key_buffer[41] == 0) LocoButtons.chs4_svet_cab_1 = 255;
            if (CHS4_axis_buffer[42, 0] == 0 && CHS4_key_buffer[42] == 0) LocoButtons.chs4_svet_cab_2 = 255;
            if (CHS4_axis_buffer[43, 0] == 0 && CHS4_key_buffer[43] == 0) LocoButtons.chs4_svet_cab_3 = 255;
            if (CHS4_axis_buffer[44, 0] == 0 && CHS4_key_buffer[44] == 0) LocoButtons.chs4_EPK_0 = 255;
            if (CHS4_axis_buffer[45, 0] == 0 && CHS4_key_buffer[45] == 0) LocoButtons.chs4_EPK_1 = 255;
            if (CHS4_axis_buffer[46, 0] == 0 && CHS4_key_buffer[46] == 0) LocoButtons.chs4_EPT_0 = 255;
            if (CHS4_axis_buffer[47, 0] == 0 && CHS4_key_buffer[47] == 0) LocoButtons.chs4_EPT_1 = 255;
            if (CHS4_axis_buffer[48, 0] == 0 && CHS4_key_buffer[48] == 0) LocoButtons.chs4_avar_nabor_0 = 255;
            if (CHS4_axis_buffer[49, 0] == 0 && CHS4_key_buffer[49] == 0) LocoButtons.chs4_avar_nabor_1 = 255;
            if (CHS4_axis_buffer[50, 0] == 0 && CHS4_key_buffer[50] == 0) LocoButtons.chs4_avar_nabor_2 = 255;
            if (CHS4_axis_buffer[51, 0] == 0 && CHS4_key_buffer[51] == 0) LocoButtons.chs4_avar_nabor_3 = 255;
            if (CHS4_axis_buffer[52, 0] == 0 && CHS4_key_buffer[52] == 0) LocoButtons.chs4_prozh_0 = 255; //0,1,2
            if (CHS4_axis_buffer[53, 0] == 0 && CHS4_key_buffer[53] == 0) LocoButtons.chs4_prozh_1 = 255;
            if (CHS4_axis_buffer[54, 0] == 0 && CHS4_key_buffer[54] == 0) LocoButtons.chs4_prozh_2 = 255;

            //chs4kvr 55
            if (CHS4KVR_axis_buffer[0, 0] == 0 && CHS4KVR_key_buffer[0] == 0) LocoButtons.chs4kvr_rev_0 = 255;//вп-0 0-1 наз-2
            if (CHS4KVR_axis_buffer[1, 0] == 0 && CHS4KVR_key_buffer[1] == 0) LocoButtons.chs4kvr_rev_vpered = 255;
            if (CHS4KVR_axis_buffer[2, 0] == 0 && CHS4KVR_key_buffer[2] == 0) LocoButtons.chs4kvr_rev_nazad = 255;

            if (CHS4KVR_axis_buffer[3, 0] == 0 && CHS4KVR_key_buffer[3] == 0) LocoButtons.chs4kvr_kontr_0 = 255; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
            if (CHS4KVR_axis_buffer[4, 0] == 0 && CHS4KVR_key_buffer[4] == 0) LocoButtons.chs4kvr_kontr_plus = 255;
            if (CHS4KVR_axis_buffer[5, 0] == 0 && CHS4KVR_key_buffer[5] == 0) LocoButtons.chs4kvr_kontr_minus = 255;
            if (CHS4KVR_axis_buffer[6, 0] == 0 && CHS4KVR_key_buffer[6] == 0) LocoButtons.chs4kvr_kontr_plus1 = 255;
            if (CHS4KVR_axis_buffer[7, 0] == 0 && CHS4KVR_key_buffer[7] == 0) LocoButtons.chs4kvr_kontr_minus1 = 255;
            if (CHS4KVR_axis_buffer[8, 0] == 0 && CHS4KVR_key_buffer[8] == 0) LocoButtons.chs4kvr_kontr_shunt1 = 255;
            if (CHS4KVR_axis_buffer[9, 0] == 0 && CHS4KVR_key_buffer[9] == 0) LocoButtons.chs4kvr_kontr_shunt2 = 255;
            if (CHS4KVR_axis_buffer[10, 0] == 0 && CHS4KVR_key_buffer[10] == 0) LocoButtons.chs4kvr_kontr_shunt3 = 255;
            if (CHS4KVR_axis_buffer[11, 0] == 0 && CHS4KVR_key_buffer[11] == 0) LocoButtons.chs4kvr_kontr_shunt4 = 255;
            if (CHS4KVR_axis_buffer[12, 0] == 0 && CHS4KVR_key_buffer[12] == 0) LocoButtons.chs4kvr_kontr_shunt5 = 255;
            if (CHS4KVR_axis_buffer[13, 0] == 0 && CHS4KVR_key_buffer[13] == 0) LocoButtons.chs4kvr_kranTM_0 = 255;
            if (CHS4KVR_axis_buffer[14, 0] == 0 && CHS4KVR_key_buffer[14] == 0) LocoButtons.chs4kvr_kranTM_1 = 255;
            if (CHS4KVR_axis_buffer[15, 0] == 0 && CHS4KVR_key_buffer[15] == 0) LocoButtons.chs4kvr_tokopr_per_0 = 255;
            if (CHS4KVR_axis_buffer[16, 0] == 0 && CHS4KVR_key_buffer[16] == 0) LocoButtons.chs4kvr_tokopr_per_1 = 255;
            if (CHS4KVR_axis_buffer[17, 0] == 0 && CHS4KVR_key_buffer[17] == 0) LocoButtons.chs4kvr_tokopr_zad_0 = 255;
            if (CHS4KVR_axis_buffer[18, 0] == 0 && CHS4KVR_key_buffer[18] == 0) LocoButtons.chs4kvr_tokopr_zad_1 = 255;
            if (CHS4KVR_axis_buffer[19, 0] == 0 && CHS4KVR_key_buffer[19] == 0) LocoButtons.chs4kvr_bv_0 = 255; //0-0 1-1 восст-2
            if (CHS4KVR_axis_buffer[20, 0] == 0 && CHS4KVR_key_buffer[20] == 0) LocoButtons.chs4kvr_bv_1 = 255;
            if (CHS4KVR_axis_buffer[21, 0] == 0 && CHS4KVR_key_buffer[21] == 0) LocoButtons.chs4kvr_bv_2 = 255;
            if (CHS4KVR_axis_buffer[22, 0] == 0 && CHS4KVR_key_buffer[22] == 0) LocoButtons.chs4kvr_komp1_0 = 255;//0Т-0,0-1,А-2,Р-3
            if (CHS4KVR_axis_buffer[23, 0] == 0 && CHS4KVR_key_buffer[23] == 0) LocoButtons.chs4kvr_komp1_1 = 255;
            if (CHS4KVR_axis_buffer[24, 0] == 0 && CHS4KVR_key_buffer[24] == 0) LocoButtons.chs4kvr_komp1_2 = 255;
            if (CHS4KVR_axis_buffer[25, 0] == 0 && CHS4KVR_key_buffer[25] == 0) LocoButtons.chs4kvr_komp2_0 = 255;
            if (CHS4KVR_axis_buffer[26, 0] == 0 && CHS4KVR_key_buffer[26] == 0) LocoButtons.chs4kvr_komp2_1 = 255;
            if (CHS4KVR_axis_buffer[27, 0] == 0 && CHS4KVR_key_buffer[27] == 0) LocoButtons.chs4kvr_komp2_2 = 255;
            if (CHS4KVR_axis_buffer[28, 0] == 0 && CHS4KVR_key_buffer[28] == 0) LocoButtons.chs4kvr_vent_0 = 255; //0-7
            if (CHS4KVR_axis_buffer[29, 0] == 0 && CHS4KVR_key_buffer[29] == 0) LocoButtons.chs4kvr_vent_1 = 255; //0-7
            if (CHS4KVR_axis_buffer[30, 0] == 0 && CHS4KVR_key_buffer[30] == 0) LocoButtons.chs4kvr_vent_2 = 255; //0-7
            if (CHS4KVR_axis_buffer[31, 0] == 0 && CHS4KVR_key_buffer[31] == 0) LocoButtons.chs4kvr_vent_3 = 255; //0-7
            if (CHS4KVR_axis_buffer[32, 0] == 0 && CHS4KVR_key_buffer[32] == 0) LocoButtons.chs4kvr_vent_4 = 255; //0-7
            if (CHS4KVR_axis_buffer[33, 0] == 0 && CHS4KVR_key_buffer[33] == 0) LocoButtons.chs4kvr_vent_5 = 255; //0-7
            if (CHS4KVR_axis_buffer[34, 0] == 0 && CHS4KVR_key_buffer[34] == 0) LocoButtons.chs4kvr_vent_6 = 255; //0-7
            if (CHS4KVR_axis_buffer[35, 0] == 0 && CHS4KVR_key_buffer[35] == 0) LocoButtons.chs4kvr_vent_7 = 255; //0-7
            if (CHS4KVR_axis_buffer[36, 0] == 0 && CHS4KVR_key_buffer[36] == 0) LocoButtons.chs4kvr_vspom_komp_0 = 255;//0,0-1,песок-2,Авто-3
            if (CHS4KVR_axis_buffer[37, 0] == 0 && CHS4KVR_key_buffer[37] == 0) LocoButtons.chs4kvr_vspom_komp_1 = 255;
            if (CHS4KVR_axis_buffer[38, 0] == 0 && CHS4KVR_key_buffer[38] == 0) LocoButtons.chs4kvr_vspom_komp_2 = 255;
            if (CHS4KVR_axis_buffer[39, 0] == 0 && CHS4KVR_key_buffer[39] == 0) LocoButtons.chs4kvr_vspom_komp_3 = 255;
            if (CHS4KVR_axis_buffer[40, 0] == 0 && CHS4KVR_key_buffer[40] == 0) LocoButtons.chs4kvr_svet_cab_0 = 255;//зел-0,приб-1,0-2,общ-3
            if (CHS4KVR_axis_buffer[41, 0] == 0 && CHS4KVR_key_buffer[41] == 0) LocoButtons.chs4kvr_svet_cab_1 = 255;
            if (CHS4KVR_axis_buffer[42, 0] == 0 && CHS4KVR_key_buffer[42] == 0) LocoButtons.chs4kvr_svet_cab_2 = 255;
            if (CHS4KVR_axis_buffer[43, 0] == 0 && CHS4KVR_key_buffer[43] == 0) LocoButtons.chs4kvr_svet_cab_3 = 255;
            if (CHS4KVR_axis_buffer[44, 0] == 0 && CHS4KVR_key_buffer[44] == 0) LocoButtons.chs4kvr_EPK_0 = 255;
            if (CHS4KVR_axis_buffer[45, 0] == 0 && CHS4KVR_key_buffer[45] == 0) LocoButtons.chs4kvr_EPK_1 = 255;
            if (CHS4KVR_axis_buffer[46, 0] == 0 && CHS4KVR_key_buffer[46] == 0) LocoButtons.chs4kvr_EPT_0 = 255;
            if (CHS4KVR_axis_buffer[47, 0] == 0 && CHS4KVR_key_buffer[47] == 0) LocoButtons.chs4kvr_EPT_1 = 255;
            if (CHS4KVR_axis_buffer[48, 0] == 0 && CHS4KVR_key_buffer[48] == 0) LocoButtons.chs4kvr_avar_nabor_0 = 255;
            if (CHS4KVR_axis_buffer[49, 0] == 0 && CHS4KVR_key_buffer[49] == 0) LocoButtons.chs4kvr_avar_nabor_1 = 255;
            if (CHS4KVR_axis_buffer[50, 0] == 0 && CHS4KVR_key_buffer[50] == 0) LocoButtons.chs4kvr_avar_nabor_2 = 255;
            if (CHS4KVR_axis_buffer[51, 0] == 0 && CHS4KVR_key_buffer[51] == 0) LocoButtons.chs4kvr_avar_nabor_3 = 255;
            if (CHS4KVR_axis_buffer[52, 0] == 0 && CHS4KVR_key_buffer[52] == 0) LocoButtons.chs4kvr_prozh_0 = 255; //0,1,2
            if (CHS4KVR_axis_buffer[53, 0] == 0 && CHS4KVR_key_buffer[53] == 0) LocoButtons.chs4kvr_prozh_1 = 255;
            if (CHS4KVR_axis_buffer[54, 0] == 0 && CHS4KVR_key_buffer[54] == 0) LocoButtons.chs4kvr_prozh_2 = 255;

            //chs4t 52
            if (CHS4T_axis_buffer[0, 0] == 0 && CHS4T_key_buffer[0] == 0) LocoButtons.chs4t_rev_0 = 255;//вп-0 0-1 наз-2
            if (CHS4T_axis_buffer[1, 0] == 0 && CHS4T_key_buffer[1] == 0) LocoButtons.chs4t_rev_vpered = 255;
            if (CHS4T_axis_buffer[2, 0] == 0 && CHS4T_key_buffer[2] == 0) LocoButtons.chs4t_rev_nazad = 255;

            if (CHS4T_axis_buffer[3, 0] == 0 && CHS4T_key_buffer[3] == 0) LocoButtons.chs4t_kontr_0 = 255; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
            if (CHS4T_axis_buffer[4, 0] == 0 && CHS4T_key_buffer[4] == 0) LocoButtons.chs4t_kontr_plus = 255;
            if (CHS4T_axis_buffer[5, 0] == 0 && CHS4T_key_buffer[5] == 0) LocoButtons.chs4t_kontr_minus = 255;
            if (CHS4T_axis_buffer[6, 0] == 0 && CHS4T_key_buffer[6] == 0) LocoButtons.chs4t_kontr_plus1 = 255;
            if (CHS4T_axis_buffer[7, 0] == 0 && CHS4T_key_buffer[7] == 0) LocoButtons.chs4t_kontr_minus1 = 255;
            if (CHS4T_axis_buffer[8, 0] == 0 && CHS4T_key_buffer[8] == 0) LocoButtons.chs4t_kontr_shunt1 = 255;
            if (CHS4T_axis_buffer[9, 0] == 0 && CHS4T_key_buffer[9] == 0) LocoButtons.chs4t_kontr_shunt2 = 255;
            if (CHS4T_axis_buffer[10, 0] == 0 && CHS4T_key_buffer[10] == 0) LocoButtons.chs4t_kontr_shunt3 = 255;
            if (CHS4T_axis_buffer[11, 0] == 0 && CHS4T_key_buffer[11] == 0) LocoButtons.chs4t_kontr_shunt4 = 255;
            if (CHS4T_axis_buffer[12, 0] == 0 && CHS4T_key_buffer[12] == 0) LocoButtons.chs4t_kontr_shunt5 = 255;
            if (CHS4T_axis_buffer[13, 0] == 0 && CHS4T_key_buffer[13] == 0) LocoButtons.chs4t_kranTM_0 = 255;
            if (CHS4T_axis_buffer[14, 0] == 0 && CHS4T_key_buffer[14] == 0) LocoButtons.chs4t_kranTM_1 = 255;
            if (CHS4T_axis_buffer[15, 0] == 0 && CHS4T_key_buffer[15] == 0) LocoButtons.chs4t_tokopr_per_0 = 255;
            if (CHS4T_axis_buffer[16, 0] == 0 && CHS4T_key_buffer[16] == 0) LocoButtons.chs4t_tokopr_per_1 = 255;
            if (CHS4T_axis_buffer[17, 0] == 0 && CHS4T_key_buffer[17] == 0) LocoButtons.chs4t_tokopr_zad_0 = 255;
            if (CHS4T_axis_buffer[18, 0] == 0 && CHS4T_key_buffer[18] == 0) LocoButtons.chs4t_tokopr_zad_1 = 255;
            if (CHS4T_axis_buffer[19, 0] == 0 && CHS4T_key_buffer[19] == 0) LocoButtons.chs4t_bv_0 = 255; //0-0 1-1 восст-2
            if (CHS4T_axis_buffer[20, 0] == 0 && CHS4T_key_buffer[20] == 0) LocoButtons.chs4t_bv_1 = 255;
            if (CHS4T_axis_buffer[21, 0] == 0 && CHS4T_key_buffer[21] == 0) LocoButtons.chs4t_bv_2 = 255;
            if (CHS4T_axis_buffer[22, 0] == 0 && CHS4T_key_buffer[22] == 0) LocoButtons.chs4t_komp1_0 = 255;//0Т-0,0-1,А-2,Р-3
            if (CHS4T_axis_buffer[23, 0] == 0 && CHS4T_key_buffer[23] == 0) LocoButtons.chs4t_komp1_1 = 255;
            if (CHS4T_axis_buffer[24, 0] == 0 && CHS4T_key_buffer[24] == 0) LocoButtons.chs4t_komp1_2 = 255;
            if (CHS4T_axis_buffer[25, 0] == 0 && CHS4T_key_buffer[25] == 0) LocoButtons.chs4t_komp2_0 = 255;
            if (CHS4T_axis_buffer[26, 0] == 0 && CHS4T_key_buffer[26] == 0) LocoButtons.chs4t_komp2_1 = 255;
            if (CHS4T_axis_buffer[27, 0] == 0 && CHS4T_key_buffer[27] == 0) LocoButtons.chs4t_komp2_2 = 255;
            if (CHS4T_axis_buffer[28, 0] == 0 && CHS4T_key_buffer[28] == 0) LocoButtons.chs4t_vent_0 = 255; //1,2раб,0-выкл
            if (CHS4T_axis_buffer[29, 0] == 0 && CHS4T_key_buffer[29] == 0) LocoButtons.chs4t_vent_1 = 255;
            if (CHS4T_axis_buffer[30, 0] == 0 && CHS4T_key_buffer[30] == 0) LocoButtons.chs4t_vent_2 = 255;
            if (CHS4T_axis_buffer[31, 0] == 0 && CHS4T_key_buffer[31] == 0) LocoButtons.chs4t_vspom_komp_0 = 255;//0,0-1,песок-2,Авто-3
            if (CHS4T_axis_buffer[32, 0] == 0 && CHS4T_key_buffer[32] == 0) LocoButtons.chs4t_vspom_komp_1 = 255;
            if (CHS4T_axis_buffer[33, 0] == 0 && CHS4T_key_buffer[33] == 0) LocoButtons.chs4t_vspom_komp_2 = 255;
            if (CHS4T_axis_buffer[34, 0] == 0 && CHS4T_key_buffer[34] == 0) LocoButtons.chs4t_vspom_komp_3 = 255;
            if (CHS4T_axis_buffer[35, 0] == 0 && CHS4T_key_buffer[35] == 0) LocoButtons.chs4t_svet_cab_0 = 255;//зел-0,приб-1,0-2,общ-3
            if (CHS4T_axis_buffer[36, 0] == 0 && CHS4T_key_buffer[36] == 0) LocoButtons.chs4t_svet_cab_1 = 255;
            if (CHS4T_axis_buffer[37, 0] == 0 && CHS4T_key_buffer[37] == 0) LocoButtons.chs4t_svet_cab_2 = 255;
            if (CHS4T_axis_buffer[38, 0] == 0 && CHS4T_key_buffer[38] == 0) LocoButtons.chs4t_svet_cab_3 = 255;
            if (CHS4T_axis_buffer[39, 0] == 0 && CHS4T_key_buffer[39] == 0) LocoButtons.chs4t_EPK_0 = 255;
            if (CHS4T_axis_buffer[40, 0] == 0 && CHS4T_key_buffer[40] == 0) LocoButtons.chs4t_EPK_1 = 255;
            if (CHS4T_axis_buffer[41, 0] == 0 && CHS4T_key_buffer[41] == 0) LocoButtons.chs4t_EPT_0 = 255;
            if (CHS4T_axis_buffer[42, 0] == 0 && CHS4T_key_buffer[42] == 0) LocoButtons.chs4t_EPT_1 = 255;
            if (CHS4T_axis_buffer[43, 0] == 0 && CHS4T_key_buffer[43] == 0) LocoButtons.chs4t_avar_nabor_0 = 255;
            if (CHS4T_axis_buffer[44, 0] == 0 && CHS4T_key_buffer[44] == 0) LocoButtons.chs4t_avar_nabor_1 = 255;
            if (CHS4T_axis_buffer[45, 0] == 0 && CHS4T_key_buffer[45] == 0) LocoButtons.chs4t_avar_nabor_2 = 255;
            if (CHS4T_axis_buffer[46, 0] == 0 && CHS4T_key_buffer[46] == 0) LocoButtons.chs4t_avar_nabor_3 = 255;
            if (CHS4T_axis_buffer[47, 0] == 0 && CHS4T_key_buffer[47] == 0) LocoButtons.chs4t_prozh_0 = 255; //0,1,2
            if (CHS4T_axis_buffer[48, 0] == 0 && CHS4T_key_buffer[48] == 0) LocoButtons.chs4t_prozh_1 = 255;
            if (CHS4T_axis_buffer[49, 0] == 0 && CHS4T_key_buffer[49] == 0) LocoButtons.chs4t_prozh_2 = 255;
            if (CHS4T_axis_buffer[50, 0] == 0 && CHS4T_key_buffer[50] == 0) LocoButtons.chs4t_zhalyzi_0 = 255;
            if (CHS4T_axis_buffer[51, 0] == 0 && CHS4T_key_buffer[51] == 0) LocoButtons.chs4t_zhalyzi_1 = 255;

            //chs7 45
            if (CHS7_axis_buffer[0, 0] == 0 && CHS7_key_buffer[0] == 0) LocoButtons.chs7_rev_0 = 255;//вп1 0-0 нз255
            if (CHS7_axis_buffer[1, 0] == 0 && CHS7_key_buffer[1] == 0) LocoButtons.chs7_rev_vpered = 255;
            if (CHS7_axis_buffer[2, 0] == 0 && CHS7_key_buffer[2] == 0) LocoButtons.chs7_rev_nazad = 255;

            if (CHS7_axis_buffer[3, 0] == 0 && CHS7_key_buffer[3] == 0) LocoButtons.chs7_kontr_0 = 255; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
            if (CHS7_axis_buffer[4, 0] == 0 && CHS7_key_buffer[4] == 0) LocoButtons.chs7_kontr_plus = 255;
            if (CHS7_axis_buffer[5, 0] == 0 && CHS7_key_buffer[5] == 0) LocoButtons.chs7_kontr_minus = 255;
            if (CHS7_axis_buffer[6, 0] == 0 && CHS7_key_buffer[6] == 0) LocoButtons.chs7_kontr_plus1 = 255;
            if (CHS7_axis_buffer[7, 0] == 0 && CHS7_key_buffer[7] == 0) LocoButtons.chs7_kontr_minus1 = 255;
            if (CHS7_axis_buffer[8, 0] == 0 && CHS7_key_buffer[8] == 0) LocoButtons.chs7_kontr_shunt1 = 255;
            if (CHS7_axis_buffer[9, 0] == 0 && CHS7_key_buffer[9] == 0) LocoButtons.chs7_kontr_shunt2 = 255;
            if (CHS7_axis_buffer[10, 0] == 0 && CHS7_key_buffer[10] == 0) LocoButtons.chs7_kontr_shunt3 = 255;
            if (CHS7_axis_buffer[11, 0] == 0 && CHS7_key_buffer[11] == 0) LocoButtons.chs7_kontr_shunt4 = 255;
            if (CHS7_axis_buffer[12, 0] == 0 && CHS7_key_buffer[12] == 0) LocoButtons.chs7_kontr_shunt5 = 255;
            if (CHS7_axis_buffer[13, 0] == 0 && CHS7_key_buffer[13] == 0) LocoButtons.chs7_kranTM_0 = 255;
            if (CHS7_axis_buffer[14, 0] == 0 && CHS7_key_buffer[14] == 0) LocoButtons.chs7_kranTM_1 = 255;
            if (CHS7_axis_buffer[15, 0] == 0 && CHS7_key_buffer[15] == 0) LocoButtons.chs7_tokopr_per_0 = 255;//0-2 2через shift I,O
            if (CHS7_axis_buffer[16, 0] == 0 && CHS7_key_buffer[16] == 0) LocoButtons.chs7_tokopr_per_1 = 255;
            if (CHS7_axis_buffer[17, 0] == 0 && CHS7_key_buffer[17] == 0) LocoButtons.chs7_tokopr_per_2 = 255;
            if (CHS7_axis_buffer[18, 0] == 0 && CHS7_key_buffer[18] == 0) LocoButtons.chs7_tokopr_zad_0 = 255;
            if (CHS7_axis_buffer[19, 0] == 0 && CHS7_key_buffer[19] == 0) LocoButtons.chs7_tokopr_zad_1 = 255;
            if (CHS7_axis_buffer[20, 0] == 0 && CHS7_key_buffer[20] == 0) LocoButtons.chs7_tokopr_zad_2 = 255;
            if (CHS7_axis_buffer[21, 0] == 0 && CHS7_key_buffer[21] == 0) LocoButtons.chs7_bv_0 = 255; //0-0 1-1 восст-2 через shift P
            if (CHS7_axis_buffer[22, 0] == 0 && CHS7_key_buffer[22] == 0) LocoButtons.chs7_bv_1 = 255;
            if (CHS7_axis_buffer[23, 0] == 0 && CHS7_key_buffer[23] == 0) LocoButtons.chs7_bv_2 = 255;
            if (CHS7_axis_buffer[24, 0] == 0 && CHS7_key_buffer[24] == 0) LocoButtons.chs7_komp1_0 = 255;//0-0,1А,2Р
            if (CHS7_axis_buffer[25, 0] == 0 && CHS7_key_buffer[25] == 0) LocoButtons.chs7_komp1_1 = 255;
            if (CHS7_axis_buffer[26, 0] == 0 && CHS7_key_buffer[26] == 0) LocoButtons.chs7_komp1_2 = 255;
            if (CHS7_axis_buffer[27, 0] == 0 && CHS7_key_buffer[27] == 0) LocoButtons.chs7_komp2_0 = 255;
            if (CHS7_axis_buffer[28, 0] == 0 && CHS7_key_buffer[28] == 0) LocoButtons.chs7_komp2_1 = 255;
            if (CHS7_axis_buffer[29, 0] == 0 && CHS7_key_buffer[29] == 0) LocoButtons.chs7_komp2_2 = 255;
            if (CHS7_axis_buffer[30, 0] == 0 && CHS7_key_buffer[30] == 0) LocoButtons.chs7_vent_0 = 255; //0выкл 1вкл_пр 255вкл_лев
            if (CHS7_axis_buffer[31, 0] == 0 && CHS7_key_buffer[31] == 0) LocoButtons.chs7_vent_1 = 255;
            if (CHS7_axis_buffer[32, 0] == 0 && CHS7_key_buffer[32] == 0) LocoButtons.chs7_vent_2 = 255;
            if (CHS7_axis_buffer[33, 0] == 0 && CHS7_key_buffer[33] == 0) LocoButtons.chs7_sbros_SP = 255;
            if (CHS7_axis_buffer[34, 0] == 0 && CHS7_key_buffer[34] == 0) LocoButtons.chs7_svet_cab_0 = 255;//0выкл,приб-1,2общ
            if (CHS7_axis_buffer[35, 0] == 0 && CHS7_key_buffer[35] == 0) LocoButtons.chs7_svet_cab_1 = 255;
            if (CHS7_axis_buffer[36, 0] == 0 && CHS7_key_buffer[36] == 0) LocoButtons.chs7_svet_cab_2 = 255;
            if (CHS7_axis_buffer[37, 0] == 0 && CHS7_key_buffer[37] == 0) LocoButtons.chs7_EPK_0 = 255;
            if (CHS7_axis_buffer[38, 0] == 0 && CHS7_key_buffer[38] == 0) LocoButtons.chs7_EPK_1 = 255;
            if (CHS7_axis_buffer[39, 0] == 0 && CHS7_key_buffer[39] == 0) LocoButtons.chs7_EPT_0 = 255;
            if (CHS7_axis_buffer[40, 0] == 0 && CHS7_key_buffer[40] == 0) LocoButtons.chs7_EPT_1 = 255;
            if (CHS7_axis_buffer[41, 0] == 0 && CHS7_key_buffer[41] == 0) LocoButtons.chs7_prozh_0 = 255;//float 0-1,75
            if (CHS7_axis_buffer[42, 0] == 0 && CHS7_key_buffer[42] == 0) LocoButtons.chs7_prozh_1 = 255;
            if (CHS7_axis_buffer[43, 0] == 0 && CHS7_key_buffer[43] == 0) LocoButtons.chs7_prozh_2 = 255;
            if (CHS7_axis_buffer[44, 0] == 0 && CHS7_key_buffer[44] == 0) LocoButtons.chs7_zhalyzi1_0 = 255;
            if (CHS7_axis_buffer[45, 0] == 0 && CHS7_key_buffer[45] == 0) LocoButtons.chs7_zhalyzi1_1 = 255;

            //chs8 63
            if (CHS8_axis_buffer[0, 0] == 0 && CHS8_key_buffer[0] == 0) LocoButtons.chs8_rev_0 = 255;//вп0 0-1 нз2
            if (CHS8_axis_buffer[1, 0] == 0 && CHS8_key_buffer[1] == 0) LocoButtons.chs8_rev_vpered = 255;
            if (CHS8_axis_buffer[2, 0] == 0 && CHS8_key_buffer[2] == 0) LocoButtons.chs8_rev_nazad = 255;

            if (CHS8_axis_buffer[3, 0] == 0 && CHS8_key_buffer[3] == 0) LocoButtons.chs8_kontr_0 = 255; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
            if (CHS8_axis_buffer[4, 0] == 0 && CHS8_key_buffer[4] == 0) LocoButtons.chs8_kontr_plus = 255;
            if (CHS8_axis_buffer[5, 0] == 0 && CHS8_key_buffer[5] == 0) LocoButtons.chs8_kontr_minus = 255;
            if (CHS8_axis_buffer[6, 0] == 0 && CHS8_key_buffer[6] == 0) LocoButtons.chs8_kontr_plus1 = 255;
            if (CHS8_axis_buffer[7, 0] == 0 && CHS8_key_buffer[7] == 0) LocoButtons.chs8_kontr_minus1 = 255;
            if (CHS8_axis_buffer[8, 0] == 0 && CHS8_key_buffer[8] == 0) LocoButtons.chs8_kontr_shunt1 = 255;
            if (CHS8_axis_buffer[9, 0] == 0 && CHS8_key_buffer[9] == 0) LocoButtons.chs8_kontr_shunt2 = 255;
            if (CHS8_axis_buffer[10, 0] == 0 && CHS8_key_buffer[10] == 0) LocoButtons.chs8_kontr_shunt3 = 255;
            if (CHS8_axis_buffer[11, 0] == 0 && CHS8_key_buffer[11] == 0) LocoButtons.chs8_kontr_shunt4 = 255;
            if (CHS8_axis_buffer[12, 0] == 0 && CHS8_key_buffer[12] == 0) LocoButtons.chs8_kontr_shunt5 = 255;
            if (CHS8_axis_buffer[13, 0] == 0 && CHS8_key_buffer[13] == 0) LocoButtons.chs8_kranTM_0 = 255;
            if (CHS8_axis_buffer[14, 0] == 0 && CHS8_key_buffer[14] == 0) LocoButtons.chs8_kranTM_1 = 255;
            if (CHS8_axis_buffer[15, 0] == 0 && CHS8_key_buffer[15] == 0) LocoButtons.chs8_tokopr_per_0 = 255;//0-1
            if (CHS8_axis_buffer[16, 0] == 0 && CHS8_key_buffer[16] == 0) LocoButtons.chs8_tokopr_per_1 = 255;
            if (CHS8_axis_buffer[17, 0] == 0 && CHS8_key_buffer[17] == 0) LocoButtons.chs8_tokopr_zad_0 = 255;
            if (CHS8_axis_buffer[18, 0] == 0 && CHS8_key_buffer[18] == 0) LocoButtons.chs8_tokopr_zad_1 = 255;
            if (CHS8_axis_buffer[19, 0] == 0 && CHS8_key_buffer[19] == 0) LocoButtons.chs8_bv_0 = 255; //вкл БВ 0-0 1-1 
            if (CHS8_axis_buffer[20, 0] == 0 && CHS8_key_buffer[20] == 0) LocoButtons.chs8_bv_1 = 255;
            if (CHS8_axis_buffer[21, 0] == 0 && CHS8_key_buffer[21] == 0) LocoButtons.chs8_vosst_bv = 255;//через K
            if (CHS8_axis_buffer[22, 0] == 0 && CHS8_key_buffer[22] == 0) LocoButtons.chs8_komp1_0 = 255;//0выкл,1А,2Р
            if (CHS8_axis_buffer[23, 0] == 0 && CHS8_key_buffer[23] == 0) LocoButtons.chs8_komp1_1 = 255;
            if (CHS8_axis_buffer[24, 0] == 0 && CHS8_key_buffer[24] == 0) LocoButtons.chs8_komp1_2 = 255;
            if (CHS8_axis_buffer[25, 0] == 0 && CHS8_key_buffer[25] == 0) LocoButtons.chs8_komp2_0 = 255;
            if (CHS8_axis_buffer[26, 0] == 0 && CHS8_key_buffer[26] == 0) LocoButtons.chs8_komp2_1 = 255;
            if (CHS8_axis_buffer[27, 0] == 0 && CHS8_key_buffer[27] == 0) LocoButtons.chs8_komp2_2 = 255;
            if (CHS8_axis_buffer[28, 0] == 0 && CHS8_key_buffer[28] == 0) LocoButtons.chs8_vent1_0 = 255; //0выкл,1авто,2-4раб
            if (CHS8_axis_buffer[29, 0] == 0 && CHS8_key_buffer[29] == 0) LocoButtons.chs8_vent1_1 = 255;
            if (CHS8_axis_buffer[30, 0] == 0 && CHS8_key_buffer[30] == 0) LocoButtons.chs8_vent1_2 = 255;
            if (CHS8_axis_buffer[31, 0] == 0 && CHS8_key_buffer[31] == 0) LocoButtons.chs8_vent1_3 = 255;
            if (CHS8_axis_buffer[32, 0] == 0 && CHS8_key_buffer[32] == 0) LocoButtons.chs8_vent1_4 = 255;
            if (CHS8_axis_buffer[33, 0] == 0 && CHS8_key_buffer[33] == 0) LocoButtons.chs8_vent2_0 = 255; //0выкл,1авто,2-4раб
            if (CHS8_axis_buffer[34, 0] == 0 && CHS8_key_buffer[34] == 0) LocoButtons.chs8_vent2_1 = 255;
            if (CHS8_axis_buffer[35, 0] == 0 && CHS8_key_buffer[35] == 0) LocoButtons.chs8_vent2_2 = 255;
            if (CHS8_axis_buffer[36, 0] == 0 && CHS8_key_buffer[36] == 0) LocoButtons.chs8_vent2_3 = 255;
            if (CHS8_axis_buffer[37, 0] == 0 && CHS8_key_buffer[37] == 0) LocoButtons.chs8_vent2_4 = 255;
            if (CHS8_axis_buffer[38, 0] == 0 && CHS8_key_buffer[38] == 0) LocoButtons.chs8_vspom_komp_0 = 255;//0выкл,1песок,2авто,3комп
            if (CHS8_axis_buffer[39, 0] == 0 && CHS8_key_buffer[39] == 0) LocoButtons.chs8_vspom_komp_1 = 255;
            if (CHS8_axis_buffer[40, 0] == 0 && CHS8_key_buffer[40] == 0) LocoButtons.chs8_vspom_komp_2 = 255;
            if (CHS8_axis_buffer[41, 0] == 0 && CHS8_key_buffer[41] == 0) LocoButtons.chs8_vspom_komp_3 = 255;
            if (CHS8_axis_buffer[42, 0] == 0 && CHS8_key_buffer[42] == 0) LocoButtons.chs8_svet_cab_0 = 255;//0зел,1приб,2выкл,3общ,4приб,5зел
            if (CHS8_axis_buffer[43, 0] == 0 && CHS8_key_buffer[43] == 0) LocoButtons.chs8_svet_cab_1 = 255;
            if (CHS8_axis_buffer[44, 0] == 0 && CHS8_key_buffer[44] == 0) LocoButtons.chs8_svet_cab_2 = 255;
            if (CHS8_axis_buffer[45, 0] == 0 && CHS8_key_buffer[45] == 0) LocoButtons.chs8_svet_cab_3 = 255;
            if (CHS8_axis_buffer[46, 0] == 0 && CHS8_key_buffer[46] == 0) LocoButtons.chs8_svet_cab_4 = 255;
            if (CHS8_axis_buffer[47, 0] == 0 && CHS8_key_buffer[47] == 0) LocoButtons.chs8_svet_cab_5 = 255;
            if (CHS8_axis_buffer[48, 0] == 0 && CHS8_key_buffer[48] == 0) LocoButtons.chs8_EPK_0 = 255;
            if (CHS8_axis_buffer[49, 0] == 0 && CHS8_key_buffer[49] == 0) LocoButtons.chs8_EPK_1 = 255;
            if (CHS8_axis_buffer[50, 0] == 0 && CHS8_key_buffer[50] == 0) LocoButtons.chs8_EPT_0 = 255;
            if (CHS8_axis_buffer[51, 0] == 0 && CHS8_key_buffer[51] == 0) LocoButtons.chs8_EPT_1 = 255;
            if (CHS8_axis_buffer[52, 0] == 0 && CHS8_key_buffer[52] == 0) LocoButtons.chs8_avar_nabor_0 = 255;
            if (CHS8_axis_buffer[53, 0] == 0 && CHS8_key_buffer[53] == 0) LocoButtons.chs8_avar_nabor_1 = 255;
            if (CHS8_axis_buffer[54, 0] == 0 && CHS8_key_buffer[54] == 0) LocoButtons.chs8_avar_nabor_2 = 255;
            if (CHS8_axis_buffer[55, 0] == 0 && CHS8_key_buffer[55] == 0) LocoButtons.chs8_avar_nabor_3 = 255;
            if (CHS8_axis_buffer[56, 0] == 0 && CHS8_key_buffer[56] == 0) LocoButtons.chs8_prozh_0 = 255; //0,1,2
            if (CHS8_axis_buffer[57, 0] == 0 && CHS8_key_buffer[57] == 0) LocoButtons.chs8_prozh_1 = 255;
            if (CHS8_axis_buffer[58, 0] == 0 && CHS8_key_buffer[58] == 0) LocoButtons.chs8_prozh_2 = 255;
            if (CHS8_axis_buffer[59, 0] == 0 && CHS8_key_buffer[59] == 0) LocoButtons.chs8_reost_torm_proverka = 255;//0выкл,1проверка через backspace
            if (CHS8_axis_buffer[60, 0] == 0 && CHS8_key_buffer[60] == 0) LocoButtons.chs8_reost_torm_0 = 255;//0выкл,1середина,2торм через >
            if (CHS8_axis_buffer[61, 0] == 0 && CHS8_key_buffer[61] == 0) LocoButtons.chs8_reost_torm_1 = 255;
            if (CHS8_axis_buffer[62, 0] == 0 && CHS8_key_buffer[62] == 0) LocoButtons.chs8_reost_torm_2 = 255;

            //vl11 82
            if (VL11M_axis_buffer[0, 0] == 0 && VL11M_key_buffer[0] == 0) LocoButtons.vl11_rev_0 = 255;//вп1 0-0 нз255
            if (VL11M_axis_buffer[1, 0] == 0 && VL11M_key_buffer[1] == 0) LocoButtons.vl11_rev_vpered = 255;
            if (VL11M_axis_buffer[2, 0] == 0 && VL11M_key_buffer[2] == 0) LocoButtons.vl11_rev_nazad = 255;

            if (VL11M_axis_buffer[3, 0] == 0 && VL11M_key_buffer[3] == 0) LocoButtons.vl11_kontr_0 = 255;
            if (VL11M_axis_buffer[4, 0] == 0 && VL11M_key_buffer[4] == 0) LocoButtons.vl11_kontr_1 = 255;
            if (VL11M_axis_buffer[5, 0] == 0 && VL11M_key_buffer[5] == 0) LocoButtons.vl11_kontr_2 = 255;
            if (VL11M_axis_buffer[6, 0] == 0 && VL11M_key_buffer[6] == 0) LocoButtons.vl11_kontr_3 = 255;
            if (VL11M_axis_buffer[7, 0] == 0 && VL11M_key_buffer[7] == 0) LocoButtons.vl11_kontr_4 = 255;
            if (VL11M_axis_buffer[8, 0] == 0 && VL11M_key_buffer[8] == 0) LocoButtons.vl11_kontr_5 = 255;
            if (VL11M_axis_buffer[9, 0] == 0 && VL11M_key_buffer[9] == 0) LocoButtons.vl11_kontr_6 = 255;
            if (VL11M_axis_buffer[10, 0] == 0 && VL11M_key_buffer[10] == 0) LocoButtons.vl11_kontr_7 = 255;
            if (VL11M_axis_buffer[11, 0] == 0 && VL11M_key_buffer[11] == 0) LocoButtons.vl11_kontr_8 = 255;
            if (VL11M_axis_buffer[12, 0] == 0 && VL11M_key_buffer[12] == 0) LocoButtons.vl11_kontr_9 = 255;
            if (VL11M_axis_buffer[13, 0] == 0 && VL11M_key_buffer[13] == 0) LocoButtons.vl11_kontr_10 = 255;
            if (VL11M_axis_buffer[14, 0] == 0 && VL11M_key_buffer[14] == 0) LocoButtons.vl11_kontr_11 = 255;
            if (VL11M_axis_buffer[15, 0] == 0 && VL11M_key_buffer[15] == 0) LocoButtons.vl11_kontr_12 = 255;
            if (VL11M_axis_buffer[16, 0] == 0 && VL11M_key_buffer[16] == 0) LocoButtons.vl11_kontr_13 = 255;
            if (VL11M_axis_buffer[17, 0] == 0 && VL11M_key_buffer[17] == 0) LocoButtons.vl11_kontr_14 = 255;
            if (VL11M_axis_buffer[18, 0] == 0 && VL11M_key_buffer[18] == 0) LocoButtons.vl11_kontr_15 = 255;
            if (VL11M_axis_buffer[19, 0] == 0 && VL11M_key_buffer[19] == 0) LocoButtons.vl11_kontr_16 = 255;
            if (VL11M_axis_buffer[20, 0] == 0 && VL11M_key_buffer[20] == 0) LocoButtons.vl11_kontr_17 = 255;
            if (VL11M_axis_buffer[21, 0] == 0 && VL11M_key_buffer[21] == 0) LocoButtons.vl11_kontr_18 = 255;
            if (VL11M_axis_buffer[22, 0] == 0 && VL11M_key_buffer[22] == 0) LocoButtons.vl11_kontr_19 = 255;
            if (VL11M_axis_buffer[23, 0] == 0 && VL11M_key_buffer[23] == 0) LocoButtons.vl11_kontr_20 = 255;
            if (VL11M_axis_buffer[24, 0] == 0 && VL11M_key_buffer[24] == 0) LocoButtons.vl11_kontr_21 = 255;
            if (VL11M_axis_buffer[25, 0] == 0 && VL11M_key_buffer[25] == 0) LocoButtons.vl11_kontr_22 = 255;
            if (VL11M_axis_buffer[26, 0] == 0 && VL11M_key_buffer[26] == 0) LocoButtons.vl11_kontr_23 = 255;
            if (VL11M_axis_buffer[27, 0] == 0 && VL11M_key_buffer[27] == 0) LocoButtons.vl11_kontr_24 = 255;
            if (VL11M_axis_buffer[28, 0] == 0 && VL11M_key_buffer[28] == 0) LocoButtons.vl11_kontr_25 = 255;
            if (VL11M_axis_buffer[29, 0] == 0 && VL11M_key_buffer[29] == 0) LocoButtons.vl11_kontr_26 = 255;
            if (VL11M_axis_buffer[30, 0] == 0 && VL11M_key_buffer[30] == 0) LocoButtons.vl11_kontr_27 = 255;
            if (VL11M_axis_buffer[31, 0] == 0 && VL11M_key_buffer[31] == 0) LocoButtons.vl11_kontr_28 = 255;
            if (VL11M_axis_buffer[32, 0] == 0 && VL11M_key_buffer[32] == 0) LocoButtons.vl11_kontr_29 = 255;
            if (VL11M_axis_buffer[33, 0] == 0 && VL11M_key_buffer[33] == 0) LocoButtons.vl11_kontr_30 = 255;
            if (VL11M_axis_buffer[34, 0] == 0 && VL11M_key_buffer[34] == 0) LocoButtons.vl11_kontr_31 = 255;
            if (VL11M_axis_buffer[35, 0] == 0 && VL11M_key_buffer[35] == 0) LocoButtons.vl11_kontr_32 = 255;
            if (VL11M_axis_buffer[36, 0] == 0 && VL11M_key_buffer[36] == 0) LocoButtons.vl11_kontr_33 = 255;
            if (VL11M_axis_buffer[37, 0] == 0 && VL11M_key_buffer[37] == 0) LocoButtons.vl11_kontr_34 = 255;
            if (VL11M_axis_buffer[38, 0] == 0 && VL11M_key_buffer[38] == 0) LocoButtons.vl11_kontr_35 = 255;
            if (VL11M_axis_buffer[39, 0] == 0 && VL11M_key_buffer[39] == 0) LocoButtons.vl11_kontr_36 = 255;
            if (VL11M_axis_buffer[40, 0] == 0 && VL11M_key_buffer[40] == 0) LocoButtons.vl11_kontr_37 = 255;
            if (VL11M_axis_buffer[41, 0] == 0 && VL11M_key_buffer[41] == 0) LocoButtons.vl11_kontr_38 = 255;
            if (VL11M_axis_buffer[42, 0] == 0 && VL11M_key_buffer[42] == 0) LocoButtons.vl11_kontr_39 = 255;
            if (VL11M_axis_buffer[43, 0] == 0 && VL11M_key_buffer[43] == 0) LocoButtons.vl11_kontr_40 = 255;
            if (VL11M_axis_buffer[44, 0] == 0 && VL11M_key_buffer[44] == 0) LocoButtons.vl11_kontr_41 = 255;
            if (VL11M_axis_buffer[45, 0] == 0 && VL11M_key_buffer[45] == 0) LocoButtons.vl11_kontr_42 = 255;
            if (VL11M_axis_buffer[46, 0] == 0 && VL11M_key_buffer[46] == 0) LocoButtons.vl11_kontr_43 = 255;
            if (VL11M_axis_buffer[47, 0] == 0 && VL11M_key_buffer[47] == 0) LocoButtons.vl11_kontr_44 = 255;
            if (VL11M_axis_buffer[48, 0] == 0 && VL11M_key_buffer[48] == 0) LocoButtons.vl11_kontr_45 = 255;
            if (VL11M_axis_buffer[49, 0] == 0 && VL11M_key_buffer[49] == 0) LocoButtons.vl11_kontr_46 = 255;
            if (VL11M_axis_buffer[50, 0] == 0 && VL11M_key_buffer[50] == 0) LocoButtons.vl11_kontr_47 = 255;
            if (VL11M_axis_buffer[51, 0] == 0 && VL11M_key_buffer[51] == 0) LocoButtons.vl11_kontr_48 = 255;

            if (VL11M_axis_buffer[52, 0] == 0 && VL11M_key_buffer[52] == 0) LocoButtons.vl11_kontr_shunt_0 = 255;//0выкл 255-252
            if (VL11M_axis_buffer[53, 0] == 0 && VL11M_key_buffer[53] == 0) LocoButtons.vl11_kontr_shunt_1 = 255;
            if (VL11M_axis_buffer[54, 0] == 0 && VL11M_key_buffer[54] == 0) LocoButtons.vl11_kontr_shunt_2 = 255;
            if (VL11M_axis_buffer[55, 0] == 0 && VL11M_key_buffer[55] == 0) LocoButtons.vl11_kontr_shunt_3 = 255;
            if (VL11M_axis_buffer[56, 0] == 0 && VL11M_key_buffer[56] == 0) LocoButtons.vl11_kontr_shunt_4 = 255;

            if (VL11M_axis_buffer[57, 0] == 0 && VL11M_key_buffer[57] == 0) LocoButtons.vl11_kranTM_0 = 255;
            if (VL11M_axis_buffer[58, 0] == 0 && VL11M_key_buffer[58] == 0) LocoButtons.vl11_kranTM_1 = 255;
            if (VL11M_axis_buffer[59, 0] == 0 && VL11M_key_buffer[59] == 0) LocoButtons.vl11_tokopr_obshiy_0 = 255;
            if (VL11M_axis_buffer[60, 0] == 0 && VL11M_key_buffer[60] == 0) LocoButtons.vl11_tokopr_obshiy_1 = 255;
            if (VL11M_axis_buffer[61, 0] == 0 && VL11M_key_buffer[61] == 0) LocoButtons.vl11_tokopr_per_0 = 255;
            if (VL11M_axis_buffer[62, 0] == 0 && VL11M_key_buffer[62] == 0) LocoButtons.vl11_tokopr_per_1 = 255;
            if (VL11M_axis_buffer[63, 0] == 0 && VL11M_key_buffer[63] == 0) LocoButtons.vl11_tokopr_zad_0 = 255;
            if (VL11M_axis_buffer[64, 0] == 0 && VL11M_key_buffer[64] == 0) LocoButtons.vl11_tokopr_zad_1 = 255;
            if (VL11M_axis_buffer[65, 0] == 0 && VL11M_key_buffer[65] == 0) LocoButtons.vl11_bv_0 = 255; //БВ 0-0 1-1
            if (VL11M_axis_buffer[66, 0] == 0 && VL11M_key_buffer[66] == 0) LocoButtons.vl11_bv_1 = 255;
            if (VL11M_axis_buffer[67, 0] == 0 && VL11M_key_buffer[67] == 0) LocoButtons.vl11_vosst_bv = 255;//через K
            if (VL11M_axis_buffer[68, 0] == 0 && VL11M_key_buffer[68] == 0) LocoButtons.vl11_komp_0 = 255;//0выкл,1вкл
            if (VL11M_axis_buffer[69, 0] == 0 && VL11M_key_buffer[69] == 0) LocoButtons.vl11_komp_1 = 255;
            if (VL11M_axis_buffer[70, 0] == 0 && VL11M_key_buffer[70] == 0) LocoButtons.vl11_vent_0 = 255; //0выкл,1низ,2выс
            if (VL11M_axis_buffer[71, 0] == 0 && VL11M_key_buffer[71] == 0) LocoButtons.vl11_vent_1 = 255;
            if (VL11M_axis_buffer[72, 0] == 0 && VL11M_key_buffer[72] == 0) LocoButtons.vl11_vent_2 = 255;
            if (VL11M_axis_buffer[73, 0] == 0 && VL11M_key_buffer[73] == 0) LocoButtons.vl11_svet_cab_0 = 255;//0выкл,1приб,2общ
            if (VL11M_axis_buffer[74, 0] == 0 && VL11M_key_buffer[74] == 0) LocoButtons.vl11_svet_cab_1 = 255;
            if (VL11M_axis_buffer[75, 0] == 0 && VL11M_key_buffer[75] == 0) LocoButtons.vl11_svet_cab_2 = 255;
            if (VL11M_axis_buffer[76, 0] == 0 && VL11M_key_buffer[76] == 0) LocoButtons.vl11_EPK_0 = 255;
            if (VL11M_axis_buffer[77, 0] == 0 && VL11M_key_buffer[77] == 0) LocoButtons.vl11_EPK_1 = 255;
            if (VL11M_axis_buffer[78, 0] == 0 && VL11M_key_buffer[78] == 0) LocoButtons.vl11_prozh_0 = 255; //float 0-1,875
            if (VL11M_axis_buffer[79, 0] == 0 && VL11M_key_buffer[79] == 0) LocoButtons.vl11_prozh_1 = 255;
            if (VL11M_axis_buffer[80, 0] == 0 && VL11M_key_buffer[80] == 0) LocoButtons.vl11_prozh_2 = 255;
            if (VL11M_axis_buffer[81, 0] == 0 && VL11M_key_buffer[81] == 0) LocoButtons.vl11_sign_0 = 255;
            if (VL11M_axis_buffer[82, 0] == 0 && VL11M_key_buffer[82] == 0) LocoButtons.vl11_sign_1 = 255;

            //vl82 83
            if (VL82M_axis_buffer[0, 0] == 0 && VL82M_key_buffer[0] == 0) LocoButtons.vl82_rev_0 = 255;//0нз,1-0,2вп
            if (VL82M_axis_buffer[1, 0] == 0 && VL82M_key_buffer[1] == 0) LocoButtons.vl82_rev_vpered = 255;
            if (VL82M_axis_buffer[2, 0] == 0 && VL82M_key_buffer[2] == 0) LocoButtons.vl82_rev_nazad = 255;

            if (VL82M_axis_buffer[3, 0] == 0 && VL82M_key_buffer[3] == 0) LocoButtons.vl82_kontr_bv = 255;//0-38 БВ_255 ,клавD для откл БВ
            if (VL82M_axis_buffer[4, 0] == 0 && VL82M_key_buffer[4] == 0) LocoButtons.vl82_kontr_0 = 255;
            if (VL82M_axis_buffer[5, 0] == 0 && VL82M_key_buffer[5] == 0) LocoButtons.vl82_kontr_1 = 255;
            if (VL82M_axis_buffer[6, 0] == 0 && VL82M_key_buffer[6] == 0) LocoButtons.vl82_kontr_2 = 255;
            if (VL82M_axis_buffer[7, 0] == 0 && VL82M_key_buffer[7] == 0) LocoButtons.vl82_kontr_3 = 255;
            if (VL82M_axis_buffer[8, 0] == 0 && VL82M_key_buffer[8] == 0) LocoButtons.vl82_kontr_4 = 255;
            if (VL82M_axis_buffer[9, 0] == 0 && VL82M_key_buffer[9] == 0) LocoButtons.vl82_kontr_5 = 255;
            if (VL82M_axis_buffer[10, 0] == 0 && VL82M_key_buffer[10] == 0) LocoButtons.vl82_kontr_6 = 255;
            if (VL82M_axis_buffer[11, 0] == 0 && VL82M_key_buffer[11] == 0) LocoButtons.vl82_kontr_7 = 255;
            if (VL82M_axis_buffer[12, 0] == 0 && VL82M_key_buffer[12] == 0) LocoButtons.vl82_kontr_8 = 255;
            if (VL82M_axis_buffer[13, 0] == 0 && VL82M_key_buffer[13] == 0) LocoButtons.vl82_kontr_9 = 255;
            if (VL82M_axis_buffer[14, 0] == 0 && VL82M_key_buffer[14] == 0) LocoButtons.vl82_kontr_10 = 255;
            if (VL82M_axis_buffer[15, 0] == 0 && VL82M_key_buffer[15] == 0) LocoButtons.vl82_kontr_11 = 255;
            if (VL82M_axis_buffer[16, 0] == 0 && VL82M_key_buffer[16] == 0) LocoButtons.vl82_kontr_12 = 255;
            if (VL82M_axis_buffer[17, 0] == 0 && VL82M_key_buffer[17] == 0) LocoButtons.vl82_kontr_13 = 255;
            if (VL82M_axis_buffer[18, 0] == 0 && VL82M_key_buffer[18] == 0) LocoButtons.vl82_kontr_14 = 255;
            if (VL82M_axis_buffer[19, 0] == 0 && VL82M_key_buffer[19] == 0) LocoButtons.vl82_kontr_15 = 255;
            if (VL82M_axis_buffer[20, 0] == 0 && VL82M_key_buffer[20] == 0) LocoButtons.vl82_kontr_16 = 255;
            if (VL82M_axis_buffer[21, 0] == 0 && VL82M_key_buffer[21] == 0) LocoButtons.vl82_kontr_17 = 255;
            if (VL82M_axis_buffer[22, 0] == 0 && VL82M_key_buffer[22] == 0) LocoButtons.vl82_kontr_18 = 255;
            if (VL82M_axis_buffer[23, 0] == 0 && VL82M_key_buffer[23] == 0) LocoButtons.vl82_kontr_19 = 255;
            if (VL82M_axis_buffer[24, 0] == 0 && VL82M_key_buffer[24] == 0) LocoButtons.vl82_kontr_20 = 255;
            if (VL82M_axis_buffer[25, 0] == 0 && VL82M_key_buffer[25] == 0) LocoButtons.vl82_kontr_21 = 255;
            if (VL82M_axis_buffer[26, 0] == 0 && VL82M_key_buffer[26] == 0) LocoButtons.vl82_kontr_22 = 255;
            if (VL82M_axis_buffer[27, 0] == 0 && VL82M_key_buffer[27] == 0) LocoButtons.vl82_kontr_23 = 255;
            if (VL82M_axis_buffer[28, 0] == 0 && VL82M_key_buffer[28] == 0) LocoButtons.vl82_kontr_24 = 255;
            if (VL82M_axis_buffer[29, 0] == 0 && VL82M_key_buffer[29] == 0) LocoButtons.vl82_kontr_25 = 255;
            if (VL82M_axis_buffer[30, 0] == 0 && VL82M_key_buffer[30] == 0) LocoButtons.vl82_kontr_26 = 255;
            if (VL82M_axis_buffer[31, 0] == 0 && VL82M_key_buffer[31] == 0) LocoButtons.vl82_kontr_27 = 255;
            if (VL82M_axis_buffer[32, 0] == 0 && VL82M_key_buffer[32] == 0) LocoButtons.vl82_kontr_28 = 255;
            if (VL82M_axis_buffer[33, 0] == 0 && VL82M_key_buffer[33] == 0) LocoButtons.vl82_kontr_29 = 255;
            if (VL82M_axis_buffer[34, 0] == 0 && VL82M_key_buffer[34] == 0) LocoButtons.vl82_kontr_30 = 255;
            if (VL82M_axis_buffer[35, 0] == 0 && VL82M_key_buffer[35] == 0) LocoButtons.vl82_kontr_31 = 255;
            if (VL82M_axis_buffer[36, 0] == 0 && VL82M_key_buffer[36] == 0) LocoButtons.vl82_kontr_32 = 255;
            if (VL82M_axis_buffer[37, 0] == 0 && VL82M_key_buffer[37] == 0) LocoButtons.vl82_kontr_33 = 255;
            if (VL82M_axis_buffer[38, 0] == 0 && VL82M_key_buffer[38] == 0) LocoButtons.vl82_kontr_34 = 255;
            if (VL82M_axis_buffer[39, 0] == 0 && VL82M_key_buffer[39] == 0) LocoButtons.vl82_kontr_35 = 255;
            if (VL82M_axis_buffer[40, 0] == 0 && VL82M_key_buffer[40] == 0) LocoButtons.vl82_kontr_36 = 255;
            if (VL82M_axis_buffer[41, 0] == 0 && VL82M_key_buffer[41] == 0) LocoButtons.vl82_kontr_37 = 255;
            if (VL82M_axis_buffer[42, 0] == 0 && VL82M_key_buffer[42] == 0) LocoButtons.vl82_kontr_38 = 255;

            if (VL82M_axis_buffer[43, 0] == 0 && VL82M_key_buffer[43] == 0) LocoButtons.vl82_kontr_shunt_0 = 255;//0выкл,1-4шунты,255реостат
            if (VL82M_axis_buffer[44, 0] == 0 && VL82M_key_buffer[44] == 0) LocoButtons.vl82_kontr_shunt_1 = 255;
            if (VL82M_axis_buffer[45, 0] == 0 && VL82M_key_buffer[45] == 0) LocoButtons.vl82_kontr_shunt_2 = 255;
            if (VL82M_axis_buffer[46, 0] == 0 && VL82M_key_buffer[46] == 0) LocoButtons.vl82_kontr_shunt_3 = 255;
            if (VL82M_axis_buffer[47, 0] == 0 && VL82M_key_buffer[47] == 0) LocoButtons.vl82_kontr_shunt_4 = 255;
            if (VL82M_axis_buffer[48, 0] == 0 && VL82M_key_buffer[48] == 0) LocoButtons.vl82_kontr_shunt_reostat = 255;

            if (VL82M_axis_buffer[49, 0] == 0 && VL82M_key_buffer[49] == 0) LocoButtons.vl82_kranTM_0 = 255;
            if (VL82M_axis_buffer[50, 0] == 0 && VL82M_key_buffer[50] == 0) LocoButtons.vl82_kranTM_1 = 255;
            if (VL82M_axis_buffer[51, 0] == 0 && VL82M_key_buffer[51] == 0) LocoButtons.vl82_tokopr_obshiy_0 = 255;
            if (VL82M_axis_buffer[52, 0] == 0 && VL82M_key_buffer[52] == 0) LocoButtons.vl82_tokopr_obshiy_1 = 255;
            if (VL82M_axis_buffer[53, 0] == 0 && VL82M_key_buffer[53] == 0) LocoButtons.vl82_tokopr_per_0 = 255;
            if (VL82M_axis_buffer[54, 0] == 0 && VL82M_key_buffer[54] == 0) LocoButtons.vl82_tokopr_per_1 = 255;
            if (VL82M_axis_buffer[55, 0] == 0 && VL82M_key_buffer[55] == 0) LocoButtons.vl82_tokopr_zad_0 = 255;
            if (VL82M_axis_buffer[56, 0] == 0 && VL82M_key_buffer[56] == 0) LocoButtons.vl82_tokopr_zad_1 = 255;
            if (VL82M_axis_buffer[57, 0] == 0 && VL82M_key_buffer[57] == 0) LocoButtons.vl82_gv_0 = 255; //ГВ 0-0 1-1
            if (VL82M_axis_buffer[58, 0] == 0 && VL82M_key_buffer[58] == 0) LocoButtons.vl82_gv_1 = 255;
            if (VL82M_axis_buffer[59, 0] == 0 && VL82M_key_buffer[59] == 0) LocoButtons.vl82_bv_0 = 255;
            if (VL82M_axis_buffer[60, 0] == 0 && VL82M_key_buffer[60] == 0) LocoButtons.vl82_bv_1 = 255;
            if (VL82M_axis_buffer[61, 0] == 0 && VL82M_key_buffer[61] == 0) LocoButtons.vl82_vosst_gv = 255;//через K
            if (VL82M_axis_buffer[62, 0] == 0 && VL82M_key_buffer[62] == 0) LocoButtons.vl82_komp_0 = 255;//0выкл,1вкл
            if (VL82M_axis_buffer[63, 0] == 0 && VL82M_key_buffer[63] == 0) LocoButtons.vl82_komp_1 = 255;
            if (VL82M_axis_buffer[64, 0] == 0 && VL82M_key_buffer[64] == 0) LocoButtons.vl82_vent1_0 = 255;
            if (VL82M_axis_buffer[65, 0] == 0 && VL82M_key_buffer[65] == 0) LocoButtons.vl82_vent1_1 = 255;
            if (VL82M_axis_buffer[66, 0] == 0 && VL82M_key_buffer[66] == 0) LocoButtons.vl82_vent2_0 = 255;
            if (VL82M_axis_buffer[67, 0] == 0 && VL82M_key_buffer[67] == 0) LocoButtons.vl82_vent2_1 = 255;
            if (VL82M_axis_buffer[68, 0] == 0 && VL82M_key_buffer[68] == 0) LocoButtons.vl82_kvc_0 = 255;
            if (VL82M_axis_buffer[69, 0] == 0 && VL82M_key_buffer[69] == 0) LocoButtons.vl82_kvc_1 = 255;
            if (VL82M_axis_buffer[70, 0] == 0 && VL82M_key_buffer[70] == 0) LocoButtons.vl82_vozvr_kvc = 255;//через Y
            if (VL82M_axis_buffer[71, 0] == 0 && VL82M_key_buffer[71] == 0) LocoButtons.vl82_upravlenie_0 = 255;
            if (VL82M_axis_buffer[72, 0] == 0 && VL82M_key_buffer[72] == 0) LocoButtons.vl82_upravlenie_1 = 255;
            if (VL82M_axis_buffer[73, 0] == 0 && VL82M_key_buffer[73] == 0) LocoButtons.vl82_svet_cab_0 = 255;//0выкл,1приб,2общ
            if (VL82M_axis_buffer[74, 0] == 0 && VL82M_key_buffer[74] == 0) LocoButtons.vl82_svet_cab_1 = 255;
            if (VL82M_axis_buffer[75, 0] == 0 && VL82M_key_buffer[75] == 0) LocoButtons.vl82_svet_cab_2 = 255;
            if (VL82M_axis_buffer[76, 0] == 0 && VL82M_key_buffer[76] == 0) LocoButtons.vl82_EPK_0 = 255;
            if (VL82M_axis_buffer[77, 0] == 0 && VL82M_key_buffer[77] == 0) LocoButtons.vl82_EPK_1 = 255;
            if (VL82M_axis_buffer[78, 0] == 0 && VL82M_key_buffer[78] == 0) LocoButtons.vl82_prozh_0 = 255;//0,1,2
            if (VL82M_axis_buffer[79, 0] == 0 && VL82M_key_buffer[79] == 0) LocoButtons.vl82_prozh_1 = 255;
            if (VL82M_axis_buffer[80, 0] == 0 && VL82M_key_buffer[80] == 0) LocoButtons.vl82_prozh_2 = 255;
            if (VL82M_axis_buffer[81, 0] == 0 && VL82M_key_buffer[81] == 0) LocoButtons.vl82_sign_0 = 255;
            if (VL82M_axis_buffer[82, 0] == 0 && VL82M_key_buffer[82] == 0) LocoButtons.vl82_sign_1 = 255;

            //vl80t 51
            if (VL80T_axis_buffer[0, 0] == 0 && VL80T_key_buffer[0] == 0) LocoButtons.vl80t_rev_0 = 255;//255нз,0-0,1вп,2-4шунты
            if (VL80T_axis_buffer[1, 0] == 0 && VL80T_key_buffer[1] == 0) LocoButtons.vl80t_rev_vpered = 255;
            if (VL80T_axis_buffer[2, 0] == 0 && VL80T_key_buffer[2] == 0) LocoButtons.vl80t_rev_nazad = 255;
            if (VL80T_axis_buffer[3, 0] == 0 && VL80T_key_buffer[3] == 0) LocoButtons.vl80t_rev_shunt1 = 255;
            if (VL80T_axis_buffer[4, 0] == 0 && VL80T_key_buffer[4] == 0) LocoButtons.vl80t_rev_shunt2 = 255;
            if (VL80T_axis_buffer[5, 0] == 0 && VL80T_key_buffer[5] == 0) LocoButtons.vl80t_rev_shunt3 = 255;

            if (VL80T_axis_buffer[6, 0] == 0 && VL80T_key_buffer[6] == 0) LocoButtons.vl80t_kontr_bv = 255;//255выкл_бв,0-0,1ав,2рв,3фв,4фп,5рп,6ап ,клавD для откл ГВ
            if (VL80T_axis_buffer[7, 0] == 0 && VL80T_key_buffer[7] == 0) LocoButtons.vl80t_kontr_0 = 255;
            if (VL80T_axis_buffer[8, 0] == 0 && VL80T_key_buffer[8] == 0) LocoButtons.vl80t_kontr_1 = 255;
            if (VL80T_axis_buffer[9, 0] == 0 && VL80T_key_buffer[9] == 0) LocoButtons.vl80t_kontr_2 = 255;
            if (VL80T_axis_buffer[10, 0] == 0 && VL80T_key_buffer[10] == 0) LocoButtons.vl80t_kontr_3 = 255;
            if (VL80T_axis_buffer[11, 0] == 0 && VL80T_key_buffer[11] == 0) LocoButtons.vl80t_kontr_4 = 255;
            if (VL80T_axis_buffer[12, 0] == 0 && VL80T_key_buffer[12] == 0) LocoButtons.vl80t_kontr_5 = 255;
            if (VL80T_axis_buffer[13, 0] == 0 && VL80T_key_buffer[13] == 0) LocoButtons.vl80t_kontr_6 = 255;//клавA для 6 полож

            if (VL80T_axis_buffer[14, 0] == 0 && VL80T_key_buffer[14] == 0) LocoButtons.vl80t_kranTM_0 = 255;
            if (VL80T_axis_buffer[15, 0] == 0 && VL80T_key_buffer[15] == 0) LocoButtons.vl80t_kranTM_1 = 255;
            if (VL80T_axis_buffer[16, 0] == 0 && VL80T_key_buffer[16] == 0) LocoButtons.vl80t_tokopr_obshiy_0 = 255;
            if (VL80T_axis_buffer[17, 0] == 0 && VL80T_key_buffer[17] == 0) LocoButtons.vl80t_tokopr_obshiy_1 = 255;
            if (VL80T_axis_buffer[18, 0] == 0 && VL80T_key_buffer[18] == 0) LocoButtons.vl80t_tokopr_per_0 = 255;
            if (VL80T_axis_buffer[19, 0] == 0 && VL80T_key_buffer[19] == 0) LocoButtons.vl80t_tokopr_per_1 = 255;
            if (VL80T_axis_buffer[20, 0] == 0 && VL80T_key_buffer[20] == 0) LocoButtons.vl80t_tokopr_zad_0 = 255;
            if (VL80T_axis_buffer[21, 0] == 0 && VL80T_key_buffer[21] == 0) LocoButtons.vl80t_tokopr_zad_1 = 255;
            if (VL80T_axis_buffer[22, 0] == 0 && VL80T_key_buffer[22] == 0) LocoButtons.vl80t_gv_0 = 255; //ГВ 0-0 1-1
            if (VL80T_axis_buffer[23, 0] == 0 && VL80T_key_buffer[23] == 0) LocoButtons.vl80t_gv_1 = 255;
            if (VL80T_axis_buffer[24, 0] == 0 && VL80T_key_buffer[24] == 0) LocoButtons.vl80t_vosst_gv = 255;//через K
            if (VL80T_axis_buffer[25, 0] == 0 && VL80T_key_buffer[25] == 0) LocoButtons.vl80t_komp_0 = 255;//0выкл,1вкл
            if (VL80T_axis_buffer[26, 0] == 0 && VL80T_key_buffer[26] == 0) LocoButtons.vl80t_komp_1 = 255;
            if (VL80T_axis_buffer[27, 0] == 0 && VL80T_key_buffer[27] == 0) LocoButtons.vl80t_vent1_0 = 255;
            if (VL80T_axis_buffer[28, 0] == 0 && VL80T_key_buffer[28] == 0) LocoButtons.vl80t_vent1_1 = 255;
            if (VL80T_axis_buffer[29, 0] == 0 && VL80T_key_buffer[29] == 0) LocoButtons.vl80t_vent2_0 = 255;
            if (VL80T_axis_buffer[30, 0] == 0 && VL80T_key_buffer[30] == 0) LocoButtons.vl80t_vent2_1 = 255;
            if (VL80T_axis_buffer[31, 0] == 0 && VL80T_key_buffer[31] == 0) LocoButtons.vl80t_vent3_0 = 255;
            if (VL80T_axis_buffer[32, 0] == 0 && VL80T_key_buffer[32] == 0) LocoButtons.vl80t_vent3_1 = 255;
            if (VL80T_axis_buffer[33, 0] == 0 && VL80T_key_buffer[33] == 0) LocoButtons.vl80t_vent4_0 = 255;
            if (VL80T_axis_buffer[34, 0] == 0 && VL80T_key_buffer[34] == 0) LocoButtons.vl80t_vent4_1 = 255;
            if (VL80T_axis_buffer[35, 0] == 0 && VL80T_key_buffer[35] == 0) LocoButtons.vl80t_fz_0 = 255;
            if (VL80T_axis_buffer[36, 0] == 0 && VL80T_key_buffer[36] == 0) LocoButtons.vl80t_fz_1 = 255;
            if (VL80T_axis_buffer[37, 0] == 0 && VL80T_key_buffer[37] == 0) LocoButtons.vl80t_upravlenie_0 = 255;
            if (VL80T_axis_buffer[38, 0] == 0 && VL80T_key_buffer[38] == 0) LocoButtons.vl80t_upravlenie_1 = 255;
            if (VL80T_axis_buffer[39, 0] == 0 && VL80T_key_buffer[39] == 0) LocoButtons.vl80t_svet_cab_0 = 255;//0выкл,1приб,2общ
            if (VL80T_axis_buffer[40, 0] == 0 && VL80T_key_buffer[40] == 0) LocoButtons.vl80t_svet_cab_1 = 255;
            if (VL80T_axis_buffer[41, 0] == 0 && VL80T_key_buffer[41] == 0) LocoButtons.vl80t_svet_cab_2 = 255;
            if (VL80T_axis_buffer[42, 0] == 0 && VL80T_key_buffer[42] == 0) LocoButtons.vl80t_EPK_0 = 255;
            if (VL80T_axis_buffer[43, 0] == 0 && VL80T_key_buffer[43] == 0) LocoButtons.vl80t_EPK_1 = 255;
            if (VL80T_axis_buffer[44, 0] == 0 && VL80T_key_buffer[44] == 0) LocoButtons.vl80t_prozh_0 = 255;//0,1,2
            if (VL80T_axis_buffer[45, 0] == 0 && VL80T_key_buffer[45] == 0) LocoButtons.vl80t_prozh_1 = 255;
            if (VL80T_axis_buffer[46, 0] == 0 && VL80T_key_buffer[46] == 0) LocoButtons.vl80t_prozh_2 = 255;
            if (VL80T_axis_buffer[47, 0] == 0 && VL80T_key_buffer[47] == 0) LocoButtons.vl80t_sign_0 = 255;
            if (VL80T_axis_buffer[48, 0] == 0 && VL80T_key_buffer[48] == 0) LocoButtons.vl80t_sign_1 = 255;

            //vl85 80
            if (VL85_axis_buffer[0, 0] == 0 && VL85_key_buffer[0] == 0) LocoButtons.vl85_rev_0 = 255;//255нз,0-0,1вп,2-4шунты
            if (VL85_axis_buffer[1, 0] == 0 && VL85_key_buffer[1] == 0) LocoButtons.vl85_rev_vpered = 255;
            if (VL85_axis_buffer[2, 0] == 0 && VL85_key_buffer[2] == 0) LocoButtons.vl85_rev_nazad = 255;
            if (VL85_axis_buffer[3, 0] == 0 && VL85_key_buffer[3] == 0) LocoButtons.vl85_rev_shunt1 = 255;
            if (VL85_axis_buffer[4, 0] == 0 && VL85_key_buffer[4] == 0) LocoButtons.vl85_rev_shunt2 = 255;
            if (VL85_axis_buffer[5, 0] == 0 && VL85_key_buffer[5] == 0) LocoButtons.vl85_rev_shunt3 = 255;

            if (VL85_axis_buffer[6, 0] == 0 && VL85_key_buffer[6] == 0) LocoButtons.vl85_kontr_bv = 255;//0выкл,255откл.БВ,1-32поз, клав D для откл БВ
            if (VL85_axis_buffer[7, 0] == 0 && VL85_key_buffer[7] == 0) LocoButtons.vl85_kontr_0 = 255;
            if (VL85_axis_buffer[8, 0] == 0 && VL85_key_buffer[8] == 0) LocoButtons.vl85_kontr_1 = 255;
            if (VL85_axis_buffer[9, 0] == 0 && VL85_key_buffer[9] == 0) LocoButtons.vl85_kontr_2 = 255;
            if (VL85_axis_buffer[10, 0] == 0 && VL85_key_buffer[10] == 0) LocoButtons.vl85_kontr_3 = 255;
            if (VL85_axis_buffer[11, 0] == 0 && VL85_key_buffer[11] == 0) LocoButtons.vl85_kontr_4 = 255;
            if (VL85_axis_buffer[12, 0] == 0 && VL85_key_buffer[12] == 0) LocoButtons.vl85_kontr_5 = 255;
            if (VL85_axis_buffer[13, 0] == 0 && VL85_key_buffer[13] == 0) LocoButtons.vl85_kontr_6 = 255;
            if (VL85_axis_buffer[14, 0] == 0 && VL85_key_buffer[14] == 0) LocoButtons.vl85_kontr_7 = 255;
            if (VL85_axis_buffer[15, 0] == 0 && VL85_key_buffer[15] == 0) LocoButtons.vl85_kontr_8 = 255;
            if (VL85_axis_buffer[16, 0] == 0 && VL85_key_buffer[16] == 0) LocoButtons.vl85_kontr_9 = 255;
            if (VL85_axis_buffer[17, 0] == 0 && VL85_key_buffer[17] == 0) LocoButtons.vl85_kontr_10 = 255;
            if (VL85_axis_buffer[18, 0] == 0 && VL85_key_buffer[18] == 0) LocoButtons.vl85_kontr_11 = 255;
            if (VL85_axis_buffer[19, 0] == 0 && VL85_key_buffer[19] == 0) LocoButtons.vl85_kontr_12 = 255;
            if (VL85_axis_buffer[20, 0] == 0 && VL85_key_buffer[20] == 0) LocoButtons.vl85_kontr_13 = 255;
            if (VL85_axis_buffer[21, 0] == 0 && VL85_key_buffer[21] == 0) LocoButtons.vl85_kontr_14 = 255;
            if (VL85_axis_buffer[22, 0] == 0 && VL85_key_buffer[22] == 0) LocoButtons.vl85_kontr_15 = 255;
            if (VL85_axis_buffer[23, 0] == 0 && VL85_key_buffer[23] == 0) LocoButtons.vl85_kontr_16 = 255;
            if (VL85_axis_buffer[24, 0] == 0 && VL85_key_buffer[24] == 0) LocoButtons.vl85_kontr_17 = 255;
            if (VL85_axis_buffer[25, 0] == 0 && VL85_key_buffer[25] == 0) LocoButtons.vl85_kontr_18 = 255;
            if (VL85_axis_buffer[26, 0] == 0 && VL85_key_buffer[26] == 0) LocoButtons.vl85_kontr_19 = 255;
            if (VL85_axis_buffer[27, 0] == 0 && VL85_key_buffer[27] == 0) LocoButtons.vl85_kontr_20 = 255;
            if (VL85_axis_buffer[28, 0] == 0 && VL85_key_buffer[28] == 0) LocoButtons.vl85_kontr_21 = 255;
            if (VL85_axis_buffer[29, 0] == 0 && VL85_key_buffer[29] == 0) LocoButtons.vl85_kontr_22 = 255;
            if (VL85_axis_buffer[30, 0] == 0 && VL85_key_buffer[30] == 0) LocoButtons.vl85_kontr_23 = 255;
            if (VL85_axis_buffer[31, 0] == 0 && VL85_key_buffer[31] == 0) LocoButtons.vl85_kontr_24 = 255;
            if (VL85_axis_buffer[32, 0] == 0 && VL85_key_buffer[32] == 0) LocoButtons.vl85_kontr_25 = 255;
            if (VL85_axis_buffer[33, 0] == 0 && VL85_key_buffer[33] == 0) LocoButtons.vl85_kontr_26 = 255;
            if (VL85_axis_buffer[34, 0] == 0 && VL85_key_buffer[34] == 0) LocoButtons.vl85_kontr_27 = 255;
            if (VL85_axis_buffer[35, 0] == 0 && VL85_key_buffer[35] == 0) LocoButtons.vl85_kontr_28 = 255;
            if (VL85_axis_buffer[36, 0] == 0 && VL85_key_buffer[36] == 0) LocoButtons.vl85_kontr_29 = 255;
            if (VL85_axis_buffer[37, 0] == 0 && VL85_key_buffer[37] == 0) LocoButtons.vl85_kontr_30 = 255;
            if (VL85_axis_buffer[38, 0] == 0 && VL85_key_buffer[38] == 0) LocoButtons.vl85_kontr_31 = 255;
            if (VL85_axis_buffer[39, 0] == 0 && VL85_key_buffer[39] == 0) LocoButtons.vl85_kontr_32 = 255;

            if (VL85_axis_buffer[40, 0] == 0 && VL85_key_buffer[40] == 0) LocoButtons.vl85_kranTM_0 = 255;
            if (VL85_axis_buffer[41, 0] == 0 && VL85_key_buffer[41] == 0) LocoButtons.vl85_kranTM_1 = 255;
            if (VL85_axis_buffer[42, 0] == 0 && VL85_key_buffer[42] == 0) LocoButtons.vl85_tokopr_obshiy_0 = 255;
            if (VL85_axis_buffer[43, 0] == 0 && VL85_key_buffer[43] == 0) LocoButtons.vl85_tokopr_obshiy_1 = 255;
            if (VL85_axis_buffer[44, 0] == 0 && VL85_key_buffer[44] == 0) LocoButtons.vl85_tokopr_per_0 = 255;
            if (VL85_axis_buffer[45, 0] == 0 && VL85_key_buffer[45] == 0) LocoButtons.vl85_tokopr_per_1 = 255;
            if (VL85_axis_buffer[46, 0] == 0 && VL85_key_buffer[46] == 0) LocoButtons.vl85_tokopr_zad_0 = 255;
            if (VL85_axis_buffer[47, 0] == 0 && VL85_key_buffer[47] == 0) LocoButtons.vl85_tokopr_zad_1 = 255;
            if (VL85_axis_buffer[48, 0] == 0 && VL85_key_buffer[48] == 0) LocoButtons.vl85_gv_0 = 255; //ГВ 0-0 1-1
            if (VL85_axis_buffer[49, 0] == 0 && VL85_key_buffer[49] == 0) LocoButtons.vl85_gv_1 = 255;
            if (VL85_axis_buffer[50, 0] == 0 && VL85_key_buffer[50] == 0) LocoButtons.vl85_vosst_gv = 255;//через K
            if (VL85_axis_buffer[51, 0] == 0 && VL85_key_buffer[51] == 0) LocoButtons.vl85_avtoreg_140 = 255;//if (VL85_axis_buffer[222, 0] == 0 && VL85_key_buffer[222] == 0) LocoButtons.0-140
            if (VL85_axis_buffer[52, 0] == 0 && VL85_key_buffer[52] == 0) LocoButtons.vl85_avtoreg_plus = 255;
            if (VL85_axis_buffer[53, 0] == 0 && VL85_key_buffer[53] == 0) LocoButtons.vl85_avtoreg_minus = 255;
            if (VL85_axis_buffer[54, 0] == 0 && VL85_key_buffer[54] == 0) LocoButtons.vl85_komp_0 = 255;//0выкл,1вкл
            if (VL85_axis_buffer[55, 0] == 0 && VL85_key_buffer[55] == 0) LocoButtons.vl85_komp_1 = 255;
            if (VL85_axis_buffer[56, 0] == 0 && VL85_key_buffer[56] == 0) LocoButtons.vl85_vent1_0 = 255;
            if (VL85_axis_buffer[57, 0] == 0 && VL85_key_buffer[57] == 0) LocoButtons.vl85_vent1_1 = 255;
            if (VL85_axis_buffer[58, 0] == 0 && VL85_key_buffer[58] == 0) LocoButtons.vl85_vent2_0 = 255;
            if (VL85_axis_buffer[59, 0] == 0 && VL85_key_buffer[59] == 0) LocoButtons.vl85_vent2_1 = 255;
            if (VL85_axis_buffer[60, 0] == 0 && VL85_key_buffer[60] == 0) LocoButtons.vl85_vent3_0 = 255;
            if (VL85_axis_buffer[61, 0] == 0 && VL85_key_buffer[61] == 0) LocoButtons.vl85_vent3_2 = 255;
            if (VL85_axis_buffer[62, 0] == 0 && VL85_key_buffer[62] == 0) LocoButtons.vl85_vent4_0 = 255;
            if (VL85_axis_buffer[63, 0] == 0 && VL85_key_buffer[63] == 0) LocoButtons.vl85_vent4_1 = 255;
            if (VL85_axis_buffer[64, 0] == 0 && VL85_key_buffer[64] == 0) LocoButtons.vl85_fz_0 = 255;
            if (VL85_axis_buffer[65, 0] == 0 && VL85_key_buffer[65] == 0) LocoButtons.vl85_fz_1 = 255;
            if (VL85_axis_buffer[66, 0] == 0 && VL85_key_buffer[66] == 0) LocoButtons.vl85_svet_cab_0 = 255;//0выкл,1приб,2общ
            if (VL85_axis_buffer[67, 0] == 0 && VL85_key_buffer[67] == 0) LocoButtons.vl85_svet_cab_1 = 255;
            if (VL85_axis_buffer[68, 0] == 0 && VL85_key_buffer[68] == 0) LocoButtons.vl85_svet_cab_2 = 255;
            if (VL85_axis_buffer[69, 0] == 0 && VL85_key_buffer[69] == 0) LocoButtons.vl85_EPK_0 = 255;
            if (VL85_axis_buffer[70, 0] == 0 && VL85_key_buffer[70] == 0) LocoButtons.vl85_EPK_1 = 255;
            if (VL85_axis_buffer[71, 0] == 0 && VL85_key_buffer[71] == 0) LocoButtons.vl85_prozh_0 = 255;//0,1,2
            if (VL85_axis_buffer[72, 0] == 0 && VL85_key_buffer[72] == 0) LocoButtons.vl85_prozh_1 = 255;
            if (VL85_axis_buffer[73, 0] == 0 && VL85_key_buffer[73] == 0) LocoButtons.vl85_prozh_2 = 255;
            if (VL85_axis_buffer[74, 0] == 0 && VL85_key_buffer[74] == 0) LocoButtons.vl85_sign_0 = 255;
            if (VL85_axis_buffer[75, 0] == 0 && VL85_key_buffer[75] == 0) LocoButtons.vl85_sign_1 = 255;
            if (VL85_axis_buffer[76, 0] == 0 && VL85_key_buffer[76] == 0) LocoButtons.vl85_sign1_0 = 255;
            if (VL85_axis_buffer[77, 0] == 0 && VL85_key_buffer[77] == 0) LocoButtons.vl85_sign1_1 = 255;
            if (VL85_axis_buffer[78, 0] == 0 && VL85_key_buffer[78] == 0) LocoButtons.vl85_sign2_0 = 255;
            if (VL85_axis_buffer[79, 0] == 0 && VL85_key_buffer[79] == 0) LocoButtons.vl85_sign2_1 = 255;

            //tep70 35
            if (TEP70_axis_buffer[0, 0] == 0 && TEP70_key_buffer[0] == 0) LocoButtons.tep70_rev_0 = 255;//255нз,0-0,1вп
            if (TEP70_axis_buffer[1, 0] == 0 && TEP70_key_buffer[1] == 0) LocoButtons.tep70_rev_vpered = 255;
            if (TEP70_axis_buffer[2, 0] == 0 && TEP70_key_buffer[2] == 0) LocoButtons.tep70_rev_nazad = 255;

            if (TEP70_axis_buffer[3, 0] == 0 && TEP70_key_buffer[3] == 0) LocoButtons.tep70_kontr_0 = 255;//0-15
            if (TEP70_axis_buffer[4, 0] == 0 && TEP70_key_buffer[4] == 0) LocoButtons.tep70_kontr_1 = 255;
            if (TEP70_axis_buffer[5, 0] == 0 && TEP70_key_buffer[5] == 0) LocoButtons.tep70_kontr_2 = 255;
            if (TEP70_axis_buffer[6, 0] == 0 && TEP70_key_buffer[6] == 0) LocoButtons.tep70_kontr_3 = 255;
            if (TEP70_axis_buffer[7, 0] == 0 && TEP70_key_buffer[7] == 0) LocoButtons.tep70_kontr_4 = 255;
            if (TEP70_axis_buffer[8, 0] == 0 && TEP70_key_buffer[8] == 0) LocoButtons.tep70_kontr_5 = 255;
            if (TEP70_axis_buffer[9, 0] == 0 && TEP70_key_buffer[9] == 0) LocoButtons.tep70_kontr_6 = 255;
            if (TEP70_axis_buffer[10, 0] == 0 && TEP70_key_buffer[10] == 0) LocoButtons.tep70_kontr_7 = 255;
            if (TEP70_axis_buffer[11, 0] == 0 && TEP70_key_buffer[11] == 0) LocoButtons.tep70_kontr_8 = 255;
            if (TEP70_axis_buffer[12, 0] == 0 && TEP70_key_buffer[12] == 0) LocoButtons.tep70_kontr_9 = 255;
            if (TEP70_axis_buffer[13, 0] == 0 && TEP70_key_buffer[13] == 0) LocoButtons.tep70_kontr_10 = 255;
            if (TEP70_axis_buffer[14, 0] == 0 && TEP70_key_buffer[14] == 0) LocoButtons.tep70_kontr_11 = 255;
            if (TEP70_axis_buffer[15, 0] == 0 && TEP70_key_buffer[15] == 0) LocoButtons.tep70_kontr_12 = 255;
            if (TEP70_axis_buffer[16, 0] == 0 && TEP70_key_buffer[16] == 0) LocoButtons.tep70_kontr_13 = 255;
            if (TEP70_axis_buffer[17, 0] == 0 && TEP70_key_buffer[17] == 0) LocoButtons.tep70_kontr_14 = 255;
            if (TEP70_axis_buffer[18, 0] == 0 && TEP70_key_buffer[18] == 0) LocoButtons.tep70_kontr_15 = 255;

            if (TEP70_axis_buffer[19, 0] == 0 && TEP70_key_buffer[19] == 0) LocoButtons.tep70_kranTM_0 = 255;
            if (TEP70_axis_buffer[20, 0] == 0 && TEP70_key_buffer[20] == 0) LocoButtons.tep70_kranTM_1 = 255;
            if (TEP70_axis_buffer[21, 0] == 0 && TEP70_key_buffer[21] == 0) LocoButtons.tep70_nasos_0 = 255;
            if (TEP70_axis_buffer[22, 0] == 0 && TEP70_key_buffer[22] == 0) LocoButtons.tep70_nasos_1 = 255;
            if (TEP70_axis_buffer[23, 0] == 0 && TEP70_key_buffer[23] == 0) LocoButtons.tep70_pusk = 255;//через K
            if (TEP70_axis_buffer[24, 0] == 0 && TEP70_key_buffer[24] == 0) LocoButtons.tep70_upravlenie_0 = 255;
            if (TEP70_axis_buffer[25, 0] == 0 && TEP70_key_buffer[25] == 0) LocoButtons.tep70_upravlenie_1 = 255;
            if (TEP70_axis_buffer[26, 0] == 0 && TEP70_key_buffer[26] == 0) LocoButtons.tep70_svet_cab_0 = 255;//0выкл,1приб,2общ
            if (TEP70_axis_buffer[27, 0] == 0 && TEP70_key_buffer[27] == 0) LocoButtons.tep70_svet_cab_1 = 255;
            if (TEP70_axis_buffer[28, 0] == 0 && TEP70_key_buffer[28] == 0) LocoButtons.tep70_svet_cab_2 = 255;
            if (TEP70_axis_buffer[29, 0] == 0 && TEP70_key_buffer[29] == 0) LocoButtons.tep70_EPK_0 = 255;
            if (TEP70_axis_buffer[30, 0] == 0 && TEP70_key_buffer[30] == 0) LocoButtons.tep70_EPK_1 = 255;
            if (TEP70_axis_buffer[31, 0] == 0 && TEP70_key_buffer[31] == 0) LocoButtons.tep70_EPT_0 = 255;
            if (TEP70_axis_buffer[32, 0] == 0 && TEP70_key_buffer[32] == 0) LocoButtons.tep70_EPT_1 = 255;
            if (TEP70_axis_buffer[33, 0] == 0 && TEP70_key_buffer[33] == 0) LocoButtons.tep70_prozh_0 = 255;//float 0-1.75
            if (TEP70_axis_buffer[34, 0] == 0 && TEP70_key_buffer[34] == 0) LocoButtons.tep70_prozh_1 = 255;
            if (TEP70_axis_buffer[35, 0] == 0 && TEP70_key_buffer[35] == 0) LocoButtons.tep70_prozh_2 = 255;

            //te10u 46
            if (TE10U_axis_buffer[0, 0] == 0 && TE10U_key_buffer[0] == 0) LocoButtons.te10u_rev_0 = 255;//255нз,0-0,1вп
            if (TE10U_axis_buffer[1, 0] == 0 && TE10U_key_buffer[1] == 0) LocoButtons.te10u_rev_vpered = 255;
            if (TE10U_axis_buffer[2, 0] == 0 && TE10U_key_buffer[2] == 0) LocoButtons.te10u_rev_nazad = 255;

            if (TE10U_axis_buffer[3, 0] == 0 && TE10U_key_buffer[3] == 0) LocoButtons.te10u_kontr_0 = 255;//0-15
            if (TE10U_axis_buffer[4, 0] == 0 && TE10U_key_buffer[4] == 0) LocoButtons.te10u_kontr_1 = 255;
            if (TE10U_axis_buffer[5, 0] == 0 && TE10U_key_buffer[5] == 0) LocoButtons.te10u_kontr_2 = 255;
            if (TE10U_axis_buffer[6, 0] == 0 && TE10U_key_buffer[6] == 0) LocoButtons.te10u_kontr_3 = 255;
            if (TE10U_axis_buffer[7, 0] == 0 && TE10U_key_buffer[7] == 0) LocoButtons.te10u_kontr_4 = 255;
            if (TE10U_axis_buffer[8, 0] == 0 && TE10U_key_buffer[8] == 0) LocoButtons.te10u_kontr_5 = 255;
            if (TE10U_axis_buffer[9, 0] == 0 && TE10U_key_buffer[9] == 0) LocoButtons.te10u_kontr_6 = 255;
            if (TE10U_axis_buffer[10, 0] == 0 && TE10U_key_buffer[10] == 0) LocoButtons.te10u_kontr_7 = 255;
            if (TE10U_axis_buffer[11, 0] == 0 && TE10U_key_buffer[11] == 0) LocoButtons.te10u_kontr_8 = 255;
            if (TE10U_axis_buffer[12, 0] == 0 && TE10U_key_buffer[12] == 0) LocoButtons.te10u_kontr_9 = 255;
            if (TE10U_axis_buffer[13, 0] == 0 && TE10U_key_buffer[13] == 0) LocoButtons.te10u_kontr_10 = 255;
            if (TE10U_axis_buffer[14, 0] == 0 && TE10U_key_buffer[14] == 0) LocoButtons.te10u_kontr_11 = 255;
            if (TE10U_axis_buffer[15, 0] == 0 && TE10U_key_buffer[15] == 0) LocoButtons.te10u_kontr_12 = 255;
            if (TE10U_axis_buffer[16, 0] == 0 && TE10U_key_buffer[16] == 0) LocoButtons.te10u_kontr_13 = 255;
            if (TE10U_axis_buffer[17, 0] == 0 && TE10U_key_buffer[17] == 0) LocoButtons.te10u_kontr_14 = 255;
            if (TE10U_axis_buffer[18, 0] == 0 && TE10U_key_buffer[18] == 0) LocoButtons.te10u_kontr_15 = 255;

            if (TE10U_axis_buffer[19, 0] == 0 && TE10U_key_buffer[19] == 0) LocoButtons.te10u_kranTM_0 = 255;
            if (TE10U_axis_buffer[20, 0] == 0 && TE10U_key_buffer[20] == 0) LocoButtons.te10u_kranTM_1 = 255;
            if (TE10U_axis_buffer[21, 0] == 0 && TE10U_key_buffer[21] == 0) LocoButtons.te10u_nasos1_0 = 255;
            if (TE10U_axis_buffer[22, 0] == 0 && TE10U_key_buffer[22] == 0) LocoButtons.te10u_nasos1_1 = 255;
            if (TE10U_axis_buffer[23, 0] == 0 && TE10U_key_buffer[23] == 0) LocoButtons.te10u_nasos2_0 = 255;
            if (TE10U_axis_buffer[24, 0] == 0 && TE10U_key_buffer[24] == 0) LocoButtons.te10u_nasos2_1 = 255;
            if (TE10U_axis_buffer[25, 0] == 0 && TE10U_key_buffer[25] == 0) LocoButtons.te10u_pusk1 = 255;//через J
            if (TE10U_axis_buffer[26, 0] == 0 && TE10U_key_buffer[26] == 0) LocoButtons.te10u_pusk2 = 255;//через K
            if (TE10U_axis_buffer[27, 0] == 0 && TE10U_key_buffer[27] == 0) LocoButtons.te10u_upravlenie_0 = 255;
            if (TE10U_axis_buffer[28, 0] == 0 && TE10U_key_buffer[28] == 0) LocoButtons.te10u_upravlenie_1 = 255;
            if (TE10U_axis_buffer[29, 0] == 0 && TE10U_key_buffer[29] == 0) LocoButtons.te10u_dvizhenie_0 = 255;
            if (TE10U_axis_buffer[30, 0] == 0 && TE10U_key_buffer[30] == 0) LocoButtons.te10u_dvizhenie_1 = 255;
            if (TE10U_axis_buffer[31, 0] == 0 && TE10U_key_buffer[31] == 0) LocoButtons.te10u_perehody_0 = 255;
            if (TE10U_axis_buffer[32, 0] == 0 && TE10U_key_buffer[32] == 0) LocoButtons.te10u_perehody_1 = 255;
            if (TE10U_axis_buffer[33, 0] == 0 && TE10U_key_buffer[33] == 0) LocoButtons.te10u_holost1_0 = 255;
            if (TE10U_axis_buffer[34, 0] == 0 && TE10U_key_buffer[34] == 0) LocoButtons.te10u_holost1_1 = 255;
            if (TE10U_axis_buffer[35, 0] == 0 && TE10U_key_buffer[35] == 0) LocoButtons.te10u_holost2_0 = 255;
            if (TE10U_axis_buffer[36, 0] == 0 && TE10U_key_buffer[36] == 0) LocoButtons.te10u_holost2_1 = 255;
            if (TE10U_axis_buffer[37, 0] == 0 && TE10U_key_buffer[37] == 0) LocoButtons.te10u_svet_cab_0 = 255;//0выкл,1приб,2общ
            if (TE10U_axis_buffer[38, 0] == 0 && TE10U_key_buffer[38] == 0) LocoButtons.te10u_svet_cab_1 = 255;
            if (TE10U_axis_buffer[39, 0] == 0 && TE10U_key_buffer[39] == 0) LocoButtons.te10u_svet_cab_2 = 255;
            if (TE10U_axis_buffer[40, 0] == 0 && TE10U_key_buffer[40] == 0) LocoButtons.te10u_EPK_0 = 255;
            if (TE10U_axis_buffer[41, 0] == 0 && TE10U_key_buffer[41] == 0) LocoButtons.te10u_EPK_1 = 255;
            if (TE10U_axis_buffer[42, 0] == 0 && TE10U_key_buffer[42] == 0) LocoButtons.te10u_EPT_0 = 255;
            if (TE10U_axis_buffer[43, 0] == 0 && TE10U_key_buffer[43] == 0) LocoButtons.te10u_EPT_1 = 255;
            if (TE10U_axis_buffer[44, 0] == 0 && TE10U_key_buffer[44] == 0) LocoButtons.te10u_prozh_0 = 255;//float 0-1.75
            if (TE10U_axis_buffer[45, 0] == 0 && TE10U_key_buffer[45] == 0) LocoButtons.te10u_prozh_1 = 255;
            if (TE10U_axis_buffer[46, 0] == 0 && TE10U_key_buffer[46] == 0) LocoButtons.te10u_prozh_2 = 255;

            //m62 35
            if (M62_axis_buffer[0, 0] == 0 && M62_key_buffer[0] == 0) LocoButtons.m62_rev_0 = 255;//255нз,0-0,1вп
            if (M62_axis_buffer[1, 0] == 0 && M62_key_buffer[1] == 0) LocoButtons.m62_rev_vpered = 255;
            if (M62_axis_buffer[2, 0] == 0 && M62_key_buffer[2] == 0) LocoButtons.m62_rev_nazad = 255;

            if (M62_axis_buffer[3, 0] == 0 && M62_key_buffer[3] == 0) LocoButtons.m62_kontr_0 = 255;//0-15
            if (M62_axis_buffer[4, 0] == 0 && M62_key_buffer[4] == 0) LocoButtons.m62_kontr_1 = 255;
            if (M62_axis_buffer[5, 0] == 0 && M62_key_buffer[5] == 0) LocoButtons.m62_kontr_2 = 255;
            if (M62_axis_buffer[6, 0] == 0 && M62_key_buffer[6] == 0) LocoButtons.m62_kontr_3 = 255;
            if (M62_axis_buffer[7, 0] == 0 && M62_key_buffer[7] == 0) LocoButtons.m62_kontr_4 = 255;
            if (M62_axis_buffer[8, 0] == 0 && M62_key_buffer[8] == 0) LocoButtons.m62_kontr_5 = 255;
            if (M62_axis_buffer[9, 0] == 0 && M62_key_buffer[9] == 0) LocoButtons.m62_kontr_6 = 255;
            if (M62_axis_buffer[10, 0] == 0 && M62_key_buffer[10] == 0) LocoButtons.m62_kontr_7 = 255;
            if (M62_axis_buffer[11, 0] == 0 && M62_key_buffer[11] == 0) LocoButtons.m62_kontr_8 = 255;
            if (M62_axis_buffer[12, 0] == 0 && M62_key_buffer[12] == 0) LocoButtons.m62_kontr_9 = 255;
            if (M62_axis_buffer[13, 0] == 0 && M62_key_buffer[13] == 0) LocoButtons.m62_kontr_10 = 255;
            if (M62_axis_buffer[14, 0] == 0 && M62_key_buffer[14] == 0) LocoButtons.m62_kontr_11 = 255;
            if (M62_axis_buffer[15, 0] == 0 && M62_key_buffer[15] == 0) LocoButtons.m62_kontr_12 = 255;
            if (M62_axis_buffer[16, 0] == 0 && M62_key_buffer[16] == 0) LocoButtons.m62_kontr_13 = 255;
            if (M62_axis_buffer[17, 0] == 0 && M62_key_buffer[17] == 0) LocoButtons.m62_kontr_14 = 255;
            if (M62_axis_buffer[18, 0] == 0 && M62_key_buffer[18] == 0) LocoButtons.m62_kontr_15 = 255;

            if (M62_axis_buffer[19, 0] == 0 && M62_key_buffer[19] == 0) LocoButtons.m62_kranTM_0 = 255;
            if (M62_axis_buffer[20, 0] == 0 && M62_key_buffer[20] == 0) LocoButtons.m62_kranTM_1 = 255;
            if (M62_axis_buffer[21, 0] == 0 && M62_key_buffer[21] == 0) LocoButtons.m62_nasos_0 = 255;
            if (M62_axis_buffer[22, 0] == 0 && M62_key_buffer[22] == 0) LocoButtons.m62_nasos_1 = 255;
            if (M62_axis_buffer[23, 0] == 0 && M62_key_buffer[23] == 0) LocoButtons.m62_pusk = 255;//через K
            if (M62_axis_buffer[24, 0] == 0 && M62_key_buffer[24] == 0) LocoButtons.m62_upravlenie_0 = 255;
            if (M62_axis_buffer[25, 0] == 0 && M62_key_buffer[25] == 0) LocoButtons.m62_upravlenie_1 = 255;
            if (M62_axis_buffer[26, 0] == 0 && M62_key_buffer[26] == 0) LocoButtons.m62_perehody_0 = 255;
            if (M62_axis_buffer[27, 0] == 0 && M62_key_buffer[27] == 0) LocoButtons.m62_perehody_1 = 255;
            if (M62_axis_buffer[28, 0] == 0 && M62_key_buffer[28] == 0) LocoButtons.m62_svet_cab_0 = 255;//0выкл,1приб,2общ
            if (M62_axis_buffer[29, 0] == 0 && M62_key_buffer[29] == 0) LocoButtons.m62_svet_cab_1 = 255;
            if (M62_axis_buffer[30, 0] == 0 && M62_key_buffer[30] == 0) LocoButtons.m62_svet_cab_2 = 255;
            if (M62_axis_buffer[31, 0] == 0 && M62_key_buffer[31] == 0) LocoButtons.m62_EPK_0 = 255;
            if (M62_axis_buffer[32, 0] == 0 && M62_key_buffer[32] == 0) LocoButtons.m62_EPK_1 = 255;
            if (M62_axis_buffer[33, 0] == 0 && M62_key_buffer[33] == 0) LocoButtons.m62_prozh_0 = 255;//float 0-1.75
            if (M62_axis_buffer[34, 0] == 0 && M62_key_buffer[34] == 0) LocoButtons.m62_prozh_1 = 255;
            if (M62_axis_buffer[35, 0] == 0 && M62_key_buffer[35] == 0) LocoButtons.m62_prozh_2 = 255;

            //ed4m 29
            if (ED4M_axis_buffer[01, 0] == 0 && ED4M_key_buffer[0] == 0) LocoButtons.ed4m_rev_0 = 255;//0-0,1вп,255нз
            if (ED4M_axis_buffer[1, 0] == 0 && ED4M_key_buffer[1] == 0) LocoButtons.ed4m_rev_vpered = 255;
            if (ED4M_axis_buffer[2, 0] == 0 && ED4M_key_buffer[2] == 0) LocoButtons.ed4m_rev_nazad = 255;

            if (ED4M_axis_buffer[3, 0] == 0 && ED4M_key_buffer[3] == 0) LocoButtons.ed4m_kontr_0 = 255; //0-0,1-2ход,255-251тормоз
            if (ED4M_axis_buffer[4, 0] == 0 && ED4M_key_buffer[4] == 0) LocoButtons.ed4m_kontr_h1 = 255;
            if (ED4M_axis_buffer[5, 0] == 0 && ED4M_key_buffer[5] == 0) LocoButtons.ed4m_kontr_h2 = 255;
            if (ED9M_axis_buffer[6, 0] == 0 && ED9M_key_buffer[6] == 0) LocoButtons.ed4m_kontr_h3 = 255;
            if (ED9M_axis_buffer[7, 0] == 0 && ED9M_key_buffer[7] == 0) LocoButtons.ed4m_kontr_h4 = 255;
            if (ED9M_axis_buffer[8, 0] == 0 && ED9M_key_buffer[8] == 0) LocoButtons.ed4m_kontr_h5 = 255;
            if (ED4M_axis_buffer[9, 0] == 0 && ED4M_key_buffer[9] == 0) LocoButtons.ed4m_kontr_t1 = 255;
            if (ED4M_axis_buffer[10, 0] == 0 && ED4M_key_buffer[10] == 0) LocoButtons.ed4m_kontr_t2 = 255;
            if (ED4M_axis_buffer[11, 0] == 0 && ED4M_key_buffer[11] == 0) LocoButtons.ed4m_kontr_t3 = 255;
            if (ED4M_axis_buffer[12, 0] == 0 && ED4M_key_buffer[12] == 0) LocoButtons.ed4m_kontr_t4 = 255;
            if (ED4M_axis_buffer[13, 0] == 0 && ED4M_key_buffer[13] == 0) LocoButtons.ed4m_kontr_t5 = 255;
            if (ED4M_axis_buffer[14, 0] == 0 && ED4M_key_buffer[14] == 0) LocoButtons.ed4m_kranTM_0 = 255;
            if (ED4M_axis_buffer[15, 0] == 0 && ED4M_key_buffer[15] == 0) LocoButtons.ed4m_kranTM_1 = 255;
            if (ED4M_axis_buffer[16, 0] == 0 && ED4M_key_buffer[16] == 0) LocoButtons.ed4m_tokopr_0 = 255;
            if (ED4M_axis_buffer[17, 0] == 0 && ED4M_key_buffer[17] == 0) LocoButtons.ed4m_tokopr_1 = 255;
            if (ED4M_axis_buffer[18, 0] == 0 && ED4M_key_buffer[18] == 0) LocoButtons.ed4m_bv_0 = 255; //0-0 1-1
            if (ED4M_axis_buffer[19, 0] == 0 && ED4M_key_buffer[19] == 0) LocoButtons.ed4m_bv_1 = 255;
            if (ED4M_axis_buffer[20, 0] == 0 && ED4M_key_buffer[20] == 0) LocoButtons.ed4m_svet_cab_0 = 255;//0-1
            if (ED4M_axis_buffer[21, 0] == 0 && ED4M_key_buffer[21] == 0) LocoButtons.ed4m_svet_cab_1 = 255;
            if (ED4M_axis_buffer[22, 0] == 0 && ED4M_key_buffer[22] == 0) LocoButtons.ed4m_EPK_0 = 255;
            if (ED4M_axis_buffer[23, 0] == 0 && ED4M_key_buffer[23] == 0) LocoButtons.ed4m_EPK_1 = 255;
            if (ED4M_axis_buffer[24, 0] == 0 && ED4M_key_buffer[24] == 0) LocoButtons.ed4m_EPT_0 = 255;
            if (ED4M_axis_buffer[25, 0] == 0 && ED4M_key_buffer[25] == 0) LocoButtons.ed4m_EPT_1 = 255;
            if (ED4M_axis_buffer[26, 0] == 0 && ED4M_key_buffer[26] == 0) LocoButtons.ed4m_dvery_lev_0 = 255;
            if (ED4M_axis_buffer[27, 0] == 0 && ED4M_key_buffer[27] == 0) LocoButtons.ed4m_dvery_lev_1 = 255;
            if (ED4M_axis_buffer[28, 0] == 0 && ED4M_key_buffer[28] == 0) LocoButtons.ed4m_dvery_pr_0 = 255;
            if (ED4M_axis_buffer[29, 0] == 0 && ED4M_key_buffer[29] == 0) LocoButtons.ed4m_dvery_pr_1 = 255;
            if (ED4M_axis_buffer[30, 0] == 0 && ED4M_key_buffer[30] == 0) LocoButtons.ed4m_prozh_0 = 255; //float 0-1,625
            if (ED4M_axis_buffer[31, 0] == 0 && ED4M_key_buffer[31] == 0) LocoButtons.ed4m_prozh_1 = 255;
            if (ED4M_axis_buffer[32, 0] == 0 && ED4M_key_buffer[32] == 0) LocoButtons.ed4m_prozh_2 = 255;

            //ed9m 23
            if (ED9M_axis_buffer[0, 0] == 0 && ED9M_key_buffer[0] == 0) LocoButtons.ed9m_rev_0 = 255;//0-0,1вп,255нз
            if (ED9M_axis_buffer[1, 0] == 0 && ED9M_key_buffer[1] == 0) LocoButtons.ed9m_rev_vpered = 255;
            if (ED9M_axis_buffer[2, 0] == 0 && ED9M_key_buffer[2] == 0) LocoButtons.ed9m_rev_nazad = 255;

            if (ED9M_axis_buffer[3, 0] == 0 && ED9M_key_buffer[3] == 0) LocoButtons.ed9m_kontr_0 = 255; //0-0,1-5ход,255-251тормоз
            if (ED9M_axis_buffer[4, 0] == 0 && ED9M_key_buffer[4] == 0) LocoButtons.ed9m_kontr_h1 = 255;
            if (ED9M_axis_buffer[5, 0] == 0 && ED9M_key_buffer[5] == 0) LocoButtons.ed9m_kontr_h2 = 255;
            if (ED9M_axis_buffer[6, 0] == 0 && ED9M_key_buffer[6] == 0) LocoButtons.ed9m_kontr_t1 = 255;
            if (ED9M_axis_buffer[7, 0] == 0 && ED9M_key_buffer[7] == 0) LocoButtons.ed9m_kontr_t2 = 255;
            if (ED9M_axis_buffer[8, 0] == 0 && ED9M_key_buffer[8] == 0) LocoButtons.ed9m_kontr_t3 = 255;
            if (ED9M_axis_buffer[9, 0] == 0 && ED9M_key_buffer[9] == 0) LocoButtons.ed9m_kontr_t4 = 255;
            if (ED9M_axis_buffer[10, 0] == 0 && ED9M_key_buffer[10] == 0) LocoButtons.ed9m_kontr_t5 = 255;
            if (ED9M_axis_buffer[11, 0] == 0 && ED9M_key_buffer[11] == 0) LocoButtons.ed9m_kranTM_0 = 255;
            if (ED9M_axis_buffer[12, 0] == 0 && ED9M_key_buffer[12] == 0) LocoButtons.ed9m_kranTM_1 = 255;
            if (ED9M_axis_buffer[13, 0] == 0 && ED9M_key_buffer[13] == 0) LocoButtons.ed9m_tokopr_0 = 255;
            if (ED9M_axis_buffer[14, 0] == 0 && ED9M_key_buffer[14] == 0) LocoButtons.ed9m_tokopr_1 = 255;
            if (ED9M_axis_buffer[15, 0] == 0 && ED9M_key_buffer[15] == 0) LocoButtons.ed9m_bv_0 = 255; //0-0 1-1
            if (ED9M_axis_buffer[16, 0] == 0 && ED9M_key_buffer[16] == 0) LocoButtons.ed9m_bv_1 = 255;
            if (ED9M_axis_buffer[17, 0] == 0 && ED9M_key_buffer[17] == 0) LocoButtons.ed9m_svet_cab_0 = 255;//0-1
            if (ED9M_axis_buffer[18, 0] == 0 && ED9M_key_buffer[18] == 0) LocoButtons.ed9m_svet_cab_1 = 255;
            if (ED9M_axis_buffer[19, 0] == 0 && ED9M_key_buffer[19] == 0) LocoButtons.ed9m_EPK_0 = 255;
            if (ED9M_axis_buffer[20, 0] == 0 && ED9M_key_buffer[20] == 0) LocoButtons.ed9m_EPK_1 = 255;
            if (ED9M_axis_buffer[21, 0] == 0 && ED9M_key_buffer[21] == 0) LocoButtons.ed9m_EPT_0 = 255;
            if (ED9M_axis_buffer[22, 0] == 0 && ED9M_key_buffer[22] == 0) LocoButtons.ed9m_EPT_1 = 255;
            if (ED9M_axis_buffer[23, 0] == 0 && ED9M_key_buffer[23] == 0) LocoButtons.ed9m_dvery_lev_0 = 255;
            if (ED9M_axis_buffer[24, 0] == 0 && ED9M_key_buffer[24] == 0) LocoButtons.ed9m_dvery_lev_1 = 255;
            if (ED9M_axis_buffer[25, 0] == 0 && ED9M_key_buffer[25] == 0) LocoButtons.ed9m_dvery_pr_0 = 255;
            if (ED9M_axis_buffer[26, 0] == 0 && ED9M_key_buffer[26] == 0) LocoButtons.ed9m_dvery_pr_1 = 255;
            if (ED9M_axis_buffer[27, 0] == 0 && ED9M_key_buffer[27] == 0) LocoButtons.ed9m_prozh_0 = 255; //float 0-1,625
            if (ED9M_axis_buffer[28, 0] == 0 && ED9M_key_buffer[28] == 0) LocoButtons.ed9m_prozh_1 = 255;
            if (ED9M_axis_buffer[29, 0] == 0 && ED9M_key_buffer[29] == 0) LocoButtons.ed9m_prozh_2 = 255;

            //tem18 31
            if (tem18_axis_buffer[0, 0] == 0 && tem18_key_buffer[0] == 0) LocoButtons.tem18_rev_0 = 255;//255нз,0-0,1вп
            if (tem18_axis_buffer[1, 0] == 0 && tem18_key_buffer[1] == 0) LocoButtons.tem18_rev_vpered = 255;
            if (tem18_axis_buffer[2, 0] == 0 && tem18_key_buffer[2] == 0) LocoButtons.tem18_rev_nazad = 255;

            if (tem18_axis_buffer[3, 0] == 0 && tem18_key_buffer[3] == 0) LocoButtons.tem18_kontr_0 = 255;//0-15
            if (tem18_axis_buffer[4, 0] == 0 && tem18_key_buffer[4] == 0) LocoButtons.tem18_kontr_1 = 255;
            if (tem18_axis_buffer[5, 0] == 0 && tem18_key_buffer[5] == 0) LocoButtons.tem18_kontr_2 = 255;
            if (tem18_axis_buffer[6, 0] == 0 && tem18_key_buffer[6] == 0) LocoButtons.tem18_kontr_3 = 255;
            if (tem18_axis_buffer[7, 0] == 0 && tem18_key_buffer[7] == 0) LocoButtons.tem18_kontr_4 = 255;
            if (tem18_axis_buffer[8, 0] == 0 && tem18_key_buffer[8] == 0) LocoButtons.tem18_kontr_5 = 255;
            if (tem18_axis_buffer[9, 0] == 0 && tem18_key_buffer[9] == 0) LocoButtons.tem18_kontr_6 = 255;
            if (tem18_axis_buffer[10, 0] == 0 && tem18_key_buffer[10] == 0) LocoButtons.tem18_kontr_7 = 255;
            if (tem18_axis_buffer[11, 0] == 0 && tem18_key_buffer[11] == 0) LocoButtons.tem18_kontr_8 = 255;

            if (tem18_axis_buffer[12, 0] == 0 && tem18_key_buffer[12] == 0) LocoButtons.tem18_kranTM_0 = 255;
            if (tem18_axis_buffer[13, 0] == 0 && tem18_key_buffer[13] == 0) LocoButtons.tem18_kranTM_1 = 255;
            if (tem18_axis_buffer[14, 0] == 0 && tem18_key_buffer[14] == 0) LocoButtons.tem18_nasos_maslo0 = 255;
            if (tem18_axis_buffer[15, 0] == 0 && tem18_key_buffer[15] == 0) LocoButtons.tem18_nasos_maslo1 = 255;
            if (tem18_axis_buffer[16, 0] == 0 && tem18_key_buffer[16] == 0) LocoButtons.tem18_nasos_toplivo0 = 255;
            if (tem18_axis_buffer[17, 0] == 0 && tem18_key_buffer[17] == 0) LocoButtons.tem18_nasos_toplivo1 = 255;
            if (tem18_axis_buffer[18, 0] == 0 && tem18_key_buffer[18] == 0) LocoButtons.tem18_pusk = 255;//через K
            if (tem18_axis_buffer[19, 0] == 0 && tem18_key_buffer[19] == 0) LocoButtons.tem18_upravlenie_0 = 255;
            if (tem18_axis_buffer[20, 0] == 0 && tem18_key_buffer[20] == 0) LocoButtons.tem18_upravlenie_1 = 255;
            if (tem18_axis_buffer[21, 0] == 0 && tem18_key_buffer[21] == 0) LocoButtons.tem18_perehody_0 = 255;
            if (tem18_axis_buffer[22, 0] == 0 && tem18_key_buffer[22] == 0) LocoButtons.tem18_perehody_1 = 255;
            if (tem18_axis_buffer[23, 0] == 0 && tem18_key_buffer[23] == 0) LocoButtons.tem18_svet_cab_0 = 255;//0выкл
            if (tem18_axis_buffer[24, 0] == 0 && tem18_key_buffer[24] == 0) LocoButtons.tem18_svet_cab_1 = 255;
            if (tem18_axis_buffer[25, 0] == 0 && tem18_key_buffer[25] == 0) LocoButtons.tem18_svet_prib_0 = 255;//приб
            if (tem18_axis_buffer[26, 0] == 0 && tem18_key_buffer[26] == 0) LocoButtons.tem18_svet_prib_1 = 255;
            if (tem18_axis_buffer[27, 0] == 0 && tem18_key_buffer[27] == 0) LocoButtons.tem18_EPK_0 = 255;
            if (tem18_axis_buffer[28, 0] == 0 && tem18_key_buffer[28] == 0) LocoButtons.tem18_EPK_1 = 255;
            if (tem18_axis_buffer[29, 0] == 0 && tem18_key_buffer[29] == 0) LocoButtons.tem18_prozh_0 = 255;//float 0-1.75
            if (tem18_axis_buffer[30, 0] == 0 && tem18_key_buffer[30] == 0) LocoButtons.tem18_prozh_1 = 255;
            if (tem18_axis_buffer[31, 0] == 0 && tem18_key_buffer[31] == 0) LocoButtons.tem18_prozh_2 = 255;
        }

        //--------------------------------------------------------------------
        //Обявления переменных
        //--------------------------------------------------------------------
        public static int i_time_begin = 0;//время до запуска
        public static Int32 i_skor_COM = 0;//скорость COM порта
        public static string i_COM;
        public static int i_port_COM_open = 0;
        public static int i_delay_send = 1;//задержка приема данных
        public static int i_delay_send_HID = 1;//задержка передачи данных
        public static uint i_delay_send_key = 0;//задержка эмуляции клавиши
        public static uint i_dvery_random = 2;
        public static uint i_dvery_sec = 0;
        public static uint i_dvery_sec_flag = 1;
        public static uint i_dvery_random_current = 0;
        public static uint i_dvery_control = 0;
        public static uint i_dvery_control_off_settings = 0;
        public static uint i_dvery_close_flag = 0;
        public static uint i_lampa_LK_sec_flag = 0;
        public static string[] buffer_COM_ports;
        public static int i_step_steper_motor = 0;
        public static int i_bdit = 1;
        public static byte i_bdit_out = 1;
        public static int i_skor_tek2 = 0;               //СКОРОСТЬ ТЕКУЩАЯ
        public static int i_skor_dop = 0;
        public static UInt16 i_skor_tek = 0;
        public static int i_rasstoyanie_do_tseli = 0;


        public static byte i_skor_dop_out = 1;
        public static string i_joy_name="";
        public static string i_zdsimloco_name = "";
        public static int i_loco_find = 0;
        public static string[] b_current_process_name;
        public static int i_priem_peredacha = 3;
        public static int i_sound_peredacha = 0;//обработка звуков при передаче - 0 - нет, 1 - да
        public static string[] sb_settings;
        public static string i_path_zdsimscanner = "";
        public byte[] b_joystick_axis_numbers_update = new byte[200];
        public static int i_shum_joystick = 0;//шум джойстика
        public static int[] joystick_axis_buffer = new int[64];//буфер осей
        public static byte[] joystick_buttons_buffer;//буфер кнопок

        //--------------------------------------------------------------------
        //Объявляем массивы для буферов настроек кнопок
        //--------------------------------------------------------------------
        public static int[] Controls_key_buffer = new int[34];
        public static int[] Neshtatki_key_buffer = new int[100];
        public static int[] ES5K_key_buffer = new int[109];
        public static int[] EP1M_key_buffer = new int[112];
        public static int[] CHS2K_key_buffer = new int[32];
        public static int[] CHS4_key_buffer = new int[55];
        public static int[] CHS4KVR_key_buffer = new int[55];
        public static int[] CHS4T_key_buffer = new int[54];
        public static int[] CHS7_key_buffer = new int[46];
        public static int[] CHS8_key_buffer = new int[63];
        public static int[] VL11M_key_buffer = new int[83];
        public static int[] VL82M_key_buffer = new int[83];
        public static int[] VL80T_key_buffer = new int[49];
        public static int[] VL85_key_buffer = new int[80];
        public static int[] TEP70_key_buffer = new int[36];
        public static int[] TE10U_key_buffer = new int[47];
        public static int[] M62_key_buffer = new int[36];
        public static int[] ED4M_key_buffer = new int[33];
        public static int[] ED9M_key_buffer = new int[30];
        public static int[] tem18_key_buffer = new int[32];

        //--------------------------------------------------------------------
        //Объявляем массивы для буферов настроек точек осей
        //--------------------------------------------------------------------
        public static int[,] Controls_axis_buffer = new int[34,2];
        public static int[,] Neshtatki_axis_buffer = new int[100, 2];
        public static int[,] ES5K_axis_buffer = new int[109,2];
        public static int[,] EP1M_axis_buffer = new int[112,2];
        public static int[,] CHS2K_axis_buffer = new int[32,2];
        public static int[,] CHS4_axis_buffer = new int[55,2];
        public static int[,] CHS4KVR_axis_buffer = new int[55,2];
        public static int[,] CHS4T_axis_buffer = new int[54,2];
        public static int[,] CHS7_axis_buffer = new int[46,2];
        public static int[,] CHS8_axis_buffer = new int[63,2];
        public static int[,] VL11M_axis_buffer = new int[83,2];
        public static int[,] VL82M_axis_buffer = new int[83,2];
        public static int[,] VL80T_axis_buffer = new int[49,2];
        public static int[,] VL85_axis_buffer = new int[80,2];
        public static int[,] TEP70_axis_buffer = new int[36,2];
        public static int[,] TE10U_axis_buffer = new int[47,2];
        public static int[,] M62_axis_buffer = new int[36,2];
        public static int[,] ED4M_axis_buffer = new int[33,2];
        public static int[,] ED9M_axis_buffer = new int[30,2];
        public static int[,] tem18_axis_buffer = new int[32, 2];

        //--------------------------------------------------------------------
        //Объявляем массивы для буферов путей звуков
        //--------------------------------------------------------------------
        public static string[] controls_wav_path_key_buffer = new string[34];
        public static string[] neshtatki_wav_path_key_buffer = new string[100];
        public static string[] es5k_wav_path_key_buffer = new string[109];
        public static string[] ep1m_wav_path_key_buffer = new string[112];
        public static string[] chs2k_wav_path_key_buffer = new string[32];
        public static string[] chs4_wav_path_key_buffer = new string[55];
        public static string[] chs4kvr_wav_path_key_buffer = new string[55];
        public static string[] chs4t_wav_path_key_buffer = new string[54];
        public static string[] chs7_wav_path_key_buffer = new string[46];
        public static string[] chs8_wav_path_key_buffer = new string[63];
        public static string[] vl11_wav_path_key_buffer = new string[83];
        public static string[] vl82_wav_path_key_buffer = new string[83];
        public static string[] vl80t_wav_path_key_buffer = new string[49];
        public static string[] vl85_wav_path_key_buffer = new string[80];
        public static string[] tep70_wav_path_key_buffer = new string[36];
        public static string[] te10u_wav_path_key_buffer = new string[47];
        public static string[] m62_wav_path_key_buffer = new string[36];
        public static string[] ed4m_wav_path_key_buffer = new string[33];
        public static string[] ed9m_wav_path_key_buffer = new string[30];
        public static string[] tem18_wav_path_key_buffer = new string[32];

        //--------------------------------------------------------------------
        //Объявляем массивы для буферов для точек осей
        //--------------------------------------------------------------------
        public static int[] joystick_ARx_point_buffer;//1
        public static int[] joystick_ARy_point_buffer;//2
        public static int[] joystick_ARz_point_buffer;//3
        public static int[] joystick_AX_point_buffer;//4
        public static int[] joystick_AY_point_buffer;//5
        public static int[] joystick_AZ_point_buffer;//6
        public static int[] joystick_FRx_point_buffer;//7
        public static int[] joystick_FRy_point_buffer;//8
        public static int[] joystick_FRz_point_buffer;//9
        public static int[] joystick_FX_point_buffer;//10
        public static int[] joystick_FY_point_buffer;//11
        public static int[] joystick_FZ_point_buffer;//12
        public static int[] joystick_Rx_point_buffer;//13
        public static int[] joystick_Ry_point_buffer;//14
        public static int[] joystick_Rz_point_buffer;//15
        public static int[] joystick_VRx_point_buffer;//16
        public static int[] joystick_VRy_point_buffer;//17
        public static int[] joystick_VRz_point_buffer;//18
        public static int[] joystick_VX_point_buffer;//19
        public static int[] joystick_VY_point_buffer;//20
        public static int[] joystick_VZ_point_buffer;//21
        public static int[] joystick_X_point_buffer;//22
        public static int[] joystick_Y_point_buffer;//23
        public static int[] joystick_Z_point_buffer;//24
        public static int[] joystick_POV_point_buffer;//25 буфер для точек POV 
        public static int[] joystick_Slider_point_buffer;//26 буфера для точек др. осей, газ и пр.
        public static int[] joystick_ASlider_point_buffer;//27
        public static int[] joystick_FSlider_point_buffer;//28
        public static int[] joystick_VSlider_point_buffer;//29

        //--------------------------------------------------------------------
        //Ищем окно для отправки клавиши
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //Фокус на окно
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        SerialPort port;
        Device device;

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        public Form1()
        {  
            InitializeComponent();
            this.Text = "zdsimScanner 55.008 v9.0.0";
        }

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        private void OnTimedEvent()
        {
            console1.Clear();
            console1.ForeColor = Color.Yellow;
            console1.AppendText("осталось : " + (i_time_begin - 1) + "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
            i_time_begin--;
        }
        public int i_array = 0;

        //------------------------------------------------------------------------------------
        //Сохранение буферов кнопок, осей и точек
        //------------------------------------------------------------------------------------
        public static void SaveBuffersSettings()
        {
            //сохраняем кнопки zdsim
            Properties.Settings.Default.controls_buffer_key_settings.Clear();
            for (int i = 0; i < Controls_key_buffer.Length; i++)
            {
                Properties.Settings.Default.controls_buffer_key_settings.Add(Convert.ToString(Controls_key_buffer[i]));
            }

            Properties.Settings.Default.neshtatki_buffer_key_settings.Clear();
            for (int i = 0; i < Neshtatki_key_buffer.Length; i++)
            {
                Properties.Settings.Default.neshtatki_buffer_key_settings.Add(Convert.ToString(Neshtatki_key_buffer[i]));
            }

            Properties.Settings.Default.es5k_buffer_key_settings.Clear();
            for (int i = 0; i < ES5K_key_buffer.Length; i++)
            {
                Properties.Settings.Default.es5k_buffer_key_settings.Add(Convert.ToString(ES5K_key_buffer[i]));
            }

            Properties.Settings.Default.ep1m_buffer_key_settings.Clear();
            for (int i = 0; i < EP1M_key_buffer.Length; i++)
            {
                Properties.Settings.Default.ep1m_buffer_key_settings.Add(Convert.ToString(EP1M_key_buffer[i]));
            }

            Properties.Settings.Default.chs2k_buffer_key_settings.Clear();
            for (int i = 0; i < CHS2K_key_buffer.Length; i++)
            {
                Properties.Settings.Default.chs2k_buffer_key_settings.Add(Convert.ToString(CHS2K_key_buffer[i]));
            }

            Properties.Settings.Default.chs4_buffer_key_settings.Clear();
            for (int i = 0; i < CHS4_key_buffer.Length; i++)
            {
                Properties.Settings.Default.chs4_buffer_key_settings.Add(Convert.ToString(CHS4_key_buffer[i]));
            }

            Properties.Settings.Default.chs4kvr_buffer_key_settings.Clear();
            for (int i = 0; i < CHS4KVR_key_buffer.Length; i++)
            {
                Properties.Settings.Default.chs4kvr_buffer_key_settings.Add(Convert.ToString(CHS4KVR_key_buffer[i]));
            }

            Properties.Settings.Default.chs4t_buffer_key_settings.Clear();
            for (int i = 0; i < CHS4T_key_buffer.Length; i++)
            {
                Properties.Settings.Default.chs4t_buffer_key_settings.Add(Convert.ToString(CHS4T_key_buffer[i]));
            }

            Properties.Settings.Default.chs7_buffer_key_settings.Clear();
            for (int i = 0; i < CHS7_key_buffer.Length; i++)
            {
                Properties.Settings.Default.chs7_buffer_key_settings.Add(Convert.ToString(CHS7_key_buffer[i]));
            }

            Properties.Settings.Default.chs8_buffer_key_settings.Clear();
            for (int i = 0; i < CHS8_key_buffer.Length; i++)
            {
                Properties.Settings.Default.chs8_buffer_key_settings.Add(Convert.ToString(CHS8_key_buffer[i]));
            }

            Properties.Settings.Default.vl11_buffer_key_settings.Clear();
            for (int i = 0; i < VL11M_key_buffer.Length; i++)
            {
                Properties.Settings.Default.vl11_buffer_key_settings.Add(Convert.ToString(VL11M_key_buffer[i]));
            }

            Properties.Settings.Default.vl82_buffer_key_settings.Clear();
            for (int i = 0; i < VL82M_key_buffer.Length; i++)
            {
                Properties.Settings.Default.vl82_buffer_key_settings.Add(Convert.ToString(VL82M_key_buffer[i]));
            }

            Properties.Settings.Default.vl80t_buffer_key_settings.Clear();
            for (int i = 0; i < VL80T_key_buffer.Length; i++)
            {
                Properties.Settings.Default.vl80t_buffer_key_settings.Add(Convert.ToString(VL80T_key_buffer[i]));
            }

            Properties.Settings.Default.vl85_buffer_key_settings.Clear();
            for (int i = 0; i < VL85_key_buffer.Length; i++)
            {
                Properties.Settings.Default.vl85_buffer_key_settings.Add(Convert.ToString(VL85_key_buffer[i]));
            }

            Properties.Settings.Default.tep70_buffer_key_settings.Clear();
            for (int i = 0; i < TEP70_key_buffer.Length; i++)
            {
                Properties.Settings.Default.tep70_buffer_key_settings.Add(Convert.ToString(TEP70_key_buffer[i]));
            }

            Properties.Settings.Default.te10u_buffer_key_settings.Clear();
            for (int i = 0; i < TE10U_key_buffer.Length; i++)
            {
                Properties.Settings.Default.te10u_buffer_key_settings.Add(Convert.ToString(TE10U_key_buffer[i]));
            }

            Properties.Settings.Default.m62_buffer_key_settings.Clear();
            for (int i = 0; i < M62_key_buffer.Length; i++)
            {
                Properties.Settings.Default.m62_buffer_key_settings.Add(Convert.ToString(M62_key_buffer[i]));
            }

            Properties.Settings.Default.ed4m_buffer_key_settings.Clear();
            for (int i = 0; i < ED4M_key_buffer.Length; i++)
            {
                Properties.Settings.Default.ed4m_buffer_key_settings.Add(Convert.ToString(ED4M_key_buffer[i]));
            }

            Properties.Settings.Default.ed9m_buffer_key_settings.Clear();
            for (int i = 0; i < ED9M_key_buffer.Length; i++)
            {
                Properties.Settings.Default.ed9m_buffer_key_settings.Add(Convert.ToString(ED9M_key_buffer[i]));
            }

            Properties.Settings.Default.tem18_buffer_key_settings.Clear();
            for (int i = 0; i < tem18_key_buffer.Length; i++)
            {
                Properties.Settings.Default.tem18_buffer_key_settings.Add(Convert.ToString(tem18_key_buffer[i]));
            }

            //сохраняем оси 1 колонка буфера
            //zdsim
            Properties.Settings.Default.controls_buffer_axis_settings.Clear();
            for (int i = 0; i < Controls_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.controls_buffer_axis_settings.Add(Convert.ToString(Controls_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.neshtatki_buffer_axis_settings.Clear();
            for (int i = 0; i < Neshtatki_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.neshtatki_buffer_axis_settings.Add(Convert.ToString(Neshtatki_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.es5k_buffer_axis_settings.Clear();
            for (int i = 0; i < ES5K_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.es5k_buffer_axis_settings.Add(Convert.ToString(ES5K_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.ep1m_buffer_axis_settings.Clear();
            for (int i = 0; i < EP1M_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.ep1m_buffer_axis_settings.Add(Convert.ToString(EP1M_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.chs2k_buffer_axis_settings.Clear();
            for (int i = 0; i < CHS2K_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs2k_buffer_axis_settings.Add(Convert.ToString(CHS2K_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.chs4_buffer_axis_settings.Clear();
            for (int i = 0; i < CHS4_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs4_buffer_axis_settings.Add(Convert.ToString(CHS4_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.chs4kvr_buffer_axis_settings.Clear();
            for (int i = 0; i < CHS4KVR_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs4kvr_buffer_axis_settings.Add(Convert.ToString(CHS4KVR_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.chs4t_buffer_axis_settings.Clear();
            for (int i = 0; i < CHS4T_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs4t_buffer_axis_settings.Add(Convert.ToString(CHS4T_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.chs7_buffer_axis_settings.Clear();
            for (int i = 0; i < CHS7_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs7_buffer_axis_settings.Add(Convert.ToString(CHS7_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.chs8_buffer_axis_settings.Clear();
            for (int i = 0; i < CHS8_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs8_buffer_axis_settings.Add(Convert.ToString(CHS8_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.vl11_buffer_axis_settings.Clear();
            for (int i = 0; i < VL11M_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.vl11_buffer_axis_settings.Add(Convert.ToString(VL11M_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.vl82_buffer_axis_settings.Clear();
            for (int i = 0; i < VL82M_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.vl82_buffer_axis_settings.Add(Convert.ToString(VL82M_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.vl80t_buffer_axis_settings.Clear();
            for (int i = 0; i < VL80T_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.vl80t_buffer_axis_settings.Add(Convert.ToString(VL80T_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.vl85_buffer_axis_settings.Clear();
            for (int i = 0; i < VL85_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.vl85_buffer_axis_settings.Add(Convert.ToString(VL85_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.tep70_buffer_axis_settings.Clear();
            for (int i = 0; i < TEP70_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.tep70_buffer_axis_settings.Add(Convert.ToString(TEP70_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.te10u_buffer_axis_settings.Clear();
            for (int i = 0; i < TE10U_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.te10u_buffer_axis_settings.Add(Convert.ToString(TE10U_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.m62_buffer_axis_settings.Clear();
            for (int i = 0; i < M62_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.m62_buffer_axis_settings.Add(Convert.ToString(M62_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.ed4m_buffer_axis_settings.Clear();
            for (int i = 0; i < ED4M_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.ed4m_buffer_axis_settings.Add(Convert.ToString(ED4M_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.ed9m_buffer_axis_settings.Clear();
            for (int i = 0; i < ED9M_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.ed9m_buffer_axis_settings.Add(Convert.ToString(ED9M_axis_buffer[i, 0]));
            }
            Properties.Settings.Default.tem18_buffer_axis_settings.Clear();
            for (int i = 0; i < tem18_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.tem18_buffer_axis_settings.Add(Convert.ToString(tem18_axis_buffer[i, 0]));
            }

            //сохраняем оси 2 колонка буфера
            //zdsim
            Properties.Settings.Default.controls_buffer_axis_settings2.Clear();
            for (int i = 0; i < Controls_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.controls_buffer_axis_settings2.Add(Convert.ToString(Controls_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.neshtatki_buffer_axis_settings2.Clear();
            for (int i = 0; i < Neshtatki_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.neshtatki_buffer_axis_settings2.Add(Convert.ToString(Neshtatki_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.es5k_buffer_axis_settings2.Clear();
            for (int i = 0; i < ES5K_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.es5k_buffer_axis_settings2.Add(Convert.ToString(ES5K_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.ep1m_buffer_axis_settings2.Clear();
            for (int i = 0; i < EP1M_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.ep1m_buffer_axis_settings2.Add(Convert.ToString(EP1M_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.chs2k_buffer_axis_settings2.Clear();
            for (int i = 0; i < CHS2K_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs2k_buffer_axis_settings2.Add(Convert.ToString(CHS2K_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.chs4_buffer_axis_settings2.Clear();
            for (int i = 0; i < CHS4_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs4_buffer_axis_settings2.Add(Convert.ToString(CHS4_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.chs4kvr_buffer_axis_settings2.Clear();
            for (int i = 0; i < CHS4KVR_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs4kvr_buffer_axis_settings2.Add(Convert.ToString(CHS4KVR_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.chs4t_buffer_axis_settings2.Clear();
            for (int i = 0; i < CHS4T_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs4t_buffer_axis_settings2.Add(Convert.ToString(CHS4T_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.chs7_buffer_axis_settings2.Clear();
            for (int i = 0; i < CHS7_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs7_buffer_axis_settings2.Add(Convert.ToString(CHS7_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.chs8_buffer_axis_settings2.Clear();
            for (int i = 0; i < CHS8_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.chs8_buffer_axis_settings2.Add(Convert.ToString(CHS8_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.vl11_buffer_axis_settings2.Clear();
            for (int i = 0; i < VL11M_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.vl11_buffer_axis_settings2.Add(Convert.ToString(VL11M_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.vl82_buffer_axis_settings2.Clear();
            for (int i = 0; i < VL82M_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.vl82_buffer_axis_settings2.Add(Convert.ToString(VL82M_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.vl80t_buffer_axis_settings2.Clear();
            for (int i = 0; i < VL80T_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.vl80t_buffer_axis_settings2.Add(Convert.ToString(VL80T_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.vl85_buffer_axis_settings2.Clear();
            for (int i = 0; i < VL85_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.vl85_buffer_axis_settings2.Add(Convert.ToString(VL85_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.tep70_buffer_axis_settings2.Clear();
            for (int i = 0; i < TEP70_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.tep70_buffer_axis_settings2.Add(Convert.ToString(TEP70_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.te10u_buffer_axis_settings2.Clear();
            for (int i = 0; i < TE10U_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.te10u_buffer_axis_settings2.Add(Convert.ToString(TE10U_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.m62_buffer_axis_settings2.Clear();
            for (int i = 0; i < M62_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.m62_buffer_axis_settings2.Add(Convert.ToString(M62_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.ed4m_buffer_axis_settings2.Clear();
            for (int i = 0; i < ED4M_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.ed4m_buffer_axis_settings2.Add(Convert.ToString(ED4M_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.ed9m_buffer_axis_settings2.Clear();
            for (int i = 0; i < ED9M_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.ed9m_buffer_axis_settings2.Add(Convert.ToString(ED9M_axis_buffer[i, 1]));
            }

            Properties.Settings.Default.tem18_buffer_axis_settings2.Clear();
            for (int i = 0; i < tem18_axis_buffer.Length / 2; i++)
            {
                Properties.Settings.Default.tem18_buffer_axis_settings2.Add(Convert.ToString(tem18_axis_buffer[i, 1]));
            }

            //сохраняем точки осей
            if (joystick_ARx_point_buffer != null)
            {
                Properties.Settings.Default.ARx_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_ARx_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.ARx_point_buffer_settings.Add(Convert.ToString(joystick_ARx_point_buffer[i]));
                }
            }

            if (joystick_ARy_point_buffer != null)
            {
                Properties.Settings.Default.ARy_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_ARy_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.ARy_point_buffer_settings.Add(Convert.ToString(joystick_ARy_point_buffer[i]));
                }
            }

            if (joystick_ARz_point_buffer != null)
            {
                Properties.Settings.Default.ARz_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_ARz_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.ARz_point_buffer_settings.Add(Convert.ToString(joystick_ARz_point_buffer[i]));
                }
            }

            if (joystick_AX_point_buffer != null)
            {
                Properties.Settings.Default.AX_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_AX_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.AX_point_buffer_settings.Add(Convert.ToString(joystick_AX_point_buffer[i]));
                }
            }

            if (joystick_AY_point_buffer != null)
            {
                Properties.Settings.Default.AY_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_AY_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.AY_point_buffer_settings.Add(Convert.ToString(joystick_AY_point_buffer[i]));
                }
            }

            if (joystick_AZ_point_buffer != null)
            {
                Properties.Settings.Default.AZ_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_AZ_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.AZ_point_buffer_settings.Add(Convert.ToString(joystick_AZ_point_buffer[i]));
                }
            }

            if (joystick_FRx_point_buffer != null)
            {
                Properties.Settings.Default.FRx_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_FRx_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.FRx_point_buffer_settings.Add(Convert.ToString(joystick_FRx_point_buffer[i]));
                }
            }

            if (joystick_FRy_point_buffer != null)
            {
                Properties.Settings.Default.FRy_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_FRy_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.FRy_point_buffer_settings.Add(Convert.ToString(joystick_FRy_point_buffer[i]));
                }
            }

            if (joystick_FRz_point_buffer != null)
            {
                Properties.Settings.Default.FRz_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_FRz_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.FRz_point_buffer_settings.Add(Convert.ToString(joystick_FRz_point_buffer[i]));
                }
            }

            if (joystick_FX_point_buffer != null)
            {
                Properties.Settings.Default.FX_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_FX_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.FX_point_buffer_settings.Add(Convert.ToString(joystick_FX_point_buffer[i]));
                }
            }

            if (joystick_FY_point_buffer != null)
            {
                Properties.Settings.Default.FY_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_FY_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.FY_point_buffer_settings.Add(Convert.ToString(joystick_FY_point_buffer[i]));
                }
            }

            if (joystick_FZ_point_buffer != null)
            {
                Properties.Settings.Default.FZ_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_FZ_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.FZ_point_buffer_settings.Add(Convert.ToString(joystick_FZ_point_buffer[i]));
                }
            }

            if (joystick_Rx_point_buffer != null)
            {
                Properties.Settings.Default.Rx_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_Rx_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.Rx_point_buffer_settings.Add(Convert.ToString(joystick_Rx_point_buffer[i]));
                }
            }

            if (joystick_Ry_point_buffer != null)
            {
                Properties.Settings.Default.Ry_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_Ry_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.Ry_point_buffer_settings.Add(Convert.ToString(joystick_Ry_point_buffer[i]));
                }
            }

            if (joystick_Rz_point_buffer != null)
            {
                Properties.Settings.Default.Rz_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_Rz_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.Rz_point_buffer_settings.Add(Convert.ToString(joystick_Rz_point_buffer[i]));
                }
            }

            if (joystick_VRx_point_buffer != null)
            {
                Properties.Settings.Default.VRx_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_VRx_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.VRx_point_buffer_settings.Add(Convert.ToString(joystick_VRx_point_buffer[i]));
                }
            }

            if (joystick_VRy_point_buffer != null)
            {
                Properties.Settings.Default.VRy_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_VRy_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.VRy_point_buffer_settings.Add(Convert.ToString(joystick_VRy_point_buffer[i]));
                }
            }

            if (joystick_VRz_point_buffer != null)
            {
                Properties.Settings.Default.VRz_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_VRz_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.VRz_point_buffer_settings.Add(Convert.ToString(joystick_VRz_point_buffer[i]));
                }
            }

            if (joystick_VX_point_buffer != null)
            {
                Properties.Settings.Default.VX_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_VX_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.VX_point_buffer_settings.Add(Convert.ToString(joystick_VX_point_buffer[i]));
                }
            }

            if (joystick_VY_point_buffer != null)
            {
                Properties.Settings.Default.VY_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_VY_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.VY_point_buffer_settings.Add(Convert.ToString(joystick_VY_point_buffer[i]));
                }
            }

            if (joystick_VZ_point_buffer != null)
            {
                Properties.Settings.Default.VZ_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_VZ_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.VZ_point_buffer_settings.Add(Convert.ToString(joystick_VZ_point_buffer[i]));
                }
            }

            if (joystick_X_point_buffer != null)
            {
                Properties.Settings.Default.X_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_X_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.X_point_buffer_settings.Add(Convert.ToString(joystick_X_point_buffer[i]));
                }
            }

            if (joystick_Y_point_buffer != null)
            {
                Properties.Settings.Default.Y_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_Y_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.Y_point_buffer_settings.Add(Convert.ToString(joystick_Y_point_buffer[i]));
                }
            }

            if (joystick_Z_point_buffer != null)
            {
                Properties.Settings.Default.Z_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_Z_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.Z_point_buffer_settings.Add(Convert.ToString(joystick_Z_point_buffer[i]));
                }
            }

            if (joystick_POV_point_buffer != null)
            {
                Properties.Settings.Default.POV_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_POV_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.POV_point_buffer_settings.Add(Convert.ToString(joystick_POV_point_buffer[i]));
                }
            }

            if (joystick_Slider_point_buffer != null)
            {
                Properties.Settings.Default.Slider_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_Slider_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.Slider_point_buffer_settings.Add(Convert.ToString(joystick_Slider_point_buffer[i]));
                }
            }

            if (joystick_ASlider_point_buffer != null)
            {
                Properties.Settings.Default.ASlider_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_ASlider_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.ASlider_point_buffer_settings.Add(Convert.ToString(joystick_ASlider_point_buffer[i]));
                }
            }

            if (joystick_FSlider_point_buffer != null)
            {
                Properties.Settings.Default.FSlider_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_FSlider_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.FSlider_point_buffer_settings.Add(Convert.ToString(joystick_FSlider_point_buffer[i]));
                }
            }

            if (joystick_VSlider_point_buffer != null)
            {
                Properties.Settings.Default.VSlider_point_buffer_settings.Clear();
                for (int i = 0; i < joystick_VSlider_point_buffer.Length; i++)
                {
                    Properties.Settings.Default.VSlider_point_buffer_settings.Add(Convert.ToString(joystick_VSlider_point_buffer[i]));
                }
            }
        }

        //------------------------------------------------------------------------------------
        //Сохранение буферов звуков
        //------------------------------------------------------------------------------------
        public static void SaveWavePathBuffersSettings()
        {
            //сохраняем пути звуков zdsim
            Properties.Settings.Default.sb_controls_wav_path_data_settings.Clear();
            for (int i = 0; i < controls_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_controls_wav_path_data_settings.Add(controls_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_es5k_wav_path_data_settings.Clear();
            for (int i = 0; i < es5k_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_es5k_wav_path_data_settings.Add(es5k_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_ep1m_wav_path_data_settings.Clear();
            for (int i = 0; i < ep1m_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_ep1m_wav_path_data_settings.Add(ep1m_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs2k_wav_path_data_settings.Clear();
            for (int i = 0; i < chs2k_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs2k_wav_path_data_settings.Add(chs2k_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs4_wav_path_data_settings.Clear();
            for (int i = 0; i < chs4_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs4_wav_path_data_settings.Add(chs4_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs4kvr_wav_path_data_settings.Clear();
            for (int i = 0; i < chs4kvr_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs4kvr_wav_path_data_settings.Add(chs4kvr_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs4t_wav_path_data_settings.Clear();
            for (int i = 0; i < chs4t_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs4t_wav_path_data_settings.Add(chs4t_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs7_wav_path_data_settings.Clear();
            for (int i = 0; i < chs7_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs7_wav_path_data_settings.Add(chs7_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_chs8_wav_path_data_settings.Clear();
            for (int i = 0; i < chs8_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_chs8_wav_path_data_settings.Add(chs8_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_vl11_wav_path_data_settings.Clear();
            for (int i = 0; i < vl11_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_vl11_wav_path_data_settings.Add(vl11_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_vl82_wav_path_data_settings.Clear();
            for (int i = 0; i < vl82_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_vl82_wav_path_data_settings.Add(vl82_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_vl80t_wav_path_data_settings.Clear();
            for (int i = 0; i < vl80t_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_vl80t_wav_path_data_settings.Add(vl80t_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_vl85_wav_path_data_settings.Clear();
            for (int i = 0; i < vl85_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_vl85_wav_path_data_settings.Add(vl85_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_tep70_wav_path_data_settings.Clear();
            for (int i = 0; i < tep70_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_tep70_wav_path_data_settings.Add(tep70_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_te10u_wav_path_data_settings.Clear();
            for (int i = 0; i < te10u_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_te10u_wav_path_data_settings.Add(te10u_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_m62_wav_path_data_settings.Clear();
            for (int i = 0; i < m62_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_m62_wav_path_data_settings.Add(m62_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_ed4m_wav_path_data_settings.Clear();
            for (int i = 0; i < ed4m_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_ed4m_wav_path_data_settings.Add(ed4m_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_ed9m_wav_path_data_settings.Clear();
            for (int i = 0; i < ed9m_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_ed9m_wav_path_data_settings.Add(ed9m_wav_path_key_buffer[i]);
            }
            Properties.Settings.Default.sb_tem18_wav_path_data_settings.Clear();
            for (int i = 0; i < tem18_wav_path_key_buffer.Length; i++)
            {
                Properties.Settings.Default.sb_tem18_wav_path_data_settings.Add(tem18_wav_path_key_buffer[i]);
            }

         }

        //------------------------------------------------------------------------------------
        //Загрузка буферов кнопок, осей и точек
        //------------------------------------------------------------------------------------
        public static void LoadBuffersSettings()
        {
            //загружаем кнопки
            if (Properties.Settings.Default.controls_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < Controls_key_buffer.Length; i++)
                {
                    Controls_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.controls_buffer_key_settings[i]);
                }
            }

            if (Properties.Settings.Default.neshtatki_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < Neshtatki_key_buffer.Length; i++)
                {
                    Neshtatki_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.neshtatki_buffer_key_settings[i]);
                }
            }

            if (Properties.Settings.Default.es5k_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < ES5K_key_buffer.Length; i++)
                {
                    ES5K_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.es5k_buffer_key_settings[i]);
                }
            }

            if (Properties.Settings.Default.ep1m_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < EP1M_key_buffer.Length; i++)
                {
                    EP1M_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.ep1m_buffer_key_settings[i]);
                }
            }

            if (Properties.Settings.Default.chs2k_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < CHS2K_key_buffer.Length; i++)
                {
                    CHS2K_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.chs2k_buffer_key_settings[i]);
                }
            }

            if (Properties.Settings.Default.chs4_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < CHS4_key_buffer.Length; i++)
                {
                    CHS4_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.chs4_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.chs4kvr_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < CHS4KVR_key_buffer.Length; i++)
                {
                    CHS4KVR_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.chs4kvr_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.chs4t_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < CHS4T_key_buffer.Length; i++)
                {
                    CHS4T_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.chs4t_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.chs7_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < CHS7_key_buffer.Length; i++)
                {
                    CHS7_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.chs7_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.chs8_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < CHS8_key_buffer.Length; i++)
                {
                    CHS8_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.chs8_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.vl11_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < VL11M_key_buffer.Length; i++)
                {
                    VL11M_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.vl11_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.vl82_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < VL82M_key_buffer.Length; i++)
                {
                    VL82M_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.vl82_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.vl80t_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < VL80T_key_buffer.Length; i++)
                {
                    VL80T_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.vl80t_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.vl85_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < VL85_key_buffer.Length; i++)
                {
                    VL85_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.vl85_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.tep70_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < TEP70_key_buffer.Length; i++)
                {
                    TEP70_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.tep70_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.te10u_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < TE10U_key_buffer.Length; i++)
                {
                    TE10U_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.te10u_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.m62_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < M62_key_buffer.Length; i++)
                {
                    M62_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.m62_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.ed4m_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < ED4M_key_buffer.Length; i++)
                {
                    ED4M_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.ed4m_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.ed9m_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < ED9M_key_buffer.Length; i++)
                {
                    ED9M_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.ed9m_buffer_key_settings[i]);
                }
            }
            if (Properties.Settings.Default.tem18_buffer_key_settings[0] != "start")
            {
                for (int i = 0; i < tem18_key_buffer.Length; i++)
                {
                    tem18_key_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.tem18_buffer_key_settings[i]);
                }
            }

            //загружаем оси 1 колонка
            if (Properties.Settings.Default.controls_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < Controls_axis_buffer.Length / 2; i++)
                {
                    Controls_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.controls_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.neshtatki_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < Neshtatki_axis_buffer.Length / 2; i++)
                {
                    Neshtatki_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.neshtatki_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.es5k_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < ES5K_axis_buffer.Length / 2; i++)
                {
                    ES5K_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.es5k_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.ep1m_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < EP1M_axis_buffer.Length / 2; i++)
                {
                    EP1M_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.ep1m_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.chs2k_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < CHS2K_axis_buffer.Length / 2; i++)
                {
                    CHS2K_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.chs2k_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.chs4_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < CHS4_axis_buffer.Length / 2; i++)
                {
                    CHS4_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.chs4_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.chs4kvr_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < CHS4KVR_axis_buffer.Length / 2; i++)
                {
                    CHS4KVR_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.chs4kvr_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.chs4t_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < CHS4T_axis_buffer.Length / 2; i++)
                {
                    CHS4T_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.chs4t_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.chs7_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < CHS7_axis_buffer.Length / 2; i++)
                {
                    CHS7_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.chs7_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.chs8_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < CHS8_axis_buffer.Length / 2; i++)
                {
                    CHS8_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.chs8_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.vl11_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < VL11M_axis_buffer.Length / 2; i++)
                {
                    VL11M_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.vl11_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.vl82_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < VL82M_axis_buffer.Length / 2; i++)
                {
                    VL82M_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.vl82_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.vl80t_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < VL80T_axis_buffer.Length / 2; i++)
                {
                    VL80T_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.vl80t_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.vl85_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < VL85_axis_buffer.Length / 2; i++)
                {
                    VL85_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.vl85_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.tep70_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < TEP70_axis_buffer.Length / 2; i++)
                {
                    TEP70_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.tep70_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.te10u_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < TE10U_axis_buffer.Length / 2; i++)
                {
                    TE10U_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.te10u_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.m62_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < M62_axis_buffer.Length / 2; i++)
                {
                    M62_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.m62_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.ed4m_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < ED4M_axis_buffer.Length / 2; i++)
                {
                    ED4M_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.ed4m_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.ed9m_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < ED9M_axis_buffer.Length / 2; i++)
                {
                    ED9M_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.ed9m_buffer_axis_settings[i]);
                }
            }
            if (Properties.Settings.Default.tem18_buffer_axis_settings[0] != "start")
            {
                for (int i = 0; i < tem18_axis_buffer.Length / 2; i++)
                {
                    tem18_axis_buffer[i, 0] = Convert.ToUInt16(Properties.Settings.Default.tem18_buffer_axis_settings[i]);
                }
            }

             //загружаем оси 2 колонка
            if (Properties.Settings.Default.controls_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < Controls_axis_buffer.Length / 2; i++)
                {
                    Controls_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.controls_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.neshtatki_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < Neshtatki_axis_buffer.Length / 2; i++)
                {
                    Neshtatki_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.neshtatki_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.es5k_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < ES5K_axis_buffer.Length / 2; i++)
                {
                    ES5K_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.es5k_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.ep1m_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < EP1M_axis_buffer.Length / 2; i++)
                {
                    EP1M_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.ep1m_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.chs2k_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < CHS2K_axis_buffer.Length / 2; i++)
                {
                    CHS2K_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.chs2k_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.chs4_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < CHS4_axis_buffer.Length / 2; i++)
                {
                    CHS4_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.chs4_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.chs4kvr_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < CHS4KVR_axis_buffer.Length / 2; i++)
                {
                    CHS4KVR_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.chs4kvr_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.chs4t_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < CHS4T_axis_buffer.Length / 2; i++)
                {
                    CHS4T_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.chs4t_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.chs7_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < CHS7_axis_buffer.Length / 2; i++)
                {
                    CHS7_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.chs7_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.chs8_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < CHS8_axis_buffer.Length / 2; i++)
                {
                    CHS8_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.chs8_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.vl11_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < VL11M_axis_buffer.Length / 2; i++)
                {
                    VL11M_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.vl11_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.vl82_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < VL82M_axis_buffer.Length / 2; i++)
                {
                    VL82M_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.vl82_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.vl80t_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < VL80T_axis_buffer.Length / 2; i++)
                {
                    VL80T_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.vl80t_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.vl85_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < VL85_axis_buffer.Length / 2; i++)
                {
                    VL85_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.vl85_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.tep70_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < TEP70_axis_buffer.Length / 2; i++)
                {
                    TEP70_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.tep70_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.te10u_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < TE10U_axis_buffer.Length / 2; i++)
                {
                    TE10U_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.te10u_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.m62_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < M62_axis_buffer.Length / 2; i++)
                {
                    M62_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.m62_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.ed4m_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < ED4M_axis_buffer.Length / 2; i++)
                {
                    ED4M_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.ed4m_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.ed9m_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < ED9M_axis_buffer.Length / 2; i++)
                {
                    ED9M_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.ed9m_buffer_axis_settings2[i]);
                }
            }
            if (Properties.Settings.Default.tem18_buffer_axis_settings2[0] != "start")
            {
                for (int i = 0; i < tem18_axis_buffer.Length / 2; i++)
                {
                    tem18_axis_buffer[i, 1] = Convert.ToUInt16(Properties.Settings.Default.tem18_buffer_axis_settings2[i]);
                }
            }

            //загружаем точки осей
            if (Properties.Settings.Default.ARx_point_buffer_settings[0] != "start")
            {
                joystick_ARx_point_buffer = new int[Properties.Settings.Default.ARx_point_buffer_settings.Count];
                for (int i = 0; i < joystick_ARx_point_buffer.Length; i++)
                {
                    joystick_ARx_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.ARx_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.ARy_point_buffer_settings[0] != "start")
            {
                joystick_ARy_point_buffer = new int[Properties.Settings.Default.ARy_point_buffer_settings.Count];
                for (int i = 0; i < joystick_ARy_point_buffer.Length; i++)
                {
                    joystick_ARy_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.ARy_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.ARz_point_buffer_settings[0] != "start")
            {
                joystick_ARz_point_buffer = new int[Properties.Settings.Default.ARz_point_buffer_settings.Count];
                for (int i = 0; i < joystick_ARz_point_buffer.Length; i++)
                {
                    joystick_ARz_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.ARz_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.AX_point_buffer_settings[0] != "start")
            {
                joystick_AX_point_buffer = new int[Properties.Settings.Default.AX_point_buffer_settings.Count];
                for (int i = 0; i < joystick_AX_point_buffer.Length; i++)
                {
                    joystick_AX_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.AX_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.AY_point_buffer_settings[0] != "start")
            {
                joystick_AY_point_buffer = new int[Properties.Settings.Default.AY_point_buffer_settings.Count];
                for (int i = 0; i < joystick_AY_point_buffer.Length; i++)
                {
                    joystick_AY_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.AY_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.AZ_point_buffer_settings[0] != "start")
            {
                joystick_AZ_point_buffer = new int[Properties.Settings.Default.AZ_point_buffer_settings.Count];
                for (int i = 0; i < joystick_AZ_point_buffer.Length; i++)
                {
                    joystick_AZ_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.AZ_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.FRx_point_buffer_settings[0] != "start")
            {
                joystick_FRx_point_buffer = new int[Properties.Settings.Default.FRx_point_buffer_settings.Count];
                for (int i = 0; i < joystick_FRx_point_buffer.Length; i++)
                {
                    joystick_FRx_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.FRx_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.FRy_point_buffer_settings[0] != "start")
            {
                joystick_FRy_point_buffer = new int[Properties.Settings.Default.FRy_point_buffer_settings.Count];
                for (int i = 0; i < joystick_FRy_point_buffer.Length; i++)
                {
                    joystick_FRy_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.FRy_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.FRz_point_buffer_settings[0] != "start")
            {
                joystick_FRz_point_buffer = new int[Properties.Settings.Default.FRz_point_buffer_settings.Count];
                for (int i = 0; i < joystick_FRz_point_buffer.Length; i++)
                {
                    joystick_FRz_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.FRz_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.FX_point_buffer_settings[0] != "start")
            {
                joystick_FX_point_buffer = new int[Properties.Settings.Default.FX_point_buffer_settings.Count];
                for (int i = 0; i < joystick_FX_point_buffer.Length; i++)
                {
                    joystick_FX_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.FX_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.FY_point_buffer_settings[0] != "start")
            {
                joystick_FY_point_buffer = new int[Properties.Settings.Default.FY_point_buffer_settings.Count];
                for (int i = 0; i < joystick_FY_point_buffer.Length; i++)
                {
                    joystick_FY_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.FY_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.FZ_point_buffer_settings[0] != "start")
            {
                joystick_FZ_point_buffer = new int[Properties.Settings.Default.FZ_point_buffer_settings.Count];
                for (int i = 0; i < joystick_FZ_point_buffer.Length; i++)
                {
                    joystick_FZ_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.FZ_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.Rx_point_buffer_settings[0] != "start")
            {
                joystick_Rx_point_buffer = new int[Properties.Settings.Default.Rx_point_buffer_settings.Count];
                for (int i = 0; i < joystick_Rx_point_buffer.Length; i++)
                {
                    joystick_Rx_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.Rx_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.Ry_point_buffer_settings[0] != "start")
            {
                joystick_Ry_point_buffer = new int[Properties.Settings.Default.Ry_point_buffer_settings.Count];
                for (int i = 0; i < joystick_Ry_point_buffer.Length; i++)
                {
                    joystick_Ry_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.Ry_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.Rz_point_buffer_settings[0] != "start")
            {
                joystick_Rz_point_buffer = new int[Properties.Settings.Default.Rz_point_buffer_settings.Count];
                for (int i = 0; i < joystick_Rz_point_buffer.Length; i++)
                {
                    joystick_Rz_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.Rz_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.VRx_point_buffer_settings[0] != "start")
            {
                joystick_VRx_point_buffer = new int[Properties.Settings.Default.VRx_point_buffer_settings.Count];
                for (int i = 0; i < joystick_VRx_point_buffer.Length; i++)
                {
                    joystick_VRx_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.VRx_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.VRy_point_buffer_settings[0] != "start")
            {
                joystick_VRy_point_buffer = new int[Properties.Settings.Default.VRy_point_buffer_settings.Count];
                for (int i = 0; i < joystick_VRy_point_buffer.Length; i++)
                {
                    joystick_VRy_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.VRy_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.VRz_point_buffer_settings[0] != "start")
            {
                joystick_VRz_point_buffer = new int[Properties.Settings.Default.VRz_point_buffer_settings.Count];
                for (int i = 0; i < joystick_VRz_point_buffer.Length; i++)
                {
                    joystick_VRz_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.VRz_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.VX_point_buffer_settings[0] != "start")
            {
                joystick_VX_point_buffer = new int[Properties.Settings.Default.VX_point_buffer_settings.Count];
                for (int i = 0; i < joystick_VX_point_buffer.Length; i++)
                {
                    joystick_VX_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.VX_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.VY_point_buffer_settings[0] != "start")
            {
                joystick_VY_point_buffer = new int[Properties.Settings.Default.VY_point_buffer_settings.Count];
                for (int i = 0; i < joystick_VY_point_buffer.Length; i++)
                {
                    joystick_VY_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.VY_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.VZ_point_buffer_settings[0] != "start")
            {
                joystick_VZ_point_buffer = new int[Properties.Settings.Default.VZ_point_buffer_settings.Count];
                for (int i = 0; i < joystick_VZ_point_buffer.Length; i++)
                {
                    joystick_VZ_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.VZ_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.X_point_buffer_settings[0] != "start")
            {
                joystick_X_point_buffer = new int[Properties.Settings.Default.X_point_buffer_settings.Count];
                for (int i = 0; i < joystick_X_point_buffer.Length; i++)
                {
                    joystick_X_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.X_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.Y_point_buffer_settings[0] != "start")
            {
                joystick_Y_point_buffer = new int[Properties.Settings.Default.Y_point_buffer_settings.Count];
                for (int i = 0; i < joystick_Y_point_buffer.Length; i++)
                {
                    joystick_Y_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.Y_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.Z_point_buffer_settings[0] != "start")
            {
                joystick_Z_point_buffer = new int[Properties.Settings.Default.Z_point_buffer_settings.Count];
                for (int i = 0; i < joystick_Z_point_buffer.Length; i++)
                {
                    joystick_Z_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.Z_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.POV_point_buffer_settings[0] != "start")
            {
                joystick_POV_point_buffer = new int[Properties.Settings.Default.POV_point_buffer_settings.Count];
                for (int i = 0; i < joystick_POV_point_buffer.Length; i++)
                {
                    joystick_POV_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.POV_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.Slider_point_buffer_settings[0] != "start")
            {
                joystick_Slider_point_buffer = new int[Properties.Settings.Default.Slider_point_buffer_settings.Count];
                for (int i = 0; i < joystick_Slider_point_buffer.Length; i++)
                {
                    joystick_Slider_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.Slider_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.ASlider_point_buffer_settings[0] != "start")
            {
                joystick_ASlider_point_buffer = new int[Properties.Settings.Default.ASlider_point_buffer_settings.Count];
                for (int i = 0; i < joystick_ASlider_point_buffer.Length; i++)
                {
                    joystick_ASlider_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.ASlider_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.FSlider_point_buffer_settings[0] != "start")
            {
                joystick_FSlider_point_buffer = new int[Properties.Settings.Default.FSlider_point_buffer_settings.Count];
                for (int i = 0; i < joystick_FSlider_point_buffer.Length; i++)
                {
                    joystick_FSlider_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.FSlider_point_buffer_settings[i]);
                }
            }
            if (Properties.Settings.Default.VSlider_point_buffer_settings[0] != "start")
            {
                joystick_VSlider_point_buffer = new int[Properties.Settings.Default.VSlider_point_buffer_settings.Count];
                for (int i = 0; i < joystick_VSlider_point_buffer.Length; i++)
                {
                    joystick_VSlider_point_buffer[i] = Convert.ToUInt16(Properties.Settings.Default.VSlider_point_buffer_settings[i]);
                }
            }
            
        }

        //------------------------------------------------------------------------------------
        //Загрузка буферов звуков
        //------------------------------------------------------------------------------------
        public static void LoadWavePathBuffersSettings()
        {
            //загружаем пути звуков zdsim
            if (Properties.Settings.Default.sb_controls_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < controls_wav_path_key_buffer.Length; i++)
                {
                    controls_wav_path_key_buffer[i] = Properties.Settings.Default.sb_controls_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_neshtatki_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < neshtatki_wav_path_key_buffer.Length; i++)
                {
                    neshtatki_wav_path_key_buffer[i] = Properties.Settings.Default.sb_neshtatki_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_es5k_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < es5k_wav_path_key_buffer.Length; i++)
                {
                    es5k_wav_path_key_buffer[i] = Properties.Settings.Default.sb_es5k_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_ep1m_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < ep1m_wav_path_key_buffer.Length; i++)
                {
                    ep1m_wav_path_key_buffer[i] = Properties.Settings.Default.sb_ep1m_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs2k_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < chs2k_wav_path_key_buffer.Length; i++)
                {
                    chs2k_wav_path_key_buffer[i] = Properties.Settings.Default.sb_chs2k_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs4_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < chs4_wav_path_key_buffer.Length; i++)
                {
                    chs4_wav_path_key_buffer[i] = Properties.Settings.Default.sb_chs4_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs4kvr_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < chs4kvr_wav_path_key_buffer.Length; i++)
                {
                    chs4kvr_wav_path_key_buffer[i] = Properties.Settings.Default.sb_chs4kvr_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs4t_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < chs4t_wav_path_key_buffer.Length; i++)
                {
                    chs4t_wav_path_key_buffer[i] = Properties.Settings.Default.sb_chs4t_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs7_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < chs7_wav_path_key_buffer.Length; i++)
                {
                    chs7_wav_path_key_buffer[i] = Properties.Settings.Default.sb_chs7_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_chs8_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < chs8_wav_path_key_buffer.Length; i++)
                {
                    chs8_wav_path_key_buffer[i] = Properties.Settings.Default.sb_chs8_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_vl11_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < vl11_wav_path_key_buffer.Length; i++)
                {
                    vl11_wav_path_key_buffer[i] = Properties.Settings.Default.sb_vl11_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_vl82_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < vl82_wav_path_key_buffer.Length; i++)
                {
                    vl82_wav_path_key_buffer[i] = Properties.Settings.Default.sb_vl82_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_vl80t_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < vl80t_wav_path_key_buffer.Length; i++)
                {
                    vl80t_wav_path_key_buffer[i] = Properties.Settings.Default.sb_vl80t_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_vl85_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < vl85_wav_path_key_buffer.Length; i++)
                {
                    vl85_wav_path_key_buffer[i] = Properties.Settings.Default.sb_vl85_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_tep70_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < tep70_wav_path_key_buffer.Length; i++)
                {
                    tep70_wav_path_key_buffer[i] = Properties.Settings.Default.sb_tep70_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_te10u_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < te10u_wav_path_key_buffer.Length; i++)
                {
                    te10u_wav_path_key_buffer[i] = Properties.Settings.Default.sb_te10u_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_m62_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < m62_wav_path_key_buffer.Length; i++)
                {
                    m62_wav_path_key_buffer[i] = Properties.Settings.Default.sb_m62_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_ed4m_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < ed4m_wav_path_key_buffer.Length; i++)
                {
                    ed4m_wav_path_key_buffer[i] = Properties.Settings.Default.sb_ed4m_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_ed9m_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < ed9m_wav_path_key_buffer.Length; i++)
                {
                    ed9m_wav_path_key_buffer[i] = Properties.Settings.Default.sb_ed9m_wav_path_data_settings[i];
                }
            }
            if (Properties.Settings.Default.sb_tem18_wav_path_data_settings[0] != "start")
            {
                for (int i = 0; i < tem18_wav_path_key_buffer.Length; i++)
                {
                    tem18_wav_path_key_buffer[i] = Properties.Settings.Default.sb_tem18_wav_path_data_settings[i];
                }
            }
         }

        //------------------------------------------------------------------------------------
        //Загрузка Form1 Главный экран программы
        //------------------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            ActiveControl = button2;
            i_delay_send = Convert.ToInt16(Properties.Settings.Default.delay_send);
            i_delay_send_HID = Convert.ToInt16(Properties.Settings.Default.delay_send_HID);
            i_time_begin = Convert.ToInt16(Properties.Settings.Default.Time) * 60;
            i_skor_COM = Convert.ToInt32(Properties.Settings.Default.skor_COM);
            numericUpDown_time.Value = Properties.Settings.Default.Time;
            numericUpDown_skorCOM.Value = Properties.Settings.Default.skor_COM;
            numericUpDown_delay.Value = Properties.Settings.Default.delay_send;
            numericUpDown_delay_HID.Value = Properties.Settings.Default.delay_send_HID;
            Loco.i_skor_tek_convert = Convert.ToSingle(Properties.Settings.Default.skor_tek);
            Loco.i_tok_ept_convert = Convert.ToSingle(Properties.Settings.Default.tok_ept);
            Loco.i_napruga_ks_convert = Convert.ToSingle(Properties.Settings.Default.napr_ks);
            Loco.i_napruga_td_convert = Convert.ToSingle(Properties.Settings.Default.napr_td);
            Loco.i_tok_convert = Convert.ToSingle(Properties.Settings.Default.tok);
            Loco.i_pnevmo_convert = Convert.ToSingle(Properties.Settings.Default.pnevmatika);
            i_step_steper_motor = Convert.ToInt16(Properties.Settings.Default.step_steper_motor);
            i_bdit = Convert.ToInt16(Properties.Settings.Default.bdit);
            i_shum_joystick = Convert.ToInt16(Properties.Settings.Default.joystick_shum);
            i_priem_peredacha = Properties.Settings.Default.i_priem_peredacha;
            i_dvery_control_off_settings = Properties.Settings.Default.i_dvery_control_off_settings;
            i_sound_peredacha = Properties.Settings.Default.i_sound_peredacha;
            LoadBuffersSettings();
            LoadWavePathBuffersSettings();

            //получить путь к zdsimscanner
            i_path_zdsimscanner = Application.ExecutablePath;
            i_path_zdsimscanner = Application.ExecutablePath.Remove(i_path_zdsimscanner.LastIndexOf('\\'));


            timer_bdit.Interval = i_bdit;

            LocoMemoryHelpers.InitBuffers();
            LocoButtonsStructInit();
            Joystick_init();

            if (device != null)
            {
                UpdateJoystickState();
                console1.Clear();
                console1.ForeColor = Color.Yellow;
                console1.ScrollToCaret();
                console1.AppendText("\r\nJoy: " + device.DeviceInformation.InstanceName);
                console1.AppendText("\r\nОсей: " + device.Caps.NumberAxes.ToString());
                console1.AppendText("\r\nКнопок: " + device.Caps.NumberButtons.ToString());
                console1.AppendText("\r\nPOV: " + device.Caps.NumberPointOfViews.ToString());
            }

            Init_COM();
            combobox_Port.Items.AddRange(buffer_COM_ports);
            combobox_Port.Text = Properties.Settings.Default.COM;
            i_COM = Properties.Settings.Default.COM;
        }

        //------------------------------------------------------------------------------------
        //Закрытие Form1 Главный экран программы
        //------------------------------------------------------------------------------------
        private void Form1_Closed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Time = numericUpDown_time.Value;
            Properties.Settings.Default.COM = combobox_Port.Text;
            Properties.Settings.Default.skor_COM = numericUpDown_skorCOM.Value;
            Properties.Settings.Default.delay_send = numericUpDown_delay.Value;
            Properties.Settings.Default.delay_send_HID = numericUpDown_delay_HID.Value;
            Properties.Settings.Default.i_priem_peredacha = i_priem_peredacha;
            Properties.Settings.Default.i_dvery_control_off_settings = i_dvery_control_off_settings;
            Properties.Settings.Default.i_sound_peredacha = i_sound_peredacha;
            SaveBuffersSettings();
            SaveWavePathBuffersSettings();
            Properties.Settings.Default.Save();

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            Debug.WriteLine("Настройки сохранены в: " + config.FilePath);

            timer1.Enabled = false;
            timer2.Enabled = false;
            try
            {
                if (port.IsOpen)
                {
                    port.Close();
                }
            }
            catch
            {

            }
            DeleteXmlBinFiles();
        }

        //------------------------------------------------------------------------------------
        //Кнопка Выход главный экран программы
        //------------------------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //------------------------------------------------------------------------------------
        //Инициализация настроек для приборов значения по умолчанию
        //------------------------------------------------------------------------------------
        public static void Init_pribor()
        {
            Loco.i_skor_tek_convert = 1.475f;
            Loco.i_tok_ept_convert = 26.2f;
            Loco.i_napruga_ks_convert = 11.5f;
            Loco.i_napruga_td_convert = 2.6f;
            Loco.i_tok_convert = 6.5f;
            Loco.i_pnevmo_convert = 38.8f;
            i_delay_send = 3300;
            i_bdit = 0;
        }

        //------------------------------------------------------------------------------------
        //Кропка Пуск главный экран программы
        //------------------------------------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            string process_name = "";
            button2.Enabled = false;
            console1.Clear();
            console1.ForeColor = Color.Yellow;
            console1.ScrollToCaret();

            while (i_time_begin != 0)
            {
                OnTimedEvent();
                Thread.Sleep(1000);
            }

            timer_delay_key_50.Enabled = true;
            console1.Clear();

            if (!Loco.open_process("launcher"))
            {
                console1.ForeColor = Color.Red;
                button_stop.PerformClick();
            }
            else process_name = "launcher";

            if (!Loco.open_process("launcher"))
            {
                console1.Clear();
                console1.ForeColor = Color.Red;
                console1.AppendText("Процесс launcher.exe не найден...");
                return;
            }

            if (process_name == "launcher")
            {
                button2.Enabled = false;
                console1.Clear();
                console1.ForeColor = Color.LawnGreen;
                console1.AppendText("Обнаружен процесс launcher.exe...");
                console1.ForeColor = Color.Yellow;
                console1.AppendText("\r\nПоиск локомотива...");
                Loco.sig_loco = ProcessMemory.find_loco();
                select_loco();
            }

            if (i_loco_find == 1)
            {
                Thread.Sleep(2000);

                if (i_priem_peredacha == 1)
                {
                    timer_bdit.Enabled = true;
                    timer_delay_key_50.Enabled = true;
                    open_COM();

                    if (i_port_COM_open == 0)
                    {
                        console1.ForeColor = Color.Red;
                        console1.AppendText("\r\nВыберите COM порт или смените режим только передача");
                    }
                }
                else if (i_priem_peredacha == 2)
                {
                    timer_joystick_update.Enabled = true;
                    timer_bdit.Enabled = true;
                    timer_delay_key_50.Enabled = true;
                    timer_send_HID.Interval = i_delay_send_HID;
                    timer_send_HID.Enabled = true;
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nИдет передача данных...");
                }
                else if (i_priem_peredacha == 3)
                {
                    open_COM();

                    if (i_port_COM_open == 0)
                    {
                        console1.ForeColor = Color.Red;
                        console1.AppendText("\r\nВыберите COM порт или смените режим только передача");
                    }

                    if (i_port_COM_open == 1)
                    {
                        timer_joystick_update.Enabled = true;
                        timer_bdit.Enabled = true;
                        timer_delay_key_50.Enabled = true;
                        timer_send_HID.Interval = i_delay_send_HID;
                        timer_send_HID.Enabled = true;
                        console1.ForeColor = Color.LawnGreen;
                        console1.AppendText("\r\nИдет прием/передача данных...");
                    }
                }
                else
                {
                    console1.ForeColor = Color.Red;
                    console1.AppendText("\r\nРежим приема/передачи не выбран!");
                }
            }
            else
            {
                button_stop.PerformClick();
                console1.ForeColor = Color.Red;
                console1.AppendText("\r\nЛокомотив и данные на 1 стадии не найдены !\r\nСтадия 2 - отмена");
            }
        }

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        private void but_settings_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Owner = this;
            f2.ShowDialog();
        }

        //------------------------------------------------------------------------------------
        //Кнопка Демо режима главное окно программы
        //------------------------------------------------------------------------------------
        private void button_demo_Click(object sender, EventArgs e)
        {
            timer_delay_key_50.Enabled = true;
            string process_name = "";
            button2.Enabled = false;
            button_demo.Enabled = false;
            console1.ForeColor = Color.Yellow;
            console1.ScrollToCaret();

            while (i_time_begin != 0)
            {
                OnTimedEvent();
                Thread.Sleep(1000);
            }
            i_time_begin = Convert.ToInt16(numericUpDown_time.Value);

            if (!Loco.open_process("zlauncher"))
            {
                console1.ForeColor = Color.Red;
                button_stop.PerformClick();
            }
            else process_name = "zlauncher";

            if (!Loco.open_process("launcher"))
            {
                console1.ForeColor = Color.Red;
                button_stop.PerformClick();
            }
            else process_name = "launcher";

            if (!Loco.open_process("zlauncher") && !Loco.open_process("launcher"))
            {
                console1.Clear();
                console1.ForeColor = Color.Red;
                console1.AppendText("Процесс zlauncher.exe не найден...");
                console1.AppendText("Процесс launcher.exe не найден...");
                return;
            }

            if (process_name == "zlauncher")
            {
                button2.Enabled = false;
                button_demo.Enabled = false;
                console1.Clear();
                console1.ForeColor = Color.LawnGreen;
                console1.AppendText("Обнаружен процесс zlauncher.exe...");
                console1.ForeColor = Color.Yellow;
                console1.AppendText("\r\nПоиск локомотива...");
                Loco.sig_loco = ProcessMemory.find_loco();
                select_loco();
            }

            if (process_name == "launcher")
            {
                button2.Enabled = false;
                button_demo.Enabled = false;
                console1.Clear();
                console1.ForeColor = Color.LawnGreen;
                console1.AppendText("Обнаружен процесс launcher.exe...");
                console1.ForeColor = Color.Yellow;
                console1.AppendText("\r\nПоиск локомотива...");
                Loco.sig_loco = ProcessMemory.find_loco();
                select_loco();
            }

            Thread.Sleep(3000);
            console1.ForeColor = Color.Yellow;
            console1.AppendText("\r\nИдет демо передача...");
            timer2.Enabled = true;
        }

        //------------------------------------------------------------------------------------
        //Кнопка Стоп главное окно программы
        //------------------------------------------------------------------------------------
        private void button_stop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer_bdit.Enabled = false;
            timer_delay_key_50.Enabled = false;

            timer_joystick_update.Enabled = false;
            timer_send_HID.Enabled = false;
            LocoMemoryHelpers.InitBuffers();
            Loco.sig_loco = 0;

            Thread.Sleep(300);
            i_port_COM_open = 0;

            try
            {
                if (port.IsOpen)
                {
                    port.Close();
                }
            }

            catch
            {
                
            }

            Thread.Sleep(300);
            DeleteXmlBinFiles();
            button2.Enabled = true;
            button_demo.Enabled = true;
            console1.Clear();
            console1.ForeColor = Color.Yellow;
            console1.ScrollToCaret();
            console1.AppendText("Ожидание связи...");
            console1.AppendText("\r\nЗапустите игру и нажмите Пуск...");
        }

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        private void button_about_Click(object sender, EventArgs e)
        {
          MessageBox.Show("v8.1.0 \nДля \n-zdsimulator 54.006 + ZTE_for_ED4m_lamp");
        }

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        private void combobox_Port_SelectedIndexChanged(object sender, EventArgs e)
        {
            i_COM = combobox_Port.Text;
        }

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        private void numericUpDown_skorCOM_ValueChanged(object sender, EventArgs e)
        {
            i_skor_COM = Convert.ToInt32(numericUpDown_skorCOM.Value);
        }

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        private void numericUpDown_time_ValueChanged(object sender, EventArgs e)
        {
            i_time_begin = Convert.ToInt16(numericUpDown_time.Value) * 60;
        }

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        private void numericUpDown_delay_ValueChanged(object sender, EventArgs e)
        {
            i_delay_send = Convert.ToInt16(numericUpDown_delay.Value);
        }

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        private void numericUpDown_delay_HID_ValueChanged(object sender, EventArgs e)
        {
            i_delay_send_HID = Convert.ToInt16(numericUpDown_delay.Value);
        }

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        private void Init_COM()
        {
            // получаем список доступных портов
            buffer_COM_ports = SerialPort.GetPortNames();
        }

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        private void open_COM()
        {
            if (combobox_Port.Text == "")
            {
                i_port_COM_open = 0;
                return;
            }

            port = new SerialPort();

            if (!(port.IsOpen))
            {
                try
                {
                    // настройки порта
                    port.PortName = i_COM;
                    port.BaudRate = i_skor_COM;
                    port.DataBits = 8;
                    port.Parity = Parity.None;
                    port.StopBits = StopBits.One;
                    port.ReadTimeout = 1000;
                    port.WriteTimeout = 1000;
                    port.Open();
                }

                catch (Exception e)
                {
                    console1.ForeColor = Color.Red;
                    console1.AppendText("\r\nERROR: невозможно открыть порт:" + e.ToString());
                    return;
                }
            }

            i_port_COM_open = 1;
            console1.ForeColor = Color.LawnGreen;
            console1.AppendText("\r\nПорт открыт, идет прием данных...");

            Thread.Sleep(2000);

            timer1.Interval = i_delay_send;
            timer1.Enabled = true;
        }

        //------------------------------------------------------------------------------------
        //Инициализация джойстика
        //------------------------------------------------------------------------------------
        public void Joystick_init()
        {
            foreach (DeviceInstance instance in Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly))
            {
                device = new Device(instance.ProductGuid);
                device.SetCooperativeLevel(null, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
                
                foreach (DeviceObjectInstance doi in device.Objects)
                {
                    if ((doi.ObjectId & (int)DeviceObjectTypeFlags.Axis) != 0)
                    {
                        device.Properties.SetRange(ParameterHow.ById,doi.ObjectId, new InputRange(0, 65535));
                                                                                 //new InputRange(-32768, 32768));
                    }
                }

                device.Acquire();
                console1.ForeColor = Color.Yellow;
                console1.ScrollToCaret();
                console1.AppendText("\r\nJoy: " + device.DeviceInformation.InstanceName);
                console1.AppendText("\r\nОсей: " + device.Caps.NumberAxes.ToString());
                console1.AppendText("\r\nКнопок: " + device.Caps.NumberButtons.ToString());
                console1.AppendText("\r\nPOV: " + device.Caps.NumberPointOfViews.ToString());
                i_joy_name = Convert.ToString(device.DeviceInformation.InstanceName);
            }
            if (device == null) i_joy_name = "";
        }

        //------------------------------------------------------------------------------------
        //Обновление состояния джойстика
        //------------------------------------------------------------------------------------
        public void UpdateJoystickState()
        {
            int[] b_temp;
            JoystickState j = device.CurrentJoystickState;
            
            joystick_axis_buffer[0] = j.ARx;//обычные оси
            joystick_axis_buffer[1] = j.ARy;//обычные оси
            joystick_axis_buffer[2] = j.ARz;//обычные оси
            joystick_axis_buffer[3] = j.AX; //обычные оси
            joystick_axis_buffer[4] = j.AY; //обычные оси
            joystick_axis_buffer[5] = j.AZ; //обычные оси
            joystick_axis_buffer[6] = j.FRx;//обычные оси
            joystick_axis_buffer[7] = j.FRy;//обычные оси
            joystick_axis_buffer[8] = j.FRz;//обычные оси
            joystick_axis_buffer[9] = j.FX; //обычные оси
            joystick_axis_buffer[10] = j.FY;//обычные оси
            joystick_axis_buffer[11] = j.FZ;//обычные оси
            joystick_axis_buffer[12] = j.Rx;//обычные оси
            joystick_axis_buffer[13] = j.Ry;//обычные оси
            joystick_axis_buffer[14] = j.Rz;//обычные оси
            joystick_axis_buffer[15] = j.VRx;//обычные оси
            joystick_axis_buffer[16] = j.VRy;//обычные оси
            joystick_axis_buffer[17] = j.VRz;//обычные оси
            joystick_axis_buffer[18] = j.VX;//обычные оси
            joystick_axis_buffer[19] = j.VY;//обычные оси
            joystick_axis_buffer[20] = j.VZ;//обычные оси
            joystick_axis_buffer[21] = j.X;//обычные оси
            joystick_axis_buffer[22] = j.Y;//обычные оси
            joystick_axis_buffer[23] = j.Z;//обычные оси
            b_temp = j.GetPointOfView();//хатка
            joystick_axis_buffer[24] = b_temp[0];
            b_temp = j.GetSlider();//газ
            joystick_axis_buffer[25] = b_temp[0];
            b_temp = j.GetASlider();//...
            joystick_axis_buffer[26] = b_temp[0];
            b_temp = j.GetFSlider();//...
            joystick_axis_buffer[27] = b_temp[0];
            b_temp = j.GetVSlider();//...
            joystick_axis_buffer[28] = b_temp[0];
            joystick_buttons_buffer = j.GetButtons();//кнопки
        }

        //------------------------------------------------------------------------------------
        //Обновление состояния кнопок
        //------------------------------------------------------------------------------------
        public void UpdateLocoButtons()
        {
            int i_temp = 0;

            if (Loco.i_process_name == 6)
            {
                //проверяем кнопки в буфере Controls
                if (Controls_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.svistok = 1;
                    }
                    else LocoButtons.svistok = 0;
                }

                if (Controls_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.tifon = 1;
                    }
                    else LocoButtons.tifon = 0;
                }

                if (Controls_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.kran395_0 = 1;
                    }
                    else LocoButtons.kran395_0 = 0;
                }

                if (Controls_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.kran395_1 = 1;
                    }
                    else LocoButtons.kran395_1 = 0;
                }

                if (Controls_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.kran395_2 = 1;
                    }
                    else LocoButtons.kran395_2 = 0;
                }

                if (Controls_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.kran395_3 = 1;
                    }
                    else LocoButtons.kran395_3 = 0;
                }

                if (Controls_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.kran395_4 = 1;
                    }
                    else LocoButtons.kran395_4 = 0;
                }

                if (Controls_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.kran395_5 = 1;
                    }
                    else LocoButtons.kran395_5 = 0;
                }

                if (Controls_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.kran395_6 = 1;
                    }
                    else LocoButtons.kran395_6 = 0;
                }

                if (Controls_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.kran254_0 = 1;
                    }
                    else LocoButtons.kran254_0 = 0;
                }

                if (Controls_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.kran254_1 = 1;
                    }
                    else LocoButtons.kran254_1 = 0;
                }

                if (Controls_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.kran254_2 = 1;
                    }
                    else LocoButtons.kran254_2 = 0;
                }

                if (Controls_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.kran254_3 = 1;
                    }
                    else LocoButtons.kran254_3 = 0;
                }

                if (Controls_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.kran254_4 = 1;
                    }
                    else LocoButtons.kran254_4 = 0;
                }

                if (Controls_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.kran254_5 = 1;
                    }
                    else LocoButtons.kran254_5 = 0;
                }

                if (Controls_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.vid_vlevo = 1;
                    }
                    else LocoButtons.vid_vlevo = 0;
                }

                if (Controls_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.vid_vpravo = 1;
                    }
                    else LocoButtons.vid_vpravo = 0;
                }

                if (Controls_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.vid_vverh = 1;
                    }
                    else LocoButtons.vid_vverh = 0;
                }

                if (Controls_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.vid_vniz = 1;
                    }
                    else LocoButtons.vid_vniz = 0;
                }

                if (Controls_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.vid_zoom_in = 1;
                    }
                    else LocoButtons.vid_zoom_in = 0;
                }

                if (Controls_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.vid_zoom_out = 1;
                    }
                    else LocoButtons.vid_zoom_out = 0;
                }

                if (Controls_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.vid_outside = 1;
                    }
                    else LocoButtons.vid_outside = 0;
                }

                if (Controls_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.vid_vpered = 1;
                    }
                    else LocoButtons.vid_vpered = 0;
                }

                if (Controls_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.vid_nazad = 1;
                    }
                    else LocoButtons.vid_nazad = 0;
                }

                if (Controls_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.protyazhka_lenty = 1;
                    }
                    else LocoButtons.protyazhka_lenty = 0;
                }

                if (Controls_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.bdit_Z = 1;
                    }
                    else LocoButtons.bdit_Z = 0;
                }

                if (Controls_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.bdit_M = 1;
                    }
                    else LocoButtons.bdit_M = 0;
                }

                if (Controls_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.pesok = 1;
                    }
                    else LocoButtons.pesok = 0;
                }

                if (Controls_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.dvorniki_0 = 1;
                    }
                    else LocoButtons.dvorniki_0 = 0;
                }

                if (Controls_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.dvorniki_1 = 1;
                    }
                    else LocoButtons.dvorniki_1 = 0;
                }

                if (Controls_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.dvorniki_2 = 1;
                    }
                    else LocoButtons.dvorniki_2 = 0;
                }

                if (Controls_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.dvorniki_3 = 1;
                    }
                    else LocoButtons.dvorniki_3 = 0;
                }

                if (Controls_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.dvorniki_4 = 1;
                    }
                    else LocoButtons.dvorniki_4 = 0;
                }

                if (Controls_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[Controls_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.dvorniki_5 = 1;
                    }
                    else LocoButtons.dvorniki_5 = 0;
                }

                //проверяем кнопки в буфере нештатки
                for (int i = 0; i < 100; i++)
                {
                    if (Neshtatki_key_buffer[i] != 0)
                    {
                        if (joystick_buttons_buffer[Neshtatki_key_buffer[i] - 1] != 0)
                        {
                            LocoButtons.b_neshtatki[i] = 1;
                        }
                        else LocoButtons.b_neshtatki[i] = 0;
                    }
                }  
            }    
            
            //проверяем кнопки в буфере 2ES5K
            if (Loco.sig_loco == 1)
            {
                if (ES5K_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_0 = 1;
                    }
                    else LocoButtons.es5k_kontr_0 = 0;
                }
                if (ES5K_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h4 = 1;
                    }
                    else LocoButtons.es5k_kontr_h4 = 0;
                }
                if (ES5K_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h5 = 1;
                    }
                    else LocoButtons.es5k_kontr_h5 = 0;
                }
                if (ES5K_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h6 = 1;
                    }
                    else LocoButtons.es5k_kontr_h6 = 0;
                }
                if (ES5K_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h7 = 1;
                    }
                    else LocoButtons.es5k_kontr_h7 = 0;
                }
                if (ES5K_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h8 = 1;
                    }
                    else LocoButtons.es5k_kontr_h8 = 0;
                }
                if (ES5K_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h9 = 1;
                    }
                    else LocoButtons.es5k_kontr_h9 = 0;
                }
                if (ES5K_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h10 = 1;
                    }
                    else LocoButtons.es5k_kontr_h10 = 0;
                }
                if (ES5K_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h11 = 1;
                    }
                    else LocoButtons.es5k_kontr_h11 = 0;
                }
                if (ES5K_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h12 = 1;
                    }
                    else LocoButtons.es5k_kontr_h12 = 0;
                }
                if (ES5K_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h13 = 1;
                    }
                    else LocoButtons.es5k_kontr_h13 = 0;
                }
                if (ES5K_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h14 = 1;
                    }
                    else LocoButtons.es5k_kontr_h14 = 0;
                }
                if (ES5K_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h15 = 1;
                    }
                    else LocoButtons.es5k_kontr_h15 = 0;
                }
                if (ES5K_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h16 = 1;
                    }
                    else LocoButtons.es5k_kontr_h16 = 0;
                }
                if (ES5K_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h17 = 1;
                    }
                    else LocoButtons.es5k_kontr_h17 = 0;
                }
                if (ES5K_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h18 = 1;
                    }
                    else LocoButtons.es5k_kontr_h18 = 0;
                }
                if (ES5K_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h19 = 1;
                    }
                    else LocoButtons.es5k_kontr_h19 = 0;
                }
                if (ES5K_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h20 = 1;
                    }
                    else LocoButtons.es5k_kontr_h20 = 0;
                }
                if (ES5K_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h21 = 1;
                    }
                    else LocoButtons.es5k_kontr_h21 = 0;
                }
                if (ES5K_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h22 = 1;
                    }
                    else LocoButtons.es5k_kontr_h22 = 0;
                }
                if (ES5K_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h23 = 1;
                    }
                    else LocoButtons.es5k_kontr_h23 = 0;
                }
                if (ES5K_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h24 = 1;
                    }
                    else LocoButtons.es5k_kontr_h24 = 0;
                }
                if (ES5K_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h25 = 1;
                    }
                    else LocoButtons.es5k_kontr_h25 = 0;
                }
                if (ES5K_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h26 = 1;
                    }
                    else LocoButtons.es5k_kontr_h26 = 0;
                }
                if (ES5K_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h27 = 1;
                    }
                    else LocoButtons.es5k_kontr_h27 = 0;
                }
                if (ES5K_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h28 = 1;
                    }
                    else LocoButtons.es5k_kontr_h28 = 0;
                }
                if (ES5K_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h29 = 1;
                    }
                    else LocoButtons.es5k_kontr_h29 = 0;
                }
                if (ES5K_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h30 = 1;
                    }
                    else LocoButtons.es5k_kontr_h30 = 0;
                }
                if (ES5K_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h31 = 1;
                    }
                    else LocoButtons.es5k_kontr_h31 = 0;
                }
                if (ES5K_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h32 = 1;
                    }
                    else LocoButtons.es5k_kontr_h32 = 0;
                }
                if (ES5K_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h33 = 1;
                    }
                    else LocoButtons.es5k_kontr_h33 = 0;
                }
                if (ES5K_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h34 = 1;
                    }
                    else LocoButtons.es5k_kontr_h34 = 0;
                }
                if (ES5K_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h35 = 1;
                    }
                    else LocoButtons.es5k_kontr_h35 = 0;
                }
                if (ES5K_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_h36 = 1;
                    }
                    else LocoButtons.es5k_kontr_h36 = 0;
                }
                if (ES5K_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t4 = 1;
                    }
                    else LocoButtons.es5k_kontr_t4 = 0;
                }
                if (ES5K_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t5 = 1;
                    }
                    else LocoButtons.es5k_kontr_t5 = 0;
                }
                if (ES5K_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t6 = 1;
                    }
                    else LocoButtons.es5k_kontr_t6 = 0;
                }
                if (ES5K_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t7 = 1;
                    }
                    else LocoButtons.es5k_kontr_t7 = 0;
                }
                if (ES5K_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t8 = 1;
                    }
                    else LocoButtons.es5k_kontr_t8 = 0;
                }
                if (ES5K_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t9 = 1;
                    }
                    else LocoButtons.es5k_kontr_t9 = 0;
                }
                if (ES5K_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t10 = 1;
                    }
                    else LocoButtons.es5k_kontr_t10 = 0;
                }
                if (ES5K_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t11 = 1;
                    }
                    else LocoButtons.es5k_kontr_t11 = 0;
                }
                if (ES5K_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t12 = 1;
                    }
                    else LocoButtons.es5k_kontr_t12 = 0;
                }
                if (ES5K_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t13 = 1;
                    }
                    else LocoButtons.es5k_kontr_t13 = 0;
                }
                if (ES5K_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t14 = 1;
                    }
                    else LocoButtons.es5k_kontr_t14 = 0;
                }
                if (ES5K_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t15 = 1;
                    }
                    else LocoButtons.es5k_kontr_t15 = 0;
                }
                if (ES5K_key_buffer[46] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[46] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t16 = 1;
                    }
                    else LocoButtons.es5k_kontr_t16 = 0;
                }
                if (ES5K_key_buffer[47] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[47] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t17 = 1;
                    }
                    else LocoButtons.es5k_kontr_t17 = 0;
                }
                if (ES5K_key_buffer[48] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[48] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t18 = 1;
                    }
                    else LocoButtons.es5k_kontr_t18 = 0;
                }
                if (ES5K_key_buffer[49] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[49] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t19 = 1;
                    }
                    else LocoButtons.es5k_kontr_t19 = 0;
                }
                if (ES5K_key_buffer[50] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[50] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t20 = 1;
                    }
                    else LocoButtons.es5k_kontr_t20 = 0;
                }
                if (ES5K_key_buffer[51] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[51] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t21 = 1;
                    }
                    else LocoButtons.es5k_kontr_t21 = 0;
                }
                if (ES5K_key_buffer[52] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[52] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t22 = 1;
                    }
                    else LocoButtons.es5k_kontr_t22 = 0;
                }
                if (ES5K_key_buffer[53] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[53] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t23 = 1;
                    }
                    else LocoButtons.es5k_kontr_t23 = 0;
                }
                if (ES5K_key_buffer[54] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[54] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t24 = 1;
                    }
                    else LocoButtons.es5k_kontr_t24 = 0;
                }
                if (ES5K_key_buffer[55] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[55] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t25 = 1;
                    }
                    else LocoButtons.es5k_kontr_t25 = 0;
                }
                if (ES5K_key_buffer[56] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[56] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t26 = 1;
                    }
                    else LocoButtons.es5k_kontr_t26 = 0;
                }
                if (ES5K_key_buffer[57] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[57] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t27 = 1;
                    }
                    else LocoButtons.es5k_kontr_t27 = 0;
                }
                if (ES5K_key_buffer[58] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[58] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t28 = 1;
                    }
                    else LocoButtons.es5k_kontr_t28 = 0;
                }
                if (ES5K_key_buffer[59] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[59] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t29 = 1;
                    }
                    else LocoButtons.es5k_kontr_t29 = 0;
                }
                if (ES5K_key_buffer[60] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[60] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t30 = 1;
                    }
                    else LocoButtons.es5k_kontr_t30 = 0;
                }
                if (ES5K_key_buffer[61] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[61] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t31 = 1;
                    }
                    else LocoButtons.es5k_kontr_t31 = 0;
                }
                if (ES5K_key_buffer[62] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[62] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t32 = 1;
                    }
                    else LocoButtons.es5k_kontr_t32 = 0;
                }
                if (ES5K_key_buffer[63] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[63] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t33 = 1;
                    }
                    else LocoButtons.es5k_kontr_t33 = 0;
                }
                if (ES5K_key_buffer[64] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[64] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t34 = 1;
                    }
                    else LocoButtons.es5k_kontr_t34 = 0;
                }
                if (ES5K_key_buffer[65] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[65] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t35 = 1;
                    }
                    else LocoButtons.es5k_kontr_t35 = 0;
                }
                if (ES5K_key_buffer[66] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[66] - 1] != 0)
                    {
                        LocoButtons.es5k_kontr_t36 = 1;
                    }
                    else LocoButtons.es5k_kontr_t36 = 0;
                }
                if (ES5K_key_buffer[67] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[67] - 1] != 0)
                    {
                        LocoButtons.es5k_rev_0 = 1;
                    }
                    else LocoButtons.es5k_rev_0 = 0;
                }
                if (ES5K_key_buffer[68] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[68] - 1] != 0)
                    {
                        LocoButtons.es5k_rev_vpered = 1;
                    }
                    else LocoButtons.es5k_rev_vpered = 0;
                }
                if (ES5K_key_buffer[69] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[69] - 1] != 0)
                    {
                        LocoButtons.es5k_rev_nazad = 1;
                    }
                    else LocoButtons.es5k_rev_nazad = 0;
                }
                if (ES5K_key_buffer[70] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[71] - 1] != 0)
                    {
                        LocoButtons.es5k_reg_skor_140 = 1;
                    }
                    else LocoButtons.es5k_reg_skor_140 = 0;
                }
                if (ES5K_key_buffer[71] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[71] - 1] != 0)
                    {
                        LocoButtons.es5k_reg_skor_plus = 1;
                    }
                    else LocoButtons.es5k_reg_skor_plus = 0;
                }
                if (ES5K_key_buffer[72] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[72] - 1] != 0)
                    {
                        LocoButtons.es5k_reg_skor_minus = 1;
                    }
                    else LocoButtons.es5k_reg_skor_minus = 0;
                }
                if (ES5K_key_buffer[73] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[73] - 1] != 0)
                    {
                        LocoButtons.es5k_kranTM_0 = 1;
                    }
                    else LocoButtons.es5k_kranTM_0 = 0;
                }
                if (ES5K_key_buffer[74] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[74] - 1] != 0)
                    {
                        LocoButtons.es5k_kranTM_1 = 1;
                    }
                    else LocoButtons.es5k_kranTM_1 = 0;
                }
                if (ES5K_key_buffer[75] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[75] - 1] != 0)
                    {
                        LocoButtons.es5k_bv_0 = 1;
                    }
                    else LocoButtons.es5k_bv_0 = 0;
                }
                if (ES5K_key_buffer[76] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[76] - 1] != 0)
                    {
                        LocoButtons.es5k_bv_1 = 1;
                    }
                    else LocoButtons.es5k_bv_1 = 0;
                }
                if (ES5K_key_buffer[77] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[77] - 1] != 0)
                    {
                        LocoButtons.es5k_vozvrat_bv = 1;
                    }
                    else LocoButtons.es5k_vozvrat_bv = 0;
                }
                if (ES5K_key_buffer[78] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[78] - 1] != 0)
                    {
                        LocoButtons.es5k_tokopr_per_0 = 1;
                    }
                    else LocoButtons.es5k_tokopr_per_0 = 0;
                }
                if (ES5K_key_buffer[79] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[79] - 1] != 0)
                    {
                        LocoButtons.es5k_tokopr_per_1 = 1;
                    }
                    else LocoButtons.es5k_tokopr_per_1 = 0;
                }
                if (ES5K_key_buffer[80] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[80] - 1] != 0)
                    {
                        LocoButtons.es5k_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.es5k_tokopr_zad_0 = 0;
                }
                if (ES5K_key_buffer[81] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[81] - 1] != 0)
                    {
                        LocoButtons.es5k_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.es5k_tokopr_zad_1 = 0;
                }
                if (ES5K_key_buffer[82] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[82] - 1] != 0)
                    {
                        LocoButtons.es5k_upravlenie_0 = 1;
                    }
                    else LocoButtons.es5k_upravlenie_0 = 0;
                }
                if (ES5K_key_buffer[83] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[83] - 1] != 0)
                    {
                        LocoButtons.es5k_upravlenie_1 = 1;
                    }
                    else LocoButtons.es5k_upravlenie_1 = 0;
                }
                if (ES5K_key_buffer[84] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[84] - 1] != 0)
                    {
                        LocoButtons.es5k_komp_0 = 1;
                    }
                    else LocoButtons.es5k_komp_0 = 0;
                }
                if (ES5K_key_buffer[85] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[85] - 1] != 0)
                    {
                        LocoButtons.es5k_komp_1 = 1;
                    }
                    else LocoButtons.es5k_komp_1 = 0;
                }
                if (ES5K_key_buffer[86] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[86] - 1] != 0)
                    {
                        LocoButtons.es5k_vent1_0 = 1;
                    }
                    else LocoButtons.es5k_vent1_0 = 0;
                }
                if (ES5K_key_buffer[87] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[87] - 1] != 0)
                    {
                        LocoButtons.es5k_vent1_1 = 1;
                    }
                    else LocoButtons.es5k_vent1_1 = 0;
                }
                if (ES5K_key_buffer[88] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[88] - 1] != 0)
                    {
                        LocoButtons.es5k_vent2_0 = 1;
                    }
                    else LocoButtons.es5k_vent2_0 = 0;
                }
                if (ES5K_key_buffer[89] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[89] - 1] != 0)
                    {
                        LocoButtons.es5k_vent2_1 = 1;
                    }
                    else LocoButtons.es5k_vent2_1 = 0;
                }
                if (ES5K_key_buffer[90] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[90] - 1] != 0)
                    {
                        LocoButtons.es5k_MSUD_0 = 1;
                    }
                    else LocoButtons.es5k_MSUD_0 = 0;
                }
                if (ES5K_key_buffer[91] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[91] - 1] != 0)
                    {
                        LocoButtons.es5k_MSUD_1 = 1;
                    }
                    else LocoButtons.es5k_MSUD_1 = 0;
                }
                if (ES5K_key_buffer[92] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[92] - 1] != 0)
                    {
                        LocoButtons.es5k_vspom_mash_0 = 1;
                    }
                    else LocoButtons.es5k_vspom_mash_0 = 0;
                }
                if (ES5K_key_buffer[93] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[93] - 1] != 0)
                    {
                        LocoButtons.es5k_vspom_mash_1 = 1;
                    }
                    else LocoButtons.es5k_vspom_mash_1 = 0;
                }
                if (ES5K_key_buffer[94] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[94] - 1] != 0)
                    {
                        LocoButtons.es5k_svet_cab_0 = 1;
                    }
                    else LocoButtons.es5k_svet_cab_0 = 0;
                }
                if (ES5K_key_buffer[95] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[95] - 1] != 0)
                    {
                        LocoButtons.es5k_svet_cab_1 = 1;
                    }
                    else LocoButtons.es5k_svet_cab_1 = 0;
                }
                if (ES5K_key_buffer[96] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[96] - 1] != 0)
                    {
                        LocoButtons.es5k_EPK_0 = 1;
                    }
                    else LocoButtons.es5k_EPK_0 = 0;
                }
                if (ES5K_key_buffer[97] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[97] - 1] != 0)
                    {
                        LocoButtons.es5k_EPK_1 = 1;
                    }
                    else LocoButtons.es5k_EPK_1 = 0;
                }
                if (ES5K_key_buffer[98] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[98] - 1] != 0)
                    {
                        LocoButtons.es5k_sign_0 = 1;
                    }
                    else LocoButtons.es5k_sign_0 = 0;
                }
                if (ES5K_key_buffer[99] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[99] - 1] != 0)
                    {
                        LocoButtons.es5k_sign_1 = 1;
                    }
                    else LocoButtons.es5k_sign_1 = 0;
                }
                if (ES5K_key_buffer[100] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[100] - 1] != 0)
                    {
                        LocoButtons.es5k_signC1_0 = 1;
                    }
                    else LocoButtons.es5k_signC1_0 = 0;
                }
                if (ES5K_key_buffer[101] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[101] - 1] != 0)
                    {
                        LocoButtons.es5k_signC1_1 = 1;
                    }
                    else LocoButtons.es5k_signC1_1 = 0;
                }
                if (ES5K_key_buffer[102] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[102] - 1] != 0)
                    {
                        LocoButtons.es5k_signC2_0 = 1;
                    }
                    else LocoButtons.es5k_signC2_0 = 0;
                }
                if (ES5K_key_buffer[103] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[103] - 1] != 0)
                    {
                        LocoButtons.es5k_signC2_1 = 1;
                    }
                    else LocoButtons.es5k_signC2_1 = 0;
                }
                if (ES5K_key_buffer[104] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[104] - 1] != 0)
                    {
                        LocoButtons.es5k_prozh_0 = 1;
                    }
                    else LocoButtons.es5k_prozh_0 = 0;
                }
                if (ES5K_key_buffer[105] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[105] - 1] != 0)
                    {
                        LocoButtons.es5k_prozh_1 = 1;
                    }
                    else LocoButtons.es5k_prozh_1 = 0;
                }
                if (ES5K_key_buffer[106] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[106] - 1] != 0)
                    {
                        LocoButtons.es5k_prozh_2 = 1;
                    }
                    else LocoButtons.es5k_prozh_2 = 0;
                }
                if (ES5K_key_buffer[107] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[107] - 1] != 0)
                    {
                        LocoButtons.es5k_avtoreg_0 = 1;
                    }
                    else LocoButtons.es5k_avtoreg_0 = 0;
                }
                if (ES5K_key_buffer[108] != 0)
                {
                    if (joystick_buttons_buffer[ES5K_key_buffer[108] - 1] != 0)
                    {
                        LocoButtons.es5k_avtoreg_1 = 1;
                    }
                    else LocoButtons.es5k_avtoreg_1 = 0;
                }
            }

            //проверяем кнопки в буфере EP1M
            if (Loco.sig_loco == 2)
            {
                if (EP1M_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_0 = 1;
                    }
                    else LocoButtons.ep1m_kontr_0 = 0;
                }
                if (EP1M_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h4 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h4 = 0;
                }
                if (EP1M_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h5 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h5 = 0;
                }
                if (EP1M_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h6 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h6 = 0;
                }
                if (EP1M_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h7 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h7 = 0;
                }
                if (EP1M_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h8 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h8 = 0;
                }
                if (EP1M_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h9 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h9 = 0;
                }
                if (EP1M_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h10 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h10 = 0;
                }
                if (EP1M_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h11 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h11 = 0;
                }
                if (EP1M_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h12 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h12 = 0;
                }
                if (EP1M_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h13 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h13 = 0;
                }
                if (EP1M_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h14 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h14 = 0;
                }
                if (EP1M_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h15 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h15 = 0;
                }
                if (EP1M_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h16 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h16 = 0;
                }
                if (EP1M_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h17 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h17 = 0;
                }
                if (EP1M_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h18 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h18 = 0;
                }
                if (EP1M_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h19 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h19 = 0;
                }
                if (EP1M_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h20 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h20 = 0;
                }
                if (EP1M_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h21 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h21 = 0;
                }
                if (EP1M_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h22 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h22 = 0;
                }
                if (EP1M_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h23 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h23 = 0;
                }
                if (EP1M_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h24 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h24 = 0;
                }
                if (EP1M_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h25 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h25 = 0;
                }
                if (EP1M_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h26 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h26 = 0;
                }
                if (EP1M_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h27 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h27 = 0;
                }
                if (EP1M_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h28 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h28 = 0;
                }
                if (EP1M_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h29 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h29 = 0;
                }
                if (EP1M_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h30 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h30 = 0;
                }
                if (EP1M_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h31 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h31 = 0;
                }
                if (EP1M_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h32 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h32 = 0;
                }
                if (EP1M_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h33 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h33 = 0;
                }
                if (EP1M_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h34 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h34 = 0;
                }
                if (EP1M_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h35 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h35 = 0;
                }
                if (EP1M_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_h36 = 1;
                    }
                    else LocoButtons.ep1m_kontr_h36 = 0;
                }
                if (EP1M_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t4 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t4 = 0;
                }
                if (EP1M_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t5 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t5 = 0;
                }
                if (EP1M_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t6 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t6 = 0;
                }
                if (EP1M_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t7 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t7 = 0;
                }
                if (EP1M_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t8 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t8 = 0;
                }
                if (EP1M_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t9 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t9 = 0;
                }
                if (EP1M_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t10 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t10 = 0;
                }
                if (EP1M_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t11 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t11 = 0;
                }
                if (EP1M_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t12 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t12 = 0;
                }
                if (EP1M_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t13 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t13 = 0;
                }
                if (EP1M_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t14 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t14 = 0;
                }
                if (EP1M_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t15 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t15 = 0;
                }
                if (EP1M_key_buffer[46] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[46] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t16 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t16 = 0;
                }
                if (EP1M_key_buffer[47] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[47] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t17 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t17 = 0;
                }
                if (EP1M_key_buffer[48] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[48] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t18 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t18 = 0;
                }
                if (EP1M_key_buffer[49] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[49] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t19 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t19 = 0;
                }
                if (EP1M_key_buffer[50] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[50] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t20 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t20 = 0;
                }
                if (EP1M_key_buffer[51] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[51] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t21 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t21 = 0;
                }
                if (EP1M_key_buffer[52] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[52] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t22 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t22 = 0;
                }
                if (EP1M_key_buffer[53] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[53] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t23 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t23 = 0;
                }
                if (EP1M_key_buffer[54] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[54] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t24 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t24 = 0;
                }
                if (EP1M_key_buffer[55] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[55] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t25 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t25 = 0;
                }
                if (EP1M_key_buffer[56] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[56] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t26 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t26 = 0;
                }
                if (EP1M_key_buffer[57] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[57] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t27 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t27 = 0;
                }
                if (EP1M_key_buffer[58] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[58] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t28 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t28 = 0;
                }
                if (EP1M_key_buffer[59] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[59] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t29 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t29 = 0;
                }
                if (EP1M_key_buffer[60] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[60] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t30 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t30 = 0;
                }
                if (EP1M_key_buffer[61] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[61] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t31 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t31 = 0;
                }
                if (EP1M_key_buffer[62] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[62] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t32 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t32 = 0;
                }
                if (EP1M_key_buffer[63] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[63] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t33 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t33 = 0;
                }
                if (EP1M_key_buffer[64] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[64] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t34 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t34 = 0;
                }
                if (EP1M_key_buffer[65] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[65] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t35 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t35 = 0;
                }
                if (EP1M_key_buffer[66] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[66] - 1] != 0)
                    {
                        LocoButtons.ep1m_kontr_t36 = 1;
                    }
                    else LocoButtons.ep1m_kontr_t36 = 0;
                }
                if (EP1M_key_buffer[67] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[67] - 1] != 0)
                    {
                        LocoButtons.ep1m_rev_0 = 1;
                    }
                    else LocoButtons.ep1m_rev_0 = 0;
                }
                if (EP1M_key_buffer[68] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[68] - 1] != 0)
                    {
                        LocoButtons.ep1m_rev_vpered = 1;
                    }
                    else LocoButtons.ep1m_rev_vpered = 0;
                }
                if (EP1M_key_buffer[69] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[69] - 1] != 0)
                    {
                        LocoButtons.ep1m_rev_nazad = 1;
                    }
                    else LocoButtons.ep1m_rev_nazad = 0;
                }
                if (EP1M_key_buffer[70] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[70] - 1] != 0)
                    {
                        LocoButtons.ep1m_reg_skor_160 = 1;
                    }
                    else LocoButtons.ep1m_reg_skor_160 = 0;
                }
                if (EP1M_key_buffer[71] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[71] - 1] != 0)
                    {
                        LocoButtons.ep1m_reg_skor_plus = 1;
                    }
                    else LocoButtons.ep1m_reg_skor_plus = 0;
                }
                if (EP1M_key_buffer[72] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[72] - 1] != 0)
                    {
                        LocoButtons.ep1m_reg_skor_minus = 1;
                    }
                    else LocoButtons.ep1m_reg_skor_minus = 0;
                }
                if (EP1M_key_buffer[73] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[73] - 1] != 0)
                    {
                        LocoButtons.ep1m_kranTM_0 = 1;
                    }
                    else LocoButtons.ep1m_kranTM_0 = 0;
                }
                if (EP1M_key_buffer[74] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[74] - 1] != 0)
                    {
                        LocoButtons.ep1m_kranTM_1 = 1;
                    }
                    else LocoButtons.ep1m_kranTM_1 = 0;
                }
                if (EP1M_key_buffer[75] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[75] - 1] != 0)
                    {
                        LocoButtons.ep1m_bv_0 = 1;
                    }
                    else LocoButtons.ep1m_bv_0 = 0;
                }
                if (EP1M_key_buffer[76] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[76] - 1] != 0)
                    {
                        LocoButtons.ep1m_bv_1 = 1;
                    }
                    else LocoButtons.ep1m_bv_1 = 0;
                }
                if (EP1M_key_buffer[77] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[77] - 1] != 0)
                    {
                        LocoButtons.ep1m_vozvrat_zaschity = 1;
                    }
                    else LocoButtons.ep1m_vozvrat_zaschity = 0;
                }
                if (EP1M_key_buffer[78] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[78] - 1] != 0)
                    {
                        LocoButtons.ep1m_blok_vvk_0 = 1;
                    }
                    else LocoButtons.ep1m_blok_vvk_0 = 0;
                }
                if (EP1M_key_buffer[79] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[79] - 1] != 0)
                    {
                        LocoButtons.ep1m_blok_vvk_1 = 1;
                    }
                    else LocoButtons.ep1m_blok_vvk_1 = 0;
                }
                if (EP1M_key_buffer[80] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[80] - 1] != 0)
                    {
                        LocoButtons.ep1m_tokopr_per_0 = 1;
                    }
                    else LocoButtons.ep1m_tokopr_per_0 = 0;
                }
                if (EP1M_key_buffer[81] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[81] - 1] != 0)
                    {
                        LocoButtons.ep1m_tokopr_per_1 = 1;
                    }
                    else LocoButtons.ep1m_tokopr_per_1 = 0;
                }
                if (EP1M_key_buffer[82] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[82] - 1] != 0)
                    {
                        LocoButtons.ep1m_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.ep1m_tokopr_zad_0 = 0;
                }
                if (EP1M_key_buffer[83] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[83] - 1] != 0)
                    {
                        LocoButtons.ep1m_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.ep1m_tokopr_zad_1 = 0;
                }
                if (EP1M_key_buffer[84] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[84] - 1] != 0)
                    {
                        LocoButtons.ep1m_upravlenie = 1;
                    }
                    else LocoButtons.ep1m_upravlenie = 0;
                }
                if (EP1M_key_buffer[85] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[85] - 1] != 0)
                    {
                        LocoButtons.ep1m_upravlenie = 1;
                    }
                    else LocoButtons.ep1m_upravlenie = 0;
                }
                if (EP1M_key_buffer[86] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[86] - 1] != 0)
                    {
                        LocoButtons.ep1m_komp_0 = 1;
                    }
                    else LocoButtons.ep1m_komp_0 = 0;
                }
                if (EP1M_key_buffer[87] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[87] - 1] != 0)
                    {
                        LocoButtons.ep1m_komp_1 = 1;
                    }
                    else LocoButtons.ep1m_komp_1 = 0;
                }
                if (EP1M_key_buffer[88] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[88] - 1] != 0)
                    {
                        LocoButtons.ep1m_vent1_0 = 1;
                    }
                    else LocoButtons.ep1m_vent1_0 = 0;
                }
                if (EP1M_key_buffer[89] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[89] - 1] != 0)
                    {
                        LocoButtons.ep1m_vent1_1 = 1;
                    }
                    else LocoButtons.ep1m_vent1_1 = 0;
                }
                if (EP1M_key_buffer[90] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[90] - 1] != 0)
                    {
                        LocoButtons.ep1m_vent2_0 = 1;
                    }
                    else LocoButtons.ep1m_vent2_0 = 0;
                }
                if (EP1M_key_buffer[91] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[91] - 1] != 0)
                    {
                        LocoButtons.ep1m_vent2_1 = 1;
                    }
                    else LocoButtons.ep1m_vent2_1 = 0;
                }
                if (EP1M_key_buffer[92] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[92] - 1] != 0)
                    {
                        LocoButtons.ep1m_vent3_0 = 1;
                    }
                    else LocoButtons.ep1m_vent3_0 = 0;
                }
                if (EP1M_key_buffer[93] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[93] - 1] != 0)
                    {
                        LocoButtons.ep1m_vent3_1 = 1;
                    }
                    else LocoButtons.ep1m_vent3_1 = 0;
                }
                if (EP1M_key_buffer[94] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[94] - 1] != 0)
                    {
                        LocoButtons.ep1m_MSUD_0 = 1;
                    }
                    else LocoButtons.ep1m_MSUD_0 = 0;
                }
                if (EP1M_key_buffer[95] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[95] - 1] != 0)
                    {
                        LocoButtons.ep1m_MSUD_1 = 1;
                    }
                    else LocoButtons.ep1m_MSUD_1 = 0;
                }
                if (EP1M_key_buffer[96] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[96] - 1] != 0)
                    {
                        LocoButtons.ep1m_vspom_mash_0 = 1;
                    }
                    else LocoButtons.ep1m_vspom_mash_0 = 0;
                }
                if (EP1M_key_buffer[97] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[97] - 1] != 0)
                    {
                        LocoButtons.ep1m_vspom_mash_1 = 1;
                    }
                    else LocoButtons.ep1m_vspom_mash_1 = 0;
                }
                if (EP1M_key_buffer[98] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[98] - 1] != 0)
                    {
                        LocoButtons.ep1m_svet_cab_0 = 1;
                    }
                    else LocoButtons.ep1m_svet_cab_0 = 0;
                }
                if (EP1M_key_buffer[99] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[99] - 1] != 0)
                    {
                        LocoButtons.ep1m_svet_cab_1 = 1;
                    }
                    else LocoButtons.ep1m_svet_cab_1 = 0;
                }
                if (EP1M_key_buffer[100] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[100] - 1] != 0)
                    {
                        LocoButtons.ep1m_svet_cab_2 = 1;
                    }
                    else LocoButtons.ep1m_svet_cab_2 = 0;
                }
                if (EP1M_key_buffer[101] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[101] - 1] != 0)
                    {
                        LocoButtons.ep1m_EPK_0 = 1;
                    }
                    else LocoButtons.ep1m_EPK_0 = 0;
                }
                if (EP1M_key_buffer[102] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[102] - 1] != 0)
                    {
                        LocoButtons.ep1m_EPK_1 = 1;
                    }
                    else LocoButtons.ep1m_EPK_1 = 0;
                }
                if (EP1M_key_buffer[103] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[103] - 1] != 0)
                    {
                        LocoButtons.ep1m_EPT_0 = 1;
                    }
                    else LocoButtons.ep1m_EPT_0 = 0;
                }
                if (EP1M_key_buffer[104] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[104] - 1] != 0)
                    {
                        LocoButtons.ep1m_EPT_1 = 1;
                    }
                    else LocoButtons.ep1m_EPT_1 = 0;
                }
                if (EP1M_key_buffer[105] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[105] - 1] != 0)
                    {
                        LocoButtons.ep1m_sign_0 = 1;
                    }
                    else LocoButtons.ep1m_sign_0 = 0;
                }
                if (EP1M_key_buffer[106] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[106] - 1] != 0)
                    {
                        LocoButtons.ep1m_sign_1 = 1;
                    }
                    else LocoButtons.ep1m_sign_1 = 0;
                }
                if (EP1M_key_buffer[107] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[107] - 1] != 0)
                    {
                        LocoButtons.ep1m_prozh_0 = 1;
                    }
                    else LocoButtons.ep1m_prozh_0 = 0;
                }
                if (EP1M_key_buffer[108] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[108] - 1] != 0)
                    {
                        LocoButtons.ep1m_prozh_1 = 1;
                    }
                    else LocoButtons.ep1m_prozh_1 = 0;
                }
                if (EP1M_key_buffer[109] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[109] - 1] != 0)
                    {
                        LocoButtons.ep1m_prozh_2 = 1;
                    }
                    else LocoButtons.ep1m_prozh_2 = 0;
                }
                if (EP1M_key_buffer[110] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[110] - 1] != 0)
                    {
                        LocoButtons.ep1m_avtoreg_0 = 1;
                    }
                    else LocoButtons.ep1m_avtoreg_0 = 0;
                }
                if (EP1M_key_buffer[111] != 0)
                {
                    if (joystick_buttons_buffer[EP1M_key_buffer[111] - 1] != 0)
                    {
                        LocoButtons.ep1m_avtoreg_1 = 1;
                    }
                    else LocoButtons.ep1m_avtoreg_1 = 0;
                }
            }

            //проверяем кнопки в буфере CHS2K
            if (Loco.sig_loco == 3)
            {
                if (CHS2K_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.chs2k_rev_0 = 1;
                    }
                    else LocoButtons.chs2k_rev_0 = 0;
                }
                if (CHS2K_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.chs2k_rev_vpered = 1;
                    }
                    else LocoButtons.chs2k_rev_vpered = 0;
                }
                if (CHS2K_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.chs2k_rev_nazad = 1;
                    }
                    else LocoButtons.chs2k_rev_nazad = 0;
                }
                if (CHS2K_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.chs2k_kontr_0 = 1;
                    }
                    else LocoButtons.chs2k_kontr_0 = 0;
                }
                if (CHS2K_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.chs2k_kontr_plus = 1;
                    }
                    else LocoButtons.chs2k_kontr_plus = 0;
                }
                if (CHS2K_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.chs2k_kontr_minus = 1;
                    }
                    else LocoButtons.chs2k_kontr_minus = 0;
                }
                if (CHS2K_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.chs2k_kontr_plus1 = 1;
                    }
                    else LocoButtons.chs2k_kontr_plus1 = 0;
                }
                if (CHS2K_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.chs2k_kontr_minus1 = 1;
                    }
                    else LocoButtons.chs2k_kontr_minus1 = 0;
                }
                if (CHS2K_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.chs2k_kranTM_0 = 1;
                    }
                    else LocoButtons.chs2k_kranTM_0 = 0;
                }
                if (CHS2K_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.chs2k_kranTM_1 = 1;
                    }
                    else LocoButtons.chs2k_kranTM_1 = 0;
                }
                if (CHS2K_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.chs2k_bv_0 = 1;
                    }
                    else LocoButtons.chs2k_bv_0 = 0;
                }
                if (CHS2K_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.chs2k_bv_1 = 1;
                    }
                    else LocoButtons.chs2k_bv_1 = 0;
                }
                if (CHS2K_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.chs2k_tokopr_per_0 = 1;
                    }
                    else LocoButtons.chs2k_tokopr_per_0 = 0;
                }
                if (CHS2K_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.chs2k_tokopr_per_1 = 1;
                    }
                    else LocoButtons.chs2k_tokopr_per_1 = 0;
                }
                if (CHS2K_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.chs2k_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.chs2k_tokopr_zad_0 = 0;
                }
                if (CHS2K_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.chs2k_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.chs2k_tokopr_zad_1 = 0;
                }
                if (CHS2K_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.chs2k_komp1_0 = 1;
                    }
                    else LocoButtons.chs2k_komp1_0 = 0;
                }
                if (CHS2K_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.chs2k_komp1_1 = 1;
                    }
                    else LocoButtons.chs2k_komp1_1 = 0;
                }
                if (CHS2K_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.chs2k_komp2_0 = 1;
                    }
                    else LocoButtons.chs2k_komp2_0 = 0;
                }
                if (CHS2K_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.chs2k_komp2_1 = 1;
                    }
                    else LocoButtons.chs2k_komp2_1 = 0;
                }
                if (CHS2K_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.chs2k_vent_0 = 1;
                    }
                    else LocoButtons.chs2k_vent_0 = 0;
                }
                if (CHS2K_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.chs2k_vent_1 = 1;
                    }
                    else LocoButtons.chs2k_vent_1 = 0;
                }
                if (CHS2K_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.chs2k_svet_cab_0 = 1;
                    }
                    else LocoButtons.chs2k_svet_cab_0 = 0;
                }
                if (CHS2K_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.chs2k_svet_cab_1 = 1;
                    }
                    else LocoButtons.chs2k_svet_cab_1 = 0;
                }
                if (CHS2K_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.chs2k_svet_cab_2 = 1;
                    }
                    else LocoButtons.chs2k_svet_cab_2 = 0;
                }
                if (CHS2K_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.chs2k_EPK_0 = 1;
                    }
                    else LocoButtons.chs2k_EPK_0 = 0;
                }
                if (CHS2K_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.chs2k_EPK_1 = 1;
                    }
                    else LocoButtons.chs2k_EPK_1 = 0;
                }
                if (CHS2K_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.chs2k_EPT_0 = 1;
                    }
                    else LocoButtons.chs2k_EPT_0 = 0;
                }
                if (CHS2K_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.chs2k_EPT_1 = 1;
                    }
                    else LocoButtons.chs2k_EPT_1 = 0;
                }
                if (CHS2K_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.chs2k_prozh_0 = 1;
                    }
                    else LocoButtons.chs2k_prozh_0 = 0;
                }
                if (CHS2K_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.chs2k_prozh_1 = 1;
                    }
                    else LocoButtons.chs2k_prozh_1 = 0;
                }
                if (CHS2K_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[CHS2K_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.chs2k_prozh_2 = 1;
                    }
                    else LocoButtons.chs2k_prozh_2 = 0;
                }
            }

            //проверяем кнопки в буфере CHS4
            if (Loco.sig_loco == 4)
            {
                if (CHS4_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.chs4_rev_0 = 1;
                    }
                    else LocoButtons.chs4_rev_0 = 0;
                }
                if (CHS4_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.chs4_rev_vpered = 1;
                    }
                    else LocoButtons.chs4_rev_vpered = 0;
                }
                if (CHS4_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.chs4_rev_nazad = 1;
                    }
                    else LocoButtons.chs4_rev_nazad = 0;
                }
                if (CHS4_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.chs4_kontr_0 = 1;
                    }
                    else LocoButtons.chs4_kontr_0 = 0;
                }
                if (CHS4_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.chs4_kontr_plus = 1;
                    }
                    else LocoButtons.chs4_kontr_plus = 0;
                }
                if (CHS4_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.chs4_kontr_minus = 1;
                    }
                    else LocoButtons.chs4_kontr_minus = 0;
                }
                if (CHS4_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.chs4_kontr_plus1 = 1;
                    }
                    else LocoButtons.chs4_kontr_plus1 = 0;
                }
                if (CHS4_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.chs4_kontr_minus1 = 1;
                    }
                    else LocoButtons.chs4_kontr_minus1 = 0;
                }
                if (CHS4_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.chs4_kontr_shunt1 = 1;
                    }
                    else LocoButtons.chs4_kontr_shunt1 = 0;
                }
                if (CHS4_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.chs4_kontr_shunt2 = 1;
                    }
                    else LocoButtons.chs4_kontr_shunt2 = 0;
                }
                if (CHS4_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.chs4_kontr_shunt3 = 1;
                    }
                    else LocoButtons.chs4_kontr_shunt3 = 0;
                }
                if (CHS4_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.chs4_kontr_shunt4 = 1;
                    }
                    else LocoButtons.chs4_kontr_shunt4 = 0;
                }
                if (CHS4_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.chs4_kontr_shunt5 = 1;
                    }
                    else LocoButtons.chs4_kontr_shunt5 = 0;
                }
                if (CHS4_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.chs4_kranTM_0 = 1;
                    }
                    else LocoButtons.chs4_kranTM_0 = 0;
                }
                if (CHS4_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.chs4_kranTM_1 = 1;
                    }
                    else LocoButtons.chs4_kranTM_1 = 0;
                }
                if (CHS4_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.chs4_bv_0 = 1;
                    }
                    else LocoButtons.chs4_bv_0 = 0;
                }
                if (CHS4_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.chs4_bv_1 = 1;
                    }
                    else LocoButtons.chs4_bv_1 = 0;
                }
                if (CHS4_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.chs4_bv_2 = 1;
                    }
                    else LocoButtons.chs4_bv_2 = 0;
                }
                if (CHS4_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.chs4_tokopr_per_0 = 1;
                    }
                    else LocoButtons.chs4_tokopr_per_0 = 0;
                }
                if (CHS4_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.chs4_tokopr_per_1 = 1;
                    }
                    else LocoButtons.chs4_tokopr_per_1 = 0;
                }
                if (CHS4_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.chs4_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.chs4_tokopr_zad_0 = 0;
                }
                if (CHS4_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.chs4_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.chs4_tokopr_zad_1 = 0;
                }

                if (CHS4_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.chs4_komp1_0 = 1;
                    }
                    else LocoButtons.chs4_komp1_0 = 0;
                }
                if (CHS4_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.chs4_komp1_1 = 1;
                    }
                    else LocoButtons.chs4_komp1_1 = 0;
                }
                if (CHS4_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.chs4_komp1_2 = 1;
                    }
                    else LocoButtons.chs4_komp1_2 = 0;
                }
                if (CHS4_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.chs4_komp2_0 = 1;
                    }
                    else LocoButtons.chs4_komp2_0 = 0;
                }
                if (CHS4_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.chs4_komp2_1 = 1;
                    }
                    else LocoButtons.chs4_komp2_1 = 0;
                }
                if (CHS4_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.chs4_komp2_2 = 1;
                    }
                    else LocoButtons.chs4_komp2_2 = 0;
                }
                if (CHS4_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.chs4_vent_0 = 1;
                    }
                    else LocoButtons.chs4_vent_0 = 0;
                }
                if (CHS4_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.chs4_vent_1 = 1;
                    }
                    else LocoButtons.chs4_vent_1 = 0;
                }
                if (CHS4_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.chs4_vent_2 = 1;
                    }
                    else LocoButtons.chs4_vent_2 = 0;
                }
                if (CHS4_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.chs4_vent_3 = 1;
                    }
                    else LocoButtons.chs4_vent_3 = 0;
                }
                if (CHS4_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.chs4_vent_4 = 1;
                    }
                    else LocoButtons.chs4_vent_4 = 0;
                }
                if (CHS4_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.chs4_vent_5 = 1;
                    }
                    else LocoButtons.chs4_vent_5 = 0;
                }
                if (CHS4_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.chs4_vent_6 = 1;
                    }
                    else LocoButtons.chs4_vent_6 = 0;
                }
                if (CHS4_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.chs4_vent_7 = 1;
                    }
                    else LocoButtons.chs4_vent_7 = 0;
                }
                if (CHS4_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.chs4_vspom_komp_0 = 1;
                    }
                    else LocoButtons.chs4_vspom_komp_0 = 0;
                }
                if (CHS4_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.chs4_vspom_komp_1 = 1;
                    }
                    else LocoButtons.chs4_vspom_komp_1 = 0;
                }
                if (CHS4_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.chs4_vspom_komp_2 = 1;
                    }
                    else LocoButtons.chs4_vspom_komp_2 = 0;
                }
                if (CHS4_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.chs4_vspom_komp_3 = 1;
                    }
                    else LocoButtons.chs4_vspom_komp_3 = 0;
                }
                if (CHS4_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.chs4_svet_cab_0 = 1;
                    }
                    else LocoButtons.chs4_svet_cab_0 = 0;
                }
                if (CHS4_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.chs4_svet_cab_1 = 1;
                    }
                    else LocoButtons.chs4_svet_cab_1 = 0;
                }
                if (CHS4_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.chs4_svet_cab_2 = 1;
                    }
                    else LocoButtons.chs4_svet_cab_2 = 0;
                }
                if (CHS4_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.chs4_svet_cab_3 = 1;
                    }
                    else LocoButtons.chs4_svet_cab_3 = 0;
                }
                if (CHS4_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.chs4_EPK_0 = 1;
                    }
                    else LocoButtons.chs4_EPK_0 = 0;
                }
                if (CHS4_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.chs4_EPK_1 = 1;
                    }
                    else LocoButtons.chs4_EPK_1 = 0;
                }
                if (CHS4_key_buffer[46] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[46] - 1] != 0)
                    {
                        LocoButtons.chs4_EPT_0 = 1;
                    }
                    else LocoButtons.chs4_EPT_0 = 0;
                }
                if (CHS4_key_buffer[47] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[47] - 1] != 0)
                    {
                        LocoButtons.chs4_EPT_1 = 1;
                    }
                    else LocoButtons.chs4_EPT_1 = 0;
                }
                if (CHS4_key_buffer[48] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[48] - 1] != 0)
                    {
                        LocoButtons.chs4_avar_nabor_0 = 1;
                    }
                    else LocoButtons.chs4_avar_nabor_0 = 0;
                }
                if (CHS4_key_buffer[49] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[49] - 1] != 0)
                    {
                        LocoButtons.chs4_avar_nabor_1 = 1;
                    }
                    else LocoButtons.chs4_avar_nabor_1 = 0;
                }
                if (CHS4_key_buffer[50] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[50] - 1] != 0)
                    {
                        LocoButtons.chs4_avar_nabor_2 = 1;
                    }
                    else LocoButtons.chs4_avar_nabor_2 = 0;
                }
                if (CHS4_key_buffer[51] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[51] - 1] != 0)
                    {
                        LocoButtons.chs4_avar_nabor_3 = 1;
                    }
                    else LocoButtons.chs4_avar_nabor_3 = 0;
                }
                if (CHS4_key_buffer[52] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[52] - 1] != 0)
                    {
                        LocoButtons.chs4_prozh_0 = 1;
                    }
                    else LocoButtons.chs4_prozh_0 = 0;
                }
                if (CHS4_key_buffer[53] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[53] - 1] != 0)
                    {
                        LocoButtons.chs4_prozh_1 = 1;
                    }
                    else LocoButtons.chs4_prozh_1 = 0;
                }
                if (CHS4_key_buffer[54] != 0)
                {
                    if (joystick_buttons_buffer[CHS4_key_buffer[54] - 1] != 0)
                    {
                        LocoButtons.chs4_prozh_2 = 1;
                    }
                    else LocoButtons.chs4_prozh_2 = 0;
                }
            }

            //проверяем кнопки в буфере CHS4KVR
            if (Loco.sig_loco == 5)
            {
                if (CHS4KVR_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_rev_0 = 1;
                    }
                    else LocoButtons.chs4kvr_rev_0 = 0;
                }
                if (CHS4KVR_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_rev_vpered = 1;
                    }
                    else LocoButtons.chs4kvr_rev_vpered = 0;
                }
                if (CHS4KVR_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_rev_nazad = 1;
                    }
                    else LocoButtons.chs4kvr_rev_nazad = 0;
                }
                if (CHS4KVR_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kontr_0 = 1;
                    }
                    else LocoButtons.chs4kvr_kontr_0 = 0;
                }
                if (CHS4KVR_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kontr_plus = 1;
                    }
                    else LocoButtons.chs4kvr_kontr_plus = 0;
                }
                if (CHS4KVR_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kontr_minus = 1;
                    }
                    else LocoButtons.chs4kvr_kontr_minus = 0;
                }
                if (CHS4KVR_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kontr_plus1 = 1;
                    }
                    else LocoButtons.chs4kvr_kontr_plus1 = 0;
                }
                if (CHS4KVR_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kontr_minus1 = 1;
                    }
                    else LocoButtons.chs4kvr_kontr_minus1 = 0;
                }
                if (CHS4KVR_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kontr_shunt1 = 1;
                    }
                    else LocoButtons.chs4kvr_kontr_shunt1 = 0;
                }
                if (CHS4KVR_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kontr_shunt2 = 1;
                    }
                    else LocoButtons.chs4kvr_kontr_shunt2 = 0;
                }
                if (CHS4KVR_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kontr_shunt3 = 1;
                    }
                    else LocoButtons.chs4kvr_kontr_shunt3 = 0;
                }
                if (CHS4KVR_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kontr_shunt4 = 1;
                    }
                    else LocoButtons.chs4kvr_kontr_shunt4 = 0;
                }
                if (CHS4KVR_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kontr_shunt5 = 1;
                    }
                    else LocoButtons.chs4kvr_kontr_shunt5 = 0;
                }
                if (CHS4KVR_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kranTM_0 = 1;
                    }
                    else LocoButtons.chs4kvr_kranTM_0 = 0;
                }
                if (CHS4KVR_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_kranTM_1 = 1;
                    }
                    else LocoButtons.chs4kvr_kranTM_1 = 0;
                }
                if (CHS4KVR_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_bv_0 = 1;
                    }
                    else LocoButtons.chs4kvr_bv_0 = 0;
                }
                if (CHS4KVR_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_bv_1 = 1;
                    }
                    else LocoButtons.chs4kvr_bv_1 = 0;
                }
                if (CHS4KVR_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_bv_2 = 1;
                    }
                    else LocoButtons.chs4kvr_bv_2 = 0;
                }
                if (CHS4KVR_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_tokopr_per_0 = 1;
                    }
                    else LocoButtons.chs4kvr_tokopr_per_0 = 0;
                }
                if (CHS4KVR_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_tokopr_per_1 = 1;
                    }
                    else LocoButtons.chs4kvr_tokopr_per_1 = 0;
                }
                if (CHS4KVR_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.chs4kvr_tokopr_zad_0 = 0;
                }
                if (CHS4KVR_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.chs4kvr_tokopr_zad_1 = 0;
                }
                if (CHS4KVR_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_komp1_0 = 1;
                    }
                    else LocoButtons.chs4kvr_komp1_0 = 0;
                }
                if (CHS4KVR_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_komp1_1 = 1;
                    }
                    else LocoButtons.chs4kvr_komp1_1 = 0;
                }
                if (CHS4KVR_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_komp1_2 = 1;
                    }
                    else LocoButtons.chs4kvr_komp1_2 = 0;
                }
                if (CHS4KVR_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_komp2_0 = 1;
                    }
                    else LocoButtons.chs4kvr_komp2_0 = 0;
                }
                if (CHS4KVR_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_komp2_1 = 1;
                    }
                    else LocoButtons.chs4kvr_komp2_1 = 0;
                }
                if (CHS4KVR_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_komp2_2 = 1;
                    }
                    else LocoButtons.chs4kvr_komp2_2 = 0;
                }
                if (CHS4KVR_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vent_0 = 1;
                    }
                    else LocoButtons.chs4kvr_vent_0 = 0;
                }
                if (CHS4KVR_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vent_1 = 1;
                    }
                    else LocoButtons.chs4kvr_vent_1 = 0;
                }
                if (CHS4KVR_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vent_2 = 1;
                    }
                    else LocoButtons.chs4kvr_vent_2 = 0;
                }
                if (CHS4KVR_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vent_3 = 1;
                    }
                    else LocoButtons.chs4kvr_vent_3 = 0;
                }
                if (CHS4KVR_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vent_4 = 1;
                    }
                    else LocoButtons.chs4kvr_vent_4 = 0;
                }
                if (CHS4KVR_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vent_5 = 1;
                    }
                    else LocoButtons.chs4kvr_vent_5 = 0;
                }
                if (CHS4KVR_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vent_6 = 1;
                    }
                    else LocoButtons.chs4kvr_vent_6 = 0;
                }
                if (CHS4KVR_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vent_7 = 1;
                    }
                    else LocoButtons.chs4kvr_vent_7 = 0;
                }
                if (CHS4KVR_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vspom_komp_0 = 1;
                    }
                    else LocoButtons.chs4kvr_vspom_komp_0 = 0;
                }
                if (CHS4KVR_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vspom_komp_1 = 1;
                    }
                    else LocoButtons.chs4kvr_vspom_komp_1 = 0;
                }
                if (CHS4KVR_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vspom_komp_2 = 1;
                    }
                    else LocoButtons.chs4kvr_vspom_komp_2 = 0;
                }
                if (CHS4KVR_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_vspom_komp_3 = 1;
                    }
                    else LocoButtons.chs4kvr_vspom_komp_3 = 0;
                }
                if (CHS4KVR_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_svet_cab_0 = 1;
                    }
                    else LocoButtons.chs4kvr_svet_cab_0 = 0;
                }
                if (CHS4KVR_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_svet_cab_1 = 1;
                    }
                    else LocoButtons.chs4kvr_svet_cab_1 = 0;
                }
                if (CHS4KVR_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_svet_cab_2 = 1;
                    }
                    else LocoButtons.chs4kvr_svet_cab_2 = 0;
                }
                if (CHS4KVR_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_svet_cab_3 = 1;
                    }
                    else LocoButtons.chs4kvr_svet_cab_3 = 0;
                }
                if (CHS4KVR_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_EPK_0 = 1;
                    }
                    else LocoButtons.chs4kvr_EPK_0 = 0;
                }
                if (CHS4KVR_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_EPK_1 = 1;
                    }
                    else LocoButtons.chs4kvr_EPK_1 = 0;
                }
                if (CHS4KVR_key_buffer[46] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[46] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_EPT_0 = 1;
                    }
                    else LocoButtons.chs4kvr_EPT_0 = 0;
                }
                if (CHS4KVR_key_buffer[47] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[47] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_EPT_1 = 1;
                    }
                    else LocoButtons.chs4kvr_EPT_1 = 0;
                }
                if (CHS4KVR_key_buffer[48] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[48] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_avar_nabor_0 = 1;
                    }
                    else LocoButtons.chs4kvr_avar_nabor_0 = 0;
                }
                if (CHS4KVR_key_buffer[49] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[49] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_avar_nabor_1 = 1;
                    }
                    else LocoButtons.chs4kvr_avar_nabor_1 = 0;
                }
                if (CHS4KVR_key_buffer[50] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[50] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_avar_nabor_2 = 1;
                    }
                    else LocoButtons.chs4kvr_avar_nabor_2 = 0;
                }
                if (CHS4KVR_key_buffer[51] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[51] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_avar_nabor_3 = 1;
                    }
                    else LocoButtons.chs4kvr_avar_nabor_3 = 0;
                }
                if (CHS4KVR_key_buffer[52] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[52] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_prozh_0 = 1;
                    }
                    else LocoButtons.chs4kvr_prozh_0 = 0;
                }
                if (CHS4KVR_key_buffer[53] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[53] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_prozh_1 = 1;
                    }
                    else LocoButtons.chs4kvr_prozh_1 = 0;
                }
                if (CHS4KVR_key_buffer[54] != 0)
                {
                    if (joystick_buttons_buffer[CHS4KVR_key_buffer[54] - 1] != 0)
                    {
                        LocoButtons.chs4kvr_prozh_2 = 1;
                    }
                    else LocoButtons.chs4kvr_prozh_2 = 0;
                }
            }

            //проверяем кнопки в буфере CHS4T
            if (Loco.sig_loco == 6)
            {
                if (CHS4T_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.chs4t_rev_0 = 1;
                    }
                    else LocoButtons.chs4t_rev_0 = 0;
                }
                if (CHS4T_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.chs4t_rev_vpered = 1;
                    }
                    else LocoButtons.chs4t_rev_vpered = 0;
                }
                if (CHS4T_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.chs4t_rev_nazad = 1;
                    }
                    else LocoButtons.chs4t_rev_nazad = 0;
                }
                if (CHS4T_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.chs4t_kontr_0 = 1;
                    }
                    else LocoButtons.chs4t_kontr_0 = 0;
                }
                if (CHS4T_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.chs4t_kontr_plus = 1;
                    }
                    else LocoButtons.chs4t_kontr_plus = 0;
                }
                if (CHS4T_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.chs4t_kontr_minus = 1;
                    }
                    else LocoButtons.chs4t_kontr_minus = 0;
                }
                if (CHS4T_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.chs4t_kontr_plus1 = 1;
                    }
                    else LocoButtons.chs4t_kontr_plus1 = 0;
                }
                if (CHS4T_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.chs4t_kontr_minus1 = 1;
                    }
                    else LocoButtons.chs4t_kontr_minus1 = 0;
                }
                if (CHS4T_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.chs4t_kontr_shunt1 = 1;
                    }
                    else LocoButtons.chs4t_kontr_shunt1 = 0;
                }
                if (CHS4T_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.chs4t_kontr_shunt2 = 1;
                    }
                    else LocoButtons.chs4t_kontr_shunt2 = 0;
                }
                if (CHS4T_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.chs4t_kontr_shunt3 = 1;
                    }
                    else LocoButtons.chs4t_kontr_shunt3 = 0;
                }
                if (CHS4T_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.chs4t_kontr_shunt4 = 1;
                    }
                    else LocoButtons.chs4t_kontr_shunt4 = 0;
                }
                if (CHS4T_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.chs4t_kontr_shunt5 = 1;
                    }
                    else LocoButtons.chs4t_kontr_shunt5 = 0;
                }
                if (CHS4T_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.chs4t_kranTM_0 = 1;
                    }
                    else LocoButtons.chs4t_kranTM_0 = 0;
                }
                if (CHS4T_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.chs4t_kranTM_1 = 1;
                    }
                    else LocoButtons.chs4t_kranTM_1 = 0;
                }
                if (CHS4T_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.chs4t_bv_0 = 1;
                    }
                    else LocoButtons.chs4t_bv_0 = 0;
                }
                if (CHS4T_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.chs4t_bv_1 = 1;
                    }
                    else LocoButtons.chs4t_bv_1 = 0;
                }
                if (CHS4T_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.chs4t_bv_2 = 1;
                    }
                    else LocoButtons.chs4t_bv_2 = 0;
                }
                if (CHS4T_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.chs4t_tokopr_per_0 = 1;
                    }
                    else LocoButtons.chs4t_tokopr_per_0 = 0;
                }
                if (CHS4T_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.chs4t_tokopr_per_1 = 1;
                    }
                    else LocoButtons.chs4t_tokopr_per_1 = 0;
                }
                if (CHS4T_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.chs4t_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.chs4t_tokopr_zad_0 = 0;
                }
                if (CHS4T_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.chs4t_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.chs4t_tokopr_zad_1 = 0;
                }
                if (CHS4T_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.chs4t_komp1_0 = 1;
                    }
                    else LocoButtons.chs4t_komp1_0 = 0;
                }
                if (CHS4T_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.chs4t_komp1_1 = 1;
                    }
                    else LocoButtons.chs4t_komp1_1 = 0;
                }
                if (CHS4T_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.chs4t_komp1_2 = 1;
                    }
                    else LocoButtons.chs4t_komp1_2 = 0;
                }
                if (CHS4T_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.chs4t_komp2_0 = 1;
                    }
                    else LocoButtons.chs4t_komp2_0 = 0;
                }
                if (CHS4T_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.chs4t_komp2_1 = 1;
                    }
                    else LocoButtons.chs4t_komp2_1 = 0;
                }
                if (CHS4T_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.chs4t_komp2_2 = 1;
                    }
                    else LocoButtons.chs4t_komp2_2 = 0;
                }
                if (CHS4T_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.chs4t_vent_0 = 1;
                    }
                    else LocoButtons.chs4t_vent_0 = 0;
                }
                if (CHS4T_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.chs4t_vent_1 = 1;
                    }
                    else LocoButtons.chs4t_vent_1 = 0;
                }
                if (CHS4T_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.chs4t_vent_2 = 1;
                    }
                    else LocoButtons.chs4t_vent_2 = 0;
                }
                if (CHS4T_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.chs4t_vspom_komp_0 = 1;
                    }
                    else LocoButtons.chs4t_vspom_komp_0 = 0;
                }
                if (CHS4T_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.chs4t_vspom_komp_1 = 1;
                    }
                    else LocoButtons.chs4t_vspom_komp_1 = 0;
                }
                if (CHS4T_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.chs4t_vspom_komp_2 = 1;
                    }
                    else LocoButtons.chs4t_vspom_komp_2 = 0;
                }
                if (CHS4T_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.chs4t_vspom_komp_3 = 1;
                    }
                    else LocoButtons.chs4t_vspom_komp_3 = 0;
                }
                if (CHS4T_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.chs4t_svet_cab_0 = 1;
                    }
                    else LocoButtons.chs4t_svet_cab_0 = 0;
                }
                if (CHS4T_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.chs4t_svet_cab_1 = 1;
                    }
                    else LocoButtons.chs4t_svet_cab_1 = 0;
                }
                if (CHS4T_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.chs4t_svet_cab_2 = 1;
                    }
                    else LocoButtons.chs4t_svet_cab_2 = 0;
                }
                if (CHS4T_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.chs4t_svet_cab_3 = 1;
                    }
                    else LocoButtons.chs4t_svet_cab_3 = 0;
                }
                if (CHS4T_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.chs4t_EPK_0 = 1;
                    }
                    else LocoButtons.chs4t_EPK_0 = 0;
                }
                if (CHS4T_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.chs4t_EPK_1 = 1;
                    }
                    else LocoButtons.chs4t_EPK_1 = 0;
                }
                if (CHS4T_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.chs4t_EPT_0 = 1;
                    }
                    else LocoButtons.chs4t_EPT_0 = 0;
                }
                if (CHS4T_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.chs4t_EPT_1 = 1;
                    }
                    else LocoButtons.chs4t_EPT_1 = 0;
                }
                if (CHS4T_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.chs4t_avar_nabor_0 = 1;
                    }
                    else LocoButtons.chs4t_avar_nabor_0 = 0;
                }
                if (CHS4T_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.chs4t_avar_nabor_1 = 1;
                    }
                    else LocoButtons.chs4t_avar_nabor_1 = 0;
                }
                if (CHS4T_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.chs4t_avar_nabor_2 = 1;
                    }
                    else LocoButtons.chs4t_avar_nabor_2 = 0;
                }
                if (CHS4T_key_buffer[46] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[46] - 1] != 0)
                    {
                        LocoButtons.chs4t_avar_nabor_3 = 1;
                    }
                    else LocoButtons.chs4t_avar_nabor_3 = 0;
                }
                if (CHS4T_key_buffer[47] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[47] - 1] != 0)
                    {
                        LocoButtons.chs4t_prozh_0 = 1;
                    }
                    else LocoButtons.chs4t_prozh_0 = 0;
                }
                if (CHS4T_key_buffer[48] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[48] - 1] != 0)
                    {
                        LocoButtons.chs4t_prozh_1 = 1;
                    }
                    else LocoButtons.chs4t_prozh_1 = 0;
                }
                if (CHS4T_key_buffer[49] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[49] - 1] != 0)
                    {
                        LocoButtons.chs4t_prozh_2 = 1;
                    }
                    else LocoButtons.chs4t_prozh_2 = 0;
                }
                if (CHS4T_key_buffer[50] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[50] - 1] != 0)
                    {
                        LocoButtons.chs4t_zhalyzi_0 = 1;
                    }
                    else LocoButtons.chs4t_zhalyzi_0 = 0;
                }
                if (CHS4T_key_buffer[51] != 0)
                {
                    if (joystick_buttons_buffer[CHS4T_key_buffer[51] - 1] != 0)
                    {
                        LocoButtons.chs4t_zhalyzi_1 = 1;
                    }
                    else LocoButtons.chs4t_zhalyzi_1 = 0;
                }
            }

            //проверяем кнопки в буфере CHS7
            if (Loco.sig_loco == 7)
            {
                if (CHS7_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.chs7_rev_0 = 1;
                    }
                    else LocoButtons.chs7_rev_0 = 0;
                }
                if (CHS7_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.chs7_rev_vpered = 1;
                    }
                    else LocoButtons.chs7_rev_vpered = 0;
                }
                if (CHS7_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.chs7_rev_nazad = 1;
                    }
                    else LocoButtons.chs7_rev_nazad = 0;
                }
                if (CHS7_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.chs7_kontr_0 = 1;
                    }
                    else LocoButtons.chs7_kontr_0 = 0;
                }
                if (CHS7_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.chs7_kontr_plus = 1;
                    }
                    else LocoButtons.chs7_kontr_plus = 0;
                }
                if (CHS7_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.chs7_kontr_minus = 1;
                    }
                    else LocoButtons.chs7_kontr_minus = 0;
                }
                if (CHS7_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.chs7_kontr_plus1 = 1;
                    }
                    else LocoButtons.chs7_kontr_plus1 = 0;
                }
                if (CHS7_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.chs7_kontr_minus1 = 1;
                    }
                    else LocoButtons.chs7_kontr_minus1 = 0;
                }
                if (CHS7_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.chs7_kontr_shunt1 = 1;
                    }
                    else LocoButtons.chs7_kontr_shunt1 = 0;
                }
                if (CHS7_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.chs7_kontr_shunt2 = 1;
                    }
                    else LocoButtons.chs7_kontr_shunt2 = 0;
                }
                if (CHS7_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.chs7_kontr_shunt3 = 1;
                    }
                    else LocoButtons.chs7_kontr_shunt3 = 0;
                }
                if (CHS7_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.chs7_kontr_shunt4 = 1;
                    }
                    else LocoButtons.chs7_kontr_shunt4 = 0;
                }
                if (CHS7_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.chs7_kontr_shunt5 = 1;
                    }
                    else LocoButtons.chs7_kontr_shunt5 = 0;
                }
                if (CHS7_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.chs7_kranTM_0 = 1;
                    }
                    else LocoButtons.chs7_kranTM_0 = 0;
                }
                if (CHS7_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.chs7_kranTM_1 = 1;
                    }
                    else LocoButtons.chs7_kranTM_1 = 0;
                }
                if (CHS7_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.chs7_bv_0 = 1;
                    }
                    else LocoButtons.chs7_bv_0 = 0;
                }
                if (CHS7_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.chs7_bv_1 = 1;
                    }
                    else LocoButtons.chs7_bv_1 = 0;
                }
                if (CHS7_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.chs7_bv_2 = 1;
                    }
                    else LocoButtons.chs7_bv_2 = 0;
                }
                if (CHS7_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.chs7_tokopr_per_0 = 1;
                    }
                    else LocoButtons.chs7_tokopr_per_0 = 0;
                }
                if (CHS7_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.chs7_tokopr_per_1 = 1;
                    }
                    else LocoButtons.chs7_tokopr_per_1 = 0;
                }
                if (CHS7_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.chs7_tokopr_per_2 = 1;
                    }
                    else LocoButtons.chs7_tokopr_per_2 = 0;
                }
                if (CHS7_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.chs7_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.chs7_tokopr_zad_0 = 0;
                }
                if (CHS7_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.chs7_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.chs7_tokopr_zad_1 = 0;
                }
                if (CHS7_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.chs7_tokopr_zad_2 = 1;
                    }
                    else LocoButtons.chs7_tokopr_zad_2 = 0;
                }

                if (CHS7_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.chs7_komp1_0 = 1;
                    }
                    else LocoButtons.chs7_komp1_0 = 0;
                }
                if (CHS7_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.chs7_komp1_1 = 1;
                    }
                    else LocoButtons.chs7_komp1_1 = 0;
                }
                if (CHS7_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.chs7_komp1_2 = 1;
                    }
                    else LocoButtons.chs7_komp1_2 = 0;
                }
                if (CHS7_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.chs7_komp2_0 = 1;
                    }
                    else LocoButtons.chs7_komp2_0 = 0;
                }
                if (CHS7_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.chs7_komp2_1 = 1;
                    }
                    else LocoButtons.chs7_komp2_1 = 0;
                }
                if (CHS7_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.chs7_komp2_2 = 1;
                    }
                    else LocoButtons.chs7_komp2_2 = 0;
                }
                if (CHS7_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.chs7_vent_0 = 1;
                    }
                    else LocoButtons.chs7_vent_0 = 0;
                }
                if (CHS7_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.chs7_vent_1 = 1;
                    }
                    else LocoButtons.chs7_vent_1 = 0;
                }
                if (CHS7_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.chs7_vent_2 = 1;
                    }
                    else LocoButtons.chs7_vent_2 = 0;
                }
                if (CHS7_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.chs7_sbros_SP = 1;
                    }
                    else LocoButtons.chs7_sbros_SP = 0;
                }
                if (CHS7_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.chs7_svet_cab_0 = 1;
                    }
                    else LocoButtons.chs7_svet_cab_0 = 0;
                }
                if (CHS7_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.chs7_svet_cab_1 = 1;
                    }
                    else LocoButtons.chs7_svet_cab_1 = 0;
                }
                if (CHS7_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.chs7_svet_cab_2 = 1;
                    }
                    else LocoButtons.chs7_svet_cab_2 = 0;
                }
                if (CHS7_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.chs7_EPK_0 = 1;
                    }
                    else LocoButtons.chs7_EPK_0 = 0;
                }
                if (CHS7_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.chs7_EPK_1 = 1;
                    }
                    else LocoButtons.chs7_EPK_1 = 0;
                }
                if (CHS7_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.chs7_EPT_0 = 1;
                    }
                    else LocoButtons.chs7_EPT_0 = 0;
                }
                if (CHS7_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.chs7_EPT_1 = 1;
                    }
                    else LocoButtons.chs7_EPT_1 = 0;
                }
                if (CHS7_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.chs7_prozh_0 = 1;
                    }
                    else LocoButtons.chs7_prozh_0 = 0;
                }
                if (CHS7_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.chs7_prozh_1 = 1;
                    }
                    else LocoButtons.chs7_prozh_1 = 0;
                }
                if (CHS7_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.chs7_prozh_2 = 1;
                    }
                    else LocoButtons.chs7_prozh_2 = 0;
                }
                if (CHS7_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.chs7_zhalyzi1_0 = 1;
                    }
                    else LocoButtons.chs7_zhalyzi1_0 = 0;
                }
                if (CHS7_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[CHS7_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.chs7_zhalyzi1_1 = 1;
                    }
                    else LocoButtons.chs7_zhalyzi1_1 = 0;
                }
            }

            //проверяем кнопки в буфере CHS8
            if (Loco.sig_loco == 8)
            {
                if (CHS8_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.chs8_rev_0 = 1;
                    }
                    else LocoButtons.chs8_rev_0 = 0;
                }
                if (CHS8_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.chs8_rev_vpered = 1;
                    }
                    else LocoButtons.chs8_rev_vpered = 0;
                }
                if (CHS8_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.chs8_rev_nazad = 1;
                    }
                    else LocoButtons.chs8_rev_nazad = 0;
                }
                if (CHS8_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.chs8_kontr_0 = 1;
                    }
                    else LocoButtons.chs8_kontr_0 = 0;
                }
                if (CHS8_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.chs8_kontr_plus = 1;
                    }
                    else LocoButtons.chs8_kontr_plus = 0;
                }
                if (CHS8_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.chs8_kontr_minus = 1;
                    }
                    else LocoButtons.chs8_kontr_minus = 0;
                }
                if (CHS8_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.chs8_kontr_plus1 = 1;
                    }
                    else LocoButtons.chs8_kontr_plus1 = 0;
                }
                if (CHS8_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.chs8_kontr_minus1 = 1;
                    }
                    else LocoButtons.chs8_kontr_minus1 = 0;
                }
                if (CHS8_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.chs8_kontr_shunt1 = 1;
                    }
                    else LocoButtons.chs8_kontr_shunt1 = 0;
                }
                if (CHS8_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.chs8_kontr_shunt2 = 1;
                    }
                    else LocoButtons.chs8_kontr_shunt2 = 0;
                }
                if (CHS8_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.chs8_kontr_shunt3 = 1;
                    }
                    else LocoButtons.chs8_kontr_shunt3 = 0;
                }
                if (CHS8_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.chs8_kontr_shunt4 = 1;
                    }
                    else LocoButtons.chs8_kontr_shunt4 = 0;
                }
                if (CHS8_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.chs8_kontr_shunt5 = 1;
                    }
                    else LocoButtons.chs8_kontr_shunt5 = 0;
                }
                if (CHS8_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.chs8_kranTM_0 = 1;
                    }
                    else LocoButtons.chs8_kranTM_0 = 0;
                }
                if (CHS8_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.chs8_kranTM_1 = 1;
                    }
                    else LocoButtons.chs8_kranTM_1 = 0;
                }
                if (CHS8_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.chs8_bv_0 = 1;
                    }
                    else LocoButtons.chs8_bv_0 = 0;
                }
                if (CHS8_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.chs8_bv_1 = 1;
                    }
                    else LocoButtons.chs8_bv_1 = 0;
                }
                if (CHS8_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.chs8_vosst_bv = 1;
                    }
                    else LocoButtons.chs8_vosst_bv = 0;
                }
                if (CHS8_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.chs8_tokopr_per_0 = 1;
                    }
                    else LocoButtons.chs8_tokopr_per_0 = 0;
                }
                if (CHS8_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.chs8_tokopr_per_1 = 1;
                    }
                    else LocoButtons.chs8_tokopr_per_1 = 0;
                }
                if (CHS8_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.chs8_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.chs8_tokopr_zad_0 = 0;
                }
                if (CHS8_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.chs8_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.chs8_tokopr_zad_1 = 0;
                }
                if (CHS8_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.chs8_komp1_0 = 1;
                    }
                    else LocoButtons.chs8_komp1_0 = 0;
                }
                if (CHS8_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.chs8_komp1_1 = 1;
                    }
                    else LocoButtons.chs8_komp1_1 = 0;
                }
                if (CHS8_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.chs8_komp1_2 = 1;
                    }
                    else LocoButtons.chs8_komp1_2 = 0;
                }
                if (CHS8_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.chs8_komp2_0 = 1;
                    }
                    else LocoButtons.chs8_komp2_0 = 0;
                }
                if (CHS8_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.chs8_komp2_1 = 1;
                    }
                    else LocoButtons.chs8_komp2_1 = 0;
                }
                if (CHS8_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.chs8_komp2_2 = 1;
                    }
                    else LocoButtons.chs8_komp2_2 = 0;
                }
                if (CHS8_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.chs8_vent1_0 = 1;
                    }
                    else LocoButtons.chs8_vent1_0 = 0;
                }
                if (CHS8_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.chs8_vent1_1 = 1;
                    }
                    else LocoButtons.chs8_vent1_1 = 0;
                }
                if (CHS8_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.chs8_vent1_2 = 1;
                    }
                    else LocoButtons.chs8_vent1_2 = 0;
                }
                if (CHS8_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.chs8_vent1_3 = 1;
                    }
                    else LocoButtons.chs8_vent1_3 = 0;
                }
                if (CHS8_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.chs8_vent1_4 = 1;
                    }
                    else LocoButtons.chs8_vent1_4 = 0;
                }
                if (CHS8_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.chs8_vent2_0 = 1;
                    }
                    else LocoButtons.chs8_vent2_0 = 0;
                }
                if (CHS8_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.chs8_vent2_1 = 1;
                    }
                    else LocoButtons.chs8_vent2_1 = 0;
                }
                if (CHS8_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.chs8_vent2_2 = 1;
                    }
                    else LocoButtons.chs8_vent2_2 = 0;
                }
                if (CHS8_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.chs8_vent2_3 = 1;
                    }
                    else LocoButtons.chs8_vent2_3 = 0;
                }
                if (CHS8_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.chs8_vent2_4 = 1;
                    }
                    else LocoButtons.chs8_vent2_4 = 0;
                }
                if (CHS8_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.chs8_vspom_komp_0 = 1;
                    }
                    else LocoButtons.chs8_vspom_komp_0 = 0;
                }
                if (CHS8_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.chs8_vspom_komp_1 = 1;
                    }
                    else LocoButtons.chs8_vspom_komp_1 = 0;
                }
                if (CHS8_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.chs8_vspom_komp_2 = 1;
                    }
                    else LocoButtons.chs8_vspom_komp_2 = 0;
                }
                if (CHS8_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.chs8_vspom_komp_3 = 1;
                    }
                    else LocoButtons.chs8_vspom_komp_3 = 0;
                }
                if (CHS8_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.chs8_svet_cab_0 = 1;
                    }
                    else LocoButtons.chs8_svet_cab_0 = 0;
                }
                if (CHS8_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.chs8_svet_cab_1 = 1;
                    }
                    else LocoButtons.chs8_svet_cab_1 = 0;
                }
                if (CHS8_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.chs8_svet_cab_2 = 1;
                    }
                    else LocoButtons.chs8_svet_cab_2 = 0;
                }
                if (CHS8_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.chs8_svet_cab_3 = 1;
                    }
                    else LocoButtons.chs8_svet_cab_3 = 0;
                }
                if (CHS8_key_buffer[46] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[46] - 1] != 0)
                    {
                        LocoButtons.chs8_svet_cab_4 = 1;
                    }
                    else LocoButtons.chs8_svet_cab_4 = 0;
                }
                if (CHS8_key_buffer[47] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[47] - 1] != 0)
                    {
                        LocoButtons.chs8_svet_cab_5 = 1;
                    }
                    else LocoButtons.chs8_svet_cab_5 = 0;
                }
                if (CHS8_key_buffer[48] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[48] - 1] != 0)
                    {
                        LocoButtons.chs8_EPK_0 = 1;
                    }
                    else LocoButtons.chs8_EPK_0 = 0;
                }
                if (CHS8_key_buffer[49] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[49] - 1] != 0)
                    {
                        LocoButtons.chs8_EPK_1 = 1;
                    }
                    else LocoButtons.chs8_EPK_1 = 0;
                }
                if (CHS8_key_buffer[50] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[50] - 1] != 0)
                    {
                        LocoButtons.chs8_EPT_0 = 1;
                    }
                    else LocoButtons.chs8_EPT_0 = 0;
                }
                if (CHS8_key_buffer[51] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[51] - 1] != 0)
                    {
                        LocoButtons.chs8_EPT_1 = 1;
                    }
                    else LocoButtons.chs8_EPT_1 = 0;
                }
                if (CHS8_key_buffer[52] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[52] - 1] != 0)
                    {
                        LocoButtons.chs8_avar_nabor_0 = 1;
                    }
                    else LocoButtons.chs8_avar_nabor_0 = 0;
                }
                if (CHS8_key_buffer[53] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[53] - 1] != 0)
                    {
                        LocoButtons.chs8_avar_nabor_1 = 1;
                    }
                    else LocoButtons.chs8_avar_nabor_1 = 0;
                }
                if (CHS8_key_buffer[54] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[54] - 1] != 0)
                    {
                        LocoButtons.chs8_avar_nabor_2 = 1;
                    }
                    else LocoButtons.chs8_avar_nabor_2 = 0;
                }
                if (CHS8_key_buffer[55] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[55] - 1] != 0)
                    {
                        LocoButtons.chs8_avar_nabor_3 = 1;
                    }
                    else LocoButtons.chs8_avar_nabor_3 = 0;
                }
                if (CHS8_key_buffer[56] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[56] - 1] != 0)
                    {
                        LocoButtons.chs8_prozh_0 = 1;
                    }
                    else LocoButtons.chs8_prozh_0 = 0;
                }
                if (CHS8_key_buffer[57] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[57] - 1] != 0)
                    {
                        LocoButtons.chs8_prozh_1 = 1;
                    }
                    else LocoButtons.chs8_prozh_1 = 0;
                }
                if (CHS8_key_buffer[58] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[58] - 1] != 0)
                    {
                        LocoButtons.chs8_prozh_2 = 1;
                    }
                    else LocoButtons.chs8_prozh_2 = 0;
                }
                if (CHS8_key_buffer[59] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[59] - 1] != 0)
                    {
                        LocoButtons.chs8_reost_torm_proverka = 1;
                    }
                    else LocoButtons.chs8_reost_torm_proverka = 0;
                }
                if (CHS8_key_buffer[60] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[60] - 1] != 0)
                    {
                        LocoButtons.chs8_reost_torm_0 = 1;
                    }
                    else LocoButtons.chs8_reost_torm_0 = 0;
                }
                if (CHS8_key_buffer[61] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[61] - 1] != 0)
                    {
                        LocoButtons.chs8_reost_torm_1 = 1;
                    }
                    else LocoButtons.chs8_reost_torm_1 = 0;
                }
                if (CHS8_key_buffer[62] != 0)
                {
                    if (joystick_buttons_buffer[CHS8_key_buffer[62] - 1] != 0)
                    {
                        LocoButtons.chs8_reost_torm_2 = 1;
                    }
                    else LocoButtons.chs8_reost_torm_2 = 0;
                }
            }

            //проверяем кнопки в буфере VL11M
            if (Loco.sig_loco == 9)
            {
                if (VL11M_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.vl11_rev_0 = 1;
                    }
                    else LocoButtons.vl11_rev_0 = 0;
                }
                if (VL11M_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.vl11_rev_vpered = 1;
                    }
                    else LocoButtons.vl11_rev_vpered = 0;
                }
                if (VL11M_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.vl11_rev_nazad = 1;
                    }
                    else LocoButtons.vl11_rev_nazad = 0;
                }
                if (VL11M_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_0 = 1;
                    }
                    else LocoButtons.vl11_kontr_0 = 0;
                }
                if (VL11M_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_1 = 1;
                    }
                    else LocoButtons.vl11_kontr_1 = 0;
                }
                if (VL11M_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_2 = 1;
                    }
                    else LocoButtons.vl11_kontr_2 = 0;
                }
                if (VL11M_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_3 = 1;
                    }
                    else LocoButtons.vl11_kontr_3 = 0;
                }
                if (VL11M_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_4 = 1;
                    }
                    else LocoButtons.vl11_kontr_4 = 0;
                }
                if (VL11M_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_5 = 1;
                    }
                    else LocoButtons.vl11_kontr_5 = 0;
                }
                if (VL11M_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_6 = 1;
                    }
                    else LocoButtons.vl11_kontr_6 = 0;
                }
                if (VL11M_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_7 = 1;
                    }
                    else LocoButtons.vl11_kontr_7 = 0;
                }
                if (VL11M_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_8 = 1;
                    }
                    else LocoButtons.vl11_kontr_8 = 0;
                }
                if (VL11M_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_9 = 1;
                    }
                    else LocoButtons.vl11_kontr_9 = 0;
                }
                if (VL11M_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_10 = 1;
                    }
                    else LocoButtons.vl11_kontr_10 = 0;
                }
                if (VL11M_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_11 = 1;
                    }
                    else LocoButtons.vl11_kontr_11 = 0;
                }
                if (VL11M_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_12 = 1;
                    }
                    else LocoButtons.vl11_kontr_12 = 0;
                }
                if (VL11M_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_13 = 1;
                    }
                    else LocoButtons.vl11_kontr_13 = 0;
                }
                if (VL11M_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_14 = 1;
                    }
                    else LocoButtons.vl11_kontr_14 = 0;
                }
                if (VL11M_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_15 = 1;
                    }
                    else LocoButtons.vl11_kontr_15 = 0;
                }
                if (VL11M_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_16 = 1;
                    }
                    else LocoButtons.vl11_kontr_16 = 0;
                }
                if (VL11M_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_17 = 1;
                    }
                    else LocoButtons.vl11_kontr_17 = 0;
                }
                if (VL11M_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_18 = 1;
                    }
                    else LocoButtons.vl11_kontr_18 = 0;
                }
                if (VL11M_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_19 = 1;
                    }
                    else LocoButtons.vl11_kontr_19 = 0;
                }
                if (VL11M_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_20 = 1;
                    }
                    else LocoButtons.vl11_kontr_20 = 0;
                }
                if (VL11M_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_21 = 1;
                    }
                    else LocoButtons.vl11_kontr_21 = 0;
                }
                if (VL11M_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_22 = 1;
                    }
                    else LocoButtons.vl11_kontr_22 = 0;
                }
                if (VL11M_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_23 = 1;
                    }
                    else LocoButtons.vl11_kontr_23 = 0;
                }
                if (VL11M_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_24 = 1;
                    }
                    else LocoButtons.vl11_kontr_24 = 0;
                }
                if (VL11M_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_25 = 1;
                    }
                    else LocoButtons.vl11_kontr_25 = 0;
                }
                if (VL11M_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_26 = 1;
                    }
                    else LocoButtons.vl11_kontr_26 = 0;
                }
                if (VL11M_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_27 = 1;
                    }
                    else LocoButtons.vl11_kontr_27 = 0;
                }
                if (VL11M_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_28 = 1;
                    }
                    else LocoButtons.vl11_kontr_28 = 0;
                }
                if (VL11M_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_29 = 1;
                    }
                    else LocoButtons.vl11_kontr_29 = 0;
                }
                if (VL11M_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_30 = 1;
                    }
                    else LocoButtons.vl11_kontr_30 = 0;
                }
                if (VL11M_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_31 = 1;
                    }
                    else LocoButtons.vl11_kontr_31 = 0;
                }
                if (VL11M_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_32 = 1;
                    }
                    else LocoButtons.vl11_kontr_32 = 0;
                }
                if (VL11M_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_33 = 1;
                    }
                    else LocoButtons.vl11_kontr_33 = 0;
                }
                if (VL11M_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_34 = 1;
                    }
                    else LocoButtons.vl11_kontr_34 = 0;
                }
                if (VL11M_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_35 = 1;
                    }
                    else LocoButtons.vl11_kontr_35 = 0;
                }
                if (VL11M_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_36 = 1;
                    }
                    else LocoButtons.vl11_kontr_36 = 0;
                }
                if (VL11M_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_37 = 1;
                    }
                    else LocoButtons.vl11_kontr_37 = 0;
                }
                if (VL11M_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_38 = 1;
                    }
                    else LocoButtons.vl11_kontr_38 = 0;
                }
                if (VL11M_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_39 = 1;
                    }
                    else LocoButtons.vl11_kontr_39 = 0;
                }
                if (VL11M_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_40 = 1;
                    }
                    else LocoButtons.vl11_kontr_40 = 0;
                }
                if (VL11M_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_41 = 1;
                    }
                    else LocoButtons.vl11_kontr_41 = 0;
                }
                if (VL11M_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_42 = 1;
                    }
                    else LocoButtons.vl11_kontr_42 = 0;
                }
                if (VL11M_key_buffer[46] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[46] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_43 = 1;
                    }
                    else LocoButtons.vl11_kontr_43 = 0;
                }
                if (VL11M_key_buffer[47] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[47] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_44 = 1;
                    }
                    else LocoButtons.vl11_kontr_44 = 0;
                }
                if (VL11M_key_buffer[48] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[48] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_45 = 1;
                    }
                    else LocoButtons.vl11_kontr_45 = 0;
                }
                if (VL11M_key_buffer[49] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[49] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_46 = 1;
                    }
                    else LocoButtons.vl11_kontr_46 = 0;
                }
                if (VL11M_key_buffer[50] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[50] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_47 = 1;
                    }
                    else LocoButtons.vl11_kontr_47 = 0;
                }
                if (VL11M_key_buffer[51] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[51] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_48 = 1;
                    }
                    else LocoButtons.vl11_kontr_48 = 0;
                }
                if (VL11M_key_buffer[52] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[52] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_shunt_0 = 1;
                    }
                    else LocoButtons.vl11_kontr_shunt_0 = 0;
                }
                if (VL11M_key_buffer[53] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[53] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_shunt_1 = 1;
                    }
                    else LocoButtons.vl11_kontr_shunt_1 = 0;
                }
                if (VL11M_key_buffer[54] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[54] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_shunt_2 = 1;
                    }
                    else LocoButtons.vl11_kontr_shunt_2 = 0;
                }
                if (VL11M_key_buffer[55] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[55] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_shunt_3 = 1;
                    }
                    else LocoButtons.vl11_kontr_shunt_3 = 0;
                }
                if (VL11M_key_buffer[56] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[56] - 1] != 0)
                    {
                        LocoButtons.vl11_kontr_shunt_4 = 1;
                    }
                    else LocoButtons.vl11_kontr_shunt_4 = 0;
                }
                if (VL11M_key_buffer[57] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[57] - 1] != 0)
                    {
                        LocoButtons.vl11_kranTM_0 = 1;
                    }
                    else LocoButtons.vl11_kranTM_0 = 0;
                }
                if (VL11M_key_buffer[58] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[58] - 1] != 0)
                    {
                        LocoButtons.vl11_kranTM_1 = 1;
                    }
                    else LocoButtons.vl11_kranTM_1 = 0;
                }
                if (VL11M_key_buffer[59] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[59] - 1] != 0)
                    {
                        LocoButtons.vl11_tokopr_obshiy_0 = 1;
                    }
                    else LocoButtons.vl11_tokopr_obshiy_0 = 0;
                }
                if (VL11M_key_buffer[60] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[60] - 1] != 0)
                    {
                        LocoButtons.vl11_tokopr_obshiy_1 = 1;
                    }
                    else LocoButtons.vl11_tokopr_obshiy_1 = 0;
                }
                if (VL11M_key_buffer[61] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[61] - 1] != 0)
                    {
                        LocoButtons.vl11_tokopr_per_0 = 1;
                    }
                    else LocoButtons.vl11_tokopr_per_0 = 0;
                }
                if (VL11M_key_buffer[62] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[62] - 1] != 0)
                    {
                        LocoButtons.vl11_tokopr_per_1 = 1;
                    }
                    else LocoButtons.vl11_tokopr_per_1 = 0;
                }
                if (VL11M_key_buffer[63] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[63] - 1] != 0)
                    {
                        LocoButtons.vl11_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.vl11_tokopr_zad_0 = 0;
                }
                if (VL11M_key_buffer[64] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[64] - 1] != 0)
                    {
                        LocoButtons.vl11_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.vl11_tokopr_zad_1 = 0;
                }
                if (VL11M_key_buffer[65] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[65] - 1] != 0)
                    {
                        LocoButtons.vl11_bv_0 = 1;
                    }
                    else LocoButtons.vl11_bv_0 = 0;
                }
                if (VL11M_key_buffer[66] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[66] - 1] != 0)
                    {
                        LocoButtons.vl11_bv_1 = 1;
                    }
                    else LocoButtons.vl11_bv_1 = 0;
                }
                if (VL11M_key_buffer[67] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[67] - 1] != 0)
                    {
                        LocoButtons.vl11_vosst_bv = 1;
                    }
                    else LocoButtons.vl11_vosst_bv = 0;
                }
                if (VL11M_key_buffer[68] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[68] - 1] != 0)
                    {
                        LocoButtons.vl11_komp_0 = 1;
                    }
                    else LocoButtons.vl11_komp_0 = 0;
                }
                if (VL11M_key_buffer[69] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[69] - 1] != 0)
                    {
                        LocoButtons.vl11_komp_1 = 1;
                    }
                    else LocoButtons.vl11_komp_1 = 0;
                }
                if (VL11M_key_buffer[70] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[70] - 1] != 0)
                    {
                        LocoButtons.vl11_vent_0 = 1;
                    }
                    else LocoButtons.vl11_vent_0 = 0;
                }
                if (VL11M_key_buffer[71] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[71] - 1] != 0)
                    {
                        LocoButtons.vl11_vent_1 = 1;
                    }
                    else LocoButtons.vl11_vent_1 = 0;
                }
                if (VL11M_key_buffer[72] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[72] - 1] != 0)
                    {
                        LocoButtons.vl11_vent_2 = 1;
                    }
                    else LocoButtons.vl11_vent_2 = 0;
                }
                if (VL11M_key_buffer[73] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[73] - 1] != 0)
                    {
                        LocoButtons.vl11_svet_cab_0 = 1;
                    }
                    else LocoButtons.vl11_svet_cab_0 = 0;
                }
                if (VL11M_key_buffer[74] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[74] - 1] != 0)
                    {
                        LocoButtons.vl11_svet_cab_1 = 1;
                    }
                    else LocoButtons.vl11_svet_cab_1 = 0;
                }
                if (VL11M_key_buffer[75] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[75] - 1] != 0)
                    {
                        LocoButtons.vl11_svet_cab_2 = 1;
                    }
                    else LocoButtons.vl11_svet_cab_2 = 0;
                }
                if (VL11M_key_buffer[76] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[76] - 1] != 0)
                    {
                        LocoButtons.vl11_EPK_0 = 1;
                    }
                    else LocoButtons.vl11_EPK_0 = 0;
                }
                if (VL11M_key_buffer[77] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[77] - 1] != 0)
                    {
                        LocoButtons.vl11_EPK_1 = 1;
                    }
                    else LocoButtons.vl11_EPK_1 = 0;
                }
                if (VL11M_key_buffer[78] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[78] - 1] != 0)
                    {
                        LocoButtons.vl11_prozh_0 = 1;
                    }
                    else LocoButtons.vl11_prozh_0 = 0;
                }
                if (VL11M_key_buffer[79] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[79] - 1] != 0)
                    {
                        LocoButtons.vl11_prozh_1 = 1;
                    }
                    else LocoButtons.vl11_prozh_1 = 0;
                }
                if (VL11M_key_buffer[80] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[80] - 1] != 0)
                    {
                        LocoButtons.vl11_prozh_2 = 1;
                    }
                    else LocoButtons.vl11_prozh_2 = 0;
                }
                if (VL11M_key_buffer[81] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[81] - 1] != 0)
                    {
                        LocoButtons.vl11_sign_0 = 1;
                    }
                    else LocoButtons.vl11_sign_0 = 0;
                }
                if (VL11M_key_buffer[82] != 0)
                {
                    if (joystick_buttons_buffer[VL11M_key_buffer[82] - 1] != 0)
                    {
                        LocoButtons.vl11_sign_1 = 1;
                    }
                    else LocoButtons.vl11_sign_1 = 0;
                }
            }

            //проверяем кнопки в буфере VL82M
            if (Loco.sig_loco == 10)
            {
                if (VL82M_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.vl82_rev_0 = 1;
                    }
                    else LocoButtons.vl82_rev_0 = 0;
                }
                if (VL82M_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.vl82_rev_vpered = 1;
                    }
                    else LocoButtons.vl82_rev_vpered = 0;
                }
                if (VL82M_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.vl82_rev_nazad = 1;
                    }
                    else LocoButtons.vl82_rev_nazad = 0;
                }
                if (VL82M_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_bv = 1;
                    }
                    else LocoButtons.vl82_kontr_bv = 0;
                }
                if (VL82M_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_0 = 1;
                    }
                    else LocoButtons.vl82_kontr_0 = 0;
                }
                if (VL82M_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_1 = 1;
                    }
                    else LocoButtons.vl82_kontr_1 = 0;
                }
                if (VL82M_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_2 = 1;
                    }
                    else LocoButtons.vl82_kontr_2 = 0;
                }
                if (VL82M_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_3 = 1;
                    }
                    else LocoButtons.vl82_kontr_3 = 0;
                }
                if (VL82M_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_4 = 1;
                    }
                    else LocoButtons.vl82_kontr_4 = 0;
                }
                if (VL82M_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_5 = 1;
                    }
                    else LocoButtons.vl82_kontr_5 = 0;
                }
                if (VL82M_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_6 = 1;
                    }
                    else LocoButtons.vl82_kontr_6 = 0;
                }
                if (VL82M_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_7 = 1;
                    }
                    else LocoButtons.vl82_kontr_7 = 0;
                }
                if (VL82M_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_8 = 1;
                    }
                    else LocoButtons.vl82_kontr_8 = 0;
                }
                if (VL82M_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_9 = 1;
                    }
                    else LocoButtons.vl82_kontr_9 = 0;
                }
                if (VL82M_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_10 = 1;
                    }
                    else LocoButtons.vl82_kontr_10 = 0;
                }
                if (VL82M_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_11 = 1;
                    }
                    else LocoButtons.vl82_kontr_11 = 0;
                }
                if (VL82M_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_12 = 1;
                    }
                    else LocoButtons.vl82_kontr_12 = 0;
                }
                if (VL82M_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_13 = 1;
                    }
                    else LocoButtons.vl82_kontr_13 = 0;
                }
                if (VL82M_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_14 = 1;
                    }
                    else LocoButtons.vl82_kontr_14 = 0;
                }
                if (VL82M_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_15 = 1;
                    }
                    else LocoButtons.vl82_kontr_15 = 0;
                }
                if (VL82M_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_16 = 1;
                    }
                    else LocoButtons.vl82_kontr_16 = 0;
                }
                if (VL82M_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_17 = 1;
                    }
                    else LocoButtons.vl82_kontr_17 = 0;
                }
                if (VL82M_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_18 = 1;
                    }
                    else LocoButtons.vl82_kontr_18 = 0;
                }
                if (VL82M_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_19 = 1;
                    }
                    else LocoButtons.vl82_kontr_19 = 0;
                }
                if (VL82M_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_20 = 1;
                    }
                    else LocoButtons.vl82_kontr_20 = 0;
                }
                if (VL82M_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_21 = 1;
                    }
                    else LocoButtons.vl82_kontr_21 = 0;
                }
                if (VL82M_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_22 = 1;
                    }
                    else LocoButtons.vl82_kontr_22 = 0;
                }
                if (VL82M_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_23 = 1;
                    }
                    else LocoButtons.vl82_kontr_23 = 0;
                }
                if (VL82M_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_24 = 1;
                    }
                    else LocoButtons.vl82_kontr_24 = 0;
                }
                if (VL82M_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_25 = 1;
                    }
                    else LocoButtons.vl82_kontr_25 = 0;
                }
                if (VL82M_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_26 = 1;
                    }
                    else LocoButtons.vl82_kontr_26 = 0;
                }
                if (VL82M_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_27 = 1;
                    }
                    else LocoButtons.vl82_kontr_27 = 0;
                }
                if (VL82M_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_28 = 1;
                    }
                    else LocoButtons.vl82_kontr_28 = 0;
                }
                if (VL82M_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_29 = 1;
                    }
                    else LocoButtons.vl82_kontr_29 = 0;
                }
                if (VL82M_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_30 = 1;
                    }
                    else LocoButtons.vl82_kontr_30 = 0;
                }
                if (VL82M_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_31 = 1;
                    }
                    else LocoButtons.vl82_kontr_31 = 0;
                }
                if (VL82M_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_32 = 1;
                    }
                    else LocoButtons.vl82_kontr_32 = 0;
                }
                if (VL82M_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_33 = 1;
                    }
                    else LocoButtons.vl82_kontr_33 = 0;
                }
                if (VL82M_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_34 = 1;
                    }
                    else LocoButtons.vl82_kontr_34 = 0;
                }
                if (VL82M_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_35 = 1;
                    }
                    else LocoButtons.vl82_kontr_35 = 0;
                }
                if (VL82M_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_36 = 1;
                    }
                    else LocoButtons.vl82_kontr_36 = 0;
                }
                if (VL82M_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_37 = 1;
                    }
                    else LocoButtons.vl82_kontr_37 = 0;
                }
                if (VL82M_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_38 = 1;
                    }
                    else LocoButtons.vl82_kontr_38 = 0;
                }
                if (VL82M_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_shunt_0 = 1;
                    }
                    else LocoButtons.vl82_kontr_shunt_0 = 0;
                }
                if (VL82M_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_shunt_1 = 1;
                    }
                    else LocoButtons.vl82_kontr_shunt_1 = 0;
                }
                if (VL82M_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_shunt_2 = 1;
                    }
                    else LocoButtons.vl82_kontr_shunt_2 = 0;
                }
                if (VL82M_key_buffer[46] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[46] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_shunt_3 = 1;
                    }
                    else LocoButtons.vl82_kontr_shunt_3 = 0;
                }
                if (VL82M_key_buffer[47] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[47] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_shunt_4 = 1;
                    }
                    else LocoButtons.vl82_kontr_shunt_4 = 0;
                }
                if (VL82M_key_buffer[48] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[48] - 1] != 0)
                    {
                        LocoButtons.vl82_kontr_shunt_reostat = 1;
                    }
                    else LocoButtons.vl82_kontr_shunt_reostat = 0;
                }
                if (VL82M_key_buffer[49] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[49] - 1] != 0)
                    {
                        LocoButtons.vl82_kranTM_0 = 1;
                    }
                    else LocoButtons.vl82_kranTM_0 = 0;
                }
                if (VL82M_key_buffer[50] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[50] - 1] != 0)
                    {
                        LocoButtons.vl82_kranTM_1 = 1;
                    }
                    else LocoButtons.vl82_kranTM_1 = 0;
                }
                if (VL82M_key_buffer[51] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[51] - 1] != 0)
                    {
                        LocoButtons.vl82_tokopr_obshiy_0 = 1;
                    }
                    else LocoButtons.vl82_tokopr_obshiy_0 = 0;
                }
                if (VL82M_key_buffer[52] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[52] - 1] != 0)
                    {
                        LocoButtons.vl82_tokopr_obshiy_1 = 1;
                    }
                    else LocoButtons.vl82_tokopr_obshiy_1 = 0;
                }
                if (VL82M_key_buffer[53] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[53] - 1] != 0)
                    {
                        LocoButtons.vl82_tokopr_per_0 = 1;
                    }
                    else LocoButtons.vl82_tokopr_per_0 = 0;
                }
                if (VL82M_key_buffer[54] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[54] - 1] != 0)
                    {
                        LocoButtons.vl82_tokopr_per_1 = 1;
                    }
                    else LocoButtons.vl82_tokopr_per_1 = 0;
                }
                if (VL82M_key_buffer[55] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[55] - 1] != 0)
                    {
                        LocoButtons.vl82_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.vl82_tokopr_zad_0 = 0;
                }
                if (VL82M_key_buffer[56] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[56] - 1] != 0)
                    {
                        LocoButtons.vl82_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.vl82_tokopr_zad_1 = 0;
                }
                if (VL82M_key_buffer[57] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[57] - 1] != 0)
                    {
                        LocoButtons.vl82_gv_0 = 1;
                    }
                    else LocoButtons.vl82_gv_0 = 0;
                }
                if (VL82M_key_buffer[58] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[58] - 1] != 0)
                    {
                        LocoButtons.vl82_gv_1 = 1;
                    }
                    else LocoButtons.vl82_gv_1 = 0;
                }
                if (VL82M_key_buffer[59] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[59] - 1] != 0)
                    {
                        LocoButtons.vl82_bv_0 = 1;
                    }
                    else LocoButtons.vl82_bv_0 = 0;
                }
                if (VL82M_key_buffer[60] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[60] - 1] != 0)
                    {
                        LocoButtons.vl82_bv_1 = 1;
                    }
                    else LocoButtons.vl82_bv_1 = 0;
                }
                if (VL82M_key_buffer[61] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[61] - 1] != 0)
                    {
                        LocoButtons.vl82_vosst_gv = 1;
                    }
                    else LocoButtons.vl82_vosst_gv = 0;
                }
                if (VL82M_key_buffer[62] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[62] - 1] != 0)
                    {
                        LocoButtons.vl82_komp_0 = 1;
                    }
                    else LocoButtons.vl82_komp_0 = 0;
                }
                if (VL82M_key_buffer[63] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[63] - 1] != 0)
                    {
                        LocoButtons.vl82_komp_1 = 1;
                    }
                    else LocoButtons.vl82_komp_1 = 0;
                }
                if (VL82M_key_buffer[64] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[64] - 1] != 0)
                    {
                        LocoButtons.vl82_vent1_0 = 1;
                    }
                    else LocoButtons.vl82_vent1_0 = 0;
                }
                if (VL82M_key_buffer[65] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[65] - 1] != 0)
                    {
                        LocoButtons.vl82_vent1_1 = 1;
                    }
                    else LocoButtons.vl82_vent1_1 = 0;
                }
                if (VL82M_key_buffer[66] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[66] - 1] != 0)
                    {
                        LocoButtons.vl82_vent2_0 = 1;
                    }
                    else LocoButtons.vl82_vent2_0 = 0;
                }
                if (VL82M_key_buffer[67] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[67] - 1] != 0)
                    {
                        LocoButtons.vl82_vent2_1 = 1;
                    }
                    else LocoButtons.vl82_vent2_1 = 0;
                }
                if (VL82M_key_buffer[68] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[68] - 1] != 0)
                    {
                        LocoButtons.vl82_kvc_0 = 1;
                    }
                    else LocoButtons.vl82_kvc_0 = 0;
                }
                if (VL82M_key_buffer[69] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[69] - 1] != 0)
                    {
                        LocoButtons.vl82_kvc_1 = 1;
                    }
                    else LocoButtons.vl82_kvc_1 = 0;
                }
                if (VL82M_key_buffer[70] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[70] - 1] != 0)
                    {
                        LocoButtons.vl82_vozvr_kvc = 1;
                    }
                    else LocoButtons.vl82_vozvr_kvc = 0;
                }
                if (VL82M_key_buffer[71] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[71] - 1] != 0)
                    {
                        LocoButtons.vl82_upravlenie_0 = 1;
                    }
                    else LocoButtons.vl82_upravlenie_0 = 0;
                }
                if (VL82M_key_buffer[72] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[72] - 1] != 0)
                    {
                        LocoButtons.vl82_upravlenie_1 = 1;
                    }
                    else LocoButtons.vl82_upravlenie_1 = 0;
                }
                if (VL82M_key_buffer[73] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[73] - 1] != 0)
                    {
                        LocoButtons.vl82_svet_cab_0 = 1;
                    }
                    else LocoButtons.vl82_svet_cab_0 = 0;
                }
                if (VL82M_key_buffer[74] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[74] - 1] != 0)
                    {
                        LocoButtons.vl82_svet_cab_1 = 1;
                    }
                    else LocoButtons.vl82_svet_cab_1 = 0;
                }
                if (VL82M_key_buffer[75] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[75] - 1] != 0)
                    {
                        LocoButtons.vl82_svet_cab_2 = 1;
                    }
                    else LocoButtons.vl82_svet_cab_2 = 0;
                }
                if (VL82M_key_buffer[76] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[76] - 1] != 0)
                    {
                        LocoButtons.vl82_EPK_0 = 1;
                    }
                    else LocoButtons.vl82_EPK_0 = 0;
                }
                if (VL82M_key_buffer[77] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[77] - 1] != 0)
                    {
                        LocoButtons.vl82_EPK_1 = 1;
                    }
                    else LocoButtons.vl82_EPK_1 = 0;
                }
                if (VL82M_key_buffer[78] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[78] - 1] != 0)
                    {
                        LocoButtons.vl82_prozh_0 = 1;
                    }
                    else LocoButtons.vl82_prozh_0 = 0;
                }
                if (VL82M_key_buffer[79] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[79] - 1] != 0)
                    {
                        LocoButtons.vl82_prozh_1 = 1;
                    }
                    else LocoButtons.vl82_prozh_1 = 0;
                }
                if (VL82M_key_buffer[80] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[80] - 1] != 0)
                    {
                        LocoButtons.vl82_prozh_2 = 1;
                    }
                    else LocoButtons.vl82_prozh_2 = 0;
                }
                if (VL82M_key_buffer[81] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[81] - 1] != 0)
                    {
                        LocoButtons.vl82_sign_0 = 1;
                    }
                    else LocoButtons.vl82_sign_0 = 0;
                }
                if (VL82M_key_buffer[82] != 0)
                {
                    if (joystick_buttons_buffer[VL82M_key_buffer[82] - 1] != 0)
                    {
                        LocoButtons.vl82_sign_1 = 1;
                    }
                    else LocoButtons.vl82_sign_1 = 0;
                }
            }

            //проверяем кнопки в буфере VL80T
            if (Loco.sig_loco == 11)
            {
                if (VL80T_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.vl80t_rev_0 = 1;
                    }
                    else LocoButtons.vl80t_rev_0 = 0;
                }
                if (VL80T_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.vl80t_rev_vpered = 1;
                    }
                    else LocoButtons.vl80t_rev_vpered = 0;
                }
                if (VL80T_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.vl80t_rev_nazad = 1;
                    }
                    else LocoButtons.vl80t_rev_nazad = 0;
                }
                if (VL80T_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.vl80t_rev_shunt1 = 1;
                    }
                    else LocoButtons.vl80t_rev_shunt1 = 0;
                }
                if (VL80T_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.vl80t_rev_shunt2 = 1;
                    }
                    else LocoButtons.vl80t_rev_shunt2 = 0;
                }
                if (VL80T_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.vl80t_rev_shunt3 = 1;
                    }
                    else LocoButtons.vl80t_rev_shunt3 = 0;
                }
                if (VL80T_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.vl80t_kontr_bv = 1;
                    }
                    else LocoButtons.vl80t_kontr_bv = 0;
                }
                if (VL80T_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.vl80t_kontr_0 = 1;
                    }
                    else LocoButtons.vl80t_kontr_0 = 0;
                }
                if (VL80T_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.vl80t_kontr_1 = 1;
                    }
                    else LocoButtons.vl80t_kontr_1 = 0;
                }
                if (VL80T_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.vl80t_kontr_2 = 1;
                    }
                    else LocoButtons.vl80t_kontr_2 = 0;
                }
                if (VL80T_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.vl80t_kontr_3 = 1;
                    }
                    else LocoButtons.vl80t_kontr_3 = 0;
                }
                if (VL80T_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.vl80t_kontr_4 = 1;
                    }
                    else LocoButtons.vl80t_kontr_4 = 0;
                }
                if (VL80T_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.vl80t_kontr_5 = 1;
                    }
                    else LocoButtons.vl80t_kontr_5 = 0;
                }
                if (VL80T_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.vl80t_kontr_6 = 1;
                    }
                    else LocoButtons.vl80t_kontr_6 = 0;
                }
                if (VL80T_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.vl80t_kranTM_0 = 1;
                    }
                    else LocoButtons.vl80t_kranTM_0 = 0;
                }
                if (VL80T_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.vl80t_kranTM_1 = 1;
                    }
                    else LocoButtons.vl80t_kranTM_1 = 0;
                }
                if (VL80T_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.vl80t_tokopr_obshiy_0 = 1;
                    }
                    else LocoButtons.vl80t_tokopr_obshiy_0 = 0;
                }
                if (VL80T_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.vl80t_tokopr_obshiy_1 = 1;
                    }
                    else LocoButtons.vl80t_tokopr_obshiy_1 = 0;
                }
                if (VL80T_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.vl80t_tokopr_per_0 = 1;
                    }
                    else LocoButtons.vl80t_tokopr_per_0 = 0;
                }
                if (VL80T_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.vl80t_tokopr_per_1 = 1;
                    }
                    else LocoButtons.vl80t_tokopr_per_1 = 0;
                }
                if (VL80T_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.vl80t_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.vl80t_tokopr_zad_0 = 0;
                }
                if (VL80T_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.vl80t_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.vl80t_tokopr_zad_1 = 0;
                }
                if (VL80T_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.vl80t_gv_0 = 1;
                    }
                    else LocoButtons.vl80t_gv_0 = 0;
                }
                if (VL80T_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.vl80t_gv_1 = 1;
                    }
                    else LocoButtons.vl80t_gv_1 = 0;
                }
                if (VL80T_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.vl80t_vosst_gv = 1;
                    }
                    else LocoButtons.vl80t_vosst_gv = 0;
                }
                if (VL80T_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.vl80t_komp_0 = 1;
                    }
                    else LocoButtons.vl80t_komp_0 = 0;
                }
                if (VL80T_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.vl80t_komp_1 = 1;
                    }
                    else LocoButtons.vl80t_komp_1 = 0;
                }
                if (VL80T_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.vl80t_vent1_0 = 1;
                    }
                    else LocoButtons.vl80t_vent1_0 = 0;
                }
                if (VL80T_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.vl80t_vent1_1 = 1;
                    }
                    else LocoButtons.vl80t_vent1_1 = 0;
                }
                if (VL80T_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.vl80t_vent2_0 = 1;
                    }
                    else LocoButtons.vl80t_vent2_0 = 0;
                }
                if (VL80T_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.vl80t_vent2_1 = 1;
                    }
                    else LocoButtons.vl80t_vent2_1 = 0;
                }
                if (VL80T_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.vl80t_vent3_0 = 1;
                    }
                    else LocoButtons.vl80t_vent3_0 = 0;
                }
                if (VL80T_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.vl80t_vent3_1 = 1;
                    }
                    else LocoButtons.vl80t_vent3_1 = 0;
                }
                if (VL80T_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.vl80t_vent4_0 = 1;
                    }
                    else LocoButtons.vl80t_vent4_0 = 0;
                }
                if (VL80T_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.vl80t_vent4_1 = 1;
                    }
                    else LocoButtons.vl80t_vent4_1 = 0;
                }
                if (VL80T_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.vl80t_fz_0 = 1;
                    }
                    else LocoButtons.vl80t_fz_0 = 0;
                }
                if (VL80T_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.vl80t_fz_1 = 1;
                    }
                    else LocoButtons.vl80t_fz_1 = 0;
                }
                if (VL80T_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.vl80t_upravlenie_0 = 1;
                    }
                    else LocoButtons.vl80t_upravlenie_0 = 0;
                }
                if (VL80T_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.vl80t_upravlenie_1 = 1;
                    }
                    else LocoButtons.vl80t_upravlenie_1 = 0;
                }
                if (VL80T_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.vl80t_svet_cab_0 = 1;
                    }
                    else LocoButtons.vl80t_svet_cab_0 = 0;
                }
                if (VL80T_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.vl80t_svet_cab_1 = 1;
                    }
                    else LocoButtons.vl80t_svet_cab_1 = 0;
                }
                if (VL80T_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.vl80t_svet_cab_2 = 1;
                    }
                    else LocoButtons.vl80t_svet_cab_2 = 0;
                }
                if (VL80T_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.vl80t_EPK_0 = 1;
                    }
                    else LocoButtons.vl80t_EPK_0 = 0;
                }
                if (VL80T_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.vl80t_EPK_1 = 1;
                    }
                    else LocoButtons.vl80t_EPK_1 = 0;
                }
                if (VL80T_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.vl80t_prozh_0 = 1;
                    }
                    else LocoButtons.vl80t_prozh_0 = 0;
                }
                if (VL80T_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.vl80t_prozh_1 = 1;
                    }
                    else LocoButtons.vl80t_prozh_1 = 0;
                }
                if (VL80T_key_buffer[46] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[46] - 1] != 0)
                    {
                        LocoButtons.vl80t_prozh_2 = 1;
                    }
                    else LocoButtons.vl80t_prozh_2 = 0;
                }
                if (VL80T_key_buffer[47] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[47] - 1] != 0)
                    {
                        LocoButtons.vl80t_sign_0 = 1;
                    }
                    else LocoButtons.vl80t_sign_0 = 0;
                }
                if (VL80T_key_buffer[48] != 0)
                {
                    if (joystick_buttons_buffer[VL80T_key_buffer[48] - 1] != 0)
                    {
                        LocoButtons.vl80t_sign_1 = 1;
                    }
                    else LocoButtons.vl80t_sign_1 = 0;
                }
            }

            //проверяем кнопки в буфере VL85
            if (Loco.sig_loco == 12)
            {
                if (VL85_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.vl85_rev_0 = 1;
                    }
                    else LocoButtons.vl85_rev_0 = 0;
                }
                if (VL85_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.vl85_rev_vpered = 1;
                    }
                    else LocoButtons.vl85_rev_vpered = 0;
                }
                if (VL85_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.vl85_rev_nazad = 1;
                    }
                    else LocoButtons.vl85_rev_nazad = 0;
                }
                if (VL85_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.vl85_rev_shunt1 = 1;
                    }
                    else LocoButtons.vl85_rev_shunt1 = 0;
                }
                if (VL85_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.vl85_rev_shunt2 = 1;
                    }
                    else LocoButtons.vl85_rev_shunt2 = 0;
                }
                if (VL85_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.vl85_rev_shunt3 = 1;
                    }
                    else LocoButtons.vl85_rev_shunt3 = 0;
                }
                if (VL85_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_bv = 1;
                    }
                    else LocoButtons.vl85_kontr_bv = 0;
                }
                if (VL85_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_0 = 1;
                    }
                    else LocoButtons.vl85_kontr_0 = 0;
                }
                if (VL85_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_1 = 1;
                    }
                    else LocoButtons.vl85_kontr_1 = 0;
                }
                if (VL85_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_2 = 1;
                    }
                    else LocoButtons.vl85_kontr_2 = 0;
                }
                if (VL85_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_3 = 1;
                    }
                    else LocoButtons.vl85_kontr_3 = 0;
                }
                if (VL85_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_4 = 1;
                    }
                    else LocoButtons.vl85_kontr_4 = 0;
                }
                if (VL85_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_5 = 1;
                    }
                    else LocoButtons.vl85_kontr_5 = 0;
                }
                if (VL85_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_6 = 1;
                    }
                    else LocoButtons.vl85_kontr_6 = 0;
                }
                if (VL85_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_7 = 1;
                    }
                    else LocoButtons.vl85_kontr_7 = 0;
                }
                if (VL85_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_8 = 1;
                    }
                    else LocoButtons.vl85_kontr_8 = 0;
                }
                if (VL85_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_9 = 1;
                    }
                    else LocoButtons.vl85_kontr_9 = 0;
                }
                if (VL85_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_10 = 1;
                    }
                    else LocoButtons.vl85_kontr_10 = 0;
                }
                if (VL85_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_11 = 1;
                    }
                    else LocoButtons.vl85_kontr_11 = 0;
                }
                if (VL85_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_12 = 1;
                    }
                    else LocoButtons.vl85_kontr_12 = 0;
                }
                if (VL85_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_13 = 1;
                    }
                    else LocoButtons.vl85_kontr_13 = 0;
                }
                if (VL85_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_14 = 1;
                    }
                    else LocoButtons.vl85_kontr_14 = 0;
                }
                if (VL85_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_15 = 1;
                    }
                    else LocoButtons.vl85_kontr_15 = 0;
                }
                if (VL85_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_16 = 1;
                    }
                    else LocoButtons.vl85_kontr_16 = 0;
                }
                if (VL85_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_17 = 1;
                    }
                    else LocoButtons.vl85_kontr_17 = 0;
                }
                if (VL85_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_18 = 1;
                    }
                    else LocoButtons.vl85_kontr_18 = 0;
                }
                if (VL85_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_19 = 1;
                    }
                    else LocoButtons.vl85_kontr_19 = 0;
                }
                if (VL85_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_20 = 1;
                    }
                    else LocoButtons.vl85_kontr_20 = 0;
                }
                if (VL85_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_21 = 1;
                    }
                    else LocoButtons.vl85_kontr_21 = 0;
                }
                if (VL85_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_22 = 1;
                    }
                    else LocoButtons.vl85_kontr_22 = 0;
                }
                if (VL85_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_23 = 1;
                    }
                    else LocoButtons.vl85_kontr_23 = 0;
                }
                if (VL85_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_24 = 1;
                    }
                    else LocoButtons.vl85_kontr_24 = 0;
                }
                if (VL85_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_25 = 1;
                    }
                    else LocoButtons.vl85_kontr_25 = 0;
                }
                if (VL85_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_26 = 1;
                    }
                    else LocoButtons.vl85_kontr_26 = 0;
                }
                if (VL85_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_27 = 1;
                    }
                    else LocoButtons.vl85_kontr_27 = 0;
                }
                if (VL85_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_28 = 1;
                    }
                    else LocoButtons.vl85_kontr_28 = 0;
                }
                if (VL85_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_29 = 1;
                    }
                    else LocoButtons.vl85_kontr_29 = 0;
                }
                if (VL85_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_30 = 1;
                    }
                    else LocoButtons.vl85_kontr_30 = 0;
                }
                if (VL85_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_31 = 1;
                    }
                    else LocoButtons.vl85_kontr_31 = 0;
                }
                if (VL85_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.vl85_kontr_32 = 1;
                    }
                    else LocoButtons.vl85_kontr_32 = 0;
                }
                if (VL85_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.vl85_kranTM_0 = 1;
                    }
                    else LocoButtons.vl85_kranTM_0 = 0;
                }
                if (VL85_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.vl85_kranTM_1 = 1;
                    }
                    else LocoButtons.vl85_kranTM_1 = 0;
                }
                if (VL85_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.vl85_tokopr_obshiy_0 = 1;
                    }
                    else LocoButtons.vl85_tokopr_obshiy_0 = 0;
                }
                if (VL85_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.vl85_tokopr_obshiy_1 = 1;
                    }
                    else LocoButtons.vl85_tokopr_obshiy_1 = 0;
                }
                if (VL85_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.vl85_tokopr_per_0 = 1;
                    }
                    else LocoButtons.vl85_tokopr_per_0 = 0;
                }
                if (VL85_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.vl85_tokopr_per_1 = 1;
                    }
                    else LocoButtons.vl85_tokopr_per_1 = 0;
                }
                if (VL85_key_buffer[46] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[46] - 1] != 0)
                    {
                        LocoButtons.vl85_tokopr_zad_0 = 1;
                    }
                    else LocoButtons.vl85_tokopr_zad_0 = 0;
                }
                if (VL85_key_buffer[47] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[47] - 1] != 0)
                    {
                        LocoButtons.vl85_tokopr_zad_1 = 1;
                    }
                    else LocoButtons.vl85_tokopr_zad_1 = 0;
                }
                if (VL85_key_buffer[48] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[48] - 1] != 0)
                    {
                        LocoButtons.vl85_gv_0 = 1;
                    }
                    else LocoButtons.vl85_gv_0 = 0;
                }
                if (VL85_key_buffer[49] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[49] - 1] != 0)
                    {
                        LocoButtons.vl85_gv_1 = 1;
                    }
                    else LocoButtons.vl85_gv_1 = 0;
                }
                if (VL85_key_buffer[50] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[50] - 1] != 0)
                    {
                        LocoButtons.vl85_vosst_gv = 1;
                    }
                    else LocoButtons.vl85_vosst_gv = 0;
                }
                if (VL85_key_buffer[51] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[51] - 1] != 0)
                    {
                        LocoButtons.vl85_komp_0 = 1;
                    }
                    else LocoButtons.vl85_komp_0 = 0;
                }
                if (VL85_key_buffer[52] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[52] - 1] != 0)
                    {
                        LocoButtons.vl85_komp_1 = 1;
                    }
                    else LocoButtons.vl85_komp_1 = 0;
                }
                if (VL85_key_buffer[53] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[53] - 1] != 0)
                    {
                        LocoButtons.vl85_vent1_0 = 1;
                    }
                    else LocoButtons.vl85_vent1_0 = 0;
                }
                if (VL85_key_buffer[54] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[54] - 1] != 0)
                    {
                        LocoButtons.vl85_vent1_1 = 1;
                    }
                    else LocoButtons.vl85_vent1_1 = 0;
                }
                if (VL85_key_buffer[55] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[55] - 1] != 0)
                    {
                        LocoButtons.vl85_vent2_0 = 1;
                    }
                    else LocoButtons.vl85_vent2_0 = 0;
                }
                if (VL85_key_buffer[56] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[56] - 1] != 0)
                    {
                        LocoButtons.vl85_vent2_1 = 1;
                    }
                    else LocoButtons.vl85_vent2_1 = 0;
                }
                if (VL85_key_buffer[57] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[57] - 1] != 0)
                    {
                        LocoButtons.vl85_vent3_0 = 1;
                    }
                    else LocoButtons.vl85_vent3_0 = 0;
                }
                if (VL85_key_buffer[58] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[58] - 1] != 0)
                    {
                        LocoButtons.vl85_vent3_2 = 1;
                    }
                    else LocoButtons.vl85_vent3_2 = 0;
                }
                if (VL85_key_buffer[59] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[59] - 1] != 0)
                    {
                        LocoButtons.vl85_vent4_0 = 1;
                    }
                    else LocoButtons.vl85_vent4_0 = 0;
                }
                if (VL85_key_buffer[60] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[60] - 1] != 0)
                    {
                        LocoButtons.vl85_vent4_1 = 1;
                    }
                    else LocoButtons.vl85_vent4_1 = 0;
                }
                if (VL85_key_buffer[61] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[61] - 1] != 0)
                    {
                        LocoButtons.vl85_fz_0 = 1;
                    }
                    else LocoButtons.vl85_fz_0 = 0;
                }
                if (VL85_key_buffer[62] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[62] - 1] != 0)
                    {
                        LocoButtons.vl85_fz_1 = 1;
                    }
                    else LocoButtons.vl85_fz_1 = 0;
                }
                if (VL85_key_buffer[63] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[63] - 1] != 0)
                    {
                        LocoButtons.vl85_svet_cab_0 = 1;
                    }
                    else LocoButtons.vl85_svet_cab_0 = 0;
                }
                if (VL85_key_buffer[64] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[64] - 1] != 0)
                    {
                        LocoButtons.vl85_svet_cab_1 = 1;
                    }
                    else LocoButtons.vl85_svet_cab_1 = 0;
                }
                if (VL85_key_buffer[65] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[65] - 1] != 0)
                    {
                        LocoButtons.vl85_svet_cab_2 = 1;
                    }
                    else LocoButtons.vl85_svet_cab_2 = 0;
                }
                if (VL85_key_buffer[66] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[66] - 1] != 0)
                    {
                        LocoButtons.vl85_EPK_0 = 1;
                    }
                    else LocoButtons.vl85_EPK_0 = 0;
                }
                if (VL85_key_buffer[67] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[67] - 1] != 0)
                    {
                        LocoButtons.vl85_EPK_1 = 1;
                    }
                    else LocoButtons.vl85_EPK_1 = 0;
                }
                if (VL85_key_buffer[68] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[68] - 1] != 0)
                    {
                        LocoButtons.vl85_prozh_0 = 1;
                    }
                    else LocoButtons.vl85_prozh_0 = 0;
                }
                if (VL85_key_buffer[69] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[69] - 1] != 0)
                    {
                        LocoButtons.vl85_prozh_1 = 1;
                    }
                    else LocoButtons.vl85_prozh_1 = 0;
                }
                if (VL85_key_buffer[70] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[70] - 1] != 0)
                    {
                        LocoButtons.vl85_prozh_2 = 1;
                    }
                    else LocoButtons.vl85_prozh_2 = 0;
                }
                if (VL85_key_buffer[71] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[71] - 1] != 0)
                    {
                        LocoButtons.vl85_sign_0 = 1;
                    }
                    else LocoButtons.vl85_sign_0 = 0;
                }
                if (VL85_key_buffer[72] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[72] - 1] != 0)
                    {
                        LocoButtons.vl85_sign_1 = 1;
                    }
                    else LocoButtons.vl85_sign_1 = 0;
                }
                if (VL85_key_buffer[73] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[73] - 1] != 0)
                    {
                        LocoButtons.vl85_sign1_0 = 1;
                    }
                    else LocoButtons.vl85_sign1_0 = 0;
                }
                if (VL85_key_buffer[74] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[74] - 1] != 0)
                    {
                        LocoButtons.vl85_sign1_1 = 1;
                    }
                    else LocoButtons.vl85_sign1_1 = 0;
                }
                if (VL85_key_buffer[75] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[75] - 1] != 0)
                    {
                        LocoButtons.vl85_sign2_0 = 1;
                    }
                    else LocoButtons.vl85_sign2_0 = 0;
                }
                if (VL85_key_buffer[76] != 0)
                {
                    if (joystick_buttons_buffer[VL85_key_buffer[76] - 1] != 0)
                    {
                        LocoButtons.vl85_sign2_1 = 1;
                    }
                    else LocoButtons.vl85_sign2_1 = 0;
                }
            }

            //проверяем кнопки в буфере TEP70
            if (Loco.sig_loco == 13)
            {
                if (TEP70_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.tep70_rev_0 = 1;
                    }
                    else LocoButtons.tep70_rev_0 = 0;
                }
                if (TEP70_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.tep70_rev_vpered = 1;
                    }
                    else LocoButtons.tep70_rev_vpered = 0;
                }
                if (TEP70_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.tep70_rev_nazad = 1;
                    }
                    else LocoButtons.tep70_rev_nazad = 0;
                }
                if (TEP70_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_0 = 1;
                    }
                    else LocoButtons.tep70_kontr_0 = 0;
                }
                if (TEP70_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_1 = 1;
                    }
                    else LocoButtons.tep70_kontr_1 = 0;
                }
                if (TEP70_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_2 = 1;
                    }
                    else LocoButtons.tep70_kontr_2 = 0;
                }
                if (TEP70_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_3 = 1;
                    }
                    else LocoButtons.tep70_kontr_3 = 0;
                }
                if (TEP70_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_4 = 1;
                    }
                    else LocoButtons.tep70_kontr_4 = 0;
                }
                if (TEP70_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_5 = 1;
                    }
                    else LocoButtons.tep70_kontr_5 = 0;
                }
                if (TEP70_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_6 = 1;
                    }
                    else LocoButtons.tep70_kontr_6 = 0;
                }
                if (TEP70_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_7 = 1;
                    }
                    else LocoButtons.tep70_kontr_7 = 0;
                }
                if (TEP70_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_8 = 1;
                    }
                    else LocoButtons.tep70_kontr_8 = 0;
                }
                if (TEP70_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_9 = 1;
                    }
                    else LocoButtons.tep70_kontr_9 = 0;
                }
                if (TEP70_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_10 = 1;
                    }
                    else LocoButtons.tep70_kontr_10 = 0;
                }
                if (TEP70_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_11 = 1;
                    }
                    else LocoButtons.tep70_kontr_11 = 0;
                }
                if (TEP70_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_12 = 1;
                    }
                    else LocoButtons.tep70_kontr_12 = 0;
                }
                if (TEP70_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_13 = 1;
                    }
                    else LocoButtons.tep70_kontr_13 = 0;
                }
                if (TEP70_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_14 = 1;
                    }
                    else LocoButtons.tep70_kontr_14 = 0;
                }
                if (TEP70_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.tep70_kontr_15 = 1;
                    }
                    else LocoButtons.tep70_kontr_15 = 0;
                }
                if (TEP70_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.tep70_kranTM_0 = 1;
                    }
                    else LocoButtons.tep70_kranTM_0 = 0;
                }
                if (TEP70_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.tep70_kranTM_1 = 1;
                    }
                    else LocoButtons.tep70_kranTM_1 = 0;
                }
                if (TEP70_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.tep70_nasos_0 = 1;
                    }
                    else LocoButtons.tep70_nasos_0 = 0;
                }
                if (TEP70_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.tep70_nasos_1 = 1;
                    }
                    else LocoButtons.tep70_nasos_1 = 0;
                }
                if (TEP70_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.tep70_pusk = 1;
                    }
                    else LocoButtons.tep70_pusk = 0;
                }
                if (TEP70_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.tep70_upravlenie_0 = 1;
                    }
                    else LocoButtons.tep70_upravlenie_0 = 0;
                }
                if (TEP70_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.tep70_upravlenie_1 = 1;
                    }
                    else LocoButtons.tep70_upravlenie_1 = 0;
                }
                if (TEP70_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.tep70_svet_cab_0 = 1;
                    }
                    else LocoButtons.tep70_svet_cab_0 = 0;
                }
                if (TEP70_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.tep70_svet_cab_1 = 1;
                    }
                    else LocoButtons.tep70_svet_cab_1 = 0;
                }
                if (TEP70_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.tep70_svet_cab_2 = 1;
                    }
                    else LocoButtons.tep70_svet_cab_2 = 0;
                }
                if (TEP70_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.tep70_EPK_0 = 1;
                    }
                    else LocoButtons.tep70_EPK_0 = 0;
                }
                if (TEP70_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.tep70_EPK_1 = 1;
                    }
                    else LocoButtons.tep70_EPK_1 = 0;
                }
                if (TEP70_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.tep70_EPT_0 = 1;
                    }
                    else LocoButtons.tep70_EPT_0 = 0;
                }
                if (TEP70_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.tep70_EPT_1 = 1;
                    }
                    else LocoButtons.tep70_EPT_1 = 0;
                }
                if (TEP70_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.tep70_prozh_0 = 1;
                    }
                    else LocoButtons.tep70_prozh_0 = 0;
                }
                if (TEP70_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.tep70_prozh_1 = 1;
                    }
                    else LocoButtons.tep70_prozh_1 = 0;
                }
                if (TEP70_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[TEP70_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.tep70_prozh_2 = 1;
                    }
                    else LocoButtons.tep70_prozh_2 = 0;
                }
            }

            //проверяем кнопки в буфере TE10U
            if (Loco.sig_loco == 14)
            {
                if (TE10U_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.te10u_rev_0 = 1;
                    }
                    else LocoButtons.te10u_rev_0 = 0;
                }
                if (TE10U_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.te10u_rev_vpered = 1;
                    }
                    else LocoButtons.te10u_rev_vpered = 0;
                }
                if (TE10U_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.te10u_rev_nazad = 1;
                    }
                    else LocoButtons.te10u_rev_nazad = 0;
                }
                if (TE10U_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_0 = 1;
                    }
                    else LocoButtons.te10u_kontr_0 = 0;
                }
                if (TE10U_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_1 = 1;
                    }
                    else LocoButtons.te10u_kontr_1 = 0;
                }
                if (TE10U_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_2 = 1;
                    }
                    else LocoButtons.te10u_kontr_2 = 0;
                }
                if (TE10U_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_3 = 1;
                    }
                    else LocoButtons.te10u_kontr_3 = 0;
                }
                if (TE10U_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_4 = 1;
                    }
                    else LocoButtons.te10u_kontr_4 = 0;
                }
                if (TE10U_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_5 = 1;
                    }
                    else LocoButtons.te10u_kontr_5 = 0;
                }
                if (TE10U_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_6 = 1;
                    }
                    else LocoButtons.te10u_kontr_6 = 0;
                }
                if (TE10U_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_7 = 1;
                    }
                    else LocoButtons.te10u_kontr_7 = 0;
                }
                if (TE10U_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_8 = 1;
                    }
                    else LocoButtons.te10u_kontr_8 = 0;
                }
                if (TE10U_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_9 = 1;
                    }
                    else LocoButtons.te10u_kontr_9 = 0;
                }
                if (TE10U_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_10 = 1;
                    }
                    else LocoButtons.te10u_kontr_10 = 0;
                }
                if (TE10U_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_11 = 1;
                    }
                    else LocoButtons.te10u_kontr_11 = 0;
                }
                if (TE10U_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_12 = 1;
                    }
                    else LocoButtons.te10u_kontr_12 = 0;
                }
                if (TE10U_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_13 = 1;
                    }
                    else LocoButtons.te10u_kontr_13 = 0;
                }
                if (TE10U_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_14 = 1;
                    }
                    else LocoButtons.te10u_kontr_14 = 0;
                }
                if (TE10U_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.te10u_kontr_15 = 1;
                    }
                    else LocoButtons.te10u_kontr_15 = 0;
                }
                if (TE10U_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.te10u_kranTM_0 = 1;
                    }
                    else LocoButtons.te10u_kranTM_0 = 0;
                }
                if (TE10U_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.te10u_kranTM_1 = 1;
                    }
                    else LocoButtons.te10u_kranTM_1 = 0;
                }
                if (TE10U_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.te10u_nasos1_0 = 1;
                    }
                    else LocoButtons.te10u_nasos1_0 = 0;
                }
                if (TE10U_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.te10u_nasos1_1 = 1;
                    }
                    else LocoButtons.te10u_nasos1_1 = 0;
                }
                if (TE10U_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.te10u_nasos2_0 = 1;
                    }
                    else LocoButtons.te10u_nasos2_0 = 0;
                }
                if (TE10U_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.te10u_nasos2_1 = 1;
                    }
                    else LocoButtons.te10u_nasos2_1 = 0;
                }
                if (TE10U_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.te10u_pusk1 = 1;
                    }
                    else LocoButtons.te10u_pusk1 = 0;
                }
                if (TE10U_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.te10u_pusk2 = 1;
                    }
                    else LocoButtons.te10u_pusk2 = 0;
                }
                if (TE10U_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.te10u_upravlenie_0 = 1;
                    }
                    else LocoButtons.te10u_upravlenie_0 = 0;
                }
                if (TE10U_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.te10u_upravlenie_1 = 1;
                    }
                    else LocoButtons.te10u_upravlenie_1 = 0;
                }
                if (TE10U_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.te10u_dvizhenie_0 = 1;
                    }
                    else LocoButtons.te10u_dvizhenie_0 = 0;
                }
                if (TE10U_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.te10u_dvizhenie_1 = 1;
                    }
                    else LocoButtons.te10u_dvizhenie_1 = 0;
                }
                if (TE10U_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.te10u_perehody_0 = 1;
                    }
                    else LocoButtons.te10u_perehody_0 = 0;
                }
                if (TE10U_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.te10u_perehody_1 = 1;
                    }
                    else LocoButtons.te10u_perehody_1 = 0;
                }
                if (TE10U_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.te10u_holost1_0 = 1;
                    }
                    else LocoButtons.te10u_holost1_0 = 0;
                }
                if (TE10U_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.te10u_holost1_1 = 1;
                    }
                    else LocoButtons.te10u_holost1_1 = 0;
                }
                if (TE10U_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.te10u_holost2_0 = 1;
                    }
                    else LocoButtons.te10u_holost2_0 = 0;
                }
                if (TE10U_key_buffer[36] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[36] - 1] != 0)
                    {
                        LocoButtons.te10u_holost2_1 = 1;
                    }
                    else LocoButtons.te10u_holost2_1 = 0;
                }
                if (TE10U_key_buffer[37] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[37] - 1] != 0)
                    {
                        LocoButtons.te10u_svet_cab_0 = 1;
                    }
                    else LocoButtons.te10u_svet_cab_0 = 0;
                }
                if (TE10U_key_buffer[38] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[38] - 1] != 0)
                    {
                        LocoButtons.te10u_svet_cab_1 = 1;
                    }
                    else LocoButtons.te10u_svet_cab_1 = 0;
                }
                if (TE10U_key_buffer[39] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[39] - 1] != 0)
                    {
                        LocoButtons.te10u_svet_cab_2 = 1;
                    }
                    else LocoButtons.te10u_svet_cab_2 = 0;
                }
                if (TE10U_key_buffer[40] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[40] - 1] != 0)
                    {
                        LocoButtons.te10u_EPK_0 = 1;
                    }
                    else LocoButtons.te10u_EPK_0 = 0;
                }
                if (TE10U_key_buffer[41] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[41] - 1] != 0)
                    {
                        LocoButtons.te10u_EPK_1 = 1;
                    }
                    else LocoButtons.te10u_EPK_1 = 0;
                }
                if (TE10U_key_buffer[42] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[42] - 1] != 0)
                    {
                        LocoButtons.te10u_EPT_0 = 1;
                    }
                    else LocoButtons.te10u_EPT_0 = 0;
                }
                if (TE10U_key_buffer[43] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[43] - 1] != 0)
                    {
                        LocoButtons.te10u_EPT_1 = 1;
                    }
                    else LocoButtons.te10u_EPT_1 = 0;
                }
                if (TE10U_key_buffer[44] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[44] - 1] != 0)
                    {
                        LocoButtons.te10u_prozh_0 = 1;
                    }
                    else LocoButtons.te10u_prozh_0 = 0;
                }
                if (TE10U_key_buffer[45] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[45] - 1] != 0)
                    {
                        LocoButtons.te10u_prozh_1 = 1;
                    }
                    else LocoButtons.te10u_prozh_1 = 0;
                }
                if (TE10U_key_buffer[46] != 0)
                {
                    if (joystick_buttons_buffer[TE10U_key_buffer[46] - 1] != 0)
                    {
                        LocoButtons.te10u_prozh_2 = 1;
                    }
                    else LocoButtons.te10u_prozh_2 = 0;
                }
            }

            //проверяем кнопки в буфере M62
            if (Loco.sig_loco == 15)
            {
                if (M62_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.m62_rev_0 = 1;
                    }
                    else LocoButtons.m62_rev_0 = 0;
                }
                if (M62_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.m62_rev_vpered = 1;
                    }
                    else LocoButtons.m62_rev_vpered = 0;
                }
                if (M62_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.m62_rev_nazad = 1;
                    }
                    else LocoButtons.m62_rev_nazad = 0;
                }
                if (M62_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_0 = 1;
                    }
                    else LocoButtons.m62_kontr_0 = 0;
                }
                if (M62_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_1 = 1;
                    }
                    else LocoButtons.m62_kontr_1 = 0;
                }
                if (M62_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_2 = 1;
                    }
                    else LocoButtons.m62_kontr_2 = 0;
                }
                if (M62_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_3 = 1;
                    }
                    else LocoButtons.m62_kontr_3 = 0;
                }
                if (M62_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_4 = 1;
                    }
                    else LocoButtons.m62_kontr_4 = 0;
                }
                if (M62_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_5 = 1;
                    }
                    else LocoButtons.m62_kontr_5 = 0;
                }
                if (M62_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_6 = 1;
                    }
                    else LocoButtons.m62_kontr_6 = 0;
                }
                if (M62_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_7 = 1;
                    }
                    else LocoButtons.m62_kontr_7 = 0;
                }
                if (M62_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_8 = 1;
                    }
                    else LocoButtons.m62_kontr_8 = 0;
                }
                if (M62_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_9 = 1;
                    }
                    else LocoButtons.m62_kontr_9 = 0;
                }
                if (M62_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_10 = 1;
                    }
                    else LocoButtons.m62_kontr_10 = 0;
                }
                if (M62_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_11 = 1;
                    }
                    else LocoButtons.m62_kontr_11 = 0;
                }
                if (M62_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_12 = 1;
                    }
                    else LocoButtons.m62_kontr_12 = 0;
                }
                if (M62_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_13 = 1;
                    }
                    else LocoButtons.m62_kontr_13 = 0;
                }
                if (M62_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_14 = 1;
                    }
                    else LocoButtons.m62_kontr_14 = 0;
                }
                if (M62_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.m62_kontr_15 = 1;
                    }
                    else LocoButtons.m62_kontr_15 = 0;
                }
                if (M62_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.m62_kranTM_0 = 1;
                    }
                    else LocoButtons.m62_kranTM_0 = 0;
                }
                if (M62_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.m62_kranTM_1 = 1;
                    }
                    else LocoButtons.m62_kranTM_1 = 0;
                }
                if (M62_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.m62_nasos_0 = 1;
                    }
                    else LocoButtons.m62_nasos_0 = 0;
                }
                if (M62_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.m62_nasos_1 = 1;
                    }
                    else LocoButtons.m62_nasos_1 = 0;
                }
                if (M62_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.m62_pusk = 1;
                    }
                    else LocoButtons.m62_pusk = 0;
                }
                if (M62_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.m62_upravlenie_0 = 1;
                    }
                    else LocoButtons.m62_upravlenie_0 = 0;
                }
                if (M62_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.m62_upravlenie_1 = 1;
                    }
                    else LocoButtons.m62_upravlenie_1 = 0;
                }
                if (M62_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.m62_perehody_0 = 1;
                    }
                    else LocoButtons.m62_perehody_0 = 0;
                }
                if (M62_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.m62_perehody_1 = 1;
                    }
                    else LocoButtons.m62_perehody_1 = 0;
                }
                if (M62_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.m62_svet_cab_0 = 1;
                    }
                    else LocoButtons.m62_svet_cab_0 = 0;
                }
                if (M62_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.m62_svet_cab_1 = 1;
                    }
                    else LocoButtons.m62_svet_cab_1 = 0;
                }
                if (M62_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.m62_svet_cab_2 = 1;
                    }
                    else LocoButtons.m62_svet_cab_2 = 0;
                }
                if (M62_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.m62_EPK_0 = 1;
                    }
                    else LocoButtons.m62_EPK_0 = 0;
                }
                if (M62_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.m62_EPK_1 = 1;
                    }
                    else LocoButtons.m62_EPK_1 = 0;
                }
                if (M62_key_buffer[33] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[33] - 1] != 0)
                    {
                        LocoButtons.m62_prozh_0 = 1;
                    }
                    else LocoButtons.m62_prozh_0 = 0;
                }
                if (M62_key_buffer[34] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[34] - 1] != 0)
                    {
                        LocoButtons.m62_prozh_1 = 1;
                    }
                    else LocoButtons.m62_prozh_1 = 0;
                }
                if (M62_key_buffer[35] != 0)
                {
                    if (joystick_buttons_buffer[M62_key_buffer[35] - 1] != 0)
                    {
                        LocoButtons.m62_prozh_2 = 1;
                    }
                    else LocoButtons.m62_prozh_2 = 0;
                }
            }

            //проверяем кнопки в буфере ED4M
            if (Loco.sig_loco == 16)
            {
                if (ED4M_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.ed4m_rev_0 = 1;
                    }
                    else LocoButtons.ed4m_rev_0 = 0;
                }
                if (ED4M_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.ed4m_rev_vpered = 1;
                    }
                    else LocoButtons.ed4m_rev_vpered = 0;
                }
                if (ED4M_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.ed4m_rev_nazad = 1;
                    }
                    else LocoButtons.ed4m_rev_nazad = 0;
                }
                if (ED4M_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.ed4m_kontr_0 = 1;
                    }
                    else LocoButtons.ed4m_kontr_0 = 0;
                }
                if (ED4M_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.ed4m_kontr_h1 = 1;
                    }
                    else LocoButtons.ed4m_kontr_h1 = 0;
                }
                if (ED4M_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.ed4m_kontr_h2 = 1;
                    }
                    else LocoButtons.ed4m_kontr_h2 = 0;
                }
                if (ED4M_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.ed4m_kontr_h3 = 1;
                    }
                    else LocoButtons.ed4m_kontr_h3 = 0;
                }
                if (ED4M_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.ed4m_kontr_h4 = 1;
                    }
                    else LocoButtons.ed4m_kontr_h4 = 0;
                }
                if (ED4M_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.ed4m_kontr_h5 = 1;
                    }
                    else LocoButtons.ed4m_kontr_h5 = 0;
                }
                if (ED4M_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.ed4m_kontr_t1 = 1;
                    }
                    else LocoButtons.ed4m_kontr_t1 = 0;
                }
                if (ED4M_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.ed4m_kontr_t2 = 1;
                    }
                    else LocoButtons.ed4m_kontr_t2 = 0;
                }
                if (ED4M_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.ed4m_kontr_t3 = 1;
                    }
                    else LocoButtons.ed4m_kontr_t3 = 0;
                }
                if (ED4M_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.ed4m_kontr_t4 = 1;
                    }
                    else LocoButtons.ed4m_kontr_t4 = 0;
                }
                if (ED4M_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.ed4m_kontr_t5 = 1;
                    }
                    else LocoButtons.ed4m_kontr_t5 = 0;
                }
                if (ED4M_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.ed4m_kranTM_0 = 1;
                    }
                    else LocoButtons.ed4m_kranTM_0 = 0;
                }
                if (ED4M_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.ed4m_kranTM_1 = 1;
                    }
                    else LocoButtons.ed4m_kranTM_1 = 0;
                }
                if (ED4M_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.ed4m_tokopr_0 = 1;
                    }
                    else LocoButtons.ed4m_tokopr_0 = 0;
                }
                if (ED4M_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.ed4m_tokopr_1 = 1;
                    }
                    else LocoButtons.ed4m_tokopr_1 = 0;
                }
                if (ED4M_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.ed4m_bv_0 = 1;
                    }
                    else LocoButtons.ed4m_bv_0 = 0;
                }
                if (ED4M_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.ed4m_bv_1 = 1;
                    }
                    else LocoButtons.ed4m_bv_1 = 0;
                }
                if (ED4M_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.ed4m_svet_cab_0 = 1;
                    }
                    else LocoButtons.ed4m_svet_cab_0 = 0;
                }
                if (ED4M_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.ed4m_svet_cab_1 = 1;
                    }
                    else LocoButtons.ed4m_svet_cab_1 = 0;
                }
                if (ED4M_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.ed4m_EPK_0 = 1;
                    }
                    else LocoButtons.ed4m_EPK_0 = 0;
                }
                if (ED4M_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.ed4m_EPK_1 = 1;
                    }
                    else LocoButtons.ed4m_EPK_1 = 0;
                }
                if (ED4M_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.ed4m_EPT_0 = 1;
                    }
                    else LocoButtons.ed4m_EPT_0 = 0;
                }
                if (ED4M_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.ed4m_EPT_1 = 1;
                    }
                    else LocoButtons.ed4m_EPT_1 = 0;
                }
                if (ED4M_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.ed4m_dvery_lev_0 = 1;
                    }
                    else LocoButtons.ed4m_dvery_lev_0 = 0;
                }
                if (ED4M_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.ed4m_dvery_lev_1 = 1;
                    }
                    else LocoButtons.ed4m_dvery_lev_1 = 0;
                }
                if (ED4M_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.ed4m_dvery_pr_0 = 1;
                    }
                    else LocoButtons.ed4m_dvery_pr_0 = 0;
                }
                if (ED4M_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.ed4m_dvery_pr_1 = 1;
                    }
                    else LocoButtons.ed4m_dvery_pr_1 = 0;
                }
                if (ED4M_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.ed4m_prozh_0 = 1;
                    }
                    else LocoButtons.ed4m_prozh_0 = 0;
                }
                if (ED4M_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.ed4m_prozh_1 = 1;
                    }
                    else LocoButtons.ed4m_prozh_1 = 0;
                }
                if (ED4M_key_buffer[32] != 0)
                {
                    if (joystick_buttons_buffer[ED4M_key_buffer[32] - 1] != 0)
                    {
                        LocoButtons.ed4m_prozh_2 = 1;
                    }
                    else LocoButtons.ed4m_prozh_2 = 0;
                }
            }

            //проверяем кнопки в буфере ED9M
            if (Loco.sig_loco == 17)
            {
                if (ED9M_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.ed9m_rev_0 = 1;
                    }
                    else LocoButtons.ed9m_rev_0 = 0;
                }
                if (ED9M_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.ed9m_rev_vpered = 1;
                    }
                    else LocoButtons.ed9m_rev_vpered = 0;
                }
                if (ED9M_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.ed9m_rev_nazad = 1;
                    }
                    else LocoButtons.ed9m_rev_nazad = 0;
                }
                if (ED9M_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.ed9m_kontr_0 = 1;
                    }
                    else LocoButtons.ed9m_kontr_0 = 0;
                }
                if (ED9M_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.ed9m_kontr_h1 = 1;
                    }
                    else LocoButtons.ed9m_kontr_h1 = 0;
                }
                if (ED9M_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.ed9m_kontr_h2 = 1;
                    }
                    else LocoButtons.ed9m_kontr_h2 = 0;
                }
                if (ED9M_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.ed9m_kontr_t1 = 1;
                    }
                    else LocoButtons.ed9m_kontr_t1 = 0;
                }
                if (ED9M_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.ed9m_kontr_t2 = 1;
                    }
                    else LocoButtons.ed9m_kontr_t2 = 0;
                }
                if (ED9M_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.ed9m_kontr_t3 = 1;
                    }
                    else LocoButtons.ed9m_kontr_t3 = 0;
                }
                if (ED9M_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.ed9m_kontr_t4 = 1;
                    }
                    else LocoButtons.ed9m_kontr_t4 = 0;
                }
                if (ED9M_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.ed9m_kontr_t5 = 1;
                    }
                    else LocoButtons.ed9m_kontr_t5 = 0;
                }
                if (ED9M_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.ed9m_kranTM_0 = 1;
                    }
                    else LocoButtons.ed9m_kranTM_0 = 0;
                }
                if (ED9M_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.ed9m_kranTM_1 = 1;
                    }
                    else LocoButtons.ed9m_kranTM_1 = 0;
                }
                if (ED9M_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.ed9m_tokopr_0 = 1;
                    }
                    else LocoButtons.ed9m_tokopr_0 = 0;
                }
                if (ED9M_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.ed9m_tokopr_1 = 1;
                    }
                    else LocoButtons.ed9m_tokopr_1 = 0;
                }
                if (ED9M_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.ed9m_bv_0 = 1;
                    }
                    else LocoButtons.ed9m_bv_0 = 0;
                }
                if (ED9M_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.ed9m_bv_1 = 1;
                    }
                    else LocoButtons.ed9m_bv_1 = 0;
                }
                if (ED9M_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.ed9m_svet_cab_0 = 1;
                    }
                    else LocoButtons.ed9m_svet_cab_0 = 0;
                }
                if (ED9M_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.ed9m_svet_cab_1 = 1;
                    }
                    else LocoButtons.ed9m_svet_cab_1 = 0;
                }
                if (ED9M_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.ed9m_EPK_0 = 1;
                    }
                    else LocoButtons.ed9m_EPK_0 = 0;
                }
                if (ED9M_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.ed9m_EPK_1 = 1;
                    }
                    else LocoButtons.ed9m_EPK_1 = 0;
                }
                if (ED9M_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.ed9m_EPT_0 = 1;
                    }
                    else LocoButtons.ed9m_EPT_0 = 0;
                }
                if (ED9M_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.ed9m_EPT_1 = 1;
                    }
                    else LocoButtons.ed9m_EPT_1 = 0;
                }
                if (ED9M_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.ed9m_dvery_lev_0 = 1;
                    }
                    else LocoButtons.ed9m_dvery_lev_0 = 0;
                }
                if (ED9M_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.ed9m_dvery_lev_1 = 1;
                    }
                    else LocoButtons.ed9m_dvery_lev_1 = 0;
                }
                if (ED9M_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.ed9m_dvery_pr_0 = 1;
                    }
                    else LocoButtons.ed9m_dvery_pr_0 = 0;
                }

                if (ED9M_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.ed9m_dvery_pr_1 = 1;
                    }
                    else LocoButtons.ed9m_dvery_pr_1 = 0;
                }

                if (ED9M_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.ed9m_prozh_0 = 1;
                    }
                    else LocoButtons.ed9m_prozh_0 = 0;
                }

                if (ED9M_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.ed9m_prozh_1 = 1;
                    }
                    else LocoButtons.ed9m_prozh_1 = 0;
                }

                if (ED9M_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[ED9M_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.ed9m_prozh_2 = 1;
                    }
                    else LocoButtons.ed9m_prozh_2 = 0;
                }
            }

            //проверяем кнопки в буфере tem18
            if (Loco.sig_loco == 18)
            {
                if (tem18_key_buffer[0] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[0] - 1] != 0)
                    {
                        LocoButtons.tem18_rev_0 = 1;
                    }
                    else LocoButtons.tem18_rev_0 = 0;
                }

                if (tem18_key_buffer[1] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[1] - 1] != 0)
                    {
                        LocoButtons.tem18_rev_vpered = 1;
                    }
                    else LocoButtons.tem18_rev_vpered = 0;
                }

                if (tem18_key_buffer[2] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[2] - 1] != 0)
                    {
                        LocoButtons.tem18_rev_nazad = 1;
                    }
                    else LocoButtons.tem18_rev_nazad = 0;
                }

                if (tem18_key_buffer[3] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[3] - 1] != 0)
                    {
                        LocoButtons.tem18_kontr_0 = 1;
                    }
                    else LocoButtons.tem18_kontr_0 = 0;
                }

                if (tem18_key_buffer[4] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[4] - 1] != 0)
                    {
                        LocoButtons.tem18_kontr_1 = 1;
                    }
                    else LocoButtons.tem18_kontr_1 = 0;
                }

                if (tem18_key_buffer[5] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[5] - 1] != 0)
                    {
                        LocoButtons.tem18_kontr_2 = 1;
                    }
                    else LocoButtons.tem18_kontr_2 = 0;
                }

                if (tem18_key_buffer[6] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[6] - 1] != 0)
                    {
                        LocoButtons.tem18_kontr_3 = 1;
                    }
                    else LocoButtons.tem18_kontr_3 = 0;
                }

                if (tem18_key_buffer[7] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[7] - 1] != 0)
                    {
                        LocoButtons.tem18_kontr_4 = 1;
                    }
                    else LocoButtons.tem18_kontr_4 = 0;
                }

                if (tem18_key_buffer[8] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[8] - 1] != 0)
                    {
                        LocoButtons.tem18_kontr_5 = 1;
                    }
                    else LocoButtons.tem18_kontr_5 = 0;
                }

                if (tem18_key_buffer[9] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[9] - 1] != 0)
                    {
                        LocoButtons.tem18_kontr_6 = 1;
                    }
                    else LocoButtons.tem18_kontr_6 = 0;
                }

                if (tem18_key_buffer[10] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[10] - 1] != 0)
                    {
                        LocoButtons.tem18_kontr_7 = 1;
                    }
                    else LocoButtons.tem18_kontr_7 = 0;
                }

                if (tem18_key_buffer[11] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[11] - 1] != 0)
                    {
                        LocoButtons.tem18_kontr_8 = 1;
                    }
                    else LocoButtons.tem18_kontr_8 = 0;
                }

                if (tem18_key_buffer[12] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[12] - 1] != 0)
                    {
                        LocoButtons.tem18_kranTM_0 = 1;
                    }
                    else LocoButtons.tem18_kranTM_0 = 0;
                }

                if (tem18_key_buffer[13] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[13] - 1] != 0)
                    {
                        LocoButtons.tem18_kranTM_1 = 1;
                    }
                    else LocoButtons.tem18_kranTM_1 = 0;
                }

                if (tem18_key_buffer[14] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[14] - 1] != 0)
                    {
                        LocoButtons.tem18_nasos_maslo0 = 1;
                    }
                    else LocoButtons.tem18_nasos_maslo0 = 0;
                }

                if (tem18_key_buffer[15] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[15] - 1] != 0)
                    {
                        LocoButtons.tem18_nasos_maslo1 = 1;
                    }
                    else LocoButtons.tem18_nasos_maslo1 = 0;
                }

                if (tem18_key_buffer[16] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[16] - 1] != 0)
                    {
                        LocoButtons.tem18_nasos_toplivo0 = 1;
                    }
                    else LocoButtons.tem18_nasos_toplivo0 = 0;
                }

                if (tem18_key_buffer[17] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[17] - 1] != 0)
                    {
                        LocoButtons.tem18_nasos_toplivo1 = 1;
                    }
                    else LocoButtons.tem18_nasos_toplivo1 = 0;
                }

                if (tem18_key_buffer[18] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[18] - 1] != 0)
                    {
                        LocoButtons.tem18_pusk = 1;
                    }
                    else LocoButtons.tem18_pusk = 0;
                }

                if (tem18_key_buffer[19] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[19] - 1] != 0)
                    {
                        LocoButtons.tem18_upravlenie_0 = 1;
                    }
                    else LocoButtons.tem18_upravlenie_0 = 0;
                }

                if (tem18_key_buffer[20] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[20] - 1] != 0)
                    {
                        LocoButtons.tem18_upravlenie_1 = 1;
                    }
                    else LocoButtons.tem18_upravlenie_1 = 0;
                }

                if (tem18_key_buffer[21] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[21] - 1] != 0)
                    {
                        LocoButtons.tem18_perehody_0 = 1;
                    }
                    else LocoButtons.tem18_perehody_0 = 0;
                }

                if (tem18_key_buffer[22] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[22] - 1] != 0)
                    {
                        LocoButtons.tem18_perehody_1 = 1;
                    }
                    else LocoButtons.tem18_perehody_1 = 0;
                }

                if (tem18_key_buffer[23] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[23] - 1] != 0)
                    {
                        LocoButtons.tem18_svet_cab_0 = 1;
                    }
                    else LocoButtons.tem18_svet_cab_0 = 0;
                }

                if (tem18_key_buffer[24] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[24] - 1] != 0)
                    {
                        LocoButtons.tem18_svet_cab_1 = 1;
                    }
                    else LocoButtons.tem18_svet_cab_1 = 0;
                }

                if (tem18_key_buffer[25] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[25] - 1] != 0)
                    {
                        LocoButtons.tem18_svet_prib_0 = 1;
                    }
                    else LocoButtons.tem18_svet_prib_0 = 0;
                }

                if (tem18_key_buffer[26] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[26] - 1] != 0)
                    {
                        LocoButtons.tem18_svet_prib_1 = 1;
                    }
                    else LocoButtons.tem18_svet_prib_1 = 0;
                }

                if (tem18_key_buffer[27] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[27] - 1] != 0)
                    {
                        LocoButtons.tem18_EPK_0 = 1;
                    }
                    else LocoButtons.tem18_EPK_0 = 0;
                }

                if (tem18_key_buffer[28] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[28] - 1] != 0)
                    {
                        LocoButtons.tem18_EPK_1 = 1;
                    }
                    else LocoButtons.tem18_EPK_1 = 0;
                }

                if (tem18_key_buffer[29] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[29] - 1] != 0)
                    {
                        LocoButtons.tem18_prozh_0 = 1;
                    }
                    else LocoButtons.tem18_prozh_0 = 0;
                }

                if (tem18_key_buffer[30] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[30] - 1] != 0)
                    {
                        LocoButtons.tem18_prozh_1 = 1;
                    }
                    else LocoButtons.tem18_prozh_1 = 0;
                }

                if (tem18_key_buffer[31] != 0)
                {
                    if (joystick_buttons_buffer[tem18_key_buffer[31] - 1] != 0)
                    {
                        LocoButtons.tem18_prozh_2 = 1;
                    }
                    else LocoButtons.tem18_prozh_2 = 0;
                }
            }
        }

        //------------------------------------------------------------------------------------
        //Обновление состояния осей джойстика
        //------------------------------------------------------------------------------------
        public void UpdateLocoAxis()
        {
            if (Loco.i_process_name == 6)
            {
                //-------------------------------------------------------------------------------------------
                //проверяем точки Controls
                //-------------------------------------------------------------------------------------------
                for (int i = 0; i < 34; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (Controls_axis_buffer[i, 0] == j)
                        {
                            if (Controls_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                Controls_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (Controls_axis_buffer[0, 0] != 0) LocoButtons.svistok = b_joystick_axis_numbers_update[0];
                if (Controls_axis_buffer[1, 0] != 0) LocoButtons.tifon = b_joystick_axis_numbers_update[1];
                if (Controls_axis_buffer[2, 0] != 0) LocoButtons.kran395_0 = b_joystick_axis_numbers_update[2];
                if (Controls_axis_buffer[3, 0] != 0) LocoButtons.kran395_1 = b_joystick_axis_numbers_update[3];
                if (Controls_axis_buffer[4, 0] != 0) LocoButtons.kran395_2 = b_joystick_axis_numbers_update[4];
                if (Controls_axis_buffer[5, 0] != 0) LocoButtons.kran395_3 = b_joystick_axis_numbers_update[5];
                if (Controls_axis_buffer[6, 0] != 0) LocoButtons.kran395_4 = b_joystick_axis_numbers_update[6];
                if (Controls_axis_buffer[7, 0] != 0) LocoButtons.kran395_5 = b_joystick_axis_numbers_update[7];
                if (Controls_axis_buffer[8, 0] != 0) LocoButtons.kran395_6 = b_joystick_axis_numbers_update[8];
                if (Controls_axis_buffer[9, 0] != 0) LocoButtons.kran254_0 = b_joystick_axis_numbers_update[9];
                if (Controls_axis_buffer[10, 0] != 0) LocoButtons.kran254_1 = b_joystick_axis_numbers_update[10];
                if (Controls_axis_buffer[11, 0] != 0) LocoButtons.kran254_2 = b_joystick_axis_numbers_update[11];
                if (Controls_axis_buffer[12, 0] != 0) LocoButtons.kran254_3 = b_joystick_axis_numbers_update[12];
                if (Controls_axis_buffer[13, 0] != 0) LocoButtons.kran254_4 = b_joystick_axis_numbers_update[13];
                if (Controls_axis_buffer[14, 0] != 0) LocoButtons.kran254_5 = b_joystick_axis_numbers_update[14];
                if (Controls_axis_buffer[15, 0] != 0) LocoButtons.vid_vlevo = b_joystick_axis_numbers_update[15];
                if (Controls_axis_buffer[16, 0] != 0) LocoButtons.vid_vpravo = b_joystick_axis_numbers_update[16];
                if (Controls_axis_buffer[17, 0] != 0) LocoButtons.vid_vverh = b_joystick_axis_numbers_update[17];
                if (Controls_axis_buffer[18, 0] != 0) LocoButtons.vid_vniz = b_joystick_axis_numbers_update[18];
                if (Controls_axis_buffer[19, 0] != 0) LocoButtons.vid_zoom_in = b_joystick_axis_numbers_update[19];
                if (Controls_axis_buffer[20, 0] != 0) LocoButtons.vid_zoom_out = b_joystick_axis_numbers_update[20];
                if (Controls_axis_buffer[21, 0] != 0) LocoButtons.vid_outside = b_joystick_axis_numbers_update[21];
                if (Controls_axis_buffer[22, 0] != 0) LocoButtons.vid_vpered = b_joystick_axis_numbers_update[22];
                if (Controls_axis_buffer[23, 0] != 0) LocoButtons.vid_nazad = b_joystick_axis_numbers_update[23];
                if (Controls_axis_buffer[24, 0] != 0) LocoButtons.protyazhka_lenty = b_joystick_axis_numbers_update[24];
                if (Controls_axis_buffer[25, 0] != 0) LocoButtons.bdit_Z = b_joystick_axis_numbers_update[25];
                if (Controls_axis_buffer[26, 0] != 0) LocoButtons.bdit_M = b_joystick_axis_numbers_update[26];
                if (Controls_axis_buffer[27, 0] != 0) LocoButtons.pesok = b_joystick_axis_numbers_update[27];
                if (Controls_axis_buffer[28, 0] != 0) LocoButtons.dvorniki_0 = b_joystick_axis_numbers_update[28];
                if (Controls_axis_buffer[29, 0] != 0) LocoButtons.dvorniki_1 = b_joystick_axis_numbers_update[29];
                if (Controls_axis_buffer[30, 0] != 0) LocoButtons.dvorniki_2 = b_joystick_axis_numbers_update[30];
                if (Controls_axis_buffer[31, 0] != 0) LocoButtons.dvorniki_3 = b_joystick_axis_numbers_update[31];
                if (Controls_axis_buffer[32, 0] != 0) LocoButtons.dvorniki_4 = b_joystick_axis_numbers_update[32];
                if (Controls_axis_buffer[33, 0] != 0) LocoButtons.dvorniki_5 = b_joystick_axis_numbers_update[33];

                //-------------------------------------------------------------------------------------------
                //проверяем точки нештатки
                //-------------------------------------------------------------------------------------------
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (Neshtatki_axis_buffer[i, 0] == j)
                        {
                            if (Neshtatki_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                Neshtatki_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }
                for (int i = 0; i < 100; i++)
                {
                    if (Neshtatki_axis_buffer[i, 0] != 0) LocoButtons.b_neshtatki[i] = b_joystick_axis_numbers_update[i];
                }
            }

            //-------------------------------------------------------------------------------------------
            //проверяем точки 2es5k
            //-------------------------------------------------------------------------------------------
            if (Loco.sig_loco == 1)
            {
                for (int i = 0; i < 109; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (ES5K_axis_buffer[i, 0] == j)
                        {
                            if (ES5K_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                ES5K_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }
            
                if (ES5K_axis_buffer[0,0] != 0) LocoButtons.es5k_kontr_0 = b_joystick_axis_numbers_update[0];
                if (ES5K_axis_buffer[1,0] != 0) LocoButtons.es5k_kontr_h4 = b_joystick_axis_numbers_update[1];
                if (ES5K_axis_buffer[2,0] != 0) LocoButtons.es5k_kontr_h5 = b_joystick_axis_numbers_update[2];
                if (ES5K_axis_buffer[3,0] != 0) LocoButtons.es5k_kontr_h6 = b_joystick_axis_numbers_update[3];
                if (ES5K_axis_buffer[4,0] != 0) LocoButtons.es5k_kontr_h7 = b_joystick_axis_numbers_update[4];
                if (ES5K_axis_buffer[5,0] != 0) LocoButtons.es5k_kontr_h8 = b_joystick_axis_numbers_update[5];
                if (ES5K_axis_buffer[6,0] != 0) LocoButtons.es5k_kontr_h9 = b_joystick_axis_numbers_update[6];
                if (ES5K_axis_buffer[7,0] != 0) LocoButtons.es5k_kontr_h10 = b_joystick_axis_numbers_update[7];
                if (ES5K_axis_buffer[8,0] != 0) LocoButtons.es5k_kontr_h11 = b_joystick_axis_numbers_update[8];
                if (ES5K_axis_buffer[9,0] != 0) LocoButtons.es5k_kontr_h12 = b_joystick_axis_numbers_update[9];
                if (ES5K_axis_buffer[10,0] != 0) LocoButtons.es5k_kontr_h13 = b_joystick_axis_numbers_update[10];
                if (ES5K_axis_buffer[11,0] != 0) LocoButtons.es5k_kontr_h14 = b_joystick_axis_numbers_update[11];
                if (ES5K_axis_buffer[12,0] != 0) LocoButtons.es5k_kontr_h15 = b_joystick_axis_numbers_update[12];
                if (ES5K_axis_buffer[13,0] != 0) LocoButtons.es5k_kontr_h16 = b_joystick_axis_numbers_update[13];
                if (ES5K_axis_buffer[14,0] != 0) LocoButtons.es5k_kontr_h17 = b_joystick_axis_numbers_update[14];
                if (ES5K_axis_buffer[15,0] != 0) LocoButtons.es5k_kontr_h18 = b_joystick_axis_numbers_update[15];
                if (ES5K_axis_buffer[16,0] != 0) LocoButtons.es5k_kontr_h19 = b_joystick_axis_numbers_update[16];
                if (ES5K_axis_buffer[17,0] != 0) LocoButtons.es5k_kontr_h20 = b_joystick_axis_numbers_update[17];
                if (ES5K_axis_buffer[18,0] != 0) LocoButtons.es5k_kontr_h21 = b_joystick_axis_numbers_update[18];
                if (ES5K_axis_buffer[19,0] != 0) LocoButtons.es5k_kontr_h22 = b_joystick_axis_numbers_update[19];
                if (ES5K_axis_buffer[20,0] != 0) LocoButtons.es5k_kontr_h23 = b_joystick_axis_numbers_update[20];
                if (ES5K_axis_buffer[21,0] != 0) LocoButtons.es5k_kontr_h24 = b_joystick_axis_numbers_update[21];
                if (ES5K_axis_buffer[22,0] != 0) LocoButtons.es5k_kontr_h25 = b_joystick_axis_numbers_update[22];
                if (ES5K_axis_buffer[23,0] != 0) LocoButtons.es5k_kontr_h26 = b_joystick_axis_numbers_update[23];
                if (ES5K_axis_buffer[24,0] != 0) LocoButtons.es5k_kontr_h27 = b_joystick_axis_numbers_update[24];
                if (ES5K_axis_buffer[25,0] != 0) LocoButtons.es5k_kontr_h28 = b_joystick_axis_numbers_update[25];
                if (ES5K_axis_buffer[26,0] != 0) LocoButtons.es5k_kontr_h29 = b_joystick_axis_numbers_update[26];
                if (ES5K_axis_buffer[27,0] != 0) LocoButtons.es5k_kontr_h30 = b_joystick_axis_numbers_update[27];
                if (ES5K_axis_buffer[28,0] != 0) LocoButtons.es5k_kontr_h31 = b_joystick_axis_numbers_update[28];
                if (ES5K_axis_buffer[29,0] != 0) LocoButtons.es5k_kontr_h32 = b_joystick_axis_numbers_update[29];
                if (ES5K_axis_buffer[30,0] != 0) LocoButtons.es5k_kontr_h33 = b_joystick_axis_numbers_update[30];
                if (ES5K_axis_buffer[31,0] != 0) LocoButtons.es5k_kontr_h34 = b_joystick_axis_numbers_update[31];
                if (ES5K_axis_buffer[32,0] != 0) LocoButtons.es5k_kontr_h35 = b_joystick_axis_numbers_update[32];
                if (ES5K_axis_buffer[33,0] != 0) LocoButtons.es5k_kontr_h36 = b_joystick_axis_numbers_update[33];
                if (ES5K_axis_buffer[34,0] != 0) LocoButtons.es5k_kontr_t4 = b_joystick_axis_numbers_update[34];
                if (ES5K_axis_buffer[35,0] != 0) LocoButtons.es5k_kontr_t5 = b_joystick_axis_numbers_update[35];
                if (ES5K_axis_buffer[36,0] != 0) LocoButtons.es5k_kontr_t6 = b_joystick_axis_numbers_update[36];
                if (ES5K_axis_buffer[37,0] != 0) LocoButtons.es5k_kontr_t7 = b_joystick_axis_numbers_update[37];
                if (ES5K_axis_buffer[38,0] != 0) LocoButtons.es5k_kontr_t8 = b_joystick_axis_numbers_update[38];
                if (ES5K_axis_buffer[39,0] != 0) LocoButtons.es5k_kontr_t9 = b_joystick_axis_numbers_update[39];
                if (ES5K_axis_buffer[40,0] != 0) LocoButtons.es5k_kontr_t10 = b_joystick_axis_numbers_update[40];
                if (ES5K_axis_buffer[41,0] != 0) LocoButtons.es5k_kontr_t11 = b_joystick_axis_numbers_update[41];
                if (ES5K_axis_buffer[42,0] != 0) LocoButtons.es5k_kontr_t12 = b_joystick_axis_numbers_update[42];
                if (ES5K_axis_buffer[43,0] != 0) LocoButtons.es5k_kontr_t13 = b_joystick_axis_numbers_update[43];
                if (ES5K_axis_buffer[44,0] != 0) LocoButtons.es5k_kontr_t14 = b_joystick_axis_numbers_update[44];
                if (ES5K_axis_buffer[45,0] != 0) LocoButtons.es5k_kontr_t15 = b_joystick_axis_numbers_update[45];
                if (ES5K_axis_buffer[46,0] != 0) LocoButtons.es5k_kontr_t16 = b_joystick_axis_numbers_update[46];
                if (ES5K_axis_buffer[47,0] != 0) LocoButtons.es5k_kontr_t17 = b_joystick_axis_numbers_update[47];
                if (ES5K_axis_buffer[48,0] != 0) LocoButtons.es5k_kontr_t18 = b_joystick_axis_numbers_update[48];
                if (ES5K_axis_buffer[49,0] != 0) LocoButtons.es5k_kontr_t19 = b_joystick_axis_numbers_update[49];
                if (ES5K_axis_buffer[50,0] != 0) LocoButtons.es5k_kontr_t20 = b_joystick_axis_numbers_update[50];
                if (ES5K_axis_buffer[51,0] != 0) LocoButtons.es5k_kontr_t21 = b_joystick_axis_numbers_update[51];
                if (ES5K_axis_buffer[52,0] != 0) LocoButtons.es5k_kontr_t22 = b_joystick_axis_numbers_update[52];
                if (ES5K_axis_buffer[53,0] != 0) LocoButtons.es5k_kontr_t23 = b_joystick_axis_numbers_update[53];
                if (ES5K_axis_buffer[54,0] != 0) LocoButtons.es5k_kontr_t24 = b_joystick_axis_numbers_update[54];
                if (ES5K_axis_buffer[55,0] != 0) LocoButtons.es5k_kontr_t25 = b_joystick_axis_numbers_update[55];
                if (ES5K_axis_buffer[56,0] != 0) LocoButtons.es5k_kontr_t26 = b_joystick_axis_numbers_update[56];
                if (ES5K_axis_buffer[57,0] != 0) LocoButtons.es5k_kontr_t27 = b_joystick_axis_numbers_update[57];
                if (ES5K_axis_buffer[58,0] != 0) LocoButtons.es5k_kontr_t28 = b_joystick_axis_numbers_update[58];
                if (ES5K_axis_buffer[59,0] != 0) LocoButtons.es5k_kontr_t29 = b_joystick_axis_numbers_update[59];
                if (ES5K_axis_buffer[60,0] != 0) LocoButtons.es5k_kontr_t30 = b_joystick_axis_numbers_update[60];
                if (ES5K_axis_buffer[61,0] != 0) LocoButtons.es5k_kontr_t31 = b_joystick_axis_numbers_update[61];
                if (ES5K_axis_buffer[62,0] != 0) LocoButtons.es5k_kontr_t32 = b_joystick_axis_numbers_update[62];
                if (ES5K_axis_buffer[63,0] != 0) LocoButtons.es5k_kontr_t33 = b_joystick_axis_numbers_update[63];
                if (ES5K_axis_buffer[64,0] != 0) LocoButtons.es5k_kontr_t34 = b_joystick_axis_numbers_update[64];
                if (ES5K_axis_buffer[65,0] != 0) LocoButtons.es5k_kontr_t35 = b_joystick_axis_numbers_update[65];
                if (ES5K_axis_buffer[66,0] != 0) LocoButtons.es5k_kontr_t36 = b_joystick_axis_numbers_update[66];
                if (ES5K_axis_buffer[67,0] != 0) LocoButtons.es5k_rev_0 = b_joystick_axis_numbers_update[67];
                if (ES5K_axis_buffer[68,0] != 0) LocoButtons.es5k_rev_vpered = b_joystick_axis_numbers_update[68];
                if (ES5K_axis_buffer[69,0] != 0) LocoButtons.es5k_rev_nazad = b_joystick_axis_numbers_update[69];
                if (ES5K_axis_buffer[70,0] != 0) LocoButtons.es5k_reg_skor_140 = b_joystick_axis_numbers_update[70];
                if (ES5K_axis_buffer[71,0] != 0) LocoButtons.es5k_reg_skor_plus = b_joystick_axis_numbers_update[71];
                if (ES5K_axis_buffer[72,0] != 0) LocoButtons.es5k_reg_skor_minus = b_joystick_axis_numbers_update[72];
                if (ES5K_axis_buffer[73,0] != 0) LocoButtons.es5k_kranTM_0 = b_joystick_axis_numbers_update[73];
                if (ES5K_axis_buffer[74,0] != 0) LocoButtons.es5k_kranTM_1 = b_joystick_axis_numbers_update[74];
                if (ES5K_axis_buffer[75,0] != 0) LocoButtons.es5k_bv_0 = b_joystick_axis_numbers_update[75];
                if (ES5K_axis_buffer[76,0] != 0) LocoButtons.es5k_bv_1 = b_joystick_axis_numbers_update[76];
                if (ES5K_axis_buffer[77,0] != 0) LocoButtons.es5k_vozvrat_bv = b_joystick_axis_numbers_update[77];
                if (ES5K_axis_buffer[78,0] != 0) LocoButtons.es5k_tokopr_per_0 = b_joystick_axis_numbers_update[78];
                if (ES5K_axis_buffer[79,0] != 0) LocoButtons.es5k_tokopr_per_1 = b_joystick_axis_numbers_update[79];
                if (ES5K_axis_buffer[80,0] != 0) LocoButtons.es5k_tokopr_zad_0 = b_joystick_axis_numbers_update[80];
                if (ES5K_axis_buffer[81,0] != 0) LocoButtons.es5k_tokopr_zad_1 = b_joystick_axis_numbers_update[81];
                if (ES5K_axis_buffer[82,0] != 0) LocoButtons.es5k_upravlenie_0 = b_joystick_axis_numbers_update[82];
                if (ES5K_axis_buffer[83,0] != 0) LocoButtons.es5k_upravlenie_1 = b_joystick_axis_numbers_update[83];
                if (ES5K_axis_buffer[84,0] != 0) LocoButtons.es5k_komp_0 = b_joystick_axis_numbers_update[84];
                if (ES5K_axis_buffer[85,0] != 0) LocoButtons.es5k_komp_1 = b_joystick_axis_numbers_update[85];
                if (ES5K_axis_buffer[86,0] != 0) LocoButtons.es5k_vent1_0 = b_joystick_axis_numbers_update[86];
                if (ES5K_axis_buffer[87,0] != 0) LocoButtons.es5k_vent1_1 = b_joystick_axis_numbers_update[87];
                if (ES5K_axis_buffer[88,0] != 0) LocoButtons.es5k_vent2_0 = b_joystick_axis_numbers_update[88];
                if (ES5K_axis_buffer[89,0] != 0) LocoButtons.es5k_vent2_1 = b_joystick_axis_numbers_update[89];
                if (ES5K_axis_buffer[90,0] != 0) LocoButtons.es5k_MSUD_0 = b_joystick_axis_numbers_update[90];
                if (ES5K_axis_buffer[91,0] != 0) LocoButtons.es5k_MSUD_1 = b_joystick_axis_numbers_update[91];
                if (ES5K_axis_buffer[92,0] != 0) LocoButtons.es5k_vspom_mash_0 = b_joystick_axis_numbers_update[92];
                if (ES5K_axis_buffer[93,0] != 0) LocoButtons.es5k_vspom_mash_1 = b_joystick_axis_numbers_update[93];
                if (ES5K_axis_buffer[94,0] != 0) LocoButtons.es5k_svet_cab_0 = b_joystick_axis_numbers_update[94];
                if (ES5K_axis_buffer[95,0] != 0) LocoButtons.es5k_svet_cab_1 = b_joystick_axis_numbers_update[95];
                if (ES5K_axis_buffer[96,0] != 0) LocoButtons.es5k_EPK_0 = b_joystick_axis_numbers_update[96];
                if (ES5K_axis_buffer[97,0] != 0) LocoButtons.es5k_EPK_1 = b_joystick_axis_numbers_update[97];
                if (ES5K_axis_buffer[98,0] != 0) LocoButtons.es5k_sign_0 = b_joystick_axis_numbers_update[98];
                if (ES5K_axis_buffer[99,0] != 0) LocoButtons.es5k_sign_1 = b_joystick_axis_numbers_update[99];
                if (ES5K_axis_buffer[100,0] != 0) LocoButtons.es5k_signC1_0 = b_joystick_axis_numbers_update[100];
                if (ES5K_axis_buffer[101,0] != 0) LocoButtons.es5k_signC1_1 = b_joystick_axis_numbers_update[101];
                if (ES5K_axis_buffer[102,0] != 0) LocoButtons.es5k_signC2_0 = b_joystick_axis_numbers_update[102];
                if (ES5K_axis_buffer[103,0] != 0) LocoButtons.es5k_signC2_1 = b_joystick_axis_numbers_update[103];
                if (ES5K_axis_buffer[104,0] != 0) LocoButtons.es5k_prozh_0 = b_joystick_axis_numbers_update[104];
                if (ES5K_axis_buffer[105,0] != 0) LocoButtons.es5k_prozh_1 = b_joystick_axis_numbers_update[105];
                if (ES5K_axis_buffer[106,0] != 0) LocoButtons.es5k_prozh_2 = b_joystick_axis_numbers_update[106];
                if (ES5K_axis_buffer[107,0] != 0) LocoButtons.es5k_avtoreg_0 = b_joystick_axis_numbers_update[107];
                if (ES5K_axis_buffer[108,0] != 0) LocoButtons.es5k_avtoreg_1 = b_joystick_axis_numbers_update[108];


            }

            //-------------------------------------------------------------------------------------------
            //проверяем точки 2es5k
            //-------------------------------------------------------------------------------------------
            //проверяем точки 2ep1m
            if (Loco.sig_loco == 2)
            {
                for (int i = 0; i < 112; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (EP1M_axis_buffer[i, 0] == j)
                        {
                            if (EP1M_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                EP1M_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (EP1M_axis_buffer[0,0] != 0) LocoButtons.ep1m_kontr_0 = b_joystick_axis_numbers_update[0];
                if (EP1M_axis_buffer[1,0] != 0) LocoButtons.ep1m_kontr_h4 = b_joystick_axis_numbers_update[1];
                if (EP1M_axis_buffer[2,0] != 0) LocoButtons.ep1m_kontr_h5 = b_joystick_axis_numbers_update[2];
                if (EP1M_axis_buffer[3,0] != 0) LocoButtons.ep1m_kontr_h6 = b_joystick_axis_numbers_update[3];
                if (EP1M_axis_buffer[4,0] != 0) LocoButtons.ep1m_kontr_h7 = b_joystick_axis_numbers_update[4];
                if (EP1M_axis_buffer[5,0] != 0) LocoButtons.ep1m_kontr_h8 = b_joystick_axis_numbers_update[5];
                if (EP1M_axis_buffer[6,0] != 0) LocoButtons.ep1m_kontr_h9 = b_joystick_axis_numbers_update[6];
                if (EP1M_axis_buffer[7,0] != 0) LocoButtons.ep1m_kontr_h10 = b_joystick_axis_numbers_update[7];
                if (EP1M_axis_buffer[8,0] != 0) LocoButtons.ep1m_kontr_h11 = b_joystick_axis_numbers_update[8];
                if (EP1M_axis_buffer[9,0] != 0) LocoButtons.ep1m_kontr_h12 = b_joystick_axis_numbers_update[9];
                if (EP1M_axis_buffer[10,0] != 0) LocoButtons.ep1m_kontr_h13 = b_joystick_axis_numbers_update[10];
                if (EP1M_axis_buffer[11,0] != 0) LocoButtons.ep1m_kontr_h14 = b_joystick_axis_numbers_update[11];
                if (EP1M_axis_buffer[12,0] != 0) LocoButtons.ep1m_kontr_h15 = b_joystick_axis_numbers_update[12];
                if (EP1M_axis_buffer[13,0] != 0) LocoButtons.ep1m_kontr_h16 = b_joystick_axis_numbers_update[13];
                if (EP1M_axis_buffer[14,0] != 0) LocoButtons.ep1m_kontr_h17 = b_joystick_axis_numbers_update[14];
                if (EP1M_axis_buffer[15,0] != 0) LocoButtons.ep1m_kontr_h18 = b_joystick_axis_numbers_update[15];
                if (EP1M_axis_buffer[16,0] != 0) LocoButtons.ep1m_kontr_h19 = b_joystick_axis_numbers_update[16];
                if (EP1M_axis_buffer[17,0] != 0) LocoButtons.ep1m_kontr_h20 = b_joystick_axis_numbers_update[17];
                if (EP1M_axis_buffer[18,0] != 0) LocoButtons.ep1m_kontr_h21 = b_joystick_axis_numbers_update[18];
                if (EP1M_axis_buffer[19,0] != 0) LocoButtons.ep1m_kontr_h22 = b_joystick_axis_numbers_update[19];
                if (EP1M_axis_buffer[20,0] != 0) LocoButtons.ep1m_kontr_h23 = b_joystick_axis_numbers_update[20];
                if (EP1M_axis_buffer[21,0] != 0) LocoButtons.ep1m_kontr_h24 = b_joystick_axis_numbers_update[21];
                if (EP1M_axis_buffer[22,0] != 0) LocoButtons.ep1m_kontr_h25 = b_joystick_axis_numbers_update[22];
                if (EP1M_axis_buffer[23,0] != 0) LocoButtons.ep1m_kontr_h26 = b_joystick_axis_numbers_update[23];
                if (EP1M_axis_buffer[24,0] != 0) LocoButtons.ep1m_kontr_h27 = b_joystick_axis_numbers_update[24];
                if (EP1M_axis_buffer[25,0] != 0) LocoButtons.ep1m_kontr_h28 = b_joystick_axis_numbers_update[25];
                if (EP1M_axis_buffer[26,0] != 0) LocoButtons.ep1m_kontr_h29 = b_joystick_axis_numbers_update[26];
                if (EP1M_axis_buffer[27,0] != 0) LocoButtons.ep1m_kontr_h30 = b_joystick_axis_numbers_update[27];
                if (EP1M_axis_buffer[28,0] != 0) LocoButtons.ep1m_kontr_h31 = b_joystick_axis_numbers_update[28];
                if (EP1M_axis_buffer[29,0] != 0) LocoButtons.ep1m_kontr_h32 = b_joystick_axis_numbers_update[29];
                if (EP1M_axis_buffer[30,0] != 0) LocoButtons.ep1m_kontr_h33 = b_joystick_axis_numbers_update[30];
                if (EP1M_axis_buffer[31,0] != 0) LocoButtons.ep1m_kontr_h34 = b_joystick_axis_numbers_update[31];
                if (EP1M_axis_buffer[32,0] != 0) LocoButtons.ep1m_kontr_h35 = b_joystick_axis_numbers_update[32];
                if (EP1M_axis_buffer[33,0] != 0) LocoButtons.ep1m_kontr_h36 = b_joystick_axis_numbers_update[33];
                if (EP1M_axis_buffer[34,0] != 0) LocoButtons.ep1m_kontr_t4 = b_joystick_axis_numbers_update[34];
                if (EP1M_axis_buffer[35,0] != 0) LocoButtons.ep1m_kontr_t5 = b_joystick_axis_numbers_update[35];
                if (EP1M_axis_buffer[36,0] != 0) LocoButtons.ep1m_kontr_t6 = b_joystick_axis_numbers_update[36];
                if (EP1M_axis_buffer[37,0] != 0) LocoButtons.ep1m_kontr_t7 = b_joystick_axis_numbers_update[37];
                if (EP1M_axis_buffer[38,0] != 0) LocoButtons.ep1m_kontr_t8 = b_joystick_axis_numbers_update[38];
                if (EP1M_axis_buffer[39,0] != 0) LocoButtons.ep1m_kontr_t9 = b_joystick_axis_numbers_update[39];
                if (EP1M_axis_buffer[40,0] != 0) LocoButtons.ep1m_kontr_t10 = b_joystick_axis_numbers_update[40];
                if (EP1M_axis_buffer[41,0] != 0) LocoButtons.ep1m_kontr_t11 = b_joystick_axis_numbers_update[41];
                if (EP1M_axis_buffer[42,0] != 0) LocoButtons.ep1m_kontr_t12 = b_joystick_axis_numbers_update[42];
                if (EP1M_axis_buffer[43,0] != 0) LocoButtons.ep1m_kontr_t13 = b_joystick_axis_numbers_update[43];
                if (EP1M_axis_buffer[44,0] != 0) LocoButtons.ep1m_kontr_t14 = b_joystick_axis_numbers_update[44];
                if (EP1M_axis_buffer[45,0] != 0) LocoButtons.ep1m_kontr_t15 = b_joystick_axis_numbers_update[45];
                if (EP1M_axis_buffer[46,0] != 0) LocoButtons.ep1m_kontr_t16 = b_joystick_axis_numbers_update[46];
                if (EP1M_axis_buffer[47,0] != 0) LocoButtons.ep1m_kontr_t17 = b_joystick_axis_numbers_update[47];
                if (EP1M_axis_buffer[48,0] != 0) LocoButtons.ep1m_kontr_t18 = b_joystick_axis_numbers_update[48];
                if (EP1M_axis_buffer[49,0] != 0) LocoButtons.ep1m_kontr_t19 = b_joystick_axis_numbers_update[49];
                if (EP1M_axis_buffer[50,0] != 0) LocoButtons.ep1m_kontr_t20 = b_joystick_axis_numbers_update[50];
                if (EP1M_axis_buffer[51,0] != 0) LocoButtons.ep1m_kontr_t21 = b_joystick_axis_numbers_update[51];
                if (EP1M_axis_buffer[52,0] != 0) LocoButtons.ep1m_kontr_t22 = b_joystick_axis_numbers_update[52];
                if (EP1M_axis_buffer[53,0] != 0) LocoButtons.ep1m_kontr_t23 = b_joystick_axis_numbers_update[53];
                if (EP1M_axis_buffer[54,0] != 0) LocoButtons.ep1m_kontr_t24 = b_joystick_axis_numbers_update[54];
                if (EP1M_axis_buffer[55,0] != 0) LocoButtons.ep1m_kontr_t25 = b_joystick_axis_numbers_update[55];
                if (EP1M_axis_buffer[56,0] != 0) LocoButtons.ep1m_kontr_t26 = b_joystick_axis_numbers_update[56];
                if (EP1M_axis_buffer[57,0] != 0) LocoButtons.ep1m_kontr_t27 = b_joystick_axis_numbers_update[57];
                if (EP1M_axis_buffer[58,0] != 0) LocoButtons.ep1m_kontr_t28 = b_joystick_axis_numbers_update[58];
                if (EP1M_axis_buffer[59,0] != 0) LocoButtons.ep1m_kontr_t29 = b_joystick_axis_numbers_update[59];
                if (EP1M_axis_buffer[60,0] != 0) LocoButtons.ep1m_kontr_t30 = b_joystick_axis_numbers_update[60];
                if (EP1M_axis_buffer[61,0] != 0) LocoButtons.ep1m_kontr_t31 = b_joystick_axis_numbers_update[61];
                if (EP1M_axis_buffer[62,0] != 0) LocoButtons.ep1m_kontr_t32 = b_joystick_axis_numbers_update[62];
                if (EP1M_axis_buffer[63,0] != 0) LocoButtons.ep1m_kontr_t33 = b_joystick_axis_numbers_update[63];
                if (EP1M_axis_buffer[64,0] != 0) LocoButtons.ep1m_kontr_t34 = b_joystick_axis_numbers_update[64];
                if (EP1M_axis_buffer[65,0] != 0) LocoButtons.ep1m_kontr_t35 = b_joystick_axis_numbers_update[65];
                if (EP1M_axis_buffer[66,0] != 0) LocoButtons.ep1m_kontr_t36 = b_joystick_axis_numbers_update[66];
                if (EP1M_axis_buffer[67,0] != 0) LocoButtons.ep1m_rev_0 = b_joystick_axis_numbers_update[67];
                if (EP1M_axis_buffer[68,0] != 0) LocoButtons.ep1m_rev_vpered = b_joystick_axis_numbers_update[68];
                if (EP1M_axis_buffer[69,0] != 0) LocoButtons.ep1m_rev_nazad = b_joystick_axis_numbers_update[69];
                if (EP1M_axis_buffer[70,0] != 0) LocoButtons.ep1m_reg_skor_160 = b_joystick_axis_numbers_update[70];
                if (EP1M_axis_buffer[71,0] != 0) LocoButtons.ep1m_reg_skor_plus = b_joystick_axis_numbers_update[71];
                if (EP1M_axis_buffer[72,0] != 0) LocoButtons.ep1m_reg_skor_minus = b_joystick_axis_numbers_update[72];
                if (EP1M_axis_buffer[73,0] != 0) LocoButtons.ep1m_kranTM_0 = b_joystick_axis_numbers_update[73];
                if (EP1M_axis_buffer[74,0] != 0) LocoButtons.ep1m_kranTM_1 = b_joystick_axis_numbers_update[74];
                if (EP1M_axis_buffer[75,0] != 0) LocoButtons.ep1m_bv_0 = b_joystick_axis_numbers_update[75];
                if (EP1M_axis_buffer[76,0] != 0) LocoButtons.ep1m_bv_1 = b_joystick_axis_numbers_update[76];
                if (EP1M_axis_buffer[77,0] != 0) LocoButtons.ep1m_vozvrat_zaschity = b_joystick_axis_numbers_update[77];
                if (EP1M_axis_buffer[78,0] != 0) LocoButtons.ep1m_blok_vvk_0 = b_joystick_axis_numbers_update[78];
                if (EP1M_axis_buffer[79,0] != 0) LocoButtons.ep1m_blok_vvk_1 = b_joystick_axis_numbers_update[79];
                if (EP1M_axis_buffer[80,0] != 0) LocoButtons.ep1m_tokopr_per_0 = b_joystick_axis_numbers_update[80];
                if (EP1M_axis_buffer[81,0] != 0) LocoButtons.ep1m_tokopr_per_1 = b_joystick_axis_numbers_update[81];
                if (EP1M_axis_buffer[82,0] != 0) LocoButtons.ep1m_tokopr_zad_0 = b_joystick_axis_numbers_update[82];
                if (EP1M_axis_buffer[83,0] != 0) LocoButtons.ep1m_tokopr_zad_1 = b_joystick_axis_numbers_update[83];
                if (EP1M_axis_buffer[84,0] != 0) LocoButtons.ep1m_upravlenie = b_joystick_axis_numbers_update[84];
                if (EP1M_axis_buffer[85,0] != 0) LocoButtons.ep1m_upravlenie = b_joystick_axis_numbers_update[85];
                if (EP1M_axis_buffer[86,0] != 0) LocoButtons.ep1m_komp_0 = b_joystick_axis_numbers_update[86];
                if (EP1M_axis_buffer[87,0] != 0) LocoButtons.ep1m_komp_1 = b_joystick_axis_numbers_update[87];
                if (EP1M_axis_buffer[88,0] != 0) LocoButtons.ep1m_vent1_0 = b_joystick_axis_numbers_update[88];
                if (EP1M_axis_buffer[89,0] != 0) LocoButtons.ep1m_vent1_1 = b_joystick_axis_numbers_update[89];
                if (EP1M_axis_buffer[90,0] != 0) LocoButtons.ep1m_vent2_0 = b_joystick_axis_numbers_update[90];
                if (EP1M_axis_buffer[91,0] != 0) LocoButtons.ep1m_vent2_1 = b_joystick_axis_numbers_update[91];
                if (EP1M_axis_buffer[92,0] != 0) LocoButtons.ep1m_vent3_0 = b_joystick_axis_numbers_update[92];
                if (EP1M_axis_buffer[93,0] != 0) LocoButtons.ep1m_vent3_1 = b_joystick_axis_numbers_update[93];
                if (EP1M_axis_buffer[94,0] != 0) LocoButtons.ep1m_MSUD_0 = b_joystick_axis_numbers_update[94];
                if (EP1M_axis_buffer[95,0] != 0) LocoButtons.ep1m_MSUD_1 = b_joystick_axis_numbers_update[95];
                if (EP1M_axis_buffer[96,0] != 0) LocoButtons.ep1m_vspom_mash_0 = b_joystick_axis_numbers_update[96];
                if (EP1M_axis_buffer[97,0] != 0) LocoButtons.ep1m_vspom_mash_1 = b_joystick_axis_numbers_update[97];
                if (EP1M_axis_buffer[98,0] != 0) LocoButtons.ep1m_svet_cab_0 = b_joystick_axis_numbers_update[98];
                if (EP1M_axis_buffer[99,0] != 0) LocoButtons.ep1m_svet_cab_1 = b_joystick_axis_numbers_update[99];
                if (EP1M_axis_buffer[100,0] != 0) LocoButtons.ep1m_svet_cab_2 = b_joystick_axis_numbers_update[100];
                if (EP1M_axis_buffer[101,0] != 0) LocoButtons.ep1m_EPK_0 = b_joystick_axis_numbers_update[101];
                if (EP1M_axis_buffer[102,0] != 0) LocoButtons.ep1m_EPK_1 = b_joystick_axis_numbers_update[102];
                if (EP1M_axis_buffer[103,0] != 0) LocoButtons.ep1m_EPT_0 = b_joystick_axis_numbers_update[103];
                if (EP1M_axis_buffer[104,0] != 0) LocoButtons.ep1m_EPT_1 = b_joystick_axis_numbers_update[104];
                if (EP1M_axis_buffer[105,0] != 0) LocoButtons.ep1m_sign_0 = b_joystick_axis_numbers_update[105];
                if (EP1M_axis_buffer[106,0] != 0) LocoButtons.ep1m_sign_1 = b_joystick_axis_numbers_update[106];
                if (EP1M_axis_buffer[107,0] != 0) LocoButtons.ep1m_prozh_0 = b_joystick_axis_numbers_update[107];
                if (EP1M_axis_buffer[108,0] != 0) LocoButtons.ep1m_prozh_1 = b_joystick_axis_numbers_update[108];
                if (EP1M_axis_buffer[109,0] != 0) LocoButtons.ep1m_prozh_2 = b_joystick_axis_numbers_update[109];
                if (EP1M_axis_buffer[110,0] != 0) LocoButtons.ep1m_avtoreg_0 = b_joystick_axis_numbers_update[110];
                if (EP1M_axis_buffer[111,0] != 0) LocoButtons.ep1m_avtoreg_1 = b_joystick_axis_numbers_update[111];


            }

            //-------------------------------------------------------------------------------------------
            //проверяем точки 2es5k
            //-------------------------------------------------------------------------------------------
            //проверяем точки chs2k
            if (Loco.sig_loco == 3)
            {
                for (int i = 0; i < 32; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (CHS2K_axis_buffer[i, 0] == j)
                        {
                            if (CHS2K_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                CHS2K_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (CHS2K_axis_buffer[0,0] != 0) LocoButtons.chs2k_rev_0 = b_joystick_axis_numbers_update[0];
                if (CHS2K_axis_buffer[1,0] != 0) LocoButtons.chs2k_rev_vpered = b_joystick_axis_numbers_update[1];
                if (CHS2K_axis_buffer[2,0] != 0) LocoButtons.chs2k_rev_nazad = b_joystick_axis_numbers_update[2];
                if (CHS2K_axis_buffer[3,0] != 0) LocoButtons.chs2k_kontr_0 = b_joystick_axis_numbers_update[3];
                if (CHS2K_axis_buffer[4,0] != 0) LocoButtons.chs2k_kontr_plus = b_joystick_axis_numbers_update[4];
                if (CHS2K_axis_buffer[5,0] != 0) LocoButtons.chs2k_kontr_minus = b_joystick_axis_numbers_update[5];
                if (CHS2K_axis_buffer[6,0] != 0) LocoButtons.chs2k_kontr_plus1 = b_joystick_axis_numbers_update[6];
                if (CHS2K_axis_buffer[7,0] != 0) LocoButtons.chs2k_kontr_minus1 = b_joystick_axis_numbers_update[7];
                if (CHS2K_axis_buffer[8,0] != 0) LocoButtons.chs2k_kranTM_0 = b_joystick_axis_numbers_update[8];
                if (CHS2K_axis_buffer[9,0] != 0) LocoButtons.chs2k_kranTM_1 = b_joystick_axis_numbers_update[9];
                if (CHS2K_axis_buffer[10,0] != 0) LocoButtons.chs2k_bv_0 = b_joystick_axis_numbers_update[10];
                if (CHS2K_axis_buffer[11,0] != 0) LocoButtons.chs2k_bv_1 = b_joystick_axis_numbers_update[11];
                if (CHS2K_axis_buffer[12,0] != 0) LocoButtons.chs2k_tokopr_per_0 = b_joystick_axis_numbers_update[12];
                if (CHS2K_axis_buffer[13,0] != 0) LocoButtons.chs2k_tokopr_per_1 = b_joystick_axis_numbers_update[13];
                if (CHS2K_axis_buffer[14,0] != 0) LocoButtons.chs2k_tokopr_zad_0 = b_joystick_axis_numbers_update[14];
                if (CHS2K_axis_buffer[15,0] != 0) LocoButtons.chs2k_tokopr_zad_1 = b_joystick_axis_numbers_update[15];
                if (CHS2K_axis_buffer[16,0] != 0) LocoButtons.chs2k_komp1_0 = b_joystick_axis_numbers_update[16];
                if (CHS2K_axis_buffer[17,0] != 0) LocoButtons.chs2k_komp1_1 = b_joystick_axis_numbers_update[17];
                if (CHS2K_axis_buffer[18,0] != 0) LocoButtons.chs2k_komp2_0 = b_joystick_axis_numbers_update[18];
                if (CHS2K_axis_buffer[19,0] != 0) LocoButtons.chs2k_komp2_1 = b_joystick_axis_numbers_update[19];
                if (CHS2K_axis_buffer[20,0] != 0) LocoButtons.chs2k_vent_0 = b_joystick_axis_numbers_update[20];
                if (CHS2K_axis_buffer[21,0] != 0) LocoButtons.chs2k_vent_1 = b_joystick_axis_numbers_update[21];
                if (CHS2K_axis_buffer[22,0] != 0) LocoButtons.chs2k_svet_cab_0 = b_joystick_axis_numbers_update[22];
                if (CHS2K_axis_buffer[23,0] != 0) LocoButtons.chs2k_svet_cab_1 = b_joystick_axis_numbers_update[23];
                if (CHS2K_axis_buffer[24,0] != 0) LocoButtons.chs2k_svet_cab_2 = b_joystick_axis_numbers_update[24];
                if (CHS2K_axis_buffer[25,0] != 0) LocoButtons.chs2k_EPK_0 = b_joystick_axis_numbers_update[25];
                if (CHS2K_axis_buffer[26,0] != 0) LocoButtons.chs2k_EPK_1 = b_joystick_axis_numbers_update[26];
                if (CHS2K_axis_buffer[27,0] != 0) LocoButtons.chs2k_EPT_0 = b_joystick_axis_numbers_update[27];
                if (CHS2K_axis_buffer[28,0] != 0) LocoButtons.chs2k_EPT_1 = b_joystick_axis_numbers_update[28];
                if (CHS2K_axis_buffer[29,0] != 0) LocoButtons.chs2k_prozh_0 = b_joystick_axis_numbers_update[29];
                if (CHS2K_axis_buffer[30,0] != 0) LocoButtons.chs2k_prozh_1 = b_joystick_axis_numbers_update[30];
                if (CHS2K_axis_buffer[31,0] != 0) LocoButtons.chs2k_prozh_2 = b_joystick_axis_numbers_update[31];


            }

            //-------------------------------------------------------------------------------------------
            //проверяем точки chs4
            //-------------------------------------------------------------------------------------------
            if (Loco.sig_loco == 4)
            {
                for (int i = 0; i < 55; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (CHS4_axis_buffer[i, 0] == j)
                        {
                            if (CHS4_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                CHS4_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (CHS4_axis_buffer[0,0] != 0) LocoButtons.chs4_rev_0 = b_joystick_axis_numbers_update[0];
                if (CHS4_axis_buffer[1,0] != 0) LocoButtons.chs4_rev_vpered = b_joystick_axis_numbers_update[1];
                if (CHS4_axis_buffer[2,0] != 0) LocoButtons.chs4_rev_nazad = b_joystick_axis_numbers_update[2];
                if (CHS4_axis_buffer[3,0] != 0) LocoButtons.chs4_kontr_0 = b_joystick_axis_numbers_update[3];
                if (CHS4_axis_buffer[4,0] != 0) LocoButtons.chs4_kontr_plus = b_joystick_axis_numbers_update[4];
                if (CHS4_axis_buffer[5,0] != 0) LocoButtons.chs4_kontr_minus = b_joystick_axis_numbers_update[5];
                if (CHS4_axis_buffer[6,0] != 0) LocoButtons.chs4_kontr_plus1 = b_joystick_axis_numbers_update[6];
                if (CHS4_axis_buffer[7,0] != 0) LocoButtons.chs4_kontr_minus1 = b_joystick_axis_numbers_update[7];
                if (CHS4_axis_buffer[8,0] != 0) LocoButtons.chs4_kontr_shunt1 = b_joystick_axis_numbers_update[8];
                if (CHS4_axis_buffer[9,0] != 0) LocoButtons.chs4_kontr_shunt2 = b_joystick_axis_numbers_update[9];
                if (CHS4_axis_buffer[10,0] != 0) LocoButtons.chs4_kontr_shunt3 = b_joystick_axis_numbers_update[10];
                if (CHS4_axis_buffer[11,0] != 0) LocoButtons.chs4_kontr_shunt4 = b_joystick_axis_numbers_update[11];
                if (CHS4_axis_buffer[12,0] != 0) LocoButtons.chs4_kontr_shunt5 = b_joystick_axis_numbers_update[12];
                if (CHS4_axis_buffer[13,0] != 0) LocoButtons.chs4_kranTM_0 = b_joystick_axis_numbers_update[13];
                if (CHS4_axis_buffer[14,0] != 0) LocoButtons.chs4_kranTM_1 = b_joystick_axis_numbers_update[14];
                if (CHS4_axis_buffer[15,0] != 0) LocoButtons.chs4_bv_0 = b_joystick_axis_numbers_update[15];
                if (CHS4_axis_buffer[16,0] != 0) LocoButtons.chs4_bv_1 = b_joystick_axis_numbers_update[16];
                if (CHS4_axis_buffer[17,0] != 0) LocoButtons.chs4_bv_2 = b_joystick_axis_numbers_update[17];
                if (CHS4_axis_buffer[18,0] != 0) LocoButtons.chs4_tokopr_per_0 = b_joystick_axis_numbers_update[18];
                if (CHS4_axis_buffer[19,0] != 0) LocoButtons.chs4_tokopr_per_1 = b_joystick_axis_numbers_update[19];
                if (CHS4_axis_buffer[20,0] != 0) LocoButtons.chs4_tokopr_zad_0 = b_joystick_axis_numbers_update[20];
                if (CHS4_axis_buffer[21,0] != 0) LocoButtons.chs4_tokopr_zad_1 = b_joystick_axis_numbers_update[21];
                if (CHS4_axis_buffer[22,0] != 0) LocoButtons.chs4_komp1_0 = b_joystick_axis_numbers_update[22];
                if (CHS4_axis_buffer[23,0] != 0) LocoButtons.chs4_komp1_1 = b_joystick_axis_numbers_update[23];
                if (CHS4_axis_buffer[24,0] != 0) LocoButtons.chs4_komp1_2 = b_joystick_axis_numbers_update[24];
                if (CHS4_axis_buffer[25,0] != 0) LocoButtons.chs4_komp2_0 = b_joystick_axis_numbers_update[25];
                if (CHS4_axis_buffer[26,0] != 0) LocoButtons.chs4_komp2_1 = b_joystick_axis_numbers_update[26];
                if (CHS4_axis_buffer[27,0] != 0) LocoButtons.chs4_komp2_2 = b_joystick_axis_numbers_update[27];
                if (CHS4_axis_buffer[28,0] != 0) LocoButtons.chs4_vent_0 = b_joystick_axis_numbers_update[28];
                if (CHS4_axis_buffer[29,0] != 0) LocoButtons.chs4_vent_1 = b_joystick_axis_numbers_update[29];
                if (CHS4_axis_buffer[30,0] != 0) LocoButtons.chs4_vent_2 = b_joystick_axis_numbers_update[30];
                if (CHS4_axis_buffer[31,0] != 0) LocoButtons.chs4_vent_3 = b_joystick_axis_numbers_update[31];
                if (CHS4_axis_buffer[32,0] != 0) LocoButtons.chs4_vent_4 = b_joystick_axis_numbers_update[32];
                if (CHS4_axis_buffer[33,0] != 0) LocoButtons.chs4_vent_5 = b_joystick_axis_numbers_update[33];
                if (CHS4_axis_buffer[34,0] != 0) LocoButtons.chs4_vent_6 = b_joystick_axis_numbers_update[34];
                if (CHS4_axis_buffer[35,0] != 0) LocoButtons.chs4_vent_7 = b_joystick_axis_numbers_update[35];
                if (CHS4_axis_buffer[36,0] != 0) LocoButtons.chs4_vspom_komp_0 = b_joystick_axis_numbers_update[36];
                if (CHS4_axis_buffer[37,0] != 0) LocoButtons.chs4_vspom_komp_1 = b_joystick_axis_numbers_update[37];
                if (CHS4_axis_buffer[38,0] != 0) LocoButtons.chs4_vspom_komp_2 = b_joystick_axis_numbers_update[38];
                if (CHS4_axis_buffer[39,0] != 0) LocoButtons.chs4_vspom_komp_3 = b_joystick_axis_numbers_update[39];
                if (CHS4_axis_buffer[40,0] != 0) LocoButtons.chs4_svet_cab_0 = b_joystick_axis_numbers_update[40];
                if (CHS4_axis_buffer[41,0] != 0) LocoButtons.chs4_svet_cab_1 = b_joystick_axis_numbers_update[41];
                if (CHS4_axis_buffer[42,0] != 0) LocoButtons.chs4_svet_cab_2 = b_joystick_axis_numbers_update[42];
                if (CHS4_axis_buffer[43,0] != 0) LocoButtons.chs4_svet_cab_3 = b_joystick_axis_numbers_update[43];
                if (CHS4_axis_buffer[44,0] != 0) LocoButtons.chs4_EPK_0 = b_joystick_axis_numbers_update[44];
                if (CHS4_axis_buffer[45,0] != 0) LocoButtons.chs4_EPK_1 = b_joystick_axis_numbers_update[45];
                if (CHS4_axis_buffer[46,0] != 0) LocoButtons.chs4_EPT_0 = b_joystick_axis_numbers_update[46];
                if (CHS4_axis_buffer[47,0] != 0) LocoButtons.chs4_EPT_1 = b_joystick_axis_numbers_update[47];
                if (CHS4_axis_buffer[48,0] != 0) LocoButtons.chs4_avar_nabor_0 = b_joystick_axis_numbers_update[48];
                if (CHS4_axis_buffer[49,0] != 0) LocoButtons.chs4_avar_nabor_1 = b_joystick_axis_numbers_update[49];
                if (CHS4_axis_buffer[50,0] != 0) LocoButtons.chs4_avar_nabor_2 = b_joystick_axis_numbers_update[50];
                if (CHS4_axis_buffer[51,0] != 0) LocoButtons.chs4_avar_nabor_3 = b_joystick_axis_numbers_update[51];
                if (CHS4_axis_buffer[52,0] != 0) LocoButtons.chs4_prozh_0 = b_joystick_axis_numbers_update[52];
                if (CHS4_axis_buffer[53,0] != 0) LocoButtons.chs4_prozh_1 = b_joystick_axis_numbers_update[53];
                if (CHS4_axis_buffer[54,0] != 0) LocoButtons.chs4_prozh_2 = b_joystick_axis_numbers_update[54];

            }

            //-------------------------------------------------------------------------------------------
            //проверяем точки chs4kvr
            //-------------------------------------------------------------------------------------------
            if (Loco.sig_loco == 5)
            {
                for (int i = 0; i < 55; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (CHS4KVR_axis_buffer[i, 0] == j)
                        {
                            if (CHS4KVR_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                CHS4KVR_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (CHS4KVR_axis_buffer[0,0] != 0) LocoButtons.chs4kvr_rev_0 = b_joystick_axis_numbers_update[0];
                if (CHS4KVR_axis_buffer[1,0] != 0) LocoButtons.chs4kvr_rev_vpered = b_joystick_axis_numbers_update[1];
                if (CHS4KVR_axis_buffer[2,0] != 0) LocoButtons.chs4kvr_rev_nazad = b_joystick_axis_numbers_update[2];
                if (CHS4KVR_axis_buffer[3,0] != 0) LocoButtons.chs4kvr_kontr_0 = b_joystick_axis_numbers_update[3];
                if (CHS4KVR_axis_buffer[4,0] != 0) LocoButtons.chs4kvr_kontr_plus = b_joystick_axis_numbers_update[4];
                if (CHS4KVR_axis_buffer[5,0] != 0) LocoButtons.chs4kvr_kontr_minus = b_joystick_axis_numbers_update[5];
                if (CHS4KVR_axis_buffer[6,0] != 0) LocoButtons.chs4kvr_kontr_plus1 = b_joystick_axis_numbers_update[6];
                if (CHS4KVR_axis_buffer[7,0] != 0) LocoButtons.chs4kvr_kontr_minus1 = b_joystick_axis_numbers_update[7];
                if (CHS4KVR_axis_buffer[8,0] != 0) LocoButtons.chs4kvr_kontr_shunt1 = b_joystick_axis_numbers_update[8];
                if (CHS4KVR_axis_buffer[9,0] != 0) LocoButtons.chs4kvr_kontr_shunt2 = b_joystick_axis_numbers_update[9];
                if (CHS4KVR_axis_buffer[10,0] != 0) LocoButtons.chs4kvr_kontr_shunt3 = b_joystick_axis_numbers_update[10];
                if (CHS4KVR_axis_buffer[11,0] != 0) LocoButtons.chs4kvr_kontr_shunt4 = b_joystick_axis_numbers_update[11];
                if (CHS4KVR_axis_buffer[12,0] != 0) LocoButtons.chs4kvr_kontr_shunt5 = b_joystick_axis_numbers_update[12];
                if (CHS4KVR_axis_buffer[13,0] != 0) LocoButtons.chs4kvr_kranTM_0 = b_joystick_axis_numbers_update[13];
                if (CHS4KVR_axis_buffer[14,0] != 0) LocoButtons.chs4kvr_kranTM_1 = b_joystick_axis_numbers_update[14];
                if (CHS4KVR_axis_buffer[15,0] != 0) LocoButtons.chs4kvr_bv_0 = b_joystick_axis_numbers_update[15];
                if (CHS4KVR_axis_buffer[16,0] != 0) LocoButtons.chs4kvr_bv_1 = b_joystick_axis_numbers_update[16];
                if (CHS4KVR_axis_buffer[17,0] != 0) LocoButtons.chs4kvr_bv_2 = b_joystick_axis_numbers_update[17];
                if (CHS4KVR_axis_buffer[18,0] != 0) LocoButtons.chs4kvr_tokopr_per_0 = b_joystick_axis_numbers_update[18];
                if (CHS4KVR_axis_buffer[19,0] != 0) LocoButtons.chs4kvr_tokopr_per_1 = b_joystick_axis_numbers_update[19];
                if (CHS4KVR_axis_buffer[20,0] != 0) LocoButtons.chs4kvr_tokopr_zad_0 = b_joystick_axis_numbers_update[20];
                if (CHS4KVR_axis_buffer[21,0] != 0) LocoButtons.chs4kvr_tokopr_zad_1 = b_joystick_axis_numbers_update[21];
                if (CHS4KVR_axis_buffer[22,0] != 0) LocoButtons.chs4kvr_komp1_0 = b_joystick_axis_numbers_update[22];
                if (CHS4KVR_axis_buffer[23,0] != 0) LocoButtons.chs4kvr_komp1_1 = b_joystick_axis_numbers_update[23];
                if (CHS4KVR_axis_buffer[24,0] != 0) LocoButtons.chs4kvr_komp1_2 = b_joystick_axis_numbers_update[24];
                if (CHS4KVR_axis_buffer[25,0] != 0) LocoButtons.chs4kvr_komp2_0 = b_joystick_axis_numbers_update[25];
                if (CHS4KVR_axis_buffer[26,0] != 0) LocoButtons.chs4kvr_komp2_1 = b_joystick_axis_numbers_update[26];
                if (CHS4KVR_axis_buffer[27,0] != 0) LocoButtons.chs4kvr_komp2_2 = b_joystick_axis_numbers_update[27];
                if (CHS4KVR_axis_buffer[28,0] != 0) LocoButtons.chs4kvr_vent_0 = b_joystick_axis_numbers_update[28];
                if (CHS4KVR_axis_buffer[29,0] != 0) LocoButtons.chs4kvr_vent_1 = b_joystick_axis_numbers_update[29];
                if (CHS4KVR_axis_buffer[30,0] != 0) LocoButtons.chs4kvr_vent_2 = b_joystick_axis_numbers_update[30];
                if (CHS4KVR_axis_buffer[31,0] != 0) LocoButtons.chs4kvr_vent_3 = b_joystick_axis_numbers_update[31];
                if (CHS4KVR_axis_buffer[32,0] != 0) LocoButtons.chs4kvr_vent_4 = b_joystick_axis_numbers_update[32];
                if (CHS4KVR_axis_buffer[33,0] != 0) LocoButtons.chs4kvr_vent_5 = b_joystick_axis_numbers_update[33];
                if (CHS4KVR_axis_buffer[34,0] != 0) LocoButtons.chs4kvr_vent_6 = b_joystick_axis_numbers_update[34];
                if (CHS4KVR_axis_buffer[35,0] != 0) LocoButtons.chs4kvr_vent_7 = b_joystick_axis_numbers_update[35];
                if (CHS4KVR_axis_buffer[36,0] != 0) LocoButtons.chs4kvr_vspom_komp_0 = b_joystick_axis_numbers_update[36];
                if (CHS4KVR_axis_buffer[37,0] != 0) LocoButtons.chs4kvr_vspom_komp_1 = b_joystick_axis_numbers_update[37];
                if (CHS4KVR_axis_buffer[38,0] != 0) LocoButtons.chs4kvr_vspom_komp_2 = b_joystick_axis_numbers_update[38];
                if (CHS4KVR_axis_buffer[39,0] != 0) LocoButtons.chs4kvr_vspom_komp_3 = b_joystick_axis_numbers_update[39];
                if (CHS4KVR_axis_buffer[40,0] != 0) LocoButtons.chs4kvr_svet_cab_0 = b_joystick_axis_numbers_update[40];
                if (CHS4KVR_axis_buffer[41,0] != 0) LocoButtons.chs4kvr_svet_cab_1 = b_joystick_axis_numbers_update[41];
                if (CHS4KVR_axis_buffer[42,0] != 0) LocoButtons.chs4kvr_svet_cab_2 = b_joystick_axis_numbers_update[42];
                if (CHS4KVR_axis_buffer[43,0] != 0) LocoButtons.chs4kvr_svet_cab_3 = b_joystick_axis_numbers_update[43];
                if (CHS4KVR_axis_buffer[44,0] != 0) LocoButtons.chs4kvr_EPK_0 = b_joystick_axis_numbers_update[44];
                if (CHS4KVR_axis_buffer[45,0] != 0) LocoButtons.chs4kvr_EPK_1 = b_joystick_axis_numbers_update[45];
                if (CHS4KVR_axis_buffer[46,0] != 0) LocoButtons.chs4kvr_EPT_0 = b_joystick_axis_numbers_update[46];
                if (CHS4KVR_axis_buffer[47,0] != 0) LocoButtons.chs4kvr_EPT_1 = b_joystick_axis_numbers_update[47];
                if (CHS4KVR_axis_buffer[48,0] != 0) LocoButtons.chs4kvr_avar_nabor_0 = b_joystick_axis_numbers_update[48];
                if (CHS4KVR_axis_buffer[49,0] != 0) LocoButtons.chs4kvr_avar_nabor_1 = b_joystick_axis_numbers_update[49];
                if (CHS4KVR_axis_buffer[50,0] != 0) LocoButtons.chs4kvr_avar_nabor_2 = b_joystick_axis_numbers_update[50];
                if (CHS4KVR_axis_buffer[51,0] != 0) LocoButtons.chs4kvr_avar_nabor_3 = b_joystick_axis_numbers_update[51];
                if (CHS4KVR_axis_buffer[52,0] != 0) LocoButtons.chs4kvr_prozh_0 = b_joystick_axis_numbers_update[52];
                if (CHS4KVR_axis_buffer[53,0] != 0) LocoButtons.chs4kvr_prozh_1 = b_joystick_axis_numbers_update[53];
                if (CHS4KVR_axis_buffer[54,0] != 0) LocoButtons.chs4kvr_prozh_2 = b_joystick_axis_numbers_update[54];

            }

            //-------------------------------------------------------------------------------------------
            //проверяем точки chs4t
            //-------------------------------------------------------------------------------------------
            if (Loco.sig_loco == 6)
            {
                for (int i = 0; i < 52; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (CHS4T_axis_buffer[i, 0] == j)
                        {
                            if (CHS4T_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                CHS4T_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (CHS4T_axis_buffer[0,0] != 0) LocoButtons.chs4t_rev_0 = b_joystick_axis_numbers_update[0];
                if (CHS4T_axis_buffer[1,0] != 0) LocoButtons.chs4t_rev_vpered = b_joystick_axis_numbers_update[1];
                if (CHS4T_axis_buffer[2,0] != 0) LocoButtons.chs4t_rev_nazad = b_joystick_axis_numbers_update[2];
                if (CHS4T_axis_buffer[3,0] != 0) LocoButtons.chs4t_kontr_0 = b_joystick_axis_numbers_update[3];
                if (CHS4T_axis_buffer[4,0] != 0) LocoButtons.chs4t_kontr_plus = b_joystick_axis_numbers_update[4];
                if (CHS4T_axis_buffer[5,0] != 0) LocoButtons.chs4t_kontr_minus = b_joystick_axis_numbers_update[5];
                if (CHS4T_axis_buffer[6,0] != 0) LocoButtons.chs4t_kontr_plus1 = b_joystick_axis_numbers_update[6];
                if (CHS4T_axis_buffer[7,0] != 0) LocoButtons.chs4t_kontr_minus1 = b_joystick_axis_numbers_update[7];
                if (CHS4T_axis_buffer[8,0] != 0) LocoButtons.chs4t_kontr_shunt1 = b_joystick_axis_numbers_update[8];
                if (CHS4T_axis_buffer[9,0] != 0) LocoButtons.chs4t_kontr_shunt2 = b_joystick_axis_numbers_update[9];
                if (CHS4T_axis_buffer[10,0] != 0) LocoButtons.chs4t_kontr_shunt3 = b_joystick_axis_numbers_update[10];
                if (CHS4T_axis_buffer[11,0] != 0) LocoButtons.chs4t_kontr_shunt4 = b_joystick_axis_numbers_update[11];
                if (CHS4T_axis_buffer[12,0] != 0) LocoButtons.chs4t_kontr_shunt5 = b_joystick_axis_numbers_update[12];
                if (CHS4T_axis_buffer[13,0] != 0) LocoButtons.chs4t_kranTM_0 = b_joystick_axis_numbers_update[13];
                if (CHS4T_axis_buffer[14,0] != 0) LocoButtons.chs4t_kranTM_1 = b_joystick_axis_numbers_update[14];
                if (CHS4T_axis_buffer[15,0] != 0) LocoButtons.chs4t_bv_0 = b_joystick_axis_numbers_update[15];
                if (CHS4T_axis_buffer[16,0] != 0) LocoButtons.chs4t_bv_1 = b_joystick_axis_numbers_update[16];
                if (CHS4T_axis_buffer[17,0] != 0) LocoButtons.chs4t_bv_2 = b_joystick_axis_numbers_update[17];
                if (CHS4T_axis_buffer[18,0] != 0) LocoButtons.chs4t_tokopr_per_0 = b_joystick_axis_numbers_update[18];
                if (CHS4T_axis_buffer[19,0] != 0) LocoButtons.chs4t_tokopr_per_1 = b_joystick_axis_numbers_update[19];
                if (CHS4T_axis_buffer[20,0] != 0) LocoButtons.chs4t_tokopr_zad_0 = b_joystick_axis_numbers_update[20];
                if (CHS4T_axis_buffer[21,0] != 0) LocoButtons.chs4t_tokopr_zad_1 = b_joystick_axis_numbers_update[21];
                if (CHS4T_axis_buffer[22,0] != 0) LocoButtons.chs4t_komp1_0 = b_joystick_axis_numbers_update[22];
                if (CHS4T_axis_buffer[23,0] != 0) LocoButtons.chs4t_komp1_1 = b_joystick_axis_numbers_update[23];
                if (CHS4T_axis_buffer[24,0] != 0) LocoButtons.chs4t_komp1_2 = b_joystick_axis_numbers_update[24];
                if (CHS4T_axis_buffer[25,0] != 0) LocoButtons.chs4t_komp2_0 = b_joystick_axis_numbers_update[25];
                if (CHS4T_axis_buffer[26,0] != 0) LocoButtons.chs4t_komp2_1 = b_joystick_axis_numbers_update[26];
                if (CHS4T_axis_buffer[27,0] != 0) LocoButtons.chs4t_komp2_2 = b_joystick_axis_numbers_update[27];
                if (CHS4T_axis_buffer[28,0] != 0) LocoButtons.chs4t_vent_0 = b_joystick_axis_numbers_update[28];
                if (CHS4T_axis_buffer[29,0] != 0) LocoButtons.chs4t_vent_1 = b_joystick_axis_numbers_update[29];
                if (CHS4T_axis_buffer[30,0] != 0) LocoButtons.chs4t_vent_2 = b_joystick_axis_numbers_update[30];
                if (CHS4T_axis_buffer[31,0] != 0) LocoButtons.chs4t_vspom_komp_0 = b_joystick_axis_numbers_update[31];
                if (CHS4T_axis_buffer[32,0] != 0) LocoButtons.chs4t_vspom_komp_1 = b_joystick_axis_numbers_update[32];
                if (CHS4T_axis_buffer[33,0] != 0) LocoButtons.chs4t_vspom_komp_2 = b_joystick_axis_numbers_update[33];
                if (CHS4T_axis_buffer[34,0] != 0) LocoButtons.chs4t_vspom_komp_3 = b_joystick_axis_numbers_update[34];
                if (CHS4T_axis_buffer[35,0] != 0) LocoButtons.chs4t_svet_cab_0 = b_joystick_axis_numbers_update[35];
                if (CHS4T_axis_buffer[36,0] != 0) LocoButtons.chs4t_svet_cab_1 = b_joystick_axis_numbers_update[36];
                if (CHS4T_axis_buffer[37,0] != 0) LocoButtons.chs4t_svet_cab_2 = b_joystick_axis_numbers_update[37];
                if (CHS4T_axis_buffer[38,0] != 0) LocoButtons.chs4t_svet_cab_3 = b_joystick_axis_numbers_update[38];
                if (CHS4T_axis_buffer[39,0] != 0) LocoButtons.chs4t_EPK_0 = b_joystick_axis_numbers_update[39];
                if (CHS4T_axis_buffer[40,0] != 0) LocoButtons.chs4t_EPK_1 = b_joystick_axis_numbers_update[40];
                if (CHS4T_axis_buffer[41,0] != 0) LocoButtons.chs4t_EPT_0 = b_joystick_axis_numbers_update[41];
                if (CHS4T_axis_buffer[42,0] != 0) LocoButtons.chs4t_EPT_1 = b_joystick_axis_numbers_update[42];
                if (CHS4T_axis_buffer[43,0] != 0) LocoButtons.chs4t_avar_nabor_0 = b_joystick_axis_numbers_update[43];
                if (CHS4T_axis_buffer[44,0] != 0) LocoButtons.chs4t_avar_nabor_1 = b_joystick_axis_numbers_update[44];
                if (CHS4T_axis_buffer[45,0] != 0) LocoButtons.chs4t_avar_nabor_2 = b_joystick_axis_numbers_update[45];
                if (CHS4T_axis_buffer[46,0] != 0) LocoButtons.chs4t_avar_nabor_3 = b_joystick_axis_numbers_update[46];
                if (CHS4T_axis_buffer[47,0] != 0) LocoButtons.chs4t_prozh_0 = b_joystick_axis_numbers_update[47];
                if (CHS4T_axis_buffer[48,0] != 0) LocoButtons.chs4t_prozh_1 = b_joystick_axis_numbers_update[48];
                if (CHS4T_axis_buffer[49,0] != 0) LocoButtons.chs4t_prozh_2 = b_joystick_axis_numbers_update[49];
                if (CHS4T_axis_buffer[50,0] != 0) LocoButtons.chs4t_zhalyzi_0 = b_joystick_axis_numbers_update[50];
                if (CHS4T_axis_buffer[51,0] != 0) LocoButtons.chs4t_zhalyzi_1 = b_joystick_axis_numbers_update[51];

            }

            //-------------------------------------------------------------------------------------------
            //проверяем точки chs7
            //-------------------------------------------------------------------------------------------
            if (Loco.sig_loco == 7)
            {
                for (int i = 0; i < 46; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (CHS7_axis_buffer[i, 0] == j)
                        {
                            if (CHS7_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                CHS7_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (CHS7_axis_buffer[0,0] != 0) LocoButtons.chs7_rev_0 = b_joystick_axis_numbers_update[0];
                if (CHS7_axis_buffer[1,0] != 0) LocoButtons.chs7_rev_vpered = b_joystick_axis_numbers_update[1];
                if (CHS7_axis_buffer[2,0] != 0) LocoButtons.chs7_rev_nazad = b_joystick_axis_numbers_update[2];
                if (CHS7_axis_buffer[3,0] != 0) LocoButtons.chs7_kontr_0 = b_joystick_axis_numbers_update[3];
                if (CHS7_axis_buffer[4,0] != 0) LocoButtons.chs7_kontr_plus = b_joystick_axis_numbers_update[4];
                if (CHS7_axis_buffer[5,0] != 0) LocoButtons.chs7_kontr_minus = b_joystick_axis_numbers_update[5];
                if (CHS7_axis_buffer[6,0] != 0) LocoButtons.chs7_kontr_plus1 = b_joystick_axis_numbers_update[6];
                if (CHS7_axis_buffer[7,0] != 0) LocoButtons.chs7_kontr_minus1 = b_joystick_axis_numbers_update[7];
                if (CHS7_axis_buffer[8,0] != 0) LocoButtons.chs7_kontr_shunt1 = b_joystick_axis_numbers_update[8];
                if (CHS7_axis_buffer[9,0] != 0) LocoButtons.chs7_kontr_shunt2 = b_joystick_axis_numbers_update[9];
                if (CHS7_axis_buffer[10,0] != 0) LocoButtons.chs7_kontr_shunt3 = b_joystick_axis_numbers_update[10];
                if (CHS7_axis_buffer[11,0] != 0) LocoButtons.chs7_kontr_shunt4 = b_joystick_axis_numbers_update[11];
                if (CHS7_axis_buffer[12,0] != 0) LocoButtons.chs7_kontr_shunt5 = b_joystick_axis_numbers_update[12];
                if (CHS7_axis_buffer[13,0] != 0) LocoButtons.chs7_kranTM_0 = b_joystick_axis_numbers_update[13];
                if (CHS7_axis_buffer[14,0] != 0) LocoButtons.chs7_kranTM_1 = b_joystick_axis_numbers_update[14];
                if (CHS7_axis_buffer[15,0] != 0) LocoButtons.chs7_bv_0 = b_joystick_axis_numbers_update[15];
                if (CHS7_axis_buffer[16,0] != 0) LocoButtons.chs7_bv_1 = b_joystick_axis_numbers_update[16];
                if (CHS7_axis_buffer[17,0] != 0) LocoButtons.chs7_bv_2 = b_joystick_axis_numbers_update[17];
                if (CHS7_axis_buffer[18,0] != 0) LocoButtons.chs7_tokopr_per_0 = b_joystick_axis_numbers_update[18];
                if (CHS7_axis_buffer[19,0] != 0) LocoButtons.chs7_tokopr_per_1 = b_joystick_axis_numbers_update[19];
                if (CHS7_axis_buffer[20,0] != 0) LocoButtons.chs7_tokopr_per_2 = b_joystick_axis_numbers_update[20];
                if (CHS7_axis_buffer[21,0] != 0) LocoButtons.chs7_tokopr_zad_0 = b_joystick_axis_numbers_update[21];
                if (CHS7_axis_buffer[22,0] != 0) LocoButtons.chs7_tokopr_zad_1 = b_joystick_axis_numbers_update[22];
                if (CHS7_axis_buffer[23,0] != 0) LocoButtons.chs7_tokopr_zad_2 = b_joystick_axis_numbers_update[23];
                if (CHS7_axis_buffer[24,0] != 0) LocoButtons.chs7_komp1_0 = b_joystick_axis_numbers_update[24];
                if (CHS7_axis_buffer[25,0] != 0) LocoButtons.chs7_komp1_1 = b_joystick_axis_numbers_update[25];
                if (CHS7_axis_buffer[26,0] != 0) LocoButtons.chs7_komp1_2 = b_joystick_axis_numbers_update[26];
                if (CHS7_axis_buffer[27,0] != 0) LocoButtons.chs7_komp2_0 = b_joystick_axis_numbers_update[27];
                if (CHS7_axis_buffer[28,0] != 0) LocoButtons.chs7_komp2_1 = b_joystick_axis_numbers_update[28];
                if (CHS7_axis_buffer[29,0] != 0) LocoButtons.chs7_komp2_2 = b_joystick_axis_numbers_update[29];
                if (CHS7_axis_buffer[30,0] != 0) LocoButtons.chs7_vent_0 = b_joystick_axis_numbers_update[30];
                if (CHS7_axis_buffer[31,0] != 0) LocoButtons.chs7_vent_1 = b_joystick_axis_numbers_update[31];
                if (CHS7_axis_buffer[32,0] != 0) LocoButtons.chs7_vent_2 = b_joystick_axis_numbers_update[32];
                if (CHS7_axis_buffer[33,0] != 0) LocoButtons.chs7_sbros_SP = b_joystick_axis_numbers_update[33];
                if (CHS7_axis_buffer[34,0] != 0) LocoButtons.chs7_svet_cab_0 = b_joystick_axis_numbers_update[34];
                if (CHS7_axis_buffer[35,0] != 0) LocoButtons.chs7_svet_cab_1 = b_joystick_axis_numbers_update[35];
                if (CHS7_axis_buffer[36,0] != 0) LocoButtons.chs7_svet_cab_2 = b_joystick_axis_numbers_update[36];
                if (CHS7_axis_buffer[37,0] != 0) LocoButtons.chs7_EPK_0 = b_joystick_axis_numbers_update[37];
                if (CHS7_axis_buffer[38,0] != 0) LocoButtons.chs7_EPK_1 = b_joystick_axis_numbers_update[38];
                if (CHS7_axis_buffer[39,0] != 0) LocoButtons.chs7_EPT_0 = b_joystick_axis_numbers_update[39];
                if (CHS7_axis_buffer[40,0] != 0) LocoButtons.chs7_EPT_1 = b_joystick_axis_numbers_update[40];
                if (CHS7_axis_buffer[41,0] != 0) LocoButtons.chs7_prozh_0 = b_joystick_axis_numbers_update[41];
                if (CHS7_axis_buffer[42,0] != 0) LocoButtons.chs7_prozh_1 = b_joystick_axis_numbers_update[42];
                if (CHS7_axis_buffer[43, 0] != 0) LocoButtons.chs7_prozh_2 = b_joystick_axis_numbers_update[43];
                if (CHS7_axis_buffer[44,0] != 0) LocoButtons.chs7_zhalyzi1_0 = b_joystick_axis_numbers_update[44];
                if (CHS7_axis_buffer[45,0] != 0) LocoButtons.chs7_zhalyzi1_1 = b_joystick_axis_numbers_update[45];
            }

            //-------------------------------------------------------------------------------------------
            //проверяем точки chs8
            //-------------------------------------------------------------------------------------------
            if (Loco.sig_loco == 8)
            {
                for (int i = 0; i < 63; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (CHS8_axis_buffer[i, 0] == j)
                        {
                            if (CHS8_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                CHS8_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (CHS8_axis_buffer[0,0] != 0) LocoButtons.chs8_rev_0 = b_joystick_axis_numbers_update[0];
                if (CHS8_axis_buffer[1,0] != 0) LocoButtons.chs8_rev_vpered = b_joystick_axis_numbers_update[1];
                if (CHS8_axis_buffer[2,0] != 0) LocoButtons.chs8_rev_nazad = b_joystick_axis_numbers_update[2];
                if (CHS8_axis_buffer[3,0] != 0) LocoButtons.chs8_kontr_0 = b_joystick_axis_numbers_update[3];
                if (CHS8_axis_buffer[4,0] != 0) LocoButtons.chs8_kontr_plus = b_joystick_axis_numbers_update[4];
                if (CHS8_axis_buffer[5,0] != 0) LocoButtons.chs8_kontr_minus = b_joystick_axis_numbers_update[5];
                if (CHS8_axis_buffer[6,0] != 0) LocoButtons.chs8_kontr_plus1 = b_joystick_axis_numbers_update[6];
                if (CHS8_axis_buffer[7,0] != 0) LocoButtons.chs8_kontr_minus1 = b_joystick_axis_numbers_update[7];
                if (CHS8_axis_buffer[8,0] != 0) LocoButtons.chs8_kontr_shunt1 = b_joystick_axis_numbers_update[8];
                if (CHS8_axis_buffer[9,0] != 0) LocoButtons.chs8_kontr_shunt2 = b_joystick_axis_numbers_update[9];
                if (CHS8_axis_buffer[10,0] != 0) LocoButtons.chs8_kontr_shunt3 = b_joystick_axis_numbers_update[10];
                if (CHS8_axis_buffer[11,0] != 0) LocoButtons.chs8_kontr_shunt4 = b_joystick_axis_numbers_update[11];
                if (CHS8_axis_buffer[12,0] != 0) LocoButtons.chs8_kontr_shunt5 = b_joystick_axis_numbers_update[12];
                if (CHS8_axis_buffer[13,0] != 0) LocoButtons.chs8_kranTM_0 = b_joystick_axis_numbers_update[13];
                if (CHS8_axis_buffer[14,0] != 0) LocoButtons.chs8_kranTM_1 = b_joystick_axis_numbers_update[14];
                if (CHS8_axis_buffer[15,0] != 0) LocoButtons.chs8_bv_0 = b_joystick_axis_numbers_update[15];
                if (CHS8_axis_buffer[16,0] != 0) LocoButtons.chs8_bv_1 = b_joystick_axis_numbers_update[16];
                if (CHS8_axis_buffer[17,0] != 0) LocoButtons.chs8_vosst_bv = b_joystick_axis_numbers_update[17];
                if (CHS8_axis_buffer[18,0] != 0) LocoButtons.chs8_tokopr_per_0 = b_joystick_axis_numbers_update[18];
                if (CHS8_axis_buffer[19,0] != 0) LocoButtons.chs8_tokopr_per_1 = b_joystick_axis_numbers_update[19];
                if (CHS8_axis_buffer[20,0] != 0) LocoButtons.chs8_tokopr_zad_0 = b_joystick_axis_numbers_update[20];
                if (CHS8_axis_buffer[21,0] != 0) LocoButtons.chs8_tokopr_zad_1 = b_joystick_axis_numbers_update[21];
                if (CHS8_axis_buffer[22,0] != 0) LocoButtons.chs8_komp1_0 = b_joystick_axis_numbers_update[22];
                if (CHS8_axis_buffer[23,0] != 0) LocoButtons.chs8_komp1_1 = b_joystick_axis_numbers_update[23];
                if (CHS8_axis_buffer[24,0] != 0) LocoButtons.chs8_komp1_2 = b_joystick_axis_numbers_update[24];
                if (CHS8_axis_buffer[25,0] != 0) LocoButtons.chs8_komp2_0 = b_joystick_axis_numbers_update[25];
                if (CHS8_axis_buffer[26,0] != 0) LocoButtons.chs8_komp2_1 = b_joystick_axis_numbers_update[26];
                if (CHS8_axis_buffer[27,0] != 0) LocoButtons.chs8_komp2_2 = b_joystick_axis_numbers_update[27];
                if (CHS8_axis_buffer[28,0] != 0) LocoButtons.chs8_vent1_0 = b_joystick_axis_numbers_update[28];
                if (CHS8_axis_buffer[29,0] != 0) LocoButtons.chs8_vent1_1 = b_joystick_axis_numbers_update[29];
                if (CHS8_axis_buffer[30,0] != 0) LocoButtons.chs8_vent1_2 = b_joystick_axis_numbers_update[30];
                if (CHS8_axis_buffer[31,0] != 0) LocoButtons.chs8_vent1_3 = b_joystick_axis_numbers_update[31];
                if (CHS8_axis_buffer[32,0] != 0) LocoButtons.chs8_vent1_4 = b_joystick_axis_numbers_update[32];
                if (CHS8_axis_buffer[33,0] != 0) LocoButtons.chs8_vent2_0 = b_joystick_axis_numbers_update[33];
                if (CHS8_axis_buffer[34,0] != 0) LocoButtons.chs8_vent2_1 = b_joystick_axis_numbers_update[34];
                if (CHS8_axis_buffer[35,0] != 0) LocoButtons.chs8_vent2_2 = b_joystick_axis_numbers_update[35];
                if (CHS8_axis_buffer[36,0] != 0) LocoButtons.chs8_vent2_3 = b_joystick_axis_numbers_update[36];
                if (CHS8_axis_buffer[37,0] != 0) LocoButtons.chs8_vent2_4 = b_joystick_axis_numbers_update[37];
                if (CHS8_axis_buffer[38,0] != 0) LocoButtons.chs8_vspom_komp_0 = b_joystick_axis_numbers_update[38];
                if (CHS8_axis_buffer[39,0] != 0) LocoButtons.chs8_vspom_komp_1 = b_joystick_axis_numbers_update[39];
                if (CHS8_axis_buffer[40,0] != 0) LocoButtons.chs8_vspom_komp_2 = b_joystick_axis_numbers_update[40];
                if (CHS8_axis_buffer[41,0] != 0) LocoButtons.chs8_vspom_komp_3 = b_joystick_axis_numbers_update[41];
                if (CHS8_axis_buffer[42,0] != 0) LocoButtons.chs8_svet_cab_0 = b_joystick_axis_numbers_update[42];
                if (CHS8_axis_buffer[43,0] != 0) LocoButtons.chs8_svet_cab_1 = b_joystick_axis_numbers_update[43];
                if (CHS8_axis_buffer[44,0] != 0) LocoButtons.chs8_svet_cab_2 = b_joystick_axis_numbers_update[44];
                if (CHS8_axis_buffer[45,0] != 0) LocoButtons.chs8_svet_cab_3 = b_joystick_axis_numbers_update[45];
                if (CHS8_axis_buffer[46,0] != 0) LocoButtons.chs8_svet_cab_4 = b_joystick_axis_numbers_update[46];
                if (CHS8_axis_buffer[47,0] != 0) LocoButtons.chs8_svet_cab_5 = b_joystick_axis_numbers_update[47];
                if (CHS8_axis_buffer[48,0] != 0) LocoButtons.chs8_EPK_0 = b_joystick_axis_numbers_update[48];
                if (CHS8_axis_buffer[49,0] != 0) LocoButtons.chs8_EPK_1 = b_joystick_axis_numbers_update[49];
                if (CHS8_axis_buffer[50,0] != 0) LocoButtons.chs8_EPT_0 = b_joystick_axis_numbers_update[50];
                if (CHS8_axis_buffer[51,0] != 0) LocoButtons.chs8_EPT_1 = b_joystick_axis_numbers_update[51];
                if (CHS8_axis_buffer[52,0] != 0) LocoButtons.chs8_avar_nabor_0 = b_joystick_axis_numbers_update[52];
                if (CHS8_axis_buffer[53,0] != 0) LocoButtons.chs8_avar_nabor_1 = b_joystick_axis_numbers_update[53];
                if (CHS8_axis_buffer[54,0] != 0) LocoButtons.chs8_avar_nabor_2 = b_joystick_axis_numbers_update[54];
                if (CHS8_axis_buffer[55,0] != 0) LocoButtons.chs8_avar_nabor_3 = b_joystick_axis_numbers_update[55];
                if (CHS8_axis_buffer[56,0] != 0) LocoButtons.chs8_prozh_0 = b_joystick_axis_numbers_update[56];
                if (CHS8_axis_buffer[57,0] != 0) LocoButtons.chs8_prozh_1 = b_joystick_axis_numbers_update[57];
                if (CHS8_axis_buffer[58,0] != 0) LocoButtons.chs8_prozh_2 = b_joystick_axis_numbers_update[58];
                if (CHS8_axis_buffer[59,0] != 0) LocoButtons.chs8_reost_torm_proverka = b_joystick_axis_numbers_update[59];
                if (CHS8_axis_buffer[60,0] != 0) LocoButtons.chs8_reost_torm_0 = b_joystick_axis_numbers_update[60];
                if (CHS8_axis_buffer[61,0] != 0) LocoButtons.chs8_reost_torm_1 = b_joystick_axis_numbers_update[61];
                if (CHS8_axis_buffer[62,0] != 0) LocoButtons.chs8_reost_torm_2 = b_joystick_axis_numbers_update[62];
            }

            //проверяем точки vl11
            if (Loco.sig_loco == 9)
            {
                for (int i = 0; i < 83; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (VL11M_axis_buffer[i, 0] == j)
                        {
                            if (VL11M_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                VL11M_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (VL11M_axis_buffer[0,0] != 0) LocoButtons.vl11_rev_0 = b_joystick_axis_numbers_update[0];
                if (VL11M_axis_buffer[1,0] != 0) LocoButtons.vl11_rev_vpered = b_joystick_axis_numbers_update[1];
                if (VL11M_axis_buffer[2,0] != 0) LocoButtons.vl11_rev_nazad = b_joystick_axis_numbers_update[2];
                if (VL11M_axis_buffer[3,0] != 0) LocoButtons.vl11_kontr_0 = b_joystick_axis_numbers_update[3];
                if (VL11M_axis_buffer[4,0] != 0) LocoButtons.vl11_kontr_1 = b_joystick_axis_numbers_update[4];
                if (VL11M_axis_buffer[5,0] != 0) LocoButtons.vl11_kontr_2 = b_joystick_axis_numbers_update[5];
                if (VL11M_axis_buffer[6,0] != 0) LocoButtons.vl11_kontr_3 = b_joystick_axis_numbers_update[6];
                if (VL11M_axis_buffer[7,0] != 0) LocoButtons.vl11_kontr_4 = b_joystick_axis_numbers_update[7];
                if (VL11M_axis_buffer[8,0] != 0) LocoButtons.vl11_kontr_5 = b_joystick_axis_numbers_update[8];
                if (VL11M_axis_buffer[9,0] != 0) LocoButtons.vl11_kontr_6 = b_joystick_axis_numbers_update[9];
                if (VL11M_axis_buffer[10,0] != 0) LocoButtons.vl11_kontr_7 = b_joystick_axis_numbers_update[10];
                if (VL11M_axis_buffer[11,0] != 0) LocoButtons.vl11_kontr_8 = b_joystick_axis_numbers_update[11];
                if (VL11M_axis_buffer[12,0] != 0) LocoButtons.vl11_kontr_9 = b_joystick_axis_numbers_update[12];
                if (VL11M_axis_buffer[13,0] != 0) LocoButtons.vl11_kontr_10 = b_joystick_axis_numbers_update[13];
                if (VL11M_axis_buffer[14,0] != 0) LocoButtons.vl11_kontr_11 = b_joystick_axis_numbers_update[14];
                if (VL11M_axis_buffer[15,0] != 0) LocoButtons.vl11_kontr_12 = b_joystick_axis_numbers_update[15];
                if (VL11M_axis_buffer[16,0] != 0) LocoButtons.vl11_kontr_13 = b_joystick_axis_numbers_update[16];
                if (VL11M_axis_buffer[17,0] != 0) LocoButtons.vl11_kontr_14 = b_joystick_axis_numbers_update[17];
                if (VL11M_axis_buffer[18,0] != 0) LocoButtons.vl11_kontr_15 = b_joystick_axis_numbers_update[18];
                if (VL11M_axis_buffer[19,0] != 0) LocoButtons.vl11_kontr_16 = b_joystick_axis_numbers_update[19];
                if (VL11M_axis_buffer[20,0] != 0) LocoButtons.vl11_kontr_17 = b_joystick_axis_numbers_update[20];
                if (VL11M_axis_buffer[21,0] != 0) LocoButtons.vl11_kontr_18 = b_joystick_axis_numbers_update[21];
                if (VL11M_axis_buffer[22,0] != 0) LocoButtons.vl11_kontr_19 = b_joystick_axis_numbers_update[22];
                if (VL11M_axis_buffer[23,0] != 0) LocoButtons.vl11_kontr_20 = b_joystick_axis_numbers_update[23];
                if (VL11M_axis_buffer[24,0] != 0) LocoButtons.vl11_kontr_21 = b_joystick_axis_numbers_update[24];
                if (VL11M_axis_buffer[25,0] != 0) LocoButtons.vl11_kontr_22 = b_joystick_axis_numbers_update[25];
                if (VL11M_axis_buffer[26,0] != 0) LocoButtons.vl11_kontr_23 = b_joystick_axis_numbers_update[26];
                if (VL11M_axis_buffer[27,0] != 0) LocoButtons.vl11_kontr_24 = b_joystick_axis_numbers_update[27];
                if (VL11M_axis_buffer[28,0] != 0) LocoButtons.vl11_kontr_25 = b_joystick_axis_numbers_update[28];
                if (VL11M_axis_buffer[29,0] != 0) LocoButtons.vl11_kontr_26 = b_joystick_axis_numbers_update[29];
                if (VL11M_axis_buffer[30,0] != 0) LocoButtons.vl11_kontr_27 = b_joystick_axis_numbers_update[30];
                if (VL11M_axis_buffer[31,0] != 0) LocoButtons.vl11_kontr_28 = b_joystick_axis_numbers_update[31];
                if (VL11M_axis_buffer[32,0] != 0) LocoButtons.vl11_kontr_29 = b_joystick_axis_numbers_update[32];
                if (VL11M_axis_buffer[33,0] != 0) LocoButtons.vl11_kontr_30 = b_joystick_axis_numbers_update[33];
                if (VL11M_axis_buffer[34,0] != 0) LocoButtons.vl11_kontr_31 = b_joystick_axis_numbers_update[34];
                if (VL11M_axis_buffer[35,0] != 0) LocoButtons.vl11_kontr_32 = b_joystick_axis_numbers_update[35];
                if (VL11M_axis_buffer[36,0] != 0) LocoButtons.vl11_kontr_33 = b_joystick_axis_numbers_update[36];
                if (VL11M_axis_buffer[37,0] != 0) LocoButtons.vl11_kontr_34 = b_joystick_axis_numbers_update[37];
                if (VL11M_axis_buffer[38,0] != 0) LocoButtons.vl11_kontr_35 = b_joystick_axis_numbers_update[38];
                if (VL11M_axis_buffer[39,0] != 0) LocoButtons.vl11_kontr_36 = b_joystick_axis_numbers_update[39];
                if (VL11M_axis_buffer[40,0] != 0) LocoButtons.vl11_kontr_37 = b_joystick_axis_numbers_update[40];
                if (VL11M_axis_buffer[41,0] != 0) LocoButtons.vl11_kontr_38 = b_joystick_axis_numbers_update[41];
                if (VL11M_axis_buffer[42,0] != 0) LocoButtons.vl11_kontr_39 = b_joystick_axis_numbers_update[42];
                if (VL11M_axis_buffer[43,0] != 0) LocoButtons.vl11_kontr_40 = b_joystick_axis_numbers_update[43];
                if (VL11M_axis_buffer[44,0] != 0) LocoButtons.vl11_kontr_41 = b_joystick_axis_numbers_update[44];
                if (VL11M_axis_buffer[45,0] != 0) LocoButtons.vl11_kontr_42 = b_joystick_axis_numbers_update[45];
                if (VL11M_axis_buffer[46,0] != 0) LocoButtons.vl11_kontr_43 = b_joystick_axis_numbers_update[46];
                if (VL11M_axis_buffer[47,0] != 0) LocoButtons.vl11_kontr_44 = b_joystick_axis_numbers_update[47];
                if (VL11M_axis_buffer[48,0] != 0) LocoButtons.vl11_kontr_45 = b_joystick_axis_numbers_update[48];
                if (VL11M_axis_buffer[49,0] != 0) LocoButtons.vl11_kontr_46 = b_joystick_axis_numbers_update[49];
                if (VL11M_axis_buffer[50,0] != 0) LocoButtons.vl11_kontr_47 = b_joystick_axis_numbers_update[50];
                if (VL11M_axis_buffer[51,0] != 0) LocoButtons.vl11_kontr_48 = b_joystick_axis_numbers_update[51];
                if (VL11M_axis_buffer[52,0] != 0) LocoButtons.vl11_kontr_shunt_0 = b_joystick_axis_numbers_update[52];
                if (VL11M_axis_buffer[53,0] != 0) LocoButtons.vl11_kontr_shunt_1 = b_joystick_axis_numbers_update[53];
                if (VL11M_axis_buffer[54,0] != 0) LocoButtons.vl11_kontr_shunt_2 = b_joystick_axis_numbers_update[54];
                if (VL11M_axis_buffer[55,0] != 0) LocoButtons.vl11_kontr_shunt_3 = b_joystick_axis_numbers_update[55];
                if (VL11M_axis_buffer[56,0] != 0) LocoButtons.vl11_kontr_shunt_4 = b_joystick_axis_numbers_update[56];
                if (VL11M_axis_buffer[57,0] != 0) LocoButtons.vl11_kranTM_0 = b_joystick_axis_numbers_update[57];
                if (VL11M_axis_buffer[58,0] != 0) LocoButtons.vl11_kranTM_1 = b_joystick_axis_numbers_update[58];
                if (VL11M_axis_buffer[59,0] != 0) LocoButtons.vl11_tokopr_obshiy_0 = b_joystick_axis_numbers_update[59];
                if (VL11M_axis_buffer[60,0] != 0) LocoButtons.vl11_tokopr_obshiy_1 = b_joystick_axis_numbers_update[60];
                if (VL11M_axis_buffer[61,0] != 0) LocoButtons.vl11_tokopr_per_0 = b_joystick_axis_numbers_update[61];
                if (VL11M_axis_buffer[62,0] != 0) LocoButtons.vl11_tokopr_per_1 = b_joystick_axis_numbers_update[62];
                if (VL11M_axis_buffer[63,0] != 0) LocoButtons.vl11_tokopr_zad_0 = b_joystick_axis_numbers_update[63];
                if (VL11M_axis_buffer[64,0] != 0) LocoButtons.vl11_tokopr_zad_1 = b_joystick_axis_numbers_update[64];
                if (VL11M_axis_buffer[65,0] != 0) LocoButtons.vl11_bv_0 = b_joystick_axis_numbers_update[65];
                if (VL11M_axis_buffer[66,0] != 0) LocoButtons.vl11_bv_1 = b_joystick_axis_numbers_update[66];
                if (VL11M_axis_buffer[67,0] != 0) LocoButtons.vl11_vosst_bv = b_joystick_axis_numbers_update[67];
                if (VL11M_axis_buffer[68,0] != 0) LocoButtons.vl11_komp_0 = b_joystick_axis_numbers_update[68];
                if (VL11M_axis_buffer[69,0] != 0) LocoButtons.vl11_komp_1 = b_joystick_axis_numbers_update[69];
                if (VL11M_axis_buffer[70,0] != 0) LocoButtons.vl11_vent_0 = b_joystick_axis_numbers_update[70];
                if (VL11M_axis_buffer[71,0] != 0) LocoButtons.vl11_vent_1 = b_joystick_axis_numbers_update[71];
                if (VL11M_axis_buffer[72,0] != 0) LocoButtons.vl11_vent_2 = b_joystick_axis_numbers_update[72];
                if (VL11M_axis_buffer[73,0] != 0) LocoButtons.vl11_svet_cab_0 = b_joystick_axis_numbers_update[73];
                if (VL11M_axis_buffer[74,0] != 0) LocoButtons.vl11_svet_cab_1 = b_joystick_axis_numbers_update[74];
                if (VL11M_axis_buffer[75,0] != 0) LocoButtons.vl11_svet_cab_2 = b_joystick_axis_numbers_update[75];
                if (VL11M_axis_buffer[76,0] != 0) LocoButtons.vl11_EPK_0 = b_joystick_axis_numbers_update[76];
                if (VL11M_axis_buffer[77,0] != 0) LocoButtons.vl11_EPK_1 = b_joystick_axis_numbers_update[77];
                if (VL11M_axis_buffer[78,0] != 0) LocoButtons.vl11_prozh_0 = b_joystick_axis_numbers_update[78];
                if (VL11M_axis_buffer[79,0] != 0) LocoButtons.vl11_prozh_1 = b_joystick_axis_numbers_update[79];
                if (VL11M_axis_buffer[80, 0] != 0) LocoButtons.vl11_prozh_2 = b_joystick_axis_numbers_update[80];
                if (VL11M_axis_buffer[81,0] != 0) LocoButtons.vl11_sign_0 = b_joystick_axis_numbers_update[81];
                if (VL11M_axis_buffer[82,0] != 0) LocoButtons.vl11_sign_1 = b_joystick_axis_numbers_update[82];
            }

            //проверяем точки vl82
            if (Loco.sig_loco == 10)
            {
                for (int i = 0; i < 83; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (VL82M_axis_buffer[i, 0] == j)
                        {
                            if (VL82M_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                VL82M_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }
                if (VL82M_axis_buffer[0,0] != 0) LocoButtons.vl82_rev_0 = b_joystick_axis_numbers_update[0];
                if (VL82M_axis_buffer[1,0] != 0) LocoButtons.vl82_rev_vpered = b_joystick_axis_numbers_update[1];
                if (VL82M_axis_buffer[2,0] != 0) LocoButtons.vl82_rev_nazad = b_joystick_axis_numbers_update[2];
                if (VL82M_axis_buffer[3,0] != 0) LocoButtons.vl82_kontr_bv = b_joystick_axis_numbers_update[3];
                if (VL82M_axis_buffer[4,0] != 0) LocoButtons.vl82_kontr_0 = b_joystick_axis_numbers_update[4];
                if (VL82M_axis_buffer[5,0] != 0) LocoButtons.vl82_kontr_1 = b_joystick_axis_numbers_update[5];
                if (VL82M_axis_buffer[6,0] != 0) LocoButtons.vl82_kontr_2 = b_joystick_axis_numbers_update[6];
                if (VL82M_axis_buffer[7,0] != 0) LocoButtons.vl82_kontr_3 = b_joystick_axis_numbers_update[7];
                if (VL82M_axis_buffer[8,0] != 0) LocoButtons.vl82_kontr_4 = b_joystick_axis_numbers_update[8];
                if (VL82M_axis_buffer[9,0] != 0) LocoButtons.vl82_kontr_5 = b_joystick_axis_numbers_update[9];
                if (VL82M_axis_buffer[10,0] != 0) LocoButtons.vl82_kontr_6 = b_joystick_axis_numbers_update[10];
                if (VL82M_axis_buffer[11,0] != 0) LocoButtons.vl82_kontr_7 = b_joystick_axis_numbers_update[11];
                if (VL82M_axis_buffer[12,0] != 0) LocoButtons.vl82_kontr_8 = b_joystick_axis_numbers_update[12];
                if (VL82M_axis_buffer[13,0] != 0) LocoButtons.vl82_kontr_9 = b_joystick_axis_numbers_update[13];
                if (VL82M_axis_buffer[14,0] != 0) LocoButtons.vl82_kontr_10 = b_joystick_axis_numbers_update[14];
                if (VL82M_axis_buffer[15,0] != 0) LocoButtons.vl82_kontr_11 = b_joystick_axis_numbers_update[15];
                if (VL82M_axis_buffer[16,0] != 0) LocoButtons.vl82_kontr_12 = b_joystick_axis_numbers_update[16];
                if (VL82M_axis_buffer[17,0] != 0) LocoButtons.vl82_kontr_13 = b_joystick_axis_numbers_update[17];
                if (VL82M_axis_buffer[18,0] != 0) LocoButtons.vl82_kontr_14 = b_joystick_axis_numbers_update[18];
                if (VL82M_axis_buffer[19,0] != 0) LocoButtons.vl82_kontr_15 = b_joystick_axis_numbers_update[19];
                if (VL82M_axis_buffer[20,0] != 0) LocoButtons.vl82_kontr_16 = b_joystick_axis_numbers_update[20];
                if (VL82M_axis_buffer[21,0] != 0) LocoButtons.vl82_kontr_17 = b_joystick_axis_numbers_update[21];
                if (VL82M_axis_buffer[22,0] != 0) LocoButtons.vl82_kontr_18 = b_joystick_axis_numbers_update[22];
                if (VL82M_axis_buffer[23,0] != 0) LocoButtons.vl82_kontr_19 = b_joystick_axis_numbers_update[23];
                if (VL82M_axis_buffer[24,0] != 0) LocoButtons.vl82_kontr_20 = b_joystick_axis_numbers_update[24];
                if (VL82M_axis_buffer[25,0] != 0) LocoButtons.vl82_kontr_21 = b_joystick_axis_numbers_update[25];
                if (VL82M_axis_buffer[26,0] != 0) LocoButtons.vl82_kontr_22 = b_joystick_axis_numbers_update[26];
                if (VL82M_axis_buffer[27,0] != 0) LocoButtons.vl82_kontr_23 = b_joystick_axis_numbers_update[27];
                if (VL82M_axis_buffer[28,0] != 0) LocoButtons.vl82_kontr_24 = b_joystick_axis_numbers_update[28];
                if (VL82M_axis_buffer[29,0] != 0) LocoButtons.vl82_kontr_25 = b_joystick_axis_numbers_update[29];
                if (VL82M_axis_buffer[30,0] != 0) LocoButtons.vl82_kontr_26 = b_joystick_axis_numbers_update[30];
                if (VL82M_axis_buffer[31,0] != 0) LocoButtons.vl82_kontr_27 = b_joystick_axis_numbers_update[31];
                if (VL82M_axis_buffer[32,0] != 0) LocoButtons.vl82_kontr_28 = b_joystick_axis_numbers_update[32];
                if (VL82M_axis_buffer[33,0] != 0) LocoButtons.vl82_kontr_29 = b_joystick_axis_numbers_update[33];
                if (VL82M_axis_buffer[34,0] != 0) LocoButtons.vl82_kontr_30 = b_joystick_axis_numbers_update[34];
                if (VL82M_axis_buffer[35,0] != 0) LocoButtons.vl82_kontr_31 = b_joystick_axis_numbers_update[35];
                if (VL82M_axis_buffer[36,0] != 0) LocoButtons.vl82_kontr_32 = b_joystick_axis_numbers_update[36];
                if (VL82M_axis_buffer[37,0] != 0) LocoButtons.vl82_kontr_33 = b_joystick_axis_numbers_update[37];
                if (VL82M_axis_buffer[38,0] != 0) LocoButtons.vl82_kontr_34 = b_joystick_axis_numbers_update[38];
                if (VL82M_axis_buffer[39,0] != 0) LocoButtons.vl82_kontr_35 = b_joystick_axis_numbers_update[39];
                if (VL82M_axis_buffer[40,0] != 0) LocoButtons.vl82_kontr_36 = b_joystick_axis_numbers_update[40];
                if (VL82M_axis_buffer[41,0] != 0) LocoButtons.vl82_kontr_37 = b_joystick_axis_numbers_update[41];
                if (VL82M_axis_buffer[42,0] != 0) LocoButtons.vl82_kontr_38 = b_joystick_axis_numbers_update[42];
                if (VL82M_axis_buffer[43,0] != 0) LocoButtons.vl82_kontr_shunt_0 = b_joystick_axis_numbers_update[43];
                if (VL82M_axis_buffer[44,0] != 0) LocoButtons.vl82_kontr_shunt_1 = b_joystick_axis_numbers_update[44];
                if (VL82M_axis_buffer[45,0] != 0) LocoButtons.vl82_kontr_shunt_2 = b_joystick_axis_numbers_update[45];
                if (VL82M_axis_buffer[46,0] != 0) LocoButtons.vl82_kontr_shunt_3 = b_joystick_axis_numbers_update[46];
                if (VL82M_axis_buffer[47,0] != 0) LocoButtons.vl82_kontr_shunt_4 = b_joystick_axis_numbers_update[47];
                if (VL82M_axis_buffer[48,0] != 0) LocoButtons.vl82_kontr_shunt_reostat = b_joystick_axis_numbers_update[48];
                if (VL82M_axis_buffer[49,0] != 0) LocoButtons.vl82_kranTM_0 = b_joystick_axis_numbers_update[49];
                if (VL82M_axis_buffer[50,0] != 0) LocoButtons.vl82_kranTM_1 = b_joystick_axis_numbers_update[50];
                if (VL82M_axis_buffer[51,0] != 0) LocoButtons.vl82_tokopr_obshiy_0 = b_joystick_axis_numbers_update[51];
                if (VL82M_axis_buffer[52,0] != 0) LocoButtons.vl82_tokopr_obshiy_1 = b_joystick_axis_numbers_update[52];
                if (VL82M_axis_buffer[53,0] != 0) LocoButtons.vl82_tokopr_per_0 = b_joystick_axis_numbers_update[53];
                if (VL82M_axis_buffer[54,0] != 0) LocoButtons.vl82_tokopr_per_1 = b_joystick_axis_numbers_update[54];
                if (VL82M_axis_buffer[55,0] != 0) LocoButtons.vl82_tokopr_zad_0 = b_joystick_axis_numbers_update[55];
                if (VL82M_axis_buffer[56,0] != 0) LocoButtons.vl82_tokopr_zad_1 = b_joystick_axis_numbers_update[56];
                if (VL82M_axis_buffer[57,0] != 0) LocoButtons.vl82_gv_0 = b_joystick_axis_numbers_update[57];
                if (VL82M_axis_buffer[58,0] != 0) LocoButtons.vl82_gv_1 = b_joystick_axis_numbers_update[58];
                if (VL82M_axis_buffer[59,0] != 0) LocoButtons.vl82_bv_0 = b_joystick_axis_numbers_update[59];
                if (VL82M_axis_buffer[60,0] != 0) LocoButtons.vl82_bv_1 = b_joystick_axis_numbers_update[60];
                if (VL82M_axis_buffer[61,0] != 0) LocoButtons.vl82_vosst_gv = b_joystick_axis_numbers_update[61];
                if (VL82M_axis_buffer[62,0] != 0) LocoButtons.vl82_komp_0 = b_joystick_axis_numbers_update[62];
                if (VL82M_axis_buffer[63,0] != 0) LocoButtons.vl82_komp_1 = b_joystick_axis_numbers_update[63];
                if (VL82M_axis_buffer[64,0] != 0) LocoButtons.vl82_vent1_0 = b_joystick_axis_numbers_update[64];
                if (VL82M_axis_buffer[65,0] != 0) LocoButtons.vl82_vent1_1 = b_joystick_axis_numbers_update[65];
                if (VL82M_axis_buffer[66,0] != 0) LocoButtons.vl82_vent2_0 = b_joystick_axis_numbers_update[66];
                if (VL82M_axis_buffer[67,0] != 0) LocoButtons.vl82_vent2_1 = b_joystick_axis_numbers_update[67];
                if (VL82M_axis_buffer[68,0] != 0) LocoButtons.vl82_kvc_0 = b_joystick_axis_numbers_update[68];
                if (VL82M_axis_buffer[69,0] != 0) LocoButtons.vl82_kvc_1 = b_joystick_axis_numbers_update[69];
                if (VL82M_axis_buffer[70,0] != 0) LocoButtons.vl82_vozvr_kvc = b_joystick_axis_numbers_update[70];
                if (VL82M_axis_buffer[71,0] != 0) LocoButtons.vl82_upravlenie_0 = b_joystick_axis_numbers_update[71];
                if (VL82M_axis_buffer[72,0] != 0) LocoButtons.vl82_upravlenie_1 = b_joystick_axis_numbers_update[72];
                if (VL82M_axis_buffer[73,0] != 0) LocoButtons.vl82_svet_cab_0 = b_joystick_axis_numbers_update[73];
                if (VL82M_axis_buffer[74,0] != 0) LocoButtons.vl82_svet_cab_1 = b_joystick_axis_numbers_update[74];
                if (VL82M_axis_buffer[75,0] != 0) LocoButtons.vl82_svet_cab_2 = b_joystick_axis_numbers_update[75];
                if (VL82M_axis_buffer[76,0] != 0) LocoButtons.vl82_EPK_0 = b_joystick_axis_numbers_update[76];
                if (VL82M_axis_buffer[77,0] != 0) LocoButtons.vl82_EPK_1 = b_joystick_axis_numbers_update[77];
                if (VL82M_axis_buffer[78,0] != 0) LocoButtons.vl82_prozh_0 = b_joystick_axis_numbers_update[78];
                if (VL82M_axis_buffer[79,0] != 0) LocoButtons.vl82_prozh_1 = b_joystick_axis_numbers_update[79];
                if (VL82M_axis_buffer[80,0] != 0) LocoButtons.vl82_prozh_2 = b_joystick_axis_numbers_update[80];
                if (VL82M_axis_buffer[81,0] != 0) LocoButtons.vl82_sign_0 = b_joystick_axis_numbers_update[81];
                if (VL82M_axis_buffer[82,0] != 0) LocoButtons.vl82_sign_1 = b_joystick_axis_numbers_update[82];
            }

            //проверяем точки vl80t
            if (Loco.sig_loco == 11)
            {
                for (int i = 0; i < 47; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (VL80T_axis_buffer[i, 0] == j)
                        {
                            if (VL80T_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                VL80T_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (VL80T_axis_buffer[0,0] != 0) LocoButtons.vl80t_rev_0 = b_joystick_axis_numbers_update[0];
                if (VL80T_axis_buffer[1,0] != 0) LocoButtons.vl80t_rev_vpered = b_joystick_axis_numbers_update[1];
                if (VL80T_axis_buffer[2,0] != 0) LocoButtons.vl80t_rev_nazad = b_joystick_axis_numbers_update[2];
                if (VL80T_axis_buffer[3,0] != 0) LocoButtons.vl80t_rev_shunt1 = b_joystick_axis_numbers_update[3];
                if (VL80T_axis_buffer[4,0] != 0) LocoButtons.vl80t_rev_shunt2 = b_joystick_axis_numbers_update[4];
                if (VL80T_axis_buffer[5,0] != 0) LocoButtons.vl80t_rev_shunt3 = b_joystick_axis_numbers_update[5];
                if (VL80T_axis_buffer[6,0] != 0) LocoButtons.vl80t_kontr_bv = b_joystick_axis_numbers_update[6];
                if (VL80T_axis_buffer[7,0] != 0) LocoButtons.vl80t_kontr_0 = b_joystick_axis_numbers_update[7];
                if (VL80T_axis_buffer[8,0] != 0) LocoButtons.vl80t_kontr_1 = b_joystick_axis_numbers_update[8];
                if (VL80T_axis_buffer[9,0] != 0) LocoButtons.vl80t_kontr_2 = b_joystick_axis_numbers_update[9];
                if (VL80T_axis_buffer[10,0] != 0) LocoButtons.vl80t_kontr_3 = b_joystick_axis_numbers_update[10];
                if (VL80T_axis_buffer[11,0] != 0) LocoButtons.vl80t_kontr_4 = b_joystick_axis_numbers_update[11];
                if (VL80T_axis_buffer[12,0] != 0) LocoButtons.vl80t_kontr_5 = b_joystick_axis_numbers_update[12];
                if (VL80T_axis_buffer[13,0] != 0) LocoButtons.vl80t_kontr_6 = b_joystick_axis_numbers_update[13];
                if (VL80T_axis_buffer[14,0] != 0) LocoButtons.vl80t_kranTM_0 = b_joystick_axis_numbers_update[14];
                if (VL80T_axis_buffer[15,0] != 0) LocoButtons.vl80t_kranTM_1 = b_joystick_axis_numbers_update[15];
                if (VL80T_axis_buffer[16,0] != 0) LocoButtons.vl80t_tokopr_obshiy_0 = b_joystick_axis_numbers_update[16];
                if (VL80T_axis_buffer[17,0] != 0) LocoButtons.vl80t_tokopr_obshiy_1 = b_joystick_axis_numbers_update[17];
                if (VL80T_axis_buffer[18,0] != 0) LocoButtons.vl80t_tokopr_per_0 = b_joystick_axis_numbers_update[18];
                if (VL80T_axis_buffer[19,0] != 0) LocoButtons.vl80t_tokopr_per_1 = b_joystick_axis_numbers_update[19];
                if (VL80T_axis_buffer[20,0] != 0) LocoButtons.vl80t_tokopr_zad_0 = b_joystick_axis_numbers_update[20];
                if (VL80T_axis_buffer[21,0] != 0) LocoButtons.vl80t_tokopr_zad_1 = b_joystick_axis_numbers_update[21];
                if (VL80T_axis_buffer[22,0] != 0) LocoButtons.vl80t_gv_0 = b_joystick_axis_numbers_update[22];
                if (VL80T_axis_buffer[23,0] != 0) LocoButtons.vl80t_gv_1 = b_joystick_axis_numbers_update[23];
                if (VL80T_axis_buffer[24,0] != 0) LocoButtons.vl80t_vosst_gv = b_joystick_axis_numbers_update[24];
                if (VL80T_axis_buffer[25,0] != 0) LocoButtons.vl80t_komp_0 = b_joystick_axis_numbers_update[25];
                if (VL80T_axis_buffer[26,0] != 0) LocoButtons.vl80t_komp_1 = b_joystick_axis_numbers_update[26];
                if (VL80T_axis_buffer[27,0] != 0) LocoButtons.vl80t_vent1_0 = b_joystick_axis_numbers_update[27];
                if (VL80T_axis_buffer[28,0] != 0) LocoButtons.vl80t_vent1_1 = b_joystick_axis_numbers_update[28];
                if (VL80T_axis_buffer[29,0] != 0) LocoButtons.vl80t_vent2_0 = b_joystick_axis_numbers_update[29];
                if (VL80T_axis_buffer[30,0] != 0) LocoButtons.vl80t_vent2_1 = b_joystick_axis_numbers_update[30];
                if (VL80T_axis_buffer[31,0] != 0) LocoButtons.vl80t_vent3_0 = b_joystick_axis_numbers_update[31];
                if (VL80T_axis_buffer[32,0] != 0) LocoButtons.vl80t_vent3_1 = b_joystick_axis_numbers_update[32];
                if (VL80T_axis_buffer[33,0] != 0) LocoButtons.vl80t_vent4_0 = b_joystick_axis_numbers_update[33];
                if (VL80T_axis_buffer[34,0] != 0) LocoButtons.vl80t_vent4_1 = b_joystick_axis_numbers_update[34];
                if (VL80T_axis_buffer[35,0] != 0) LocoButtons.vl80t_fz_0 = b_joystick_axis_numbers_update[35];
                if (VL80T_axis_buffer[36,0] != 0) LocoButtons.vl80t_fz_1 = b_joystick_axis_numbers_update[36];
                if (VL80T_axis_buffer[37,0] != 0) LocoButtons.vl80t_upravlenie_0 = b_joystick_axis_numbers_update[37];
                if (VL80T_axis_buffer[38,0] != 0) LocoButtons.vl80t_upravlenie_1 = b_joystick_axis_numbers_update[38];
                if (VL80T_axis_buffer[39,0] != 0) LocoButtons.vl80t_svet_cab_0 = b_joystick_axis_numbers_update[39];
                if (VL80T_axis_buffer[40,0] != 0) LocoButtons.vl80t_svet_cab_1 = b_joystick_axis_numbers_update[40];
                if (VL80T_axis_buffer[41,0] != 0) LocoButtons.vl80t_svet_cab_2 = b_joystick_axis_numbers_update[41];
                if (VL80T_axis_buffer[42,0] != 0) LocoButtons.vl80t_EPK_0 = b_joystick_axis_numbers_update[42];
                if (VL80T_axis_buffer[43,0] != 0) LocoButtons.vl80t_EPK_1 = b_joystick_axis_numbers_update[43];
                if (VL80T_axis_buffer[44,0] != 0) LocoButtons.vl80t_prozh_0 = b_joystick_axis_numbers_update[44];
                if (VL80T_axis_buffer[45,0] != 0) LocoButtons.vl80t_prozh_1 = b_joystick_axis_numbers_update[45];
                if (VL80T_axis_buffer[46,0] != 0) LocoButtons.vl80t_prozh_2 = b_joystick_axis_numbers_update[46];
                if (VL80T_axis_buffer[47,0] != 0) LocoButtons.vl80t_sign_0 = b_joystick_axis_numbers_update[47];
                if (VL80T_axis_buffer[48,0] != 0) LocoButtons.vl80t_sign_1 = b_joystick_axis_numbers_update[48];
            }

            //проверяем точки vl85
            if (Loco.sig_loco == 12)
            {
                for (int i = 0; i < 77; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (VL85_axis_buffer[i, 0] == j)
                        {
                            if (VL85_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                VL85_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (VL85_axis_buffer[0,0] != 0) LocoButtons.vl85_rev_0 = b_joystick_axis_numbers_update[0];
                if (VL85_axis_buffer[1,0] != 0) LocoButtons.vl85_rev_vpered = b_joystick_axis_numbers_update[1];
                if (VL85_axis_buffer[2,0] != 0) LocoButtons.vl85_rev_nazad = b_joystick_axis_numbers_update[2];
                if (VL85_axis_buffer[3,0] != 0) LocoButtons.vl85_rev_shunt1 = b_joystick_axis_numbers_update[3];
                if (VL85_axis_buffer[4,0] != 0) LocoButtons.vl85_rev_shunt2 = b_joystick_axis_numbers_update[4];
                if (VL85_axis_buffer[5,0] != 0) LocoButtons.vl85_rev_shunt3 = b_joystick_axis_numbers_update[5];
                if (VL85_axis_buffer[6,0] != 0) LocoButtons.vl85_kontr_bv = b_joystick_axis_numbers_update[6];
                if (VL85_axis_buffer[7,0] != 0) LocoButtons.vl85_kontr_0 = b_joystick_axis_numbers_update[7];
                if (VL85_axis_buffer[8,0] != 0) LocoButtons.vl85_kontr_1 = b_joystick_axis_numbers_update[8];
                if (VL85_axis_buffer[9,0] != 0) LocoButtons.vl85_kontr_2 = b_joystick_axis_numbers_update[9];
                if (VL85_axis_buffer[10,0] != 0) LocoButtons.vl85_kontr_3 = b_joystick_axis_numbers_update[10];
                if (VL85_axis_buffer[11,0] != 0) LocoButtons.vl85_kontr_4 = b_joystick_axis_numbers_update[11];
                if (VL85_axis_buffer[12,0] != 0) LocoButtons.vl85_kontr_5 = b_joystick_axis_numbers_update[12];
                if (VL85_axis_buffer[13,0] != 0) LocoButtons.vl85_kontr_6 = b_joystick_axis_numbers_update[13];
                if (VL85_axis_buffer[14,0] != 0) LocoButtons.vl85_kontr_7 = b_joystick_axis_numbers_update[14];
                if (VL85_axis_buffer[15,0] != 0) LocoButtons.vl85_kontr_8 = b_joystick_axis_numbers_update[15];
                if (VL85_axis_buffer[16,0] != 0) LocoButtons.vl85_kontr_9 = b_joystick_axis_numbers_update[16];
                if (VL85_axis_buffer[17,0] != 0) LocoButtons.vl85_kontr_10 = b_joystick_axis_numbers_update[17];
                if (VL85_axis_buffer[18,0] != 0) LocoButtons.vl85_kontr_11 = b_joystick_axis_numbers_update[18];
                if (VL85_axis_buffer[19,0] != 0) LocoButtons.vl85_kontr_12 = b_joystick_axis_numbers_update[19];
                if (VL85_axis_buffer[20,0] != 0) LocoButtons.vl85_kontr_13 = b_joystick_axis_numbers_update[20];
                if (VL85_axis_buffer[21,0] != 0) LocoButtons.vl85_kontr_14 = b_joystick_axis_numbers_update[21];
                if (VL85_axis_buffer[22,0] != 0) LocoButtons.vl85_kontr_15 = b_joystick_axis_numbers_update[22];
                if (VL85_axis_buffer[23,0] != 0) LocoButtons.vl85_kontr_16 = b_joystick_axis_numbers_update[23];
                if (VL85_axis_buffer[24,0] != 0) LocoButtons.vl85_kontr_17 = b_joystick_axis_numbers_update[24];
                if (VL85_axis_buffer[25,0] != 0) LocoButtons.vl85_kontr_18 = b_joystick_axis_numbers_update[25];
                if (VL85_axis_buffer[26,0] != 0) LocoButtons.vl85_kontr_19 = b_joystick_axis_numbers_update[26];
                if (VL85_axis_buffer[27,0] != 0) LocoButtons.vl85_kontr_20 = b_joystick_axis_numbers_update[27];
                if (VL85_axis_buffer[28,0] != 0) LocoButtons.vl85_kontr_21 = b_joystick_axis_numbers_update[28];
                if (VL85_axis_buffer[29,0] != 0) LocoButtons.vl85_kontr_22 = b_joystick_axis_numbers_update[29];
                if (VL85_axis_buffer[30,0] != 0) LocoButtons.vl85_kontr_23 = b_joystick_axis_numbers_update[30];
                if (VL85_axis_buffer[31,0] != 0) LocoButtons.vl85_kontr_24 = b_joystick_axis_numbers_update[31];
                if (VL85_axis_buffer[32,0] != 0) LocoButtons.vl85_kontr_25 = b_joystick_axis_numbers_update[32];
                if (VL85_axis_buffer[33,0] != 0) LocoButtons.vl85_kontr_26 = b_joystick_axis_numbers_update[33];
                if (VL85_axis_buffer[34,0] != 0) LocoButtons.vl85_kontr_27 = b_joystick_axis_numbers_update[34];
                if (VL85_axis_buffer[35,0] != 0) LocoButtons.vl85_kontr_28 = b_joystick_axis_numbers_update[35];
                if (VL85_axis_buffer[36,0] != 0) LocoButtons.vl85_kontr_29 = b_joystick_axis_numbers_update[36];
                if (VL85_axis_buffer[37,0] != 0) LocoButtons.vl85_kontr_30 = b_joystick_axis_numbers_update[37];
                if (VL85_axis_buffer[38,0] != 0) LocoButtons.vl85_kontr_31 = b_joystick_axis_numbers_update[38];
                if (VL85_axis_buffer[39,0] != 0) LocoButtons.vl85_kontr_32 = b_joystick_axis_numbers_update[39];
                if (VL85_axis_buffer[40,0] != 0) LocoButtons.vl85_kranTM_0 = b_joystick_axis_numbers_update[40];
                if (VL85_axis_buffer[41,0] != 0) LocoButtons.vl85_kranTM_1 = b_joystick_axis_numbers_update[41];
                if (VL85_axis_buffer[42,0] != 0) LocoButtons.vl85_tokopr_obshiy_0 = b_joystick_axis_numbers_update[42];
                if (VL85_axis_buffer[43,0] != 0) LocoButtons.vl85_tokopr_obshiy_1 = b_joystick_axis_numbers_update[43];
                if (VL85_axis_buffer[44,0] != 0) LocoButtons.vl85_tokopr_per_0 = b_joystick_axis_numbers_update[44];
                if (VL85_axis_buffer[45,0] != 0) LocoButtons.vl85_tokopr_per_1 = b_joystick_axis_numbers_update[45];
                if (VL85_axis_buffer[46,0] != 0) LocoButtons.vl85_tokopr_zad_0 = b_joystick_axis_numbers_update[46];
                if (VL85_axis_buffer[47,0] != 0) LocoButtons.vl85_tokopr_zad_1 = b_joystick_axis_numbers_update[47];
                if (VL85_axis_buffer[48,0] != 0) LocoButtons.vl85_gv_0 = b_joystick_axis_numbers_update[48];
                if (VL85_axis_buffer[49,0] != 0) LocoButtons.vl85_gv_1 = b_joystick_axis_numbers_update[49];
                if (VL85_axis_buffer[50,0] != 0) LocoButtons.vl85_vosst_gv = b_joystick_axis_numbers_update[50];
                if (VL85_axis_buffer[51,0] != 0) LocoButtons.vl85_komp_0 = b_joystick_axis_numbers_update[51];
                if (VL85_axis_buffer[52,0] != 0) LocoButtons.vl85_komp_1 = b_joystick_axis_numbers_update[52];
                if (VL85_axis_buffer[53,0] != 0) LocoButtons.vl85_vent1_0 = b_joystick_axis_numbers_update[53];
                if (VL85_axis_buffer[54,0] != 0) LocoButtons.vl85_vent1_1 = b_joystick_axis_numbers_update[54];
                if (VL85_axis_buffer[55,0] != 0) LocoButtons.vl85_vent2_0 = b_joystick_axis_numbers_update[55];
                if (VL85_axis_buffer[56,0] != 0) LocoButtons.vl85_vent2_1 = b_joystick_axis_numbers_update[56];
                if (VL85_axis_buffer[57,0] != 0) LocoButtons.vl85_vent3_0 = b_joystick_axis_numbers_update[57];
                if (VL85_axis_buffer[58,0] != 0) LocoButtons.vl85_vent3_2 = b_joystick_axis_numbers_update[58];
                if (VL85_axis_buffer[59,0] != 0) LocoButtons.vl85_vent4_0 = b_joystick_axis_numbers_update[59];
                if (VL85_axis_buffer[60,0] != 0) LocoButtons.vl85_vent4_1 = b_joystick_axis_numbers_update[60];
                if (VL85_axis_buffer[61,0] != 0) LocoButtons.vl85_fz_0 = b_joystick_axis_numbers_update[61];
                if (VL85_axis_buffer[62,0] != 0) LocoButtons.vl85_fz_1 = b_joystick_axis_numbers_update[62];
                if (VL85_axis_buffer[63,0] != 0) LocoButtons.vl85_svet_cab_0 = b_joystick_axis_numbers_update[63];
                if (VL85_axis_buffer[64,0] != 0) LocoButtons.vl85_svet_cab_1 = b_joystick_axis_numbers_update[64];
                if (VL85_axis_buffer[65,0] != 0) LocoButtons.vl85_svet_cab_2 = b_joystick_axis_numbers_update[65];
                if (VL85_axis_buffer[66,0] != 0) LocoButtons.vl85_EPK_0 = b_joystick_axis_numbers_update[66];
                if (VL85_axis_buffer[67,0] != 0) LocoButtons.vl85_EPK_1 = b_joystick_axis_numbers_update[67];
                if (VL85_axis_buffer[68,0] != 0) LocoButtons.vl85_prozh_0 = b_joystick_axis_numbers_update[68];
                if (VL85_axis_buffer[69,0] != 0) LocoButtons.vl85_prozh_1 = b_joystick_axis_numbers_update[69];
                if (VL85_axis_buffer[70,0] != 0) LocoButtons.vl85_prozh_2 = b_joystick_axis_numbers_update[70];
                if (VL85_axis_buffer[71,0] != 0) LocoButtons.vl85_sign_0 = b_joystick_axis_numbers_update[71];
                if (VL85_axis_buffer[72,0] != 0) LocoButtons.vl85_sign_1 = b_joystick_axis_numbers_update[72];
                if (VL85_axis_buffer[73,0] != 0) LocoButtons.vl85_sign1_0 = b_joystick_axis_numbers_update[73];
                if (VL85_axis_buffer[74,0] != 0) LocoButtons.vl85_sign1_1 = b_joystick_axis_numbers_update[74];
                if (VL85_axis_buffer[75,0] != 0) LocoButtons.vl85_sign2_0 = b_joystick_axis_numbers_update[75];
                if (VL85_axis_buffer[76,0] != 0) LocoButtons.vl85_sign2_1 = b_joystick_axis_numbers_update[76];
            }

            //проверяем точки tep70
            if (Loco.sig_loco == 13)
            {
                for (int i = 0; i < 36; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (TEP70_axis_buffer[i, 0] == j)
                        {
                            if (TEP70_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                TEP70_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (TEP70_axis_buffer[0,0] != 0) LocoButtons.tep70_rev_0 = b_joystick_axis_numbers_update[0];
                if (TEP70_axis_buffer[1,0] != 0) LocoButtons.tep70_rev_vpered = b_joystick_axis_numbers_update[1];
                if (TEP70_axis_buffer[2,0] != 0) LocoButtons.tep70_rev_nazad = b_joystick_axis_numbers_update[2];
                if (TEP70_axis_buffer[3,0] != 0) LocoButtons.tep70_kontr_0 = b_joystick_axis_numbers_update[3];
                if (TEP70_axis_buffer[4,0] != 0) LocoButtons.tep70_kontr_1 = b_joystick_axis_numbers_update[4];
                if (TEP70_axis_buffer[5,0] != 0) LocoButtons.tep70_kontr_2 = b_joystick_axis_numbers_update[5];
                if (TEP70_axis_buffer[6,0] != 0) LocoButtons.tep70_kontr_3 = b_joystick_axis_numbers_update[6];
                if (TEP70_axis_buffer[7,0] != 0) LocoButtons.tep70_kontr_4 = b_joystick_axis_numbers_update[7];
                if (TEP70_axis_buffer[8,0] != 0) LocoButtons.tep70_kontr_5 = b_joystick_axis_numbers_update[8];
                if (TEP70_axis_buffer[9,0] != 0) LocoButtons.tep70_kontr_6 = b_joystick_axis_numbers_update[9];
                if (TEP70_axis_buffer[10,0] != 0) LocoButtons.tep70_kontr_7 = b_joystick_axis_numbers_update[10];
                if (TEP70_axis_buffer[11,0] != 0) LocoButtons.tep70_kontr_8 = b_joystick_axis_numbers_update[11];
                if (TEP70_axis_buffer[12,0] != 0) LocoButtons.tep70_kontr_9 = b_joystick_axis_numbers_update[12];
                if (TEP70_axis_buffer[13,0] != 0) LocoButtons.tep70_kontr_10 = b_joystick_axis_numbers_update[13];
                if (TEP70_axis_buffer[14,0] != 0) LocoButtons.tep70_kontr_11 = b_joystick_axis_numbers_update[14];
                if (TEP70_axis_buffer[15,0] != 0) LocoButtons.tep70_kontr_12 = b_joystick_axis_numbers_update[15];
                if (TEP70_axis_buffer[16,0] != 0) LocoButtons.tep70_kontr_13 = b_joystick_axis_numbers_update[16];
                if (TEP70_axis_buffer[17,0] != 0) LocoButtons.tep70_kontr_14 = b_joystick_axis_numbers_update[17];
                if (TEP70_axis_buffer[18,0] != 0) LocoButtons.tep70_kontr_15 = b_joystick_axis_numbers_update[18];
                if (TEP70_axis_buffer[19,0] != 0) LocoButtons.tep70_kranTM_0 = b_joystick_axis_numbers_update[19];
                if (TEP70_axis_buffer[20,0] != 0) LocoButtons.tep70_kranTM_1 = b_joystick_axis_numbers_update[20];
                if (TEP70_axis_buffer[21,0] != 0) LocoButtons.tep70_nasos_0 = b_joystick_axis_numbers_update[21];
                if (TEP70_axis_buffer[22,0] != 0) LocoButtons.tep70_nasos_1 = b_joystick_axis_numbers_update[22];
                if (TEP70_axis_buffer[23,0] != 0) LocoButtons.tep70_pusk = b_joystick_axis_numbers_update[23];
                if (TEP70_axis_buffer[24,0] != 0) LocoButtons.tep70_upravlenie_0 = b_joystick_axis_numbers_update[24];
                if (TEP70_axis_buffer[25,0] != 0) LocoButtons.tep70_upravlenie_1 = b_joystick_axis_numbers_update[25];
                if (TEP70_axis_buffer[26,0] != 0) LocoButtons.tep70_svet_cab_0 = b_joystick_axis_numbers_update[26];
                if (TEP70_axis_buffer[27,0] != 0) LocoButtons.tep70_svet_cab_1 = b_joystick_axis_numbers_update[27];
                if (TEP70_axis_buffer[28,0] != 0) LocoButtons.tep70_svet_cab_2 = b_joystick_axis_numbers_update[28];
                if (TEP70_axis_buffer[29,0] != 0) LocoButtons.tep70_EPK_0 = b_joystick_axis_numbers_update[29];
                if (TEP70_axis_buffer[30,0] != 0) LocoButtons.tep70_EPK_1 = b_joystick_axis_numbers_update[30];
                if (TEP70_axis_buffer[31,0] != 0) LocoButtons.tep70_EPT_0 = b_joystick_axis_numbers_update[31];
                if (TEP70_axis_buffer[32,0] != 0) LocoButtons.tep70_EPT_1 = b_joystick_axis_numbers_update[32];
                if (TEP70_axis_buffer[33,0] != 0) LocoButtons.tep70_prozh_0 = b_joystick_axis_numbers_update[33];
                if (TEP70_axis_buffer[34,0] != 0) LocoButtons.tep70_prozh_1 = b_joystick_axis_numbers_update[34];
                if (TEP70_axis_buffer[35, 0] != 0) LocoButtons.tep70_prozh_2 = b_joystick_axis_numbers_update[35];
            }

            //проверяем точки te10u
            if (Loco.sig_loco == 14)
            {
                for (int i = 0; i < 47; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (TE10U_axis_buffer[i, 0] == j)
                        {
                            if (TE10U_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                TE10U_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (TE10U_axis_buffer[0,0] != 0) LocoButtons.te10u_rev_0 = b_joystick_axis_numbers_update[0];
                if (TE10U_axis_buffer[1,0] != 0) LocoButtons.te10u_rev_vpered = b_joystick_axis_numbers_update[1];
                if (TE10U_axis_buffer[2,0] != 0) LocoButtons.te10u_rev_nazad = b_joystick_axis_numbers_update[2];
                if (TE10U_axis_buffer[3,0] != 0) LocoButtons.te10u_kontr_0 = b_joystick_axis_numbers_update[3];
                if (TE10U_axis_buffer[4,0] != 0) LocoButtons.te10u_kontr_1 = b_joystick_axis_numbers_update[4];
                if (TE10U_axis_buffer[5,0] != 0) LocoButtons.te10u_kontr_2 = b_joystick_axis_numbers_update[5];
                if (TE10U_axis_buffer[6,0] != 0) LocoButtons.te10u_kontr_3 = b_joystick_axis_numbers_update[6];
                if (TE10U_axis_buffer[7,0] != 0) LocoButtons.te10u_kontr_4 = b_joystick_axis_numbers_update[7];
                if (TE10U_axis_buffer[8,0] != 0) LocoButtons.te10u_kontr_5 = b_joystick_axis_numbers_update[8];
                if (TE10U_axis_buffer[9,0] != 0) LocoButtons.te10u_kontr_6 = b_joystick_axis_numbers_update[9];
                if (TE10U_axis_buffer[10,0] != 0) LocoButtons.te10u_kontr_7 = b_joystick_axis_numbers_update[10];
                if (TE10U_axis_buffer[11,0] != 0) LocoButtons.te10u_kontr_8 = b_joystick_axis_numbers_update[11];
                if (TE10U_axis_buffer[12,0] != 0) LocoButtons.te10u_kontr_9 = b_joystick_axis_numbers_update[12];
                if (TE10U_axis_buffer[13,0] != 0) LocoButtons.te10u_kontr_10 = b_joystick_axis_numbers_update[13];
                if (TE10U_axis_buffer[14,0] != 0) LocoButtons.te10u_kontr_11 = b_joystick_axis_numbers_update[14];
                if (TE10U_axis_buffer[15,0] != 0) LocoButtons.te10u_kontr_12 = b_joystick_axis_numbers_update[15];
                if (TE10U_axis_buffer[16,0] != 0) LocoButtons.te10u_kontr_13 = b_joystick_axis_numbers_update[16];
                if (TE10U_axis_buffer[17,0] != 0) LocoButtons.te10u_kontr_14 = b_joystick_axis_numbers_update[17];
                if (TE10U_axis_buffer[18,0] != 0) LocoButtons.te10u_kontr_15 = b_joystick_axis_numbers_update[18];
                if (TE10U_axis_buffer[19,0] != 0) LocoButtons.te10u_kranTM_0 = b_joystick_axis_numbers_update[19];
                if (TE10U_axis_buffer[20,0] != 0) LocoButtons.te10u_kranTM_1 = b_joystick_axis_numbers_update[20];
                if (TE10U_axis_buffer[21,0] != 0) LocoButtons.te10u_nasos1_0 = b_joystick_axis_numbers_update[21];
                if (TE10U_axis_buffer[22,0] != 0) LocoButtons.te10u_nasos1_1 = b_joystick_axis_numbers_update[22];
                if (TE10U_axis_buffer[23,0] != 0) LocoButtons.te10u_nasos2_0 = b_joystick_axis_numbers_update[23];
                if (TE10U_axis_buffer[24,0] != 0) LocoButtons.te10u_nasos2_1 = b_joystick_axis_numbers_update[24];
                if (TE10U_axis_buffer[25,0] != 0) LocoButtons.te10u_pusk1 = b_joystick_axis_numbers_update[25];
                if (TE10U_axis_buffer[26,0] != 0) LocoButtons.te10u_pusk2 = b_joystick_axis_numbers_update[26];
                if (TE10U_axis_buffer[27,0] != 0) LocoButtons.te10u_upravlenie_0 = b_joystick_axis_numbers_update[27];
                if (TE10U_axis_buffer[28,0] != 0) LocoButtons.te10u_upravlenie_1 = b_joystick_axis_numbers_update[28];
                if (TE10U_axis_buffer[29,0] != 0) LocoButtons.te10u_dvizhenie_0 = b_joystick_axis_numbers_update[29];
                if (TE10U_axis_buffer[30,0] != 0) LocoButtons.te10u_dvizhenie_1 = b_joystick_axis_numbers_update[30];
                if (TE10U_axis_buffer[31,0] != 0) LocoButtons.te10u_perehody_0 = b_joystick_axis_numbers_update[31];
                if (TE10U_axis_buffer[32,0] != 0) LocoButtons.te10u_perehody_1 = b_joystick_axis_numbers_update[32];
                if (TE10U_axis_buffer[33,0] != 0) LocoButtons.te10u_holost1_0 = b_joystick_axis_numbers_update[33];
                if (TE10U_axis_buffer[34,0] != 0) LocoButtons.te10u_holost1_1 = b_joystick_axis_numbers_update[34];
                if (TE10U_axis_buffer[35,0] != 0) LocoButtons.te10u_holost2_0 = b_joystick_axis_numbers_update[35];
                if (TE10U_axis_buffer[36,0] != 0) LocoButtons.te10u_holost2_1 = b_joystick_axis_numbers_update[36];
                if (TE10U_axis_buffer[37,0] != 0) LocoButtons.te10u_svet_cab_0 = b_joystick_axis_numbers_update[37];
                if (TE10U_axis_buffer[38,0] != 0) LocoButtons.te10u_svet_cab_1 = b_joystick_axis_numbers_update[38];
                if (TE10U_axis_buffer[39,0] != 0) LocoButtons.te10u_svet_cab_2 = b_joystick_axis_numbers_update[39];
                if (TE10U_axis_buffer[40,0] != 0) LocoButtons.te10u_EPK_0 = b_joystick_axis_numbers_update[40];
                if (TE10U_axis_buffer[41,0] != 0) LocoButtons.te10u_EPK_1 = b_joystick_axis_numbers_update[41];
                if (TE10U_axis_buffer[42,0] != 0) LocoButtons.te10u_EPT_0 = b_joystick_axis_numbers_update[42];
                if (TE10U_axis_buffer[43,0] != 0) LocoButtons.te10u_EPT_1 = b_joystick_axis_numbers_update[43];
                if (TE10U_axis_buffer[44,0] != 0) LocoButtons.te10u_prozh_0 = b_joystick_axis_numbers_update[44];
                if (TE10U_axis_buffer[45,0] != 0) LocoButtons.te10u_prozh_1 = b_joystick_axis_numbers_update[45];
                if (TE10U_axis_buffer[46, 0] != 0) LocoButtons.te10u_prozh_2 = b_joystick_axis_numbers_update[46];
            }

            //проверяем точки m62
            if (Loco.sig_loco == 15)
            {
                for (int i = 0; i < 36; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (M62_axis_buffer[i, 0] == j)
                        {
                            if (M62_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                M62_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (M62_axis_buffer[0,0] != 0) LocoButtons.m62_rev_0 = b_joystick_axis_numbers_update[0];
                if (M62_axis_buffer[1,0] != 0) LocoButtons.m62_rev_vpered = b_joystick_axis_numbers_update[1];
                if (M62_axis_buffer[2,0] != 0) LocoButtons.m62_rev_nazad = b_joystick_axis_numbers_update[2];
                if (M62_axis_buffer[3,0] != 0) LocoButtons.m62_kontr_0 = b_joystick_axis_numbers_update[3];
                if (M62_axis_buffer[4,0] != 0) LocoButtons.m62_kontr_1 = b_joystick_axis_numbers_update[4];
                if (M62_axis_buffer[5,0] != 0) LocoButtons.m62_kontr_2 = b_joystick_axis_numbers_update[5];
                if (M62_axis_buffer[6,0] != 0) LocoButtons.m62_kontr_3 = b_joystick_axis_numbers_update[6];
                if (M62_axis_buffer[7,0] != 0) LocoButtons.m62_kontr_4 = b_joystick_axis_numbers_update[7];
                if (M62_axis_buffer[8,0] != 0) LocoButtons.m62_kontr_5 = b_joystick_axis_numbers_update[8];
                if (M62_axis_buffer[9,0] != 0) LocoButtons.m62_kontr_6 = b_joystick_axis_numbers_update[9];
                if (M62_axis_buffer[10,0] != 0) LocoButtons.m62_kontr_7 = b_joystick_axis_numbers_update[10];
                if (M62_axis_buffer[11,0] != 0) LocoButtons.m62_kontr_8 = b_joystick_axis_numbers_update[11];
                if (M62_axis_buffer[12,0] != 0) LocoButtons.m62_kontr_9 = b_joystick_axis_numbers_update[12];
                if (M62_axis_buffer[13,0] != 0) LocoButtons.m62_kontr_10 = b_joystick_axis_numbers_update[13];
                if (M62_axis_buffer[14,0] != 0) LocoButtons.m62_kontr_11 = b_joystick_axis_numbers_update[14];
                if (M62_axis_buffer[15,0] != 0) LocoButtons.m62_kontr_12 = b_joystick_axis_numbers_update[15];
                if (M62_axis_buffer[16,0] != 0) LocoButtons.m62_kontr_13 = b_joystick_axis_numbers_update[16];
                if (M62_axis_buffer[17,0] != 0) LocoButtons.m62_kontr_14 = b_joystick_axis_numbers_update[17];
                if (M62_axis_buffer[18,0] != 0) LocoButtons.m62_kontr_15 = b_joystick_axis_numbers_update[18];
                if (M62_axis_buffer[19,0] != 0) LocoButtons.m62_kranTM_0 = b_joystick_axis_numbers_update[19];
                if (M62_axis_buffer[20,0] != 0) LocoButtons.m62_kranTM_1 = b_joystick_axis_numbers_update[20];
                if (M62_axis_buffer[21,0] != 0) LocoButtons.m62_nasos_0 = b_joystick_axis_numbers_update[21];
                if (M62_axis_buffer[22,0] != 0) LocoButtons.m62_nasos_1 = b_joystick_axis_numbers_update[22];
                if (M62_axis_buffer[23,0] != 0) LocoButtons.m62_pusk = b_joystick_axis_numbers_update[23];
                if (M62_axis_buffer[24,0] != 0) LocoButtons.m62_upravlenie_0 = b_joystick_axis_numbers_update[24];
                if (M62_axis_buffer[25,0] != 0) LocoButtons.m62_upravlenie_0 = b_joystick_axis_numbers_update[25];
                if (M62_axis_buffer[26,0] != 0) LocoButtons.m62_perehody_0 = b_joystick_axis_numbers_update[26];
                if (M62_axis_buffer[27,0] != 0) LocoButtons.m62_perehody_1 = b_joystick_axis_numbers_update[27];
                if (M62_axis_buffer[28,0] != 0) LocoButtons.m62_svet_cab_0 = b_joystick_axis_numbers_update[28];
                if (M62_axis_buffer[29,0] != 0) LocoButtons.m62_svet_cab_1 = b_joystick_axis_numbers_update[29];
                if (M62_axis_buffer[30,0] != 0) LocoButtons.m62_svet_cab_2 = b_joystick_axis_numbers_update[30];
                if (M62_axis_buffer[31,0] != 0) LocoButtons.m62_EPK_0 = b_joystick_axis_numbers_update[31];
                if (M62_axis_buffer[32,0] != 0) LocoButtons.m62_EPK_1 = b_joystick_axis_numbers_update[32];
                if (M62_axis_buffer[33,0] != 0) LocoButtons.m62_prozh_0 = b_joystick_axis_numbers_update[33];
                if (M62_axis_buffer[34,0] != 0) LocoButtons.m62_prozh_1 = b_joystick_axis_numbers_update[34];
                if (M62_axis_buffer[35, 0] != 0) LocoButtons.m62_prozh_2 = b_joystick_axis_numbers_update[35];
            }

            //проверяем точки ed4m
            if (Loco.sig_loco == 16)
            {
                for (int i = 0; i < 33; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (ED4M_axis_buffer[i, 0] == j)
                        {
                            if (ED4M_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                ED4M_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (ED4M_axis_buffer[0,0] != 0) LocoButtons.ed4m_rev_0 = b_joystick_axis_numbers_update[0];
                if (ED4M_axis_buffer[1,0] != 0) LocoButtons.ed4m_rev_vpered = b_joystick_axis_numbers_update[1];
                if (ED4M_axis_buffer[2,0] != 0) LocoButtons.ed4m_rev_nazad = b_joystick_axis_numbers_update[2];
                if (ED4M_axis_buffer[3,0] != 0) LocoButtons.ed4m_kontr_0 = b_joystick_axis_numbers_update[3];
                if (ED4M_axis_buffer[4,0] != 0) LocoButtons.ed4m_kontr_h1 = b_joystick_axis_numbers_update[4];
                if (ED4M_axis_buffer[5,0] != 0) LocoButtons.ed4m_kontr_h2 = b_joystick_axis_numbers_update[5];
                if (ED4M_axis_buffer[6, 0] != 0) LocoButtons.ed4m_kontr_h3 = b_joystick_axis_numbers_update[6];
                if (ED4M_axis_buffer[7, 0] != 0) LocoButtons.ed4m_kontr_h4 = b_joystick_axis_numbers_update[7];
                if (ED4M_axis_buffer[8, 0] != 0) LocoButtons.ed4m_kontr_h5 = b_joystick_axis_numbers_update[8];
                if (ED4M_axis_buffer[9,0] != 0) LocoButtons.ed4m_kontr_t1 = b_joystick_axis_numbers_update[9];
                if (ED4M_axis_buffer[10,0] != 0) LocoButtons.ed4m_kontr_t2 = b_joystick_axis_numbers_update[10];
                if (ED4M_axis_buffer[11,0] != 0) LocoButtons.ed4m_kontr_t3 = b_joystick_axis_numbers_update[11];
                if (ED4M_axis_buffer[12,0] != 0) LocoButtons.ed4m_kontr_t4 = b_joystick_axis_numbers_update[12];
                if (ED4M_axis_buffer[13,0] != 0) LocoButtons.ed4m_kontr_t5 = b_joystick_axis_numbers_update[13];
                if (ED4M_axis_buffer[14,0] != 0) LocoButtons.ed4m_kranTM_0 = b_joystick_axis_numbers_update[14];
                if (ED4M_axis_buffer[15,0] != 0) LocoButtons.ed4m_kranTM_1 = b_joystick_axis_numbers_update[15];
                if (ED4M_axis_buffer[16,0] != 0) LocoButtons.ed4m_bv_0 = b_joystick_axis_numbers_update[16];
                if (ED4M_axis_buffer[17,0] != 0) LocoButtons.ed4m_bv_1 = b_joystick_axis_numbers_update[17];
                if (ED4M_axis_buffer[18,0] != 0) LocoButtons.ed4m_tokopr_0 = b_joystick_axis_numbers_update[18];
                if (ED4M_axis_buffer[19,0] != 0) LocoButtons.ed4m_tokopr_1 = b_joystick_axis_numbers_update[19];
                if (ED4M_axis_buffer[20,0] != 0) LocoButtons.ed4m_svet_cab_0 = b_joystick_axis_numbers_update[20];
                if (ED4M_axis_buffer[21,0] != 0) LocoButtons.ed4m_svet_cab_1 = b_joystick_axis_numbers_update[21];
                if (ED4M_axis_buffer[22,0] != 0) LocoButtons.ed4m_EPK_0 = b_joystick_axis_numbers_update[22];
                if (ED4M_axis_buffer[23,0] != 0) LocoButtons.ed4m_EPK_1 = b_joystick_axis_numbers_update[23];
                if (ED4M_axis_buffer[24,0] != 0) LocoButtons.ed4m_EPT_0 = b_joystick_axis_numbers_update[24];
                if (ED4M_axis_buffer[25,0] != 0) LocoButtons.ed4m_EPT_1 = b_joystick_axis_numbers_update[25];
                if (ED4M_axis_buffer[26,0] != 0) LocoButtons.ed4m_dvery_lev_0 = b_joystick_axis_numbers_update[26];
                if (ED4M_axis_buffer[27,0] != 0) LocoButtons.ed4m_dvery_lev_1 = b_joystick_axis_numbers_update[27];
                if (ED4M_axis_buffer[28,0] != 0) LocoButtons.ed4m_dvery_pr_0 = b_joystick_axis_numbers_update[28];
                if (ED4M_axis_buffer[29,0] != 0) LocoButtons.ed4m_dvery_pr_1 = b_joystick_axis_numbers_update[29];
                if (ED4M_axis_buffer[30,0] != 0) LocoButtons.ed4m_prozh_0 = b_joystick_axis_numbers_update[30];
                if (ED4M_axis_buffer[31,0] != 0) LocoButtons.ed4m_prozh_1 = b_joystick_axis_numbers_update[31];
                if (ED4M_axis_buffer[32, 0] != 0) LocoButtons.ed4m_prozh_2 = b_joystick_axis_numbers_update[32];
            }

            //проверяем точки ed9m
            if (Loco.sig_loco == 17)
            {
                for (int i = 0; i < 30; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (ED9M_axis_buffer[i, 0] == j)
                        {
                            if (ED9M_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                ED9M_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }
                if (ED9M_axis_buffer[0,0] != 0) LocoButtons.ed9m_rev_0 = b_joystick_axis_numbers_update[0];
                if (ED9M_axis_buffer[1,0] != 0) LocoButtons.ed9m_rev_vpered = b_joystick_axis_numbers_update[1];
                if (ED9M_axis_buffer[2,0] != 0) LocoButtons.ed9m_rev_nazad = b_joystick_axis_numbers_update[2];
                if (ED9M_axis_buffer[3,0] != 0) LocoButtons.ed9m_kontr_0 = b_joystick_axis_numbers_update[3];
                if (ED9M_axis_buffer[4,0] != 0) LocoButtons.ed9m_kontr_h1 = b_joystick_axis_numbers_update[4];
                if (ED9M_axis_buffer[5,0] != 0) LocoButtons.ed9m_kontr_h2 = b_joystick_axis_numbers_update[5];
                if (ED9M_axis_buffer[6,0] != 0) LocoButtons.ed9m_kontr_t1 = b_joystick_axis_numbers_update[6];
                if (ED9M_axis_buffer[7,0] != 0) LocoButtons.ed9m_kontr_t2 = b_joystick_axis_numbers_update[7];
                if (ED9M_axis_buffer[8,0] != 0) LocoButtons.ed9m_kontr_t3 = b_joystick_axis_numbers_update[8];
                if (ED9M_axis_buffer[9,0] != 0) LocoButtons.ed9m_kontr_t4 = b_joystick_axis_numbers_update[9];
                if (ED9M_axis_buffer[10,0] != 0) LocoButtons.ed9m_kontr_t5 = b_joystick_axis_numbers_update[10];
                if (ED9M_axis_buffer[11,0] != 0) LocoButtons.ed9m_kranTM_0 = b_joystick_axis_numbers_update[11];
                if (ED9M_axis_buffer[12,0] != 0) LocoButtons.ed9m_kranTM_1 = b_joystick_axis_numbers_update[12];
                if (ED9M_axis_buffer[13,0] != 0) LocoButtons.ed9m_bv_0 = b_joystick_axis_numbers_update[13];
                if (ED9M_axis_buffer[14,0] != 0) LocoButtons.ed9m_bv_1 = b_joystick_axis_numbers_update[14];
                if (ED9M_axis_buffer[15,0] != 0) LocoButtons.ed9m_tokopr_0 = b_joystick_axis_numbers_update[15];
                if (ED9M_axis_buffer[16,0] != 0) LocoButtons.ed9m_tokopr_1 = b_joystick_axis_numbers_update[16];
                if (ED9M_axis_buffer[17,0] != 0) LocoButtons.ed9m_svet_cab_0 = b_joystick_axis_numbers_update[17];
                if (ED9M_axis_buffer[18,0] != 0) LocoButtons.ed9m_svet_cab_1 = b_joystick_axis_numbers_update[18];
                if (ED9M_axis_buffer[19,0] != 0) LocoButtons.ed9m_EPK_0 = b_joystick_axis_numbers_update[19];
                if (ED9M_axis_buffer[20,0] != 0) LocoButtons.ed9m_EPK_1 = b_joystick_axis_numbers_update[20];
                if (ED9M_axis_buffer[21,0] != 0) LocoButtons.ed9m_EPT_0 = b_joystick_axis_numbers_update[21];
                if (ED9M_axis_buffer[22,0] != 0) LocoButtons.ed9m_EPT_1 = b_joystick_axis_numbers_update[22];
                if (ED9M_axis_buffer[23,0] != 0) LocoButtons.ed9m_dvery_lev_0 = b_joystick_axis_numbers_update[23];
                if (ED9M_axis_buffer[24,0] != 0) LocoButtons.ed9m_dvery_lev_1 = b_joystick_axis_numbers_update[24];
                if (ED9M_axis_buffer[25,0] != 0) LocoButtons.ed9m_dvery_pr_0 = b_joystick_axis_numbers_update[25];
                if (ED9M_axis_buffer[26,0] != 0) LocoButtons.ed9m_dvery_pr_1 = b_joystick_axis_numbers_update[26];
                if (ED9M_axis_buffer[27,0] != 0) LocoButtons.ed9m_prozh_0 = b_joystick_axis_numbers_update[27];
                if (ED9M_axis_buffer[28,0] != 0) LocoButtons.ed9m_prozh_1 = b_joystick_axis_numbers_update[28];
                if (ED9M_axis_buffer[29, 0] != 0) LocoButtons.ed9m_prozh_1 = b_joystick_axis_numbers_update[29];
            }

            //проверяем точки tem18
            if (Loco.sig_loco == 18)
            {
                for (int i = 0; i < 32; i++)
                {
                    for (int j = 1; j <= 28; j++)
                    {
                        if (tem18_axis_buffer[i, 0] == j)
                        {
                            if (tem18_axis_buffer[i, 1] > joystick_axis_buffer[j - 1] - i_shum_joystick &&
                                tem18_axis_buffer[i, 1] < joystick_axis_buffer[j - 1] + i_shum_joystick)
                            {
                                b_joystick_axis_numbers_update[i] = 1;
                            }
                            else b_joystick_axis_numbers_update[i] = 0;
                        }
                    }
                }

                if (tem18_axis_buffer[0, 0] != 0) LocoButtons.tem18_rev_0 = b_joystick_axis_numbers_update[0];
                if (tem18_axis_buffer[1, 0] != 0) LocoButtons.tem18_rev_vpered = b_joystick_axis_numbers_update[1];
                if (tem18_axis_buffer[2, 0] != 0) LocoButtons.tem18_rev_nazad = b_joystick_axis_numbers_update[2];
                if (tem18_axis_buffer[3, 0] != 0) LocoButtons.tem18_kontr_0 = b_joystick_axis_numbers_update[3];
                if (tem18_axis_buffer[4, 0] != 0) LocoButtons.tem18_kontr_1 = b_joystick_axis_numbers_update[4];
                if (tem18_axis_buffer[5, 0] != 0) LocoButtons.tem18_kontr_2 = b_joystick_axis_numbers_update[5];
                if (tem18_axis_buffer[6, 0] != 0) LocoButtons.tem18_kontr_3 = b_joystick_axis_numbers_update[6];
                if (tem18_axis_buffer[7, 0] != 0) LocoButtons.tem18_kontr_4 = b_joystick_axis_numbers_update[7];
                if (tem18_axis_buffer[8, 0] != 0) LocoButtons.tem18_kontr_5 = b_joystick_axis_numbers_update[8];
                if (tem18_axis_buffer[9, 0] != 0) LocoButtons.tem18_kontr_6 = b_joystick_axis_numbers_update[9];
                if (tem18_axis_buffer[10, 0] != 0) LocoButtons.tem18_kontr_7 = b_joystick_axis_numbers_update[10];
                if (tem18_axis_buffer[11, 0] != 0) LocoButtons.tem18_kontr_8 = b_joystick_axis_numbers_update[11];
                if (tem18_axis_buffer[12, 0] != 0) LocoButtons.tem18_kranTM_0 = b_joystick_axis_numbers_update[12];
                if (tem18_axis_buffer[13, 0] != 0) LocoButtons.tem18_kranTM_1 = b_joystick_axis_numbers_update[13];
                if (tem18_axis_buffer[14, 0] != 0) LocoButtons.tem18_nasos_maslo0 = b_joystick_axis_numbers_update[14];
                if (tem18_axis_buffer[15, 0] != 0) LocoButtons.tem18_nasos_maslo1 = b_joystick_axis_numbers_update[15];
                if (tem18_axis_buffer[16, 0] != 0) LocoButtons.tem18_nasos_toplivo0 = b_joystick_axis_numbers_update[16];
                if (tem18_axis_buffer[17, 0] != 0) LocoButtons.tem18_nasos_toplivo1 = b_joystick_axis_numbers_update[17];
                if (tem18_axis_buffer[18, 0] != 0) LocoButtons.tem18_pusk = b_joystick_axis_numbers_update[18];
                if (tem18_axis_buffer[19, 0] != 0) LocoButtons.tem18_upravlenie_0 = b_joystick_axis_numbers_update[19];
                if (tem18_axis_buffer[20, 0] != 0) LocoButtons.tem18_upravlenie_0 = b_joystick_axis_numbers_update[20];
                if (tem18_axis_buffer[21, 0] != 0) LocoButtons.tem18_perehody_0 = b_joystick_axis_numbers_update[21];
                if (tem18_axis_buffer[22, 0] != 0) LocoButtons.tem18_perehody_1 = b_joystick_axis_numbers_update[22];
                if (tem18_axis_buffer[23, 0] != 0) LocoButtons.tem18_svet_cab_0 = b_joystick_axis_numbers_update[23];
                if (tem18_axis_buffer[24, 0] != 0) LocoButtons.tem18_svet_cab_1 = b_joystick_axis_numbers_update[24];
                if (tem18_axis_buffer[25, 0] != 0) LocoButtons.tem18_svet_prib_0 = b_joystick_axis_numbers_update[25];
                if (tem18_axis_buffer[26, 0] != 0) LocoButtons.tem18_svet_prib_1 = b_joystick_axis_numbers_update[26];
                if (tem18_axis_buffer[27, 0] != 0) LocoButtons.tem18_EPK_0 = b_joystick_axis_numbers_update[27];
                if (tem18_axis_buffer[28, 0] != 0) LocoButtons.tem18_EPK_1 = b_joystick_axis_numbers_update[28];
                if (tem18_axis_buffer[29, 0] != 0) LocoButtons.tem18_prozh_0 = b_joystick_axis_numbers_update[29];
                if (tem18_axis_buffer[30, 0] != 0) LocoButtons.tem18_prozh_1 = b_joystick_axis_numbers_update[30];
                if (tem18_axis_buffer[31, 0] != 0) LocoButtons.tem18_prozh_2 = b_joystick_axis_numbers_update[31];
            }
        }

        //------------------------------------------------------------------------------------
        // Выбираем локомотив и выводим его название в консоль, 
        // если локомотив найден и передаем указатели пневматики и электрики
        //------------------------------------------------------------------------------------
        private void select_loco()
        {
            switch (Loco.sig_loco2)
            {
                case LocoType.Unknown:
                    i_loco_find = 0;
                    Loco.sig_loco = 0;                      // тип локомотива старый формат
                    console1.ForeColor = Color.Red;
                    console1.AppendText("\r\nЛокомотив не найден !");
                    break;

                case LocoType.ES5K:
                    i_loco_find = 1;
                    Loco.sig_loco = 1;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден 2es5k...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.EP1m:
                    i_loco_find = 1;
                    Loco.sig_loco = 2;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден ep1m...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.CHS2K:
                    i_loco_find = 1;
                    Loco.sig_loco = 3;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден chs2k...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.CHS4:
                    i_loco_find = 1;
                    Loco.sig_loco = 4;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден chs4...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.CHS4KVR:
                    i_loco_find = 1;
                    Loco.sig_loco = 5;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден chs4kvr...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.CHS4T:
                    i_loco_find = 1;
                    Loco.sig_loco = 6;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден chs4t...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.CHS7:
                    i_loco_find = 1;
                    Loco.sig_loco = 7;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден chs7...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.CHS8:
                    i_loco_find = 1;
                    Loco.sig_loco = 8;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден chs8...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.VL11m:
                    i_loco_find = 1;
                    Loco.sig_loco = 9;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден vl11m...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.VL82m:
                    i_loco_find = 1;
                    Loco.sig_loco = 10;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден vl82m...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.VL80t:
                    i_loco_find = 1;
                    Loco.sig_loco = 11;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден vl80t...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.VL85:
                    i_loco_find = 1;
                    Loco.sig_loco = 12;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден vl85...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.TEP70:
                    i_loco_find = 1;
                    Loco.sig_loco = 13;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден tep70...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.TE10U:
                    i_loco_find = 1;
                    Loco.sig_loco = 14;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден 2te10u...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.M62:
                    i_loco_find = 1;
                    Loco.sig_loco = 15;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден m62...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.ED4M:
                    i_loco_find = 1;
                    Loco.sig_loco = 16;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден ed4m...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.ED9M:
                    i_loco_find = 1;
                    Loco.sig_loco = 17;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден ed9m...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;

                case LocoType.TEM18:
                    i_loco_find = 1;
                    Loco.sig_loco = 18;                      // тип локомотива старый формат
                    console1.ForeColor = Color.LawnGreen;
                    console1.AppendText("\r\nНайден tem18...");
                    Loco.sig_pos_pnevm = ProcessMemory.find_pnevm();
                    Loco.sig_pos_elektro = ProcessMemory.find_elektro();
                    break;
            }

            if (Loco.sig_loco2 != LocoType.Unknown)
            {
                if (Loco.sig_pos_pnevm != 0)
                {
                    console1.AppendText("\r\nпневматика " + "0x" + Convert.ToString(Loco.sig_pos_pnevm, 16));
                }

                if (Loco.sig_pos_elektro != 0)
                {
                    console1.AppendText("\r\nэлектрика " + "0x" + Convert.ToString(Loco.sig_pos_elektro, 16));
                }

                // название через словарь
                if (LocoSignatures.Names.TryGetValue(Loco.sig_loco2, out string locoName))
                {
                    console1.AppendText("\r\nЛоко код " + Loco.locoCode + $" ({locoName})");
                }
                else
                {
                    console1.AppendText("\r\nЛоко код " + Loco.locoCode);
                }

                console1.AppendText("\r\nТип лока " + Loco.sig_loco2); // выведет имя из enum
            }
        }

        //------------------------------------------------------------------------------------
        //Процедура получения данных из локомотива 
        //------------------------------------------------------------------------------------
        private void ReadLoco()
        {
            switch (Loco.i_process_name)
            {
                case 6: // ZDSimulator V54.006
                    ReadLoco_V54_006();
                break;

                case 7: // ZDSimulator V55.008
                      ReadLoco_V55_008();
                break;

                default:
                        // можно fallback сделать
                break;
            }
        }

        //------------------------------------------------------------------------------------
        // ZDSimulator V54.006
        //------------------------------------------------------------------------------------
        private void ReadLoco_V54_006()
        {
            switch (Loco.sig_loco2)
            {
                case LocoType.ES5K:
                    Readers.LocoRead_V54_006.Read_2ES5K(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.EP1m:
                    Readers.LocoRead_V54_006.Read_EP1M(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.CHS2K:
                    Readers.LocoRead_V54_006.Read_CHS2K(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.CHS4:
                    Readers.LocoRead_V54_006.Read_CHS4(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.CHS4KVR:
                    Readers.LocoRead_V54_006.Read_CHS4KVR(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.CHS4T:
                    Readers.LocoRead_V54_006.Read_CHS4T(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.CHS7:
                    Readers.LocoRead_V54_006.Read_CHS7(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.CHS8:
                    Readers.LocoRead_V54_006.Read_CHS8(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.VL11m:
                    Readers.LocoRead_V54_006.Read_VL11M(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.VL82m:
                    Readers.LocoRead_V54_006.Read_VL82M(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.VL80t:
                    Readers.LocoRead_V54_006.Read_VL80T(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.VL85:
                    Readers.LocoRead_V54_006.Read_VL85(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.TEP70:
                    Readers.LocoRead_V54_006.Read_TEP70(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.TE10U:
                    Readers.LocoRead_V54_006.Read_2TE10U(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.M62:
                    Readers.LocoRead_V54_006.Read_M62(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.ED4M:
                    Readers.LocoRead_V54_006.Read_ED4M(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.ED9M:
                    Readers.LocoRead_V54_006.Read_ED9M(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;

                case LocoType.TEM18:
                    Readers.LocoRead_V54_006.Read_TEM18(Loco.sig_pos_pnevm, Loco.sig_pos_elektro); 
                    break;
            }
        }

        //------------------------------------------------------------------------------------
        // ZDSimulator V55.008
        //------------------------------------------------------------------------------------
        private void ReadLoco_V55_008()
        {
            switch (Loco.sig_loco2)
            {
                case LocoType.ES5K:
                    Readers.LocoRead_V55_008.Read_2ES5K(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.EP1m:
                    Readers.LocoRead_V55_008.Read_EP1M(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.CHS2K:
                    Readers.LocoRead_V55_008.Read_CHS2K(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.CHS4:
                    Readers.LocoRead_V55_008.Read_CHS4(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.CHS4KVR:
                    Readers.LocoRead_V55_008.Read_CHS4KVR(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.CHS4T:
                    Readers.LocoRead_V55_008.Read_CHS4T(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.CHS7:
                    Readers.LocoRead_V55_008.Read_CHS7(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.CHS8:
                    Readers.LocoRead_V55_008.Read_CHS8(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.VL11m:
                    Readers.LocoRead_V55_008.Read_VL11M(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.VL82m:
                    Readers.LocoRead_V55_008.Read_VL82M(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.VL80t:
                    Readers.LocoRead_V55_008.Read_VL80T(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.VL85:
                    Readers.LocoRead_V55_008.Read_VL85(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.TEP70:
                    Readers.LocoRead_V55_008.Read_TEP70(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.TE10U:
                    Readers.LocoRead_V55_008.Read_2TE10U(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.M62:
                    Readers.LocoRead_V55_008.Read_M62(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.ED4M:
                    Readers.LocoRead_V55_008.Read_ED4M(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.ED9M:
                    Readers.LocoRead_V55_008.Read_ED9M(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;

                case LocoType.TEM18:
                    Readers.LocoRead_V55_008.Read_TEM18(Loco.sig_pos_pnevm, Loco.sig_pos_elektro);
                    break;
            }
        }

        //------------------------------------------------------------------------------------
        //Отправка данных в COM порт
        //------------------------------------------------------------------------------------
        private void SendToComPort()
        {
            //признак буфера
            LocoMemoryHelpers.out_buffer[60] = 0xa0;
            LocoMemoryHelpers.out_buffer[61] = 0xb0;
            LocoMemoryHelpers.out_buffer[62] = 0xc0;
            LocoMemoryHelpers.out_buffer[63] = 0xd0;
            port.Write(LocoMemoryHelpers.out_buffer, 0, LocoMemoryHelpers.out_buffer.Length);
        }

        //------------------------------------------------------------------------------------
        //Таймер 1 выбор и чтение локомотива, лампа бдительности, превышение скорости,
        //задержка контроля дверей, отправка данных в com порт 
        //timer1
        //this.timer1.Interval = 1000;
        //this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        //------------------------------------------------------------------------------------
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Loco.i_baze_address_flag_fail == 0)
            {
                ReadLoco();//Вызываем процедуру получения данных из локомотива 

                LocoMemoryHelpers.temp_buffer = BitConverter.GetBytes(i_step_steper_motor);
                Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 57, 2);

                //лампа бдительности
                if (Loco.i_bdit_current == 1)
                {
                    LocoMemoryHelpers.temp_buffer = BitConverter.GetBytes(i_bdit_out);
                    Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 5, 1);
                }
                else
                {
                    LocoMemoryHelpers.temp_buffer = BitConverter.GetBytes(Loco.i_bdit_current);
                    Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 5, 1);
                }

                
                // превышение скорости
                if (i_skor_dop <= (i_skor_tek + 3) &&
                    (Loco.sig_loco == 1 || Loco.sig_loco == 2 || Loco.sig_loco == 3 ||
                     Loco.sig_loco == 6 || Loco.sig_loco == 7 || Loco.sig_loco == 12 ||
                     Loco.sig_loco == 14 || Loco.sig_loco == 16 || Loco.sig_loco == 17 ||
                     Loco.sig_loco == 18))
                {
                    Console.WriteLine($"[DEBUG] Превышение скорости! i_skor_tek={i_skor_tek}, i_skor_dop={i_skor_dop}, loco={Loco.sig_loco}");

                    if (i_skor_dop_out == 1)
                    {
                        LocoMemoryHelpers.temp_buffer = BitConverter.GetBytes(i_skor_dop);
                        Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 0, 2);
                        Console.WriteLine($"[DEBUG] В out_buffer записано ограничение: {i_skor_dop}");
                    }

                    if (i_skor_dop_out == 0)
                    {
                        LocoMemoryHelpers.temp_buffer = new byte[] { 255, 255 };
                        Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 0, 2);
                        Console.WriteLine("[DEBUG] В out_buffer записано FF FF (заглушка)");
                    }
                }
                else
                {
                    LocoMemoryHelpers.temp_buffer = BitConverter.GetBytes(i_skor_dop);
                    Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 0, 2);
                    Console.WriteLine($"[DEBUG] Превышения нет. В out_buffer записано ограничение: {i_skor_dop}");
                }

                



                
                //задержка контроля дверей
                if (Loco.sig_loco == 16 || Loco.sig_loco == 17)
                {
                    if (Loco.i_dvery_current == 1 && i_dvery_close_flag == 0)
                    {
                        if (i_dvery_sec_flag == 1)
                        {
                            i_dvery_sec_flag = 0;
                            i_dvery_sec = 0;
                            i_dvery_random_current = i_dvery_random;
                            timer_dvery_delay.Enabled = true;
                        }

                        if (i_dvery_sec > i_dvery_random_current)
                        {
                            timer_dvery_delay.Enabled = false;
                            i_dvery_sec = 0;
                            i_dvery_sec_flag = 1;
                            i_dvery_close_flag = 1;
                            LocoMemoryHelpers.temp_buffer = new byte[] { 1 };
                            Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 46, 1);
                        }
                        else
                        {
                            LocoMemoryHelpers.temp_buffer = new byte[] { 0 };
                            Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 46, 1);
                        }
                    }

                    if (Loco.i_dvery_current == 0)
                    {
                        i_dvery_close_flag = 0;
                        i_dvery_sec_flag = 1;
                        LocoMemoryHelpers.temp_buffer = new byte[] { 0 };
                        Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 46, 1);
                    }
                }
                


                SendToComPort(); //Отправка данных в COM порт
            }
            else
            {
                button_stop.PerformClick();
            }
        }

        //------------------------------------------------------------------------------------
        //timer2 Для вывода в Демо режиме
        //this.timer2.Interval = 3000;
        //this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
        //------------------------------------------------------------------------------------
        private void timer2_Tick(object sender, EventArgs e)
        {
            // Сначала читаем данные по номеру локомотива
            ReadLoco();//Вызываем процедуру получения данных из локомотива 

            
            // превышение скорости
            if (i_skor_dop <= (i_skor_tek + 3) &&
                (Loco.sig_loco == 1 || Loco.sig_loco == 2 || Loco.sig_loco == 3 ||
                 Loco.sig_loco == 6 || Loco.sig_loco == 7 || Loco.sig_loco == 12 ||
                 Loco.sig_loco == 14 || Loco.sig_loco == 16 || Loco.sig_loco == 17 ||
                 Loco.sig_loco == 18))
            {
                Console.WriteLine($"[DEBUG] Превышение скорости! i_skor_tek={i_skor_tek}, i_skor_dop={i_skor_dop}, loco={Loco.sig_loco}");
                
                //Мигание значения индикатора при привышении скорости
                if (i_skor_dop_out == 1)
                {
                    LocoMemoryHelpers.temp_buffer = BitConverter.GetBytes(i_skor_dop);
                    Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 0, 2);
                    Console.WriteLine($"[DEBUG] В out_buffer записано Текущая скорость: {i_skor_tek}");
                    Console.WriteLine($"[DEBUG] В out_buffer записано Ограничение скорости: {i_skor_dop}");
                }

                if (i_skor_dop_out == 0)
                {
                    LocoMemoryHelpers.temp_buffer = new byte[] { 255, 255 };
                    Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 0, 2);
                    Console.WriteLine("[DEBUG] В out_buffer записано FF FF (заглушка)");
                }
            }
            else
            {
                LocoMemoryHelpers.temp_buffer = BitConverter.GetBytes(i_skor_dop);
                Array.Copy(LocoMemoryHelpers.temp_buffer, 0, LocoMemoryHelpers.out_buffer, 0, 2);
                Console.WriteLine($"[DEBUG] Превышения нет. В out_buffer записано: i_skor_tek ={ i_skor_tek}, i_skor_dop ={ i_skor_dop}");
            }

            

            
            // Теперь разбираем буфер и показываем разные отчёты
            console1.Clear();
            switch (Loco.sig_loco)
            {
                // Вывод отладки для локомотива 2Se5k
                case 1:
                    Display2Se5kInfo(LocoMemoryHelpers.out_buffer);
                    break;


                // Вывод отладки для локомотива ed4m
                case 16:
                   //console1.AppendText($"\r\nСкорость текущая: {Loco.out_buffer[2]}");
                   //console1.AppendText($"\r\nОграничение скорости: {i_skor_dop}");
                    DisplayEd4mInfo(LocoMemoryHelpers.out_buffer);
                    break;

                // Примеры для других типов можно оформить аналогичным образом:
                // case 1:
                //     Display2Se5kInfo(Loco.out_buffer);
                //     break;
                // case 2:
                //     DisplayEp1mInfo(Loco.out_buffer);
                //     break;

                default:
                    console1.AppendText("Для данного типа локомотива отчёт не реализован.\r\n");
                    break;
            }

            console1.AppendText("\r\n\r\n***** Для просмотра разверните экран ******");
        }

        //------------------------------------------------------------------------------------
        //Таймер записи данных полученных из HID джойстика в игру
        //------------------------------------------------------------------------------------
        private void timer_send_HID_Tick(object sender, EventArgs e)
        {
            if (Loco.i_baze_address_flag_fail == 0)
            {
                if (Loco.i_process_name == 6)
                {

                    //LocoWrite.write_controls();
                    //LocoWrite.write_neshtatki();
                    /*
                    if (Loco.sig_loco == 1) LocoWrite.write_2es5k();
                    if (Loco.sig_loco == 2) LocoWrite.write_ep1m();
                    if (Loco.sig_loco == 3) LocoWrite.write_chs2k();
                    if (Loco.sig_loco == 4) LocoWrite.write_chs4();
                    if (Loco.sig_loco == 5) LocoWrite.write_chs4kvr();
                    if (Loco.sig_loco == 6) LocoWrite.write_chs4t();
                    if (Loco.sig_loco == 7) LocoWrite.write_chs7();
                    if (Loco.sig_loco == 8) LocoWrite.write_chs8();
                    if (Loco.sig_loco == 9) LocoWrite.write_vl11();
                    if (Loco.sig_loco == 10) LocoWrite.write_vl82();
                    if (Loco.sig_loco == 11) LocoWrite.write_vl80t();
                    if (Loco.sig_loco == 12) LocoWrite.write_vl85();
                    if (Loco.sig_loco == 13) LocoWrite.write_tep70();
                    if (Loco.sig_loco == 14) LocoWrite.write_2te10u();
                    if (Loco.sig_loco == 15) LocoWrite.write_m62();
                    if (Loco.sig_loco == 16) LocoWrite.write_ed4m();
                    if (Loco.sig_loco == 17) LocoWrite.write_ed9m();
                    if (Loco.sig_loco == 18) LocoWrite.write_tem18();
                    */
                }
            }
            else
            {
                button_stop.PerformClick();
            }
        }

        //------------------------------------------------------------------------------------
        //Вывод в консоль для Демо режима
        //------------------------------------------------------------------------------------
        private void DisplayEd4mInfo(byte[] buffer)
        {
            // Вспомогательная локальная буферная переменная, чтобы не писать повторно
            byte[] tmp = new byte[2];

            // скорость дополнительная (int16) — байты 0–1
            Array.Copy(buffer, 0, tmp, 0, 2);
            console1.AppendText($"\r\nОграничение скорости = {BitConverter.ToInt16(tmp, 0)}");

            // скорость текущая (int16) — байты 2–3
            Array.Copy(buffer, 2, tmp, 0, 2);
            console1.AppendText($"\r\nСкорость текущая = {BitConverter.ToInt16(tmp, 0)}");

            // АЛС — байт 4
            console1.AppendText($"\r\nАЛС = {buffer[4]}");

            // Расстояние до цели в метрах
            console1.AppendText($"\r\nРасстояние до цели в метрах = {Form1.i_rasstoyanie_do_tseli}");

            // бдительность — байт 5
            console1.AppendText($"\r\nБдительность = {buffer[5]}");

            // ток ЭПТ — байты 8–9
            Array.Copy(buffer, 8, tmp, 0, 2);
            console1.AppendText($"\r\nТок эпт = {BitConverter.ToInt16(tmp, 0)}");

            // часы/мин/сек — байты 10,11,12
            console1.AppendText($"\r\nВремя: {buffer[10]}:{buffer[11]}:{buffer[12]}");

            // напряжение КС — байты 13–14
            Array.Copy(buffer, 13, tmp, 0, 2);
            console1.AppendText($"\r\nНапряжение КС = {BitConverter.ToInt16(tmp, 0)}");

            // контроллер — байт 15
            console1.AppendText($"\r\nКонтроллер = {buffer[15]}");

            // манометры НМ, ТМ, УР, ТЦ — байты 29–36
            int[] offsets = { 29, 31, 33, 35 };
            string[] names = { "НМ", "ТМ", "УР", "ТЦ" };

            StringBuilder sb = new StringBuilder();
            sb.Append("Манометры: ");

            for (int i = 0; i < offsets.Length; i++)
            {
                Array.Copy(buffer, offsets[i], tmp, 0, 2);
                int value = BitConverter.ToInt16(tmp, 0);

                sb.Append($"{names[i]}={value}");
                if (i < offsets.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            


            // выводим в console1
            console1.AppendText("\r\n" + sb.ToString());






            /*
            // Лампы ZTE и прочие — просто выводим байты по индексам
            console1.AppendText("\r\n\r\nЛампы:");
            var lampInfo = new (int index, string label)[]
            {
               (37, "ТЦ"), (38, "ЭПТ-О"), (39, "ЭПТ-П"), (40, "ЭПТ-Т"),
               (41, "ПР ZTE"), (42, "ВЦ ZTE"), (43, "EPK_State SIM"),
               (44, "ЛК ZTE"), (47, "двери ZTE"), (48, "О ZTE"),
               (49, "РН ZTE"), (52, "БВ ZTE"), (53, "К ZTE"),
               (54, "РБ Бокс. SIM"), (55, "СОТ ZTE"), (56, "СОТx ZTE")
            };
            */


            // Лампы прочие — просто выводим байты по индексам
            console1.AppendText("\r\n\r\nЛампы:");
            var lampInfo = new (int index, string label)[]
            {
               (37, "ТЦ"), (38, "ЭПТ-О"), (39, "ЭПТ-П"), (40, "ЭПТ-Т"),
               (43, "EPK_State"), 
               (54, "РБ Бокс"), (56, "СОТx ZTE")
            };


            foreach (var (idx, lbl) in lampInfo)
            {
                console1.AppendText($"\r\n{lbl} = {buffer[idx]}");
            }
        }



        private void Display2Se5kInfo(byte[] buffer)
        {
            // Вспомогательная локальная буферная переменная, чтобы не писать повторно
            byte[] tmp = new byte[2];

            // скорость дополнительная (int16) — байты 0–1
            Array.Copy(buffer, 0, tmp, 0, 2);
            console1.AppendText($"\r\nОграничение скорости = {BitConverter.ToInt16(tmp, 0)}");

            // скорость текущая (int16) — байты 2–3
            Array.Copy(buffer, 2, tmp, 0, 2);
            console1.AppendText($"\r\nСкорость текущая = {BitConverter.ToInt16(tmp, 0)}");

            // АЛС — байт 4
            console1.AppendText($"\r\nАЛС = {buffer[4]}");

            // Расстояние до цели в метрах
            console1.AppendText($"\r\nРасстояние до цели в метрах = {Form1.i_rasstoyanie_do_tseli}");

            // бдительность — байт 5
            console1.AppendText($"\r\nБдительность = {buffer[5]}");

            // ток ЭПТ — байты 8–9
            Array.Copy(buffer, 8, tmp, 0, 2);
            console1.AppendText($"\r\nТок эпт = {BitConverter.ToInt16(tmp, 0)}");

            // часы/мин/сек — байты 10,11,12
            console1.AppendText($"\r\nВремя: {buffer[10]}:{buffer[11]}:{buffer[12]}");

            // напряжение КС — байты 13–14
            Array.Copy(buffer, 13, tmp, 0, 2);
            console1.AppendText($"\r\nНапряжение КС = {BitConverter.ToInt16(tmp, 0)}");

            // напряжение ТД — байты 15–16
            Array.Copy(buffer, 13, tmp, 0, 2);
            console1.AppendText($"\r\nНапряжение КС = {BitConverter.ToInt16(tmp, 0)}");

            // Ток1 — байты 17–18
            Array.Copy(buffer, 13, tmp, 0, 2);
            console1.AppendText($"\r\nНапряжение КС = {BitConverter.ToInt16(tmp, 0)}");

            // контроллер — байт 19
            console1.AppendText($"\r\nКонтроллер = {buffer[15]}");

            // манометры НМ, ТМ, УР, ТЦ — байты 29–36
            int[] offsets = { 29, 31, 33, 35 };
            string[] names = { "НМ", "ТМ", "УР", "ТЦ" };

            StringBuilder sb = new StringBuilder();
            sb.Append("Манометры: ");

            for (int i = 0; i < offsets.Length; i++)
            {
                Array.Copy(buffer, offsets[i], tmp, 0, 2);
                int value = BitConverter.ToInt16(tmp, 0);

                sb.Append($"{names[i]}={value}");
                if (i < offsets.Length - 1)
                {
                    sb.Append(", ");
                }
            }


            // выводим в console1
            console1.AppendText("\r\n" + sb.ToString());

            // Лампы прочие — просто выводим байты по индексам
            console1.AppendText("\r\n\r\nЛампы:");
            var lampInfo = new (int index, string label)[]
            {
               (37, "ТЦ"), (38, "ЭПТ-О"), (39, "ЭПТ-П"), (40, "ЭПТ-Т"),
               (43, "EPK_State"),
               (54, "РБ Бокс"), (56, "СОТx ZTE")
            };


            foreach (var (idx, lbl) in lampInfo)
            {
                console1.AppendText($"\r\n{lbl} = {buffer[idx]}");
            }
        }

        //------------------------------------------------------------------------------------
        //Таймер бдительности
        //------------------------------------------------------------------------------------
        private void timer_bdit_Tick(object sender, EventArgs e)
        {
            if (i_bdit_out == 0) i_bdit_out = 1; else i_bdit_out = 0;
        }

        //------------------------------------------------------------------------------------
        //Таймер обновления данных джойстика
        //------------------------------------------------------------------------------------
        private void timer_joystick_update_Tick(object sender, EventArgs e)
        {
            if (device == null)
            {
                Joystick_init();
            }

            if (device == null)
            {
                return;
            }
            else
            {
                UpdateJoystickState();
                UpdateLocoButtons();
                UpdateLocoAxis();
            }
        }
       
        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        private void timer_delay_key_Tick(object sender, EventArgs e)
        {
            if (i_dvery_random > 5) i_dvery_random = 0;
            i_dvery_random++;
            i_delay_send_key++;
        }

        //------------------------------------------------------------------------------------
        //Таймер открытия дверей
        //------------------------------------------------------------------------------------
        private void timer_dvery_delay_Tick_1(object sender, EventArgs e)
        {
            //задержка дверей
            if (i_dvery_sec > 8)
            {
                i_dvery_sec = 0;
            }
            i_dvery_sec++;
        }
        
        //------------------------------------------------------------------------------------
        //Таймер 500мс для мигания ограничения скорости и и лампы ЛК
        //------------------------------------------------------------------------------------
        private void timer_500ms_Tick(object sender, EventArgs e)
        {
            if (i_skor_dop_out == 0)
            {
                i_skor_dop_out = 1;
            }
            else
            {
                i_skor_dop_out = 0;
            }

            i_lampa_LK_sec_flag ++;

            if (i_lampa_LK_sec_flag > 1)
            {
                i_lampa_LK_sec_flag = 0;
                Loco.i_lampa_LK = 0;
            }
            
        }

        //------------------------------------------------------------------------------------
        //Таймер дрожания оборотов дизеля
        //------------------------------------------------------------------------------------
        public static float f_oborot_disel = 0;
        int flag_oborot_disel = 0;
        private void timer_oborot_disel_Tick(object sender, EventArgs e)
        {
            if (flag_oborot_disel == 0)
            {
                flag_oborot_disel = 1;
                f_oborot_disel = 1.03f;
            }
            else
            {
                flag_oborot_disel = 0;
                f_oborot_disel = 0.97f;
            }
        }

        //------------------------------------------------------------------------------------
        //
        //------------------------------------------------------------------------------------
        //удаляем файлы xml и bin из папки zdsimscanner
        public static void DeleteXmlBinFiles()
        {
            string[] files_xml = Directory.GetFiles(i_path_zdsimscanner, @"*.xml");
            string[] files_bin = Directory.GetFiles(i_path_zdsimscanner, @"*.bin");
            string[] files_bin_railworks = null;

            foreach (string fl in files_xml)
            {
                try
                {
                    File.SetAttributes(fl, FileAttributes.Normal);
                    File.Delete(fl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось удалить файлы xml\nиз папки zdsimscanner\nУдалите вручную");
                }
            }

            foreach (string fl in files_bin)
            {
                try
                {
                    File.SetAttributes(fl, FileAttributes.Normal);
                    File.Delete(fl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось удалить файлы bin\nиз папки zdsimscanner\nУдалите вручную");
                }
            }
        }
    }
}