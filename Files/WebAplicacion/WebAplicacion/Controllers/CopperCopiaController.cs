using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAplicacion.Controllers
{
    public class CopperCopiaController : Controller
    {
        // GET: Copper
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GraficoPiechartBancos()
        {
            string sConnString = "Data Source=(local);Initial Catalog=COPPER_V16;Integrated Security=True";
            //string sConnString = "Data Source=HDVMSQLDES;Initial Catalog=COPPER_V16;User ID=sa;Password=S0p0rt3";
            String query = " Select a.ano_codano as Año, a.mes_codmes as IdMes, " +
                " (Case When a.mes_codmes=0 Then '00-Ape' " +
                " When a.mes_codmes=1 Then '01-Ene'  " +
                " When a.mes_codmes=2 Then '02-Feb'  " +
                " When a.mes_codmes=3 Then '03-Mar'  " +
                " When a.mes_codmes=4 Then '04-Apr'  " +
                " When a.mes_codmes=5 Then '05-May'  " +
                " When a.mes_codmes=6 Then '06-Jun'  " +
                " When a.mes_codmes=7 Then '07-Jul'  " +
                " When a.mes_codmes=8 Then '08-Ago'  " +
                " When a.mes_codmes=9 Then '09-Sep'  " +
                " When a.mes_codmes=10 Then '10-Oct' " +
                " When a.mes_codmes=11 Then '11-Nov' " +
                " When a.mes_codmes=12 Then '12-Dec' " +
                " When a.mes_codmes=13 Then '13-Cie' " +
                " Else 'NDE' End) as Mes,  " +
                " datepart(week,b.adc_feccon) as Semana, " +
                " b.adc_feccon as Fecha, " +
                " a.cct_codcct as IDCuenta, a.cct_codcct + '- ' + c.cct_descct as Cuenta, " +
                " a.ccr_codepk as IDAuxiliar, Isnull(j.ccr_codccr,'') as CodAuxiliar, j.ccr_codccr + '- ' + j.ccr_nomaux as Auxiliar, Left(Isnull(j.ccr_nomaux,''),5) as AuxiliarCorto,  " +
                " Left(a.add_numdoc,4) as SerDoc,  " +
                " a.tdo_codtdo as TDoc, a.tdo_codtdo+'-'+a.add_numdoc as NroDoc,  " +
                " a.tdo_codref as TRef, a.tdo_codref+'-'+a.add_numref as NroRef,  " +
                " a.cco_codepk as IdCCosto, i.cco_codcco + '-' + i.cco_descco as CCosto,  " +
                " (Case When a.add_debhab='1' Then 'Debe' When a.add_debhab='2' Then 'Haber' Else 'Error' End) as TipoDH,  " +
                " a.mon_codepk as IdMoneda,  " +
                " (Case When a.add_debhab='1' Then a.add_impnac Else 0 End) as Debe_Sol,  " +
                " (Case When a.add_debhab='2' Then a.add_impnac Else 0 End) as Habe_Sol,  " +
                " (Case When a.add_debhab='1' Then a.add_impdol Else 0 End) as Debe_Dol,  " +
                " (Case When a.add_debhab='2' Then a.add_impdol Else 0 End) as Habe_Dol,  " +
                " (Case When a.add_debhab='1' Then 1 Else -1 End)*a.add_impnac as Soles,  " +
                " (Case When a.add_debhab='1' Then 1 Else -1 End)*a.add_impdol as Dolar,  " +
                " b.sdi_codepk as IdSubDiario, " +
                " b.adc_codepk as IdAsiento,  " +
                " b.adc_tcanac as TCambio_Cab, " +
                " a.add_gloadd as Glosa, " +
                " a.add_usuact as AUD_User, " +
                " a.add_fecact as AUD_Fecha, " +
                " a.add_estado as Estado, " +
                " d.cct_codcct + '-' + d.cct_descct as Cuenta5, " +
                " e.cct_codcct + '-' + e.cct_descct as Cuenta4, " +
                " Left(f.cct_codcct,3) as IDCuenta3, Rtrim(f.cct_codcct) + '- ' + f.cct_descct as Cuenta3, " +
                " g.cct_codcct + '-' + g.cct_descct as Cuenta2, " +
                " h.cct_codcct + '-' + h.cct_descct as Cuenta1, " +
                " L.gfc_codgfc + '-' + L.gfc_desgfc as Grupo_FIN, " +
                " rtrim(o.sdi_codsdi) + '-' + o.sdi_dessdi as SubDiario, " +
                " Str(b.ano_codano,4)+'-'+Str(b.mes_codmes,2)+'-'+rtrim(o.sdi_codsdi)+'-'+b.adc_numadc as Asiento, " +
                " (rtrim(isnull(q.tac_codtac,'')) + (case when len(rtrim(isnull(q.tac_codtac,'')))>0 then '-' else '' end) + rtrim(isnull(q.tac_destac,'')) ) as Tipo_Transaccion, " +
                " (rtrim(isnull(r.gcc_codgcc,'')) + (case when len(rtrim(isnull(r.gcc_codgcc,'')))>0 then '-' else '' end) + rtrim(isnull(r.gcc_desgcc,'')) ) as Grupo_Costo, " +
                " p.tcc_codepk as ID_CtaCte,  " +
                " ( Isnull(p.tdo_codtdo,'')+(Case when Len(rtrim(Isnull(p.tdo_codtdo,'')))>0 Then '-' else '' end) + Isnull(p.tcc_numdoc,'') ) as Doc_CtaCte,  " +
                " r0.rge_codrge as IdRepGerencia, r0.rge_codrge + '-' + r0.rge_desrge as RepGerencia, r0.rge_codrge + '-' + r0.rge_desing as RegGerencia_EN,  " +
                " (Case  " +
                "    When Left(r1.rg1_codrg1,2)='02' And r0.rge_codrge='001' And Isnull(i.cco_codcco,'')<>'' " +
                "         Then (Case When Left(Isnull(i.cco_codcco,''),1)='1' Then r1.rg1_codrg1 " +
                "                   Else '030' End) " +
                "    Else r1.rg1_codrg1 End) as IdGruGerencia, " +
                " (Case  " +
                "    When Left(r1.rg1_codrg1,2)='02' And r0.rge_codrge='001' And Isnull(i.cco_codcco,'')<>'' " +
                "         Then (Case When Left(Isnull(i.cco_codcco,''),1)='1' Then r1.rg1_codrg1 + '-' + r1.rg1_desrg1 " +
                "                   Else '030-Otros Gastos' End) " +
                "    Else r1.rg1_codrg1 + '-' + r1.rg1_desrg1 End) as GruGerencia,  " +
                " (Case  " +
                "    When Left(r1.rg1_codrg1,2)='02' And r0.rge_codrge='001' And Isnull(i.cco_codcco,'')<>'' " +
                "         Then (Case When Left(Isnull(i.cco_codcco,''),1)='1' Then r1.rg1_codrg1 + '-' + r1.rg1_desing " +
                "                   Else '030-Expenses' End) " +
                "    Else r1.rg1_codrg1 + '-' + r1.rg1_desing End) as GruGerencia_EN " +
                " into  #tmpBancos " +
                " from ASIE_DIAR_ADD A " +
                " Left Join ASIE_DIAR_ADC B on a.cia_codcia=b.cia_codcia and a.suc_codsuc=b.suc_codsuc and a.adc_codepk=b.adc_codepk " +
                " Left Join cuen_cont_cct C on a.cia_codcia=c.cia_codcia and a.cct_codcct=c.cct_codcct " +
                " Left Join cuen_cont_cct D on c.cia_codcia=d.cia_codcia and Left(c.cct_codcct,5)=d.cct_codcct " +
                " Left Join cuen_cont_cct E on c.cia_codcia=e.cia_codcia and Left(c.cct_codcct,4)=e.cct_codcct " +
                " Left Join cuen_cont_cct F on c.cia_codcia=f.cia_codcia and Left(c.cct_codcct,3)=F.cct_codcct " +
                " Left Join cuen_cont_cct G on c.cia_codcia=g.cia_codcia and Left(c.cct_codcct,2)=g.cct_codcct " +
                " Left Join cuen_cont_cct H on c.cia_codcia=h.cia_codcia and Left(c.cct_codcct,1)=h.cct_codcct " +
                " Left Join Cent_cost_cco I on a.cia_codcia=i.cia_codcia and a.cco_codepk=i.cco_codepk " +
                " Left Join Cuen_Corr_Ccr J On a.cia_codcia=j.cia_codcia and a.ccr_codepk=j.ccr_codepk " +
                " Left Join GRUP_FINA_GFD K On a.cia_codcia=k.cia_codcia and a.cct_codcct=k.cct_codcct " +
                " Left Join GRUP_FINA_GFC L On k.cia_codcia=l.cia_codcia and k.gfc_codgfc=l.gfc_codgfc " +
                " Left Join SUBDIARIO_SDI O On b.cia_codcia=o.cia_codcia and b.sdi_codepk=o.sdi_codepk " +
                " Left Join TRAN_CTACTE_TCC P On b.adc_codepk=p.adc_codepk and b.cia_codcia=p.cia_codcia and b.sdi_codepk=p.sdi_Codepk " +
                " Left Join TIPO_ASIE_TAC Q on p.tac_codepk=q.tac_codepk and p.cia_codcia=q.cia_codcia " +
                " Left Join GRUP_CENCOS_GCC R On i.cia_codcia=r.cia_codcia and i.gcc_codgcc=r.gcc_codgcc " +
                " Left Join HD_Repo_Gere_RG2 R2 on a.cia_codcia=r2.cia_codcia and a.cct_codcct=r2.cct_codcct " +
                " Left Join HD_Repo_Gere_RG1 R1 on r2.rg1_codrg1=r1.rg1_codrg1 and r2.rge_codrge=r1.rge_codrge  " +
                " Left Join HD_Repo_Gere_RGE R0 on r1.rge_codrge=r0.rge_codrge  " +
                " Where a.cia_codcia=1 and a.ano_codano=2021 and a.mes_codmes<=02 and b.adc_feccon<=convert(smalldatetime,'17/02/2021',103) " +
                " select Cuenta,Sum (Debe_Dol - Habe_Dol)as Total from #tmpBancos " +
                " where IdRepGerencia='003' and IdGruGerencia='A10' and Mes<='2' " +
                " group by  " +
                " 	 IDCuenta,Cuenta ";

            List<object> chartData = new List<object>();

            using (SqlConnection con = new SqlConnection(sConnString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new object[]
                            {
                                sdr["Cuenta"], sdr["Total"]
                            });
                        }
                    }

                    con.Close();
                }
            }
            // return Json(new Response {Action=Accion, Message = Mensaje }, JsonRequestBehavior.AllowGet );
            return Json(chartData);
        }

        [HttpPost]
        public JsonResult GraficoColumnchartBancos()
        {
            string sConnString = "Data Source=(local);Initial Catalog=COPPER_V16;Integrated Security=True";
            //string sConnString = "Data Source=HDVMSQLDES;Initial Catalog=COPPER_V16;User ID=sa;Password=S0p0rt3";
            String query = " Select a.ano_codano as Año, a.mes_codmes as IdMes, " +
                " (Case When a.mes_codmes=0 Then '00-Ape' " +
                " When a.mes_codmes=1 Then '01-Ene'  " +
                " When a.mes_codmes=2 Then '02-Feb'  " +
                " When a.mes_codmes=3 Then '03-Mar'  " +
                " When a.mes_codmes=4 Then '04-Apr'  " +
                " When a.mes_codmes=5 Then '05-May'  " +
                " When a.mes_codmes=6 Then '06-Jun'  " +
                " When a.mes_codmes=7 Then '07-Jul'  " +
                " When a.mes_codmes=8 Then '08-Ago'  " +
                " When a.mes_codmes=9 Then '09-Sep'  " +
                " When a.mes_codmes=10 Then '10-Oct' " +
                " When a.mes_codmes=11 Then '11-Nov' " +
                " When a.mes_codmes=12 Then '12-Dec' " +
                " When a.mes_codmes=13 Then '13-Cie' " +
                " Else 'NDE' End) as Mes,  " +
                " datepart(week,b.adc_feccon) as Semana, " +
                " b.adc_feccon as Fecha, " +
                " a.cct_codcct as IDCuenta, a.cct_codcct + '- ' + c.cct_descct as Cuenta, " +
                " a.ccr_codepk as IDAuxiliar, Isnull(j.ccr_codccr,'') as CodAuxiliar, j.ccr_codccr + '- ' + j.ccr_nomaux as Auxiliar, Left(Isnull(j.ccr_nomaux,''),5) as AuxiliarCorto,  " +
                " Left(a.add_numdoc,4) as SerDoc,  " +
                " a.tdo_codtdo as TDoc, a.tdo_codtdo+'-'+a.add_numdoc as NroDoc,  " +
                " a.tdo_codref as TRef, a.tdo_codref+'-'+a.add_numref as NroRef,  " +
                " a.cco_codepk as IdCCosto, i.cco_codcco + '-' + i.cco_descco as CCosto,  " +
                " (Case When a.add_debhab='1' Then 'Debe' When a.add_debhab='2' Then 'Haber' Else 'Error' End) as TipoDH,  " +
                " a.mon_codepk as IdMoneda,  " +
                " (Case When a.add_debhab='1' Then a.add_impnac Else 0 End) as Debe_Sol,  " +
                " (Case When a.add_debhab='2' Then a.add_impnac Else 0 End) as Habe_Sol,  " +
                " (Case When a.add_debhab='1' Then a.add_impdol Else 0 End) as Debe_Dol,  " +
                " (Case When a.add_debhab='2' Then a.add_impdol Else 0 End) as Habe_Dol,  " +
                " (Case When a.add_debhab='1' Then 1 Else -1 End)*a.add_impnac as Soles,  " +
                " (Case When a.add_debhab='1' Then 1 Else -1 End)*a.add_impdol as Dolar,  " +
                " b.sdi_codepk as IdSubDiario, " +
                " b.adc_codepk as IdAsiento,  " +
                " b.adc_tcanac as TCambio_Cab, " +
                " a.add_gloadd as Glosa, " +
                " a.add_usuact as AUD_User, " +
                " a.add_fecact as AUD_Fecha, " +
                " a.add_estado as Estado, " +
                " d.cct_codcct + '-' + d.cct_descct as Cuenta5, " +
                " e.cct_codcct + '-' + e.cct_descct as Cuenta4, " +
                " Left(f.cct_codcct,3) as IDCuenta3, Rtrim(f.cct_codcct) + '- ' + f.cct_descct as Cuenta3, " +
                " g.cct_codcct + '-' + g.cct_descct as Cuenta2, " +
                " h.cct_codcct + '-' + h.cct_descct as Cuenta1, " +
                " L.gfc_codgfc + '-' + L.gfc_desgfc as Grupo_FIN, " +
                " rtrim(o.sdi_codsdi) + '-' + o.sdi_dessdi as SubDiario, " +
                " Str(b.ano_codano,4)+'-'+Str(b.mes_codmes,2)+'-'+rtrim(o.sdi_codsdi)+'-'+b.adc_numadc as Asiento, " +
                " (rtrim(isnull(q.tac_codtac,'')) + (case when len(rtrim(isnull(q.tac_codtac,'')))>0 then '-' else '' end) + rtrim(isnull(q.tac_destac,'')) ) as Tipo_Transaccion, " +
                " (rtrim(isnull(r.gcc_codgcc,'')) + (case when len(rtrim(isnull(r.gcc_codgcc,'')))>0 then '-' else '' end) + rtrim(isnull(r.gcc_desgcc,'')) ) as Grupo_Costo, " +
                " p.tcc_codepk as ID_CtaCte,  " +
                " ( Isnull(p.tdo_codtdo,'')+(Case when Len(rtrim(Isnull(p.tdo_codtdo,'')))>0 Then '-' else '' end) + Isnull(p.tcc_numdoc,'') ) as Doc_CtaCte,  " +
                " r0.rge_codrge as IdRepGerencia, r0.rge_codrge + '-' + r0.rge_desrge as RepGerencia, r0.rge_codrge + '-' + r0.rge_desing as RegGerencia_EN,  " +
                " (Case  " +
                "    When Left(r1.rg1_codrg1,2)='02' And r0.rge_codrge='001' And Isnull(i.cco_codcco,'')<>'' " +
                "         Then (Case When Left(Isnull(i.cco_codcco,''),1)='1' Then r1.rg1_codrg1 " +
                "                   Else '030' End) " +
                "    Else r1.rg1_codrg1 End) as IdGruGerencia, " +
                " (Case  " +
                "    When Left(r1.rg1_codrg1,2)='02' And r0.rge_codrge='001' And Isnull(i.cco_codcco,'')<>'' " +
                "         Then (Case When Left(Isnull(i.cco_codcco,''),1)='1' Then r1.rg1_codrg1 + '-' + r1.rg1_desrg1 " +
                "                   Else '030-Otros Gastos' End) " +
                "    Else r1.rg1_codrg1 + '-' + r1.rg1_desrg1 End) as GruGerencia,  " +
                " (Case  " +
                "    When Left(r1.rg1_codrg1,2)='02' And r0.rge_codrge='001' And Isnull(i.cco_codcco,'')<>'' " +
                "         Then (Case When Left(Isnull(i.cco_codcco,''),1)='1' Then r1.rg1_codrg1 + '-' + r1.rg1_desing " +
                "                   Else '030-Expenses' End) " +
                "    Else r1.rg1_codrg1 + '-' + r1.rg1_desing End) as GruGerencia_EN " +
                " into  #tmpBancos " +
                " from ASIE_DIAR_ADD A " +
                " Left Join ASIE_DIAR_ADC B on a.cia_codcia=b.cia_codcia and a.suc_codsuc=b.suc_codsuc and a.adc_codepk=b.adc_codepk " +
                " Left Join cuen_cont_cct C on a.cia_codcia=c.cia_codcia and a.cct_codcct=c.cct_codcct " +
                " Left Join cuen_cont_cct D on c.cia_codcia=d.cia_codcia and Left(c.cct_codcct,5)=d.cct_codcct " +
                " Left Join cuen_cont_cct E on c.cia_codcia=e.cia_codcia and Left(c.cct_codcct,4)=e.cct_codcct " +
                " Left Join cuen_cont_cct F on c.cia_codcia=f.cia_codcia and Left(c.cct_codcct,3)=F.cct_codcct " +
                " Left Join cuen_cont_cct G on c.cia_codcia=g.cia_codcia and Left(c.cct_codcct,2)=g.cct_codcct " +
                " Left Join cuen_cont_cct H on c.cia_codcia=h.cia_codcia and Left(c.cct_codcct,1)=h.cct_codcct " +
                " Left Join Cent_cost_cco I on a.cia_codcia=i.cia_codcia and a.cco_codepk=i.cco_codepk " +
                " Left Join Cuen_Corr_Ccr J On a.cia_codcia=j.cia_codcia and a.ccr_codepk=j.ccr_codepk " +
                " Left Join GRUP_FINA_GFD K On a.cia_codcia=k.cia_codcia and a.cct_codcct=k.cct_codcct " +
                " Left Join GRUP_FINA_GFC L On k.cia_codcia=l.cia_codcia and k.gfc_codgfc=l.gfc_codgfc " +
                " Left Join SUBDIARIO_SDI O On b.cia_codcia=o.cia_codcia and b.sdi_codepk=o.sdi_codepk " +
                " Left Join TRAN_CTACTE_TCC P On b.adc_codepk=p.adc_codepk and b.cia_codcia=p.cia_codcia and b.sdi_codepk=p.sdi_Codepk " +
                " Left Join TIPO_ASIE_TAC Q on p.tac_codepk=q.tac_codepk and p.cia_codcia=q.cia_codcia " +
                " Left Join GRUP_CENCOS_GCC R On i.cia_codcia=r.cia_codcia and i.gcc_codgcc=r.gcc_codgcc " +
                " Left Join HD_Repo_Gere_RG2 R2 on a.cia_codcia=r2.cia_codcia and a.cct_codcct=r2.cct_codcct " +
                " Left Join HD_Repo_Gere_RG1 R1 on r2.rg1_codrg1=r1.rg1_codrg1 and r2.rge_codrge=r1.rge_codrge  " +
                " Left Join HD_Repo_Gere_RGE R0 on r1.rge_codrge=r0.rge_codrge  " +
                " Where a.cia_codcia=1 and a.ano_codano=2021 and a.mes_codmes<=02 and b.adc_feccon<=convert(smalldatetime,'17/02/2021',103) " +
                " select Cuenta,Sum (Debe_Dol - Habe_Dol)as Total from #tmpBancos " +
                " where IdRepGerencia='003' and IdGruGerencia='A10' and Mes<='2' " +
                " group by  " +
                " 	 IDCuenta,Cuenta ";

            List<object> chartData = new List<object>();

            using (SqlConnection con = new SqlConnection(sConnString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new object[]
                            {
                                sdr["Cuenta"], sdr["Total"]
                            });
                        }
                    }

                    con.Close();
                }
            }
            // return Json(new Response {Action=Accion, Message = Mensaje }, JsonRequestBehavior.AllowGet );
            return Json(chartData);
        }

        [HttpPost]
        public JsonResult GraficoAcumuladoAnual()
        {
            string sConnString = "Data Source=(local);Initial Catalog=COPPER_V16;Integrated Security=True";
            //string sConnString = "Data Source=HDVMSQLDES;Initial Catalog=COPPER_V16;User ID=sa;Password=S0p0rt3";
            String query = " Select " +
                           " a.ano_codano as Año, a.mes_codmes as IdMes, " +
                           " (Case " +
                           " When a.mes_codmes = 0 Then '00-Ape'  " +
                           " When a.mes_codmes = 1 Then '01-Ene'  " +
                           " When a.mes_codmes = 2 Then '02-Feb'  " +
                           " When a.mes_codmes = 3 Then '03-Mar'  " +
                           " When a.mes_codmes = 4 Then '04-Apr'  " +
                           " When a.mes_codmes = 5 Then '05-May'  " +
                           " When a.mes_codmes = 6 Then '06-Jun'  " +
                           " When a.mes_codmes = 7 Then '07-Jul'  " +
                           " When a.mes_codmes = 8 Then '08-Ago'  " +
                           " When a.mes_codmes = 9 Then '09-Sep'  " +
                           " When a.mes_codmes = 10 Then '10-Oct' " +
                           " When a.mes_codmes = 11 Then '11-Nov' " +
                           " When a.mes_codmes = 12 Then '12-Dec' " +
                           " When a.mes_codmes = 13 Then '13-Cie' " +
                           " Else 'NDE' End) as Mes, " +
                           " Sum(a.add_impdol * (Case When a.add_debhab = '1' then - 1 else 1 end)) as Dolar, " +
                           " Sum(a.add_impnac * (Case When a.add_debhab = '1' then - 1 else 1 end)) as Soles  " +
                           " from ASIE_DIAR_ADD A " +
                           " Left Join ASIE_DIAR_ADC B on a.cia_codcia = b.cia_codcia and a.suc_codsuc = b.suc_codsuc and a.adc_codepk = b.adc_codepk " +
                           " Left Join HD_Repo_Gere_RG2 R2 on a.cia_codcia = r2.cia_codcia and a.cct_codcct = r2.cct_codcct " +
                           " Left Join HD_Repo_Gere_RG1 R1 on r2.rg1_codrg1 = r1.rg1_codrg1 and r2.rge_codrge = r1.rge_codrge " +
                           " Left Join HD_Repo_Gere_RGE R0 on r1.rge_codrge = r0.rge_codrge " +
                           " Where a.cia_codcia = 1 and a.ano_codano >= '2021' and a.mes_codmes=1  " +
                           "                     and r0.rge_codrge = '001'  " +
                           "       and CONVERT(varchar(10), b.adc_feccon, 103) <= '2021-02-17' " +
                           "     Group By a.ano_codano, a.mes_codmes ";

            List<object> chartData = new List<object>();
            string jsondata;
            using (SqlConnection con = new SqlConnection(sConnString))
            {
                DataTable TableData = new DataTable();
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    TableData.Clear();
                    da.Fill(TableData);

                    jsondata = JsonConvert.SerializeObject(TableData);


                    con.Close();
                }
            }
            // return Json(new Response {Action=Accion, Message = Mensaje }, JsonRequestBehavior.AllowGet );
            return Json(jsondata);

            
        }
    }
}
