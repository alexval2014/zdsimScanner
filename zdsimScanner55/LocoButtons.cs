namespace zdsimScanner
{
    class LocoButtons
    {
        //Controls 28
        public static byte svistok;
        public static byte tifon;
        public static byte kran395_0; //от 0 до 6
        public static byte kran395_1;
        public static byte kran395_2;
        public static byte kran395_3;
        public static byte kran395_4;
        public static byte kran395_5;
        public static byte kran395_6;
        public static byte kran254_0; //float от от -1, 0-4
        public static byte kran254_1;
        public static byte kran254_2;
        public static byte kran254_3;
        public static byte kran254_4;
        public static byte kran254_5;
        public static byte vid_vlevo;
        public static byte vid_vpravo;//double от -0,5 до +0,5
        public static byte vid_vverh;
        public static byte vid_vniz;//double от -0,5 до +0,5
        public static byte vid_zoom_in;
        public static byte vid_zoom_out;//float от 3.5_3.21875_2.5
        public static byte vid_outside; //0-каб, далее по кол-ву ваг
        public static byte vid_vpered;
        public static byte vid_nazad;//float от 6.2_6.619999886_8
        public static byte protyazhka_lenty;//int по 3000
        public static byte bdit_Z;
        public static byte bdit_M;
        public static byte pesok;
        public static byte dvorniki_0;
        public static byte dvorniki_1;
        public static byte dvorniki_2;
        public static byte dvorniki_3;
        public static byte dvorniki_4;
        public static byte dvorniki_5;

        //нештатки 100
        public static byte[] b_neshtatki = new byte[100];

        //2es5k 109
        public static byte es5k_kontr_0;
        public static byte es5k_kontr_h4;
        public static byte es5k_kontr_h5;
        public static byte es5k_kontr_h6;
        public static byte es5k_kontr_h7;
        public static byte es5k_kontr_h8;
        public static byte es5k_kontr_h9;
        public static byte es5k_kontr_h10;
        public static byte es5k_kontr_h11;
        public static byte es5k_kontr_h12;
        public static byte es5k_kontr_h13;
        public static byte es5k_kontr_h14;
        public static byte es5k_kontr_h15;
        public static byte es5k_kontr_h16;
        public static byte es5k_kontr_h17;
        public static byte es5k_kontr_h18;
        public static byte es5k_kontr_h19;
        public static byte es5k_kontr_h20;
        public static byte es5k_kontr_h21;
        public static byte es5k_kontr_h22;
        public static byte es5k_kontr_h23;
        public static byte es5k_kontr_h24;
        public static byte es5k_kontr_h25;
        public static byte es5k_kontr_h26;
        public static byte es5k_kontr_h27;
        public static byte es5k_kontr_h28;
        public static byte es5k_kontr_h29;
        public static byte es5k_kontr_h30;
        public static byte es5k_kontr_h31;
        public static byte es5k_kontr_h32;
        public static byte es5k_kontr_h33;
        public static byte es5k_kontr_h34;
        public static byte es5k_kontr_h35;
        public static byte es5k_kontr_h36;

        public static byte es5k_kontr_t4;
        public static byte es5k_kontr_t5;
        public static byte es5k_kontr_t6;
        public static byte es5k_kontr_t7;
        public static byte es5k_kontr_t8;
        public static byte es5k_kontr_t9;
        public static byte es5k_kontr_t10;
        public static byte es5k_kontr_t11;
        public static byte es5k_kontr_t12;
        public static byte es5k_kontr_t13;
        public static byte es5k_kontr_t14;
        public static byte es5k_kontr_t15;
        public static byte es5k_kontr_t16;
        public static byte es5k_kontr_t17;
        public static byte es5k_kontr_t18;
        public static byte es5k_kontr_t19;
        public static byte es5k_kontr_t20;
        public static byte es5k_kontr_t21;
        public static byte es5k_kontr_t22;
        public static byte es5k_kontr_t23;
        public static byte es5k_kontr_t24;
        public static byte es5k_kontr_t25;
        public static byte es5k_kontr_t26;
        public static byte es5k_kontr_t27;
        public static byte es5k_kontr_t28;
        public static byte es5k_kontr_t29;
        public static byte es5k_kontr_t30;
        public static byte es5k_kontr_t31;
        public static byte es5k_kontr_t32;
        public static byte es5k_kontr_t33;
        public static byte es5k_kontr_t34;
        public static byte es5k_kontr_t35;
        public static byte es5k_kontr_t36;//float -36 -4 0 4 36

        public static byte es5k_rev_0;//int вп-1 0-0 наз-FFFF
        public static byte es5k_rev_vpered;
        public static byte es5k_rev_nazad;

        public static byte es5k_reg_skor_140;//float 0-140
        public static byte es5k_reg_skor_plus;//по 5км
        public static byte es5k_reg_skor_minus;
        public static byte es5k_kranTM_0;
        public static byte es5k_kranTM_1;
        public static byte es5k_bv_0;
        public static byte es5k_bv_1;
        public static byte es5k_vozvrat_bv;
        public static byte es5k_tokopr_per_0;
        public static byte es5k_tokopr_per_1;
        public static byte es5k_tokopr_zad_0;
        public static byte es5k_tokopr_zad_1;
        public static byte es5k_upravlenie_0;
        public static byte es5k_upravlenie_1;
        public static byte es5k_komp_0;
        public static byte es5k_komp_1;
        public static byte es5k_vent1_0;
        public static byte es5k_vent1_1;
        public static byte es5k_vent2_0;
        public static byte es5k_vent2_1;
        public static byte es5k_MSUD_0;
        public static byte es5k_MSUD_1;
        public static byte es5k_vspom_mash_0;
        public static byte es5k_vspom_mash_1;
        public static byte es5k_svet_cab_0;//0,1
        public static byte es5k_svet_cab_1;
        public static byte es5k_EPK_0;
        public static byte es5k_EPK_1;
        public static byte es5k_sign_0;
        public static byte es5k_sign_1;
        public static byte es5k_signC1_0;
        public static byte es5k_signC1_1;
        public static byte es5k_signC2_0;
        public static byte es5k_signC2_1;
        public static byte es5k_prozh_0;//0,1,2
        public static byte es5k_prozh_1;
        public static byte es5k_prozh_2;
        public static byte es5k_avtoreg_0;
        public static byte es5k_avtoreg_1;

        //ep1m 111
        public static byte ep1m_kontr_0;
        public static byte ep1m_kontr_h4;
        public static byte ep1m_kontr_h5;
        public static byte ep1m_kontr_h6;
        public static byte ep1m_kontr_h7;
        public static byte ep1m_kontr_h8;
        public static byte ep1m_kontr_h9;
        public static byte ep1m_kontr_h10;
        public static byte ep1m_kontr_h11;
        public static byte ep1m_kontr_h12;
        public static byte ep1m_kontr_h13;
        public static byte ep1m_kontr_h14;
        public static byte ep1m_kontr_h15;
        public static byte ep1m_kontr_h16;
        public static byte ep1m_kontr_h17;
        public static byte ep1m_kontr_h18;
        public static byte ep1m_kontr_h19;
        public static byte ep1m_kontr_h20;
        public static byte ep1m_kontr_h21;
        public static byte ep1m_kontr_h22;
        public static byte ep1m_kontr_h23;
        public static byte ep1m_kontr_h24;
        public static byte ep1m_kontr_h25;
        public static byte ep1m_kontr_h26;
        public static byte ep1m_kontr_h27;
        public static byte ep1m_kontr_h28;
        public static byte ep1m_kontr_h29;
        public static byte ep1m_kontr_h30;
        public static byte ep1m_kontr_h31;
        public static byte ep1m_kontr_h32;
        public static byte ep1m_kontr_h33;
        public static byte ep1m_kontr_h34;
        public static byte ep1m_kontr_h35;
        public static byte ep1m_kontr_h36;

        public static byte ep1m_kontr_t4;
        public static byte ep1m_kontr_t5;
        public static byte ep1m_kontr_t6;
        public static byte ep1m_kontr_t7;
        public static byte ep1m_kontr_t8;
        public static byte ep1m_kontr_t9;
        public static byte ep1m_kontr_t10;
        public static byte ep1m_kontr_t11;
        public static byte ep1m_kontr_t12;
        public static byte ep1m_kontr_t13;
        public static byte ep1m_kontr_t14;
        public static byte ep1m_kontr_t15;
        public static byte ep1m_kontr_t16;
        public static byte ep1m_kontr_t17;
        public static byte ep1m_kontr_t18;
        public static byte ep1m_kontr_t19;
        public static byte ep1m_kontr_t20;
        public static byte ep1m_kontr_t21;
        public static byte ep1m_kontr_t22;
        public static byte ep1m_kontr_t23;
        public static byte ep1m_kontr_t24;
        public static byte ep1m_kontr_t25;
        public static byte ep1m_kontr_t26;
        public static byte ep1m_kontr_t27;
        public static byte ep1m_kontr_t28;
        public static byte ep1m_kontr_t29;
        public static byte ep1m_kontr_t30;
        public static byte ep1m_kontr_t31;
        public static byte ep1m_kontr_t32;
        public static byte ep1m_kontr_t33;
        public static byte ep1m_kontr_t34;
        public static byte ep1m_kontr_t35;
        public static byte ep1m_kontr_t36;//float -36 -4 0 4 36

        public static byte ep1m_rev_0;//int вп-1 0-0 наз-FFFF
        public static byte ep1m_rev_vpered;
        public static byte ep1m_rev_nazad;

        public static byte ep1m_reg_skor_160;//float 0-160
        public static byte ep1m_reg_skor_plus;//по 5км
        public static byte ep1m_reg_skor_minus;
        public static byte ep1m_kranTM_0;
        public static byte ep1m_kranTM_1;
        public static byte ep1m_bv_0;
        public static byte ep1m_bv_1;
        public static byte ep1m_vozvrat_zaschity;
        public static byte ep1m_blok_vvk_0;
        public static byte ep1m_blok_vvk_1;
        public static byte ep1m_tokopr_per_0;
        public static byte ep1m_tokopr_per_1;
        public static byte ep1m_tokopr_zad_0;
        public static byte ep1m_tokopr_zad_1;
        public static byte ep1m_upravlenie;
        public static byte ep1m_komp_0;
        public static byte ep1m_komp_1;
        public static byte ep1m_vent1_0;
        public static byte ep1m_vent1_1;
        public static byte ep1m_vent2_0;
        public static byte ep1m_vent2_1;
        public static byte ep1m_vent3_0;
        public static byte ep1m_vent3_1;
        public static byte ep1m_MSUD_0;
        public static byte ep1m_MSUD_1;
        public static byte ep1m_vspom_mash_0;
        public static byte ep1m_vspom_mash_1;
        public static byte ep1m_svet_cab_0;//0,1,2
        public static byte ep1m_svet_cab_1;
        public static byte ep1m_svet_cab_2;
        public static byte ep1m_EPK_0;
        public static byte ep1m_EPK_1;
        public static byte ep1m_EPT_0;
        public static byte ep1m_EPT_1;
        public static byte ep1m_sign_0;
        public static byte ep1m_sign_1;
        public static byte ep1m_prozh_0;//0,1,2
        public static byte ep1m_prozh_1;
        public static byte ep1m_prozh_2;
        public static byte ep1m_avtoreg_0;
        public static byte ep1m_avtoreg_1;

        //chs2k 31
        public static byte chs2k_rev_0;//int вп-1 0-0 наз-FFFF
        public static byte chs2k_rev_vpered;
        public static byte chs2k_rev_nazad;

        public static byte chs2k_kontr_0;
        public static byte chs2k_kontr_plus;
        public static byte chs2k_kontr_minus;
        public static byte chs2k_kontr_plus1;
        public static byte chs2k_kontr_minus1;
        public static byte chs2k_kranTM_0;
        public static byte chs2k_kranTM_1;
        public static byte chs2k_bv_0; //через P
        public static byte chs2k_bv_1;//через Shift P
        public static byte chs2k_tokopr_per_0;
        public static byte chs2k_tokopr_per_1;
        public static byte chs2k_tokopr_zad_0;
        public static byte chs2k_tokopr_zad_1;
        public static byte chs2k_komp1_0;
        public static byte chs2k_komp1_1;
        public static byte chs2k_komp2_0;
        public static byte chs2k_komp2_1;
        public static byte chs2k_vent_0;
        public static byte chs2k_vent_1;
        public static byte chs2k_svet_cab_0;//0,1,2
        public static byte chs2k_svet_cab_1;
        public static byte chs2k_svet_cab_2;
        public static byte chs2k_EPK_0;
        public static byte chs2k_EPK_1;
        public static byte chs2k_EPT_0;
        public static byte chs2k_EPT_1;
        public static byte chs2k_prozh_0;//float 0-1.75
        public static byte chs2k_prozh_1;
        public static byte chs2k_prozh_2;

        //chs4 55
        public static byte chs4_rev_0;//вп-0 0-1 наз-2
        public static byte chs4_rev_vpered;
        public static byte chs4_rev_nazad;

        public static byte chs4_kontr_0; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
        public static byte chs4_kontr_plus;
        public static byte chs4_kontr_minus;
        public static byte chs4_kontr_plus1;
        public static byte chs4_kontr_minus1;
        public static byte chs4_kontr_shunt1;
        public static byte chs4_kontr_shunt2;
        public static byte chs4_kontr_shunt3;
        public static byte chs4_kontr_shunt4;
        public static byte chs4_kontr_shunt5;
        public static byte chs4_kranTM_0;
        public static byte chs4_kranTM_1;
        public static byte chs4_tokopr_per_0;
        public static byte chs4_tokopr_per_1;
        public static byte chs4_tokopr_zad_0;
        public static byte chs4_tokopr_zad_1;
        public static byte chs4_bv_0; //0-0 1-1 восст-2
        public static byte chs4_bv_1;
        public static byte chs4_bv_2;
        public static byte chs4_komp1_0;//0-2
        public static byte chs4_komp1_1;
        public static byte chs4_komp1_2;
        public static byte chs4_komp2_0;
        public static byte chs4_komp2_1;
        public static byte chs4_komp2_2;
        public static byte chs4_vent_0; //0-7
        public static byte chs4_vent_1;
        public static byte chs4_vent_2;
        public static byte chs4_vent_3;
        public static byte chs4_vent_4;
        public static byte chs4_vent_5;
        public static byte chs4_vent_6;
        public static byte chs4_vent_7;
        public static byte chs4_vspom_komp_0;//0,0-1,песок-2,Авто-3
        public static byte chs4_vspom_komp_1;
        public static byte chs4_vspom_komp_2;
        public static byte chs4_vspom_komp_3;
        public static byte chs4_svet_cab_0;//зел-0,приб-1,0-2,общ-3
        public static byte chs4_svet_cab_1;
        public static byte chs4_svet_cab_2;
        public static byte chs4_svet_cab_3;
        public static byte chs4_EPK_0;
        public static byte chs4_EPK_1;
        public static byte chs4_EPT_0;
        public static byte chs4_EPT_1;
        public static byte chs4_avar_nabor_0;
        public static byte chs4_avar_nabor_1;
        public static byte chs4_avar_nabor_2;
        public static byte chs4_avar_nabor_3;
        public static byte chs4_prozh_0; //0,1,2
        public static byte chs4_prozh_1;
        public static byte chs4_prozh_2;

        //chs4kvr 55
        public static byte chs4kvr_rev_0;//вп-0 0-1 наз-2
        public static byte chs4kvr_rev_vpered;
        public static byte chs4kvr_rev_nazad;

        public static byte chs4kvr_kontr_0; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
        public static byte chs4kvr_kontr_plus;
        public static byte chs4kvr_kontr_minus;
        public static byte chs4kvr_kontr_plus1;
        public static byte chs4kvr_kontr_minus1;
        public static byte chs4kvr_kontr_shunt1;
        public static byte chs4kvr_kontr_shunt2;
        public static byte chs4kvr_kontr_shunt3;
        public static byte chs4kvr_kontr_shunt4;
        public static byte chs4kvr_kontr_shunt5;
        public static byte chs4kvr_kranTM_0;
        public static byte chs4kvr_kranTM_1;
        public static byte chs4kvr_tokopr_per_0;
        public static byte chs4kvr_tokopr_per_1;
        public static byte chs4kvr_tokopr_zad_0;
        public static byte chs4kvr_tokopr_zad_1;
        public static byte chs4kvr_bv_0; //0-0 1-1 восст-2
        public static byte chs4kvr_bv_1;
        public static byte chs4kvr_bv_2;
        public static byte chs4kvr_komp1_0;//0Т-0,0-1,А-2,Р-3
        public static byte chs4kvr_komp1_1;
        public static byte chs4kvr_komp1_2;
        public static byte chs4kvr_komp2_0;
        public static byte chs4kvr_komp2_1;
        public static byte chs4kvr_komp2_2;
        public static byte chs4kvr_vent_0; //0-7
        public static byte chs4kvr_vent_1; //0-7
        public static byte chs4kvr_vent_2; //0-7
        public static byte chs4kvr_vent_3; //0-7
        public static byte chs4kvr_vent_4; //0-7
        public static byte chs4kvr_vent_5; //0-7
        public static byte chs4kvr_vent_6; //0-7
        public static byte chs4kvr_vent_7; //0-7
        public static byte chs4kvr_vspom_komp_0;//0,0-1,песок-2,Авто-3
        public static byte chs4kvr_vspom_komp_1;
        public static byte chs4kvr_vspom_komp_2;
        public static byte chs4kvr_vspom_komp_3;
        public static byte chs4kvr_svet_cab_0;//зел-0,приб-1,0-2,общ-3
        public static byte chs4kvr_svet_cab_1;
        public static byte chs4kvr_svet_cab_2;
        public static byte chs4kvr_svet_cab_3;
        public static byte chs4kvr_EPK_0;
        public static byte chs4kvr_EPK_1;
        public static byte chs4kvr_EPT_0;
        public static byte chs4kvr_EPT_1;
        public static byte chs4kvr_avar_nabor_0;
        public static byte chs4kvr_avar_nabor_1;
        public static byte chs4kvr_avar_nabor_2;
        public static byte chs4kvr_avar_nabor_3;
        public static byte chs4kvr_prozh_0; //0,1,2
        public static byte chs4kvr_prozh_1;
        public static byte chs4kvr_prozh_2;

        //chs4t 52
        public static byte chs4t_rev_0;//вп-0 0-1 наз-2
        public static byte chs4t_rev_vpered;
        public static byte chs4t_rev_nazad;

        public static byte chs4t_kontr_0; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
        public static byte chs4t_kontr_plus;
        public static byte chs4t_kontr_minus;
        public static byte chs4t_kontr_plus1;
        public static byte chs4t_kontr_minus1;
        public static byte chs4t_kontr_shunt1;
        public static byte chs4t_kontr_shunt2;
        public static byte chs4t_kontr_shunt3;
        public static byte chs4t_kontr_shunt4;
        public static byte chs4t_kontr_shunt5;
        public static byte chs4t_kranTM_0;
        public static byte chs4t_kranTM_1;
        public static byte chs4t_tokopr_per_0;
        public static byte chs4t_tokopr_per_1;
        public static byte chs4t_tokopr_zad_0;
        public static byte chs4t_tokopr_zad_1;
        public static byte chs4t_bv_0; //0-0 1-1 восст-2
        public static byte chs4t_bv_1;
        public static byte chs4t_bv_2;
        public static byte chs4t_komp1_0;//0Т-0,0-1,А-2,Р-3
        public static byte chs4t_komp1_1;
        public static byte chs4t_komp1_2;
        public static byte chs4t_komp2_0;
        public static byte chs4t_komp2_1;
        public static byte chs4t_komp2_2;
        public static byte chs4t_vent_0; //1,2раб,0-выкл
        public static byte chs4t_vent_1;
        public static byte chs4t_vent_2;
        public static byte chs4t_vspom_komp_0;//0,0-1,песок-2,Авто-3
        public static byte chs4t_vspom_komp_1;
        public static byte chs4t_vspom_komp_2;
        public static byte chs4t_vspom_komp_3;
        public static byte chs4t_svet_cab_0;//зел-0,приб-1,0-2,общ-3
        public static byte chs4t_svet_cab_1;
        public static byte chs4t_svet_cab_2;
        public static byte chs4t_svet_cab_3;
        public static byte chs4t_EPK_0;
        public static byte chs4t_EPK_1;
        public static byte chs4t_EPT_0;
        public static byte chs4t_EPT_1;
        public static byte chs4t_avar_nabor_0;
        public static byte chs4t_avar_nabor_1;
        public static byte chs4t_avar_nabor_2;
        public static byte chs4t_avar_nabor_3;
        public static byte chs4t_prozh_0; //0,1,2
        public static byte chs4t_prozh_1;
        public static byte chs4t_prozh_2;
        public static byte chs4t_zhalyzi_0;
        public static byte chs4t_zhalyzi_1;

        //chs7 45
        public static byte chs7_rev_0;//вп1 0-0 нз255
        public static byte chs7_rev_vpered;
        public static byte chs7_rev_nazad;

        public static byte chs7_kontr_0; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
        public static byte chs7_kontr_plus;
        public static byte chs7_kontr_minus;
        public static byte chs7_kontr_plus1;
        public static byte chs7_kontr_minus1;
        public static byte chs7_kontr_shunt1;
        public static byte chs7_kontr_shunt2;
        public static byte chs7_kontr_shunt3;
        public static byte chs7_kontr_shunt4;
        public static byte chs7_kontr_shunt5;
        public static byte chs7_kranTM_0;
        public static byte chs7_kranTM_1;
        public static byte chs7_tokopr_per_0;//0-2 2через shift I,O
        public static byte chs7_tokopr_per_1;
        public static byte chs7_tokopr_per_2;
        public static byte chs7_tokopr_zad_0;
        public static byte chs7_tokopr_zad_1;
        public static byte chs7_tokopr_zad_2;
        public static byte chs7_bv_0; //0-0 1-1 восст-2 через shift P
        public static byte chs7_bv_1;
        public static byte chs7_bv_2;
        public static byte chs7_komp1_0;//0-0,1А,2Р
        public static byte chs7_komp1_1;
        public static byte chs7_komp1_2;
        public static byte chs7_komp2_0;
        public static byte chs7_komp2_1;
        public static byte chs7_komp2_2;
        public static byte chs7_vent_0; //0выкл 1вкл_пр 255вкл_лев
        public static byte chs7_vent_1;
        public static byte chs7_vent_2;
        public static byte chs7_sbros_SP;
        public static byte chs7_svet_cab_0;//0выкл,приб-1,2общ
        public static byte chs7_svet_cab_1;
        public static byte chs7_svet_cab_2;
        public static byte chs7_EPK_0;
        public static byte chs7_EPK_1;
        public static byte chs7_EPT_0;
        public static byte chs7_EPT_1;
        public static byte chs7_prozh_0;//float 0-1,75
        public static byte chs7_prozh_1;
        public static byte chs7_prozh_2;
        public static byte chs7_zhalyzi1_0;
        public static byte chs7_zhalyzi1_1;

        //chs8 63
        public static byte chs8_rev_0;//вп0 0-1 нз2
        public static byte chs8_rev_vpered;
        public static byte chs8_rev_nazad;

        public static byte chs8_kontr_0; //00 1(+1) 2(+) 255(-1) 254(-) 5-9(шунты)
        public static byte chs8_kontr_plus;
        public static byte chs8_kontr_minus;
        public static byte chs8_kontr_plus1;
        public static byte chs8_kontr_minus1;
        public static byte chs8_kontr_shunt1;
        public static byte chs8_kontr_shunt2;
        public static byte chs8_kontr_shunt3;
        public static byte chs8_kontr_shunt4;
        public static byte chs8_kontr_shunt5;
        public static byte chs8_kranTM_0;
        public static byte chs8_kranTM_1;
        public static byte chs8_tokopr_per_0;//0-1
        public static byte chs8_tokopr_per_1;
        public static byte chs8_tokopr_zad_0;
        public static byte chs8_tokopr_zad_1;
        public static byte chs8_bv_0; //вкл БВ 0-0 1-1 
        public static byte chs8_bv_1;
        public static byte chs8_vosst_bv;//через K
        public static byte chs8_komp1_0;//0выкл,1А,2Р
        public static byte chs8_komp1_1;
        public static byte chs8_komp1_2;
        public static byte chs8_komp2_0;
        public static byte chs8_komp2_1;
        public static byte chs8_komp2_2;
        public static byte chs8_vent1_0; //0выкл,1авто,2-4раб
        public static byte chs8_vent1_1;
        public static byte chs8_vent1_2;
        public static byte chs8_vent1_3;
        public static byte chs8_vent1_4;
        public static byte chs8_vent2_0; //0выкл,1авто,2-4раб
        public static byte chs8_vent2_1;
        public static byte chs8_vent2_2;
        public static byte chs8_vent2_3;
        public static byte chs8_vent2_4;
        public static byte chs8_vspom_komp_0;//0выкл,1песок,2авто,3комп
        public static byte chs8_vspom_komp_1;
        public static byte chs8_vspom_komp_2;
        public static byte chs8_vspom_komp_3;
        public static byte chs8_svet_cab_0;//0зел,1приб,2выкл,3общ,4приб,5зел
        public static byte chs8_svet_cab_1;
        public static byte chs8_svet_cab_2;
        public static byte chs8_svet_cab_3;
        public static byte chs8_svet_cab_4;
        public static byte chs8_svet_cab_5;
        public static byte chs8_EPK_0;
        public static byte chs8_EPK_1;
        public static byte chs8_EPT_0;
        public static byte chs8_EPT_1;
        public static byte chs8_avar_nabor_0;
        public static byte chs8_avar_nabor_1;
        public static byte chs8_avar_nabor_2;
        public static byte chs8_avar_nabor_3;
        public static byte chs8_prozh_0; //0,1,2
        public static byte chs8_prozh_1;
        public static byte chs8_prozh_2;
        public static byte chs8_reost_torm_proverka;//0выкл,1проверка через backspace
        public static byte chs8_reost_torm_0;//0выкл,1середина,2торм через >
        public static byte chs8_reost_torm_1;
        public static byte chs8_reost_torm_2;

        //vl11 82
        public static byte vl11_rev_0;//вп1 0-0 нз255
        public static byte vl11_rev_vpered;
        public static byte vl11_rev_nazad;

        public static byte vl11_kontr_0;
        public static byte vl11_kontr_1;
        public static byte vl11_kontr_2;
        public static byte vl11_kontr_3;
        public static byte vl11_kontr_4;
        public static byte vl11_kontr_5;
        public static byte vl11_kontr_6;
        public static byte vl11_kontr_7;
        public static byte vl11_kontr_8;
        public static byte vl11_kontr_9;
        public static byte vl11_kontr_10;
        public static byte vl11_kontr_11;
        public static byte vl11_kontr_12;
        public static byte vl11_kontr_13;
        public static byte vl11_kontr_14;
        public static byte vl11_kontr_15;
        public static byte vl11_kontr_16;
        public static byte vl11_kontr_17;
        public static byte vl11_kontr_18;
        public static byte vl11_kontr_19;
        public static byte vl11_kontr_20;
        public static byte vl11_kontr_21;
        public static byte vl11_kontr_22;
        public static byte vl11_kontr_23;
        public static byte vl11_kontr_24;
        public static byte vl11_kontr_25;
        public static byte vl11_kontr_26;
        public static byte vl11_kontr_27;
        public static byte vl11_kontr_28;
        public static byte vl11_kontr_29;
        public static byte vl11_kontr_30;
        public static byte vl11_kontr_31;
        public static byte vl11_kontr_32;
        public static byte vl11_kontr_33;
        public static byte vl11_kontr_34;
        public static byte vl11_kontr_35;
        public static byte vl11_kontr_36;
        public static byte vl11_kontr_37;
        public static byte vl11_kontr_38;
        public static byte vl11_kontr_39;
        public static byte vl11_kontr_40;
        public static byte vl11_kontr_41;
        public static byte vl11_kontr_42;
        public static byte vl11_kontr_43;
        public static byte vl11_kontr_44;
        public static byte vl11_kontr_45;
        public static byte vl11_kontr_46;
        public static byte vl11_kontr_47;
        public static byte vl11_kontr_48;

        public static byte vl11_kontr_shunt_0;//0выкл 255-252
        public static byte vl11_kontr_shunt_1;
        public static byte vl11_kontr_shunt_2;
        public static byte vl11_kontr_shunt_3;
        public static byte vl11_kontr_shunt_4;

        public static byte vl11_kranTM_0;
        public static byte vl11_kranTM_1;
        public static byte vl11_tokopr_obshiy_0;
        public static byte vl11_tokopr_obshiy_1;
        public static byte vl11_tokopr_per_0;
        public static byte vl11_tokopr_per_1;
        public static byte vl11_tokopr_zad_0;
        public static byte vl11_tokopr_zad_1;
        public static byte vl11_bv_0; //БВ 0-0 1-1
        public static byte vl11_bv_1;
        public static byte vl11_vosst_bv;//через K
        public static byte vl11_komp_0;//0выкл,1вкл
        public static byte vl11_komp_1;
        public static byte vl11_vent_0; //0выкл,1низ,2выс
        public static byte vl11_vent_1;
        public static byte vl11_vent_2;
        public static byte vl11_svet_cab_0;//0выкл,1приб,2общ
        public static byte vl11_svet_cab_1;
        public static byte vl11_svet_cab_2;
        public static byte vl11_EPK_0;
        public static byte vl11_EPK_1;
        public static byte vl11_prozh_0; //float 0-1,875
        public static byte vl11_prozh_1;
        public static byte vl11_prozh_2;
        public static byte vl11_sign_0;
        public static byte vl11_sign_1;

        //vl82 83
        public static byte vl82_rev_0;//0нз,1-0,2вп
        public static byte vl82_rev_vpered;
        public static byte vl82_rev_nazad;

        public static byte vl82_kontr_bv;//0-38 БВ_255 ,клавD для откл БВ
        public static byte vl82_kontr_0;
        public static byte vl82_kontr_1;
        public static byte vl82_kontr_2;
        public static byte vl82_kontr_3;
        public static byte vl82_kontr_4;
        public static byte vl82_kontr_5;
        public static byte vl82_kontr_6;
        public static byte vl82_kontr_7;
        public static byte vl82_kontr_8;
        public static byte vl82_kontr_9;
        public static byte vl82_kontr_10;
        public static byte vl82_kontr_11;
        public static byte vl82_kontr_12;
        public static byte vl82_kontr_13;
        public static byte vl82_kontr_14;
        public static byte vl82_kontr_15;
        public static byte vl82_kontr_16;
        public static byte vl82_kontr_17;
        public static byte vl82_kontr_18;
        public static byte vl82_kontr_19;
        public static byte vl82_kontr_20;
        public static byte vl82_kontr_21;
        public static byte vl82_kontr_22;
        public static byte vl82_kontr_23;
        public static byte vl82_kontr_24;
        public static byte vl82_kontr_25;
        public static byte vl82_kontr_26;
        public static byte vl82_kontr_27;
        public static byte vl82_kontr_28;
        public static byte vl82_kontr_29;
        public static byte vl82_kontr_30;
        public static byte vl82_kontr_31;
        public static byte vl82_kontr_32;
        public static byte vl82_kontr_33;
        public static byte vl82_kontr_34;
        public static byte vl82_kontr_35;
        public static byte vl82_kontr_36;
        public static byte vl82_kontr_37;
        public static byte vl82_kontr_38;

        public static byte vl82_kontr_shunt_0;//0выкл,1-4шунты,255реостат
        public static byte vl82_kontr_shunt_1;
        public static byte vl82_kontr_shunt_2;
        public static byte vl82_kontr_shunt_3;
        public static byte vl82_kontr_shunt_4;
        public static byte vl82_kontr_shunt_reostat;

        public static byte vl82_kranTM_0;
        public static byte vl82_kranTM_1;
        public static byte vl82_tokopr_obshiy_0;
        public static byte vl82_tokopr_obshiy_1;
        public static byte vl82_tokopr_per_0;
        public static byte vl82_tokopr_per_1;
        public static byte vl82_tokopr_zad_0;
        public static byte vl82_tokopr_zad_1;
        public static byte vl82_gv_0; //ГВ 0-0 1-1
        public static byte vl82_gv_1;
        public static byte vl82_bv_0;
        public static byte vl82_bv_1;
        public static byte vl82_vosst_gv;//через K
        public static byte vl82_komp_0;//0выкл,1вкл
        public static byte vl82_komp_1;
        public static byte vl82_vent1_0;
        public static byte vl82_vent1_1;
        public static byte vl82_vent2_0;
        public static byte vl82_vent2_1;
        public static byte vl82_kvc_0;
        public static byte vl82_kvc_1;
        public static byte vl82_vozvr_kvc;//через Y
        public static byte vl82_upravlenie_0;
        public static byte vl82_upravlenie_1;
        public static byte vl82_svet_cab_0;//0выкл,1приб,2общ
        public static byte vl82_svet_cab_1;
        public static byte vl82_svet_cab_2;
        public static byte vl82_EPK_0;
        public static byte vl82_EPK_1;
        public static byte vl82_prozh_0;//0,1,2
        public static byte vl82_prozh_1;
        public static byte vl82_prozh_2;
        public static byte vl82_sign_0;
        public static byte vl82_sign_1;

        //vl80t 49
        public static byte vl80t_rev_0;//255нз,0-0,1вп,2-4шунты
        public static byte vl80t_rev_vpered;
        public static byte vl80t_rev_nazad;
        public static byte vl80t_rev_shunt1;
        public static byte vl80t_rev_shunt2;
        public static byte vl80t_rev_shunt3;

        public static byte vl80t_kontr_bv;//255выкл_бв,0-0,1ав,2рв,3фв,4фп,5рп,6ап ,клавD для откл ГВ
        public static byte vl80t_kontr_0;
        public static byte vl80t_kontr_1;
        public static byte vl80t_kontr_2;
        public static byte vl80t_kontr_3;
        public static byte vl80t_kontr_4;
        public static byte vl80t_kontr_5;
        public static byte vl80t_kontr_6;//клавA для 6 полож

        public static byte vl80t_kranTM_0;
        public static byte vl80t_kranTM_1;
        public static byte vl80t_tokopr_obshiy_0;
        public static byte vl80t_tokopr_obshiy_1;
        public static byte vl80t_tokopr_per_0;
        public static byte vl80t_tokopr_per_1;
        public static byte vl80t_tokopr_zad_0;
        public static byte vl80t_tokopr_zad_1;
        public static byte vl80t_gv_0; //ГВ 0-0 1-1
        public static byte vl80t_gv_1;
        public static byte vl80t_vosst_gv;//через K
        public static byte vl80t_komp_0;//0выкл,1вкл
        public static byte vl80t_komp_1;
        public static byte vl80t_vent1_0;
        public static byte vl80t_vent1_1;
        public static byte vl80t_vent2_0;
        public static byte vl80t_vent2_1;
        public static byte vl80t_vent3_0;
        public static byte vl80t_vent3_1;
        public static byte vl80t_vent4_0;
        public static byte vl80t_vent4_1;
        public static byte vl80t_fz_0;
        public static byte vl80t_fz_1;
        public static byte vl80t_upravlenie_0;
        public static byte vl80t_upravlenie_1;
        public static byte vl80t_svet_cab_0;//0выкл,1приб,2общ
        public static byte vl80t_svet_cab_1;
        public static byte vl80t_svet_cab_2;
        public static byte vl80t_EPK_0;
        public static byte vl80t_EPK_1;
        public static byte vl80t_prozh_0;//0,1,2
        public static byte vl80t_prozh_1;
        public static byte vl80t_prozh_2;
        public static byte vl80t_sign_0;
        public static byte vl80t_sign_1;

        //vl85 80
        public static byte vl85_rev_0;//255нз,0-0,1вп,2-4шунты
        public static byte vl85_rev_vpered;
        public static byte vl85_rev_nazad;
        public static byte vl85_rev_shunt1;
        public static byte vl85_rev_shunt2;
        public static byte vl85_rev_shunt3;

        public static byte vl85_kontr_bv;//0выкл,255откл.БВ,1-32поз, клав D для откл БВ
        public static byte vl85_kontr_0;
        public static byte vl85_kontr_1;
        public static byte vl85_kontr_2;
        public static byte vl85_kontr_3;
        public static byte vl85_kontr_4;
        public static byte vl85_kontr_5;
        public static byte vl85_kontr_6;
        public static byte vl85_kontr_7;
        public static byte vl85_kontr_8;
        public static byte vl85_kontr_9;
        public static byte vl85_kontr_10;
        public static byte vl85_kontr_11;
        public static byte vl85_kontr_12;
        public static byte vl85_kontr_13;
        public static byte vl85_kontr_14;
        public static byte vl85_kontr_15;
        public static byte vl85_kontr_16;
        public static byte vl85_kontr_17;
        public static byte vl85_kontr_18;
        public static byte vl85_kontr_19;
        public static byte vl85_kontr_20;
        public static byte vl85_kontr_21;
        public static byte vl85_kontr_22;
        public static byte vl85_kontr_23;
        public static byte vl85_kontr_24;
        public static byte vl85_kontr_25;
        public static byte vl85_kontr_26;
        public static byte vl85_kontr_27;
        public static byte vl85_kontr_28;
        public static byte vl85_kontr_29;
        public static byte vl85_kontr_30;
        public static byte vl85_kontr_31;
        public static byte vl85_kontr_32;

        public static byte vl85_kranTM_0;
        public static byte vl85_kranTM_1;
        public static byte vl85_tokopr_obshiy_0;
        public static byte vl85_tokopr_obshiy_1;
        public static byte vl85_tokopr_per_0;
        public static byte vl85_tokopr_per_1;
        public static byte vl85_tokopr_zad_0;
        public static byte vl85_tokopr_zad_1;
        public static byte vl85_gv_0; //ГВ 0-0 1-1
        public static byte vl85_gv_1;
        public static byte vl85_vosst_gv;//через K
        public static byte vl85_avtoreg_140;//public static byte 0-140
        public static byte vl85_avtoreg_plus;
        public static byte vl85_avtoreg_minus;
        public static byte vl85_komp_0;//0выкл,1вкл
        public static byte vl85_komp_1;
        public static byte vl85_vent1_0;
        public static byte vl85_vent1_1;
        public static byte vl85_vent2_0;
        public static byte vl85_vent2_1;
        public static byte vl85_vent3_0;
        public static byte vl85_vent3_2;
        public static byte vl85_vent4_0;
        public static byte vl85_vent4_1;
        public static byte vl85_fz_0;
        public static byte vl85_fz_1;
        public static byte vl85_svet_cab_0;//0выкл,1приб,2общ
        public static byte vl85_svet_cab_1;
        public static byte vl85_svet_cab_2;
        public static byte vl85_EPK_0;
        public static byte vl85_EPK_1;
        public static byte vl85_prozh_0;//0,1,2
        public static byte vl85_prozh_1;
        public static byte vl85_prozh_2;
        public static byte vl85_sign_0;
        public static byte vl85_sign_1;
        public static byte vl85_sign1_0;
        public static byte vl85_sign1_1;
        public static byte vl85_sign2_0;
        public static byte vl85_sign2_1;

        //tep70 35
        public static byte tep70_rev_0;//255нз,0-0,1вп
        public static byte tep70_rev_vpered;
        public static byte tep70_rev_nazad;

        public static byte tep70_kontr_0;//0-15
        public static byte tep70_kontr_1;
        public static byte tep70_kontr_2;
        public static byte tep70_kontr_3;
        public static byte tep70_kontr_4;
        public static byte tep70_kontr_5;
        public static byte tep70_kontr_6;
        public static byte tep70_kontr_7;
        public static byte tep70_kontr_8;
        public static byte tep70_kontr_9;
        public static byte tep70_kontr_10;
        public static byte tep70_kontr_11;
        public static byte tep70_kontr_12;
        public static byte tep70_kontr_13;
        public static byte tep70_kontr_14;
        public static byte tep70_kontr_15;

        public static byte tep70_kranTM_0;
        public static byte tep70_kranTM_1;
        public static byte tep70_nasos_0;
        public static byte tep70_nasos_1;
        public static byte tep70_pusk;//через K
        public static byte tep70_upravlenie_0;
        public static byte tep70_upravlenie_1;
        public static byte tep70_svet_cab_0;//0выкл,1приб,2общ
        public static byte tep70_svet_cab_1;
        public static byte tep70_svet_cab_2;
        public static byte tep70_EPK_0;
        public static byte tep70_EPK_1;
        public static byte tep70_EPT_0;
        public static byte tep70_EPT_1;
        public static byte tep70_prozh_0;//float 0-1.75
        public static byte tep70_prozh_1;
        public static byte tep70_prozh_2;

        //te10u 46
        public static byte te10u_rev_0;//255нз,0-0,1вп
        public static byte te10u_rev_vpered;
        public static byte te10u_rev_nazad;

        public static byte te10u_kontr_0;//0-15
        public static byte te10u_kontr_1;
        public static byte te10u_kontr_2;
        public static byte te10u_kontr_3;
        public static byte te10u_kontr_4;
        public static byte te10u_kontr_5;
        public static byte te10u_kontr_6;
        public static byte te10u_kontr_7;
        public static byte te10u_kontr_8;
        public static byte te10u_kontr_9;
        public static byte te10u_kontr_10;
        public static byte te10u_kontr_11;
        public static byte te10u_kontr_12;
        public static byte te10u_kontr_13;
        public static byte te10u_kontr_14;
        public static byte te10u_kontr_15;

        public static byte te10u_kranTM_0;
        public static byte te10u_kranTM_1;
        public static byte te10u_nasos1_0;
        public static byte te10u_nasos1_1;
        public static byte te10u_nasos2_0;
        public static byte te10u_nasos2_1;
        public static byte te10u_pusk1;//через J
        public static byte te10u_pusk2;//через K
        public static byte te10u_upravlenie_0;
        public static byte te10u_upravlenie_1;
        public static byte te10u_dvizhenie_0;
        public static byte te10u_dvizhenie_1;
        public static byte te10u_perehody_0;
        public static byte te10u_perehody_1;
        public static byte te10u_holost1_0;
        public static byte te10u_holost1_1;
        public static byte te10u_holost2_0;
        public static byte te10u_holost2_1;
        public static byte te10u_svet_cab_0;//0выкл,1приб,2общ
        public static byte te10u_svet_cab_1;
        public static byte te10u_svet_cab_2;
        public static byte te10u_EPK_0;
        public static byte te10u_EPK_1;
        public static byte te10u_EPT_0;
        public static byte te10u_EPT_1;
        public static byte te10u_prozh_0;//float 0-1.75
        public static byte te10u_prozh_1;
        public static byte te10u_prozh_2;

        //m62 35
        public static byte m62_rev_0;//255нз,0-0,1вп
        public static byte m62_rev_vpered;
        public static byte m62_rev_nazad;

        public static byte m62_kontr_0;//0-15
        public static byte m62_kontr_1;
        public static byte m62_kontr_2;
        public static byte m62_kontr_3;
        public static byte m62_kontr_4;
        public static byte m62_kontr_5;
        public static byte m62_kontr_6;
        public static byte m62_kontr_7;
        public static byte m62_kontr_8;
        public static byte m62_kontr_9;
        public static byte m62_kontr_10;
        public static byte m62_kontr_11;
        public static byte m62_kontr_12;
        public static byte m62_kontr_13;
        public static byte m62_kontr_14;
        public static byte m62_kontr_15;

        public static byte m62_kranTM_0;
        public static byte m62_kranTM_1;
        public static byte m62_nasos_0;
        public static byte m62_nasos_1;
        public static byte m62_pusk;//через K
        public static byte m62_upravlenie_0;
        public static byte m62_upravlenie_1;
        public static byte m62_perehody_0;
        public static byte m62_perehody_1;
        public static byte m62_svet_cab_0;//0выкл,1приб,2общ
        public static byte m62_svet_cab_1;
        public static byte m62_svet_cab_2;
        public static byte m62_EPK_0;
        public static byte m62_EPK_1;
        public static byte m62_prozh_0;//float 0-1.75
        public static byte m62_prozh_1;
        public static byte m62_prozh_2;

        //ed4m 29
        public static byte ed4m_rev_0;//0-0,1вп,255нз
        public static byte ed4m_rev_vpered;
        public static byte ed4m_rev_nazad;

        public static byte ed4m_kontr_0; //0-0,1-2ход,255-251тормоз
        public static byte ed4m_kontr_h1;
        public static byte ed4m_kontr_h2;
        public static byte ed4m_kontr_h3;
        public static byte ed4m_kontr_h4;
        public static byte ed4m_kontr_h5;
        public static byte ed4m_kontr_t1;
        public static byte ed4m_kontr_t2;
        public static byte ed4m_kontr_t3;
        public static byte ed4m_kontr_t4;
        public static byte ed4m_kontr_t5;
        public static byte ed4m_kranTM_0;
        public static byte ed4m_kranTM_1;
        public static byte ed4m_tokopr_0;
        public static byte ed4m_tokopr_1;
        public static byte ed4m_bv_0; //0-0 1-1
        public static byte ed4m_bv_1;
        public static byte ed4m_svet_cab_0;//0-1
        public static byte ed4m_svet_cab_1;
        public static byte ed4m_EPK_0;
        public static byte ed4m_EPK_1;
        public static byte ed4m_EPT_0;
        public static byte ed4m_EPT_1;
        public static byte ed4m_dvery_lev_0;
        public static byte ed4m_dvery_lev_1;
        public static byte ed4m_dvery_pr_0;
        public static byte ed4m_dvery_pr_1;
        public static byte ed4m_prozh_0; //float 0-1,625
        public static byte ed4m_prozh_1;
        public static byte ed4m_prozh_2;

        //ed9m 29
        public static byte ed9m_rev_0;//0-0,1вп,255нз
        public static byte ed9m_rev_vpered;
        public static byte ed9m_rev_nazad;

        public static byte ed9m_kontr_0; //0-0,1-5ход,255-251тормоз
        public static byte ed9m_kontr_h1;
        public static byte ed9m_kontr_h2;
        public static byte ed9m_kontr_t1;
        public static byte ed9m_kontr_t2;
        public static byte ed9m_kontr_t3;
        public static byte ed9m_kontr_t4;
        public static byte ed9m_kontr_t5;
        public static byte ed9m_kranTM_0;
        public static byte ed9m_kranTM_1;
        public static byte ed9m_tokopr_0;
        public static byte ed9m_tokopr_1;
        public static byte ed9m_bv_0; //0-0 1-1
        public static byte ed9m_bv_1;
        public static byte ed9m_svet_cab_0;//0-1
        public static byte ed9m_svet_cab_1;
        public static byte ed9m_EPK_0;
        public static byte ed9m_EPK_1;
        public static byte ed9m_EPT_0;
        public static byte ed9m_EPT_1;
        public static byte ed9m_dvery_lev_0;
        public static byte ed9m_dvery_lev_1;
        public static byte ed9m_dvery_pr_0;
        public static byte ed9m_dvery_pr_1;
        public static byte ed9m_prozh_0; //float 0-1,625
        public static byte ed9m_prozh_1;
        public static byte ed9m_prozh_2;

        //tem18 31
        public static byte tem18_rev_0;//255нз,0-0,1вп
        public static byte tem18_rev_vpered;
        public static byte tem18_rev_nazad;

        public static byte tem18_kontr_0;//0-15
        public static byte tem18_kontr_1;
        public static byte tem18_kontr_2;
        public static byte tem18_kontr_3;
        public static byte tem18_kontr_4;
        public static byte tem18_kontr_5;
        public static byte tem18_kontr_6;
        public static byte tem18_kontr_7;
        public static byte tem18_kontr_8;

        public static byte tem18_kranTM_0;
        public static byte tem18_kranTM_1;
        public static byte tem18_nasos_maslo0;
        public static byte tem18_nasos_maslo1;
        public static byte tem18_nasos_toplivo0;
        public static byte tem18_nasos_toplivo1;
        public static byte tem18_pusk;//через K
        public static byte tem18_upravlenie_0;
        public static byte tem18_upravlenie_1;
        public static byte tem18_perehody_0;
        public static byte tem18_perehody_1;
        public static byte tem18_svet_cab_0;//0выкл,1вкл
        public static byte tem18_svet_cab_1;
        public static byte tem18_svet_prib_0;//0выкл,1вкл
        public static byte tem18_svet_prib_1;
        public static byte tem18_EPK_0;
        public static byte tem18_EPK_1;
        public static byte tem18_prozh_0;//float 0-1.75
        public static byte tem18_prozh_1;
        public static byte tem18_prozh_2;

    }
}
