using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using DataCore;
using System.Data;
namespace SoftType_S
{
    public class csFunction
    {
        public string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
        public string Sql_word_2 = ";Persist Security Info=False";
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";
        public readonly string datapath = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_DataPath", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");

        public static readonly string AccessLink = "";
        public static string str_GZDBH = "";
        public static string str_pkId = "";
        public static string str_DQBM = "";
        public ObservableCollection<MeterBaseInfoFactor> GetBaseInfo(string CheckTime, string SQL)
        {
            ObservableCollection<MeterBaseInfoFactor> Temp_Base = new ObservableCollection<MeterBaseInfoFactor>();

            using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + datapath + Sql_word_2))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                OleDbCommand cmd = new OleDbCommand(SQL, conn);
                OleDbDataReader Myreader = null;
                Myreader = cmd.ExecuteReader();

                while (Myreader.Read())
                {
                    Temp_Base.Add(new MeterBaseInfoFactor()
                    {
                        PK_LNG_METER_ID = Myreader["PK_LNG_METER_ID"].ToString(),
                        LNG_BENCH_POINT_NO = Myreader["LNG_BENCH_POINT_NO"].ToString(),
                        AVR_ASSET_NO = Myreader["AVR_ASSET_NO"].ToString(),
                        AVR_UB = Myreader["AVR_UB"].ToString(),
                        AVR_IB = Myreader["AVR_IB"].ToString(),
                        AVR_TEST_PERSON = Myreader["AVR_TEST_PERSON"].ToString(),
                        AVR_TOTAL_CONCLUSION = Myreader["AVR_TOTAL_CONCLUSION"].ToString(),
                        CHR_UPLOAD_FLAG = Myreader["CHR_UPLOAD_FLAG"].ToString(),
                        AVR_SEAL_1 = Myreader["AVR_SEAL_1"].ToString(),
                        AVR_SEAL_2 = Myreader["AVR_SEAL_2"].ToString(),
                        AVR_SEAL_3 = Myreader["AVR_SEAL_3"].ToString(),
                    });
                }
                conn.Close();
                return Temp_Base;
            }
        }
        public static string MeterZCBH = "";
        public  string UpdateToOracle(List<string> lisIntoInformation)
        {

            //blnOK = Get_VT_SB_JKDNBJDJL(ref listSQL);   //电能表检定记录

            //blnOK = Get_VT_SB_JKDNBJDWC(ref listSQL);   //误差记录

            //blnOK = Get_VT_SB_JKRJSWC(ref  listSQL);//日计时误差

            //blnOK = Get_VT_SB_JKXLWCJL(ref  listSQL);//电能表需量记录表

            //blnOK = Get_VT_SB_JKSDTQWCJL(ref  listSQL);//电能表投切记录表

            //blnOK = Get_VT_SB_JKDNBSSJL(ref  listSQL);//电能表示数记录表

            //blnOK = VT_SB_JKDNBZZJL(ref  listSQL);//电能表走字记录表
            return null;
        }

        /// <summary>
        /// 电能表检定记录
        /// </summary>
        /// <returns></returns>
        private static List<string> Get_VT_SB_JKDNBJDJL(string PK_ID)
        {
            bool blnOK = true;
            string strFZLXDM = "", strJDTime = "", strXBDM = "";
            List<string> Lis_Sql = new List<string>();
            string strSQL = "SELECT * FROM METER_INFO where PK_LNG_METER_ID='" + PK_ID + "'";
            OleDbConnection AccessConntion = new OleDbConnection(AccessLink);
            AccessConntion.Open();
            OleDbCommand ccmd = new OleDbCommand(strSQL, AccessConntion);
            OleDbDataReader OldRead = ccmd.ExecuteReader();
            OldRead.Read();
            string strOracleSQL = "insert into VT_SB_JKDNBJDJL (";
            string strOracleSQL_Name = "";
            string strOracleSQL_Value = "";
            string UpdateResult = "";
            try
            {
                MeterZCBH = OldRead["AVR_ASSET_NO"].ToString().Trim();
                UpdateResult = string.Format("电表：{0}", OldRead["AVR_ASSET_NO"].ToString().Trim());
                if (MeterZCBH == "")
                    MeterZCBH = OldRead["AVR_MADE_NO"].ToString().Trim();
                if (MeterZCBH == "")
                    MeterZCBH = OldRead["AVR_BAR_CODE"].ToString().Trim();

                strFZLXDM = OldRead["CHR_CT_CONNECTION_FLAG"].ToString().Trim();
                strJDTime = OldRead["DTM_TEST_DATE"].ToString().Trim();
                strXBDM = OldRead["AVR_WIRING_MODE"].ToString().Trim();

                string strValue = OldRead["AVR_IB"].ToString().Trim();
                int iIb = strValue.IndexOf("(");
                string strIb = strValue.Substring(0, iIb);

                #region ----------基本检定记录

                strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";
                strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;
                strOracleSQL_Name = strOracleSQL_Name + "SJBZ,";
                strOracleSQL_Value = strOracleSQL_Value + "','1";
                strOracleSQL_Name = strOracleSQL_Name + "BW,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OldRead["LNG_BENCH_POINT_NO"].ToString().Trim();
                strOracleSQL_Name = strOracleSQL_Name + "WD,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OldRead["AVR_TEMPERATURE"].ToString().Trim();

                strOracleSQL_Name = strOracleSQL_Name + "SD,";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OldRead["AVR_HUMIDITY"].ToString().Trim();
                strOracleSQL_Name = strOracleSQL_Name + "JDYJDM,"; //检定依据代码
                strOracleSQL_Value = strOracleSQL_Value + "','01";

                strOracleSQL_Name = strOracleSQL_Name + "JDJLDM,";  //总结论代码
                strValue = OldRead["AVR_TOTAL_CONCLUSION"].ToString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "WGJCJLDM,";    //外观标志检查结论代码
                strValue = Get_METER_RESULTS("100");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "WGBZJCJLDM,";    //外观标志检查结论代码
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                #region ----//不检定项目

                //strValue = "W";
                //strOracleSQL_Name = strOracleSQL_Name + "YQJJCJLDM,";    //元器件检查结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "JYXNSYJLDM,";    //绝缘性能试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "MCDYSYJLDM,";    //脉冲电压试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "MCDYSYJLDM,";    //脉冲电压试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "JDQZDNSZWCSYJLDM,";    //计度器总电能示值误差试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "DQYQSYJLDM,";    //电气要求试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                //strOracleSQL_Name = strOracleSQL_Name + "GLXHSYJLDM,";    //功率消耗试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "DYDYYXSYJLDM,";    //电源电压影响试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "DYFWSYJLDM,";    //电压范围试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "DYZJHDSZDYXSYJLDM,";    //电压暂降或短时中断影响试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "DYDSZDDSZDYXSYJLDM,";    //电压短时中断对时钟的影响试验结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                //strOracleSQL_Name = strOracleSQL_Name + "GNJCJLDM,";    //功能检查结论代码
                //strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;



                #endregion

                strOracleSQL_Name = strOracleSQL_Name + "JLDYSYJLDM,";  //交流电压试验结论代码
                strValue = Get_METER_RESULTS("102");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "ZQDYQSYJLDM,";  //准确度要求试验结论代码---- 基本误差试验代替
                strValue = Get_METER_RESULTS("103");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "CSSYJLDM,";  //常数试验结论代码
                strValue = Get_METER_RESULTS("106");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "QDDL,";  //起动电流
                strValue = Get_METER_START_NO_LOAD("1091", "AVR_CURRENT") + "A";
                //if (strValue != "")
                //strValue = (double.Parse(strValue) * double.Parse(strIb)).ToString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                strOracleSQL_Name = strOracleSQL_Name + "QDSYJLDM,";  //起动试验结论代码
                strValue = Get_METER_START_NO_LOAD("1091", "AVR_CONCLUSION");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "QDSYDYZ,";  //潜动试验电压值
                strValue = Get_METER_START_NO_LOAD("1101115", "AVR_VOLTAGE");
                if (strValue == "")
                    strValue = "1";
                // strValue = OldRead["AVR_UB"].ToString().Trim();
                strValue = (double.Parse(strValue) * double.Parse(OldRead["AVR_UB"].ToString().Trim())).ToString().Trim() + "V";
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;
                strOracleSQL_Name = strOracleSQL_Name + "FQDLZ,";  //防潜电流值
                strOracleSQL_Value = strOracleSQL_Value + "','0";
                strOracleSQL_Name = strOracleSQL_Name + "QISYJLDM,";  //潜动试验结论代码
                strValue = Get_METER_START_NO_LOAD("1101115", "AVR_CONCLUSION");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "JBWCSYJLDM,";  //基本误差试验结论代码
                strValue = Get_METER_RESULTS("103");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "RJSWCSYJLDM,";  //日计时误差试验结论代码
                strValue = Get_METER_COMMUNICATION("002");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "RJSWCZ,";  //日计时误差值
                strValue = Get_METER_COMMUNICATION("00201");
                int iTmpPoint = strValue.IndexOf("|");
                if (iTmpPoint > 0)
                    strValue = strValue.Substring(iTmpPoint + 1, strValue.Length - iTmpPoint - 1);

                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;



                strOracleSQL_Name = strOracleSQL_Name + "JDQZDNSZWCSYJLDM,";  //计度器总电能示值误差试验结论代码
                strValue = Get_METER_COMMUNICATION("005");
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                strOracleSQL_Name = strOracleSQL_Name + "FLSDDNSSWCSYJLDM,";  //费率时段电能示数误差试验结论代码
                strValue = Get_METER_COMMUNICATION("006");
                strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;


                //strOracleSQL_Name = strOracleSQL_Name + "XLZQWCSYJLDM,";  //需量周期误差试验结论代码
                //strValue = Get_METER_COMMUNICATION("0151");
                //strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "ZDXLWCSYJLDM,";  //最大需量误差试验结论代码
                strValue = Get_METER_COMMUNICATION("0151");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);


                strOracleSQL_Name = strOracleSQL_Name + "YZXSYJLDM,";  //一致性试验结论代码
                strValue = Get_METER_CONSISTENCY_DATA("411020700");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "WCBCSYJLDM,";  //误差变差试验结论代码
                strValue = Get_METER_CONSISTENCY_DATA("511020700");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                strOracleSQL_Name = strOracleSQL_Name + "WCYZXSYJLDM,";  //误差一致性试验结论代码
                strValue = Get_METER_CONSISTENCY_DATA("611010100");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);
                //strOracleSQL_Name = strOracleSQL_Name + "FZDLSYBCSYJLDM,";  //负载电流升降变差试验结论代码
                //strValue = Get_METER_CONSISTENCY_DATA("411010700");
                //strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "TXGNJCJLDM,";  //通信功能检查结论代码
                strValue = Get_METER_COMMUNICATION("001");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "SDTQWCSYJLDM,";  //时段投切误差试验结论代码
                strValue = Get_METER_COMMUNICATION("004");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "GPSDSJLDM,";  //GPS对时结论代码
                strValue = Get_METER_COMMUNICATION("007");
                strOracleSQL_Value = strOracleSQL_Value + "','" + ResultsCode(strValue);

                strOracleSQL_Name = strOracleSQL_Name + "JDRYBH,";  //检定员编号
                strOracleSQL_Value = strOracleSQL_Value + "','" + OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Jyy", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                ;
                strOracleSQL_Name = strOracleSQL_Name + "HYRYBH,";  //核验员编号
                strValue = OldRead["AVR_AUDIT_PERSON"].ToString().Trim();
                string strSection = "MIS_Info/UserName/Item";
                string strXmlValue = "";
                strOracleSQL_Value = strOracleSQL_Value + "','" + OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_Hyy", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                ;
                strOracleSQL_Name = strOracleSQL_Name + "DQBM,";  //地区编码
                strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;

                strOracleSQL_Name = strOracleSQL_Name + "BZZZZCBH,";  //电能计量标准设备资产编号
                strSection = "NewUser/CloumMIS/Item";
                strXmlValue = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_equipment", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                ;
                strOracleSQL_Value = strOracleSQL_Value + "','" + strXmlValue;

                #region ----------日期型处理

                strOracleSQL_Name = strOracleSQL_Name + "JDRQ,";  //检定日期
                // strValue = OldRead["DTM_TEST_DATE"].ToString().Trim();
                strValue = Convert.ToDateTime(OldRead["DTM_TEST_DATE"]).ToShortDateString().Trim() + " " + DateTime.Now.ToLongTimeString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + "',to_date('" + strValue + "','yyyy-mm-dd hh24:mi:ss')";

                strOracleSQL_Name = strOracleSQL_Name + "HYRQ";  //核验日期
                strValue = OldRead["DTM_VALID_DATE"].ToString().Trim();
                strOracleSQL_Value = strOracleSQL_Value + ",to_date('" + strValue + "','yyyy-mm-dd hh24:mi:ss')";

                #endregion


                strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";

                #endregion

                Lis_Sql.Add(strOracleSQL);

                #region ----------铅封上传


                string strSealValuePath = "NewUser/CloumMIS/Item";
                string str_Seal01 = "", str_Seal02 = "", str_Seal03 = "";
                List<string> ColSeal = new List<string>();
                str_Seal01 = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "cmb_Seal01", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                str_Seal02 = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "cmb_Seal02", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                str_Seal03 = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "cmb_Seal03", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
                str_Seal01 = SwitchSealNum(str_Seal01);
                str_Seal02 = SwitchSealNum(str_Seal02);
                str_Seal03 = SwitchSealNum(str_Seal03);

                ColSeal.Add(str_Seal01); ColSeal.Add(str_Seal02); ColSeal.Add(str_Seal03);
                for (int iCirc = 1; iCirc < 6; iCirc++)
                {
                    string strCode = "AVR_SEAL_" + iCirc.ToString().Trim();
                    strOracleSQL_Name = "";
                    strOracleSQL_Value = "";
                    if (OldRead[strCode].ToString().Trim() != "")
                    {

                        strOracleSQL = "insert into VT_SB_JKFYBGJL (";

                        strOracleSQL_Name = strOracleSQL_Name + "GZDBH,";
                        strOracleSQL_Value = strOracleSQL_Value + "'" + str_GZDBH;
                        strOracleSQL_Name = strOracleSQL_Name + "ZCBH,";
                        strOracleSQL_Value = strOracleSQL_Value + "','" + MeterZCBH;
                        strOracleSQL_Name = strOracleSQL_Name + "BGBZ,";
                        strOracleSQL_Value = strOracleSQL_Value + "','10";

                        strOracleSQL_Name = strOracleSQL_Name + "FYZCBH,";
                        strOracleSQL_Value = strOracleSQL_Value + "','" + OldRead[strCode].ToString().Trim();

                        strOracleSQL_Name = strOracleSQL_Name + "JFWZDM,";//加封位置代码-------
                        strValue = ColSeal[iCirc - 1];
                        //strValue = Get_JFWZDM(iCirc.ToString().Trim());
                        strOracleSQL_Value = strOracleSQL_Value + "','" + strValue;

                        strOracleSQL_Name = strOracleSQL_Name + "DQBM,";  //地区编码
                        strOracleSQL_Value = strOracleSQL_Value + "','" + str_DQBM;

                        strOracleSQL_Name = strOracleSQL_Name + "JFSJ";//时间
                        strValue = OldRead["DTM_TEST_DATE"].ToString().Trim();
                        strOracleSQL_Value = strOracleSQL_Value + "',to_date('" + strValue + "','yyyy-mm-dd hh24:mi:ss')";

                        strOracleSQL = strOracleSQL + strOracleSQL_Name + ")  Values (" + strOracleSQL_Value + ")";

                        Lis_Sql.Add(strOracleSQL);
                    }
                }

                #endregion


                AccessConntion.Close();
                OldRead.Close();

            }
            catch (Exception Error)
            {
                AccessConntion.Close();
                OldRead.Close();
            }
            return Lis_Sql;

        }
        /// <summary>
        /// 被检表多功能信息
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID"></param>
        /// <returns></returns>
        private static string Get_METER_COMMUNICATION(string RESULT_ID)
        {
            string strResults = "";
            string strSQL = " SELECT AVR_VALUE FROM METER_COMMUNICATION where AVR_PROJECT_NO='" + RESULT_ID + "' and FK_LNG_METER_ID='" + str_pkId + "'";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);


            return strResults;
        }

        /// <summary>
        /// 转换铅封位置代码
        /// </summary>
        /// <param name="str_Seal"></param>
        /// <returns></returns>
        private static string SwitchSealNum(string str_Seal)
        {
            string str_SealNum = "";
            switch (str_Seal.Trim())
            {
                case "左耳封":
                    str_SealNum = "01";
                    break;
                case "右耳封":
                    str_SealNum = "02";
                    break;
                case "编程小门":
                    str_SealNum = "07";
                    break;
            }
            return str_SealNum;
        }

        public string UpadataBaseInfo(string PK_ID)
        {
            int excuteSuccess=0;
            string ErrorResult;
            List<string> Excute_SQL = new List<string>();
            Excute_SQL = Get_VT_SB_JKDNBJDJL(PK_ID);
            excuteSuccess = OperateData.PublicFunction.ExcuteToOracle(Excute_SQL, out ErrorResult);
            try
            {
               
                if (excuteSuccess == 0)
                {

                    return "基本信息上传到中间库成功！";
                }
                else
                {
                    return "基本信息上传到中间库失败！" + ErrorResult;
                }


            }
            catch (Exception e)
            {
                return "基本信息上传到中间库失败！" + e.ToString();
            }
        }
        /// <summary>
        /// 结论与代号转换
        /// </summary>
        /// <param name="strResult"></param>
        /// <returns></returns>
        private static string ResultsCode(string strResult)
        {
            string strResultCode = "W";
            if (strResult.IndexOf("合格", 0) >= 0)
            {
                strResultCode = "Y";

                if (strResult.IndexOf("不", 0) >= 0)
                {
                    strResultCode = "N";
                }
            }


            else
                strResultCode = "W";
            return strResultCode;
        }
        /// <summary>
        /// 多功能结论读取
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID"></param>
        /// <returns></returns>
        private static string Get_METER_RESULTS(string RESULT_ID)
        {
            string strResults = "";
            string strSQL = " SELECT AVR_RESULT_VALUE FROM METER_RESULTS where AVR_RESULT_ID='" + RESULT_ID + "' and FK_LNG_METER_ID='" + str_pkId + "'";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);


            return strResults;
        }
        /// <summary>
        /// 启动潜动数据
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID"></param>
        /// <returns></returns>
        private static string Get_METER_START_NO_LOAD(string RESULT_ID, string strName)
        {
            string strResults = "";
            string strSQL = " SELECT " + strName + " FROM METER_START_NO_LOAD where AVR_PROJECT_NO='" + RESULT_ID + "' and  FK_LNG_METER_ID='" + str_pkId + "'";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);


            return strResults;
        }

        /// <summary>
        /// 一致性试验数据
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="RESULT_ID"></param>
        /// <returns></returns>
        private static string Get_METER_CONSISTENCY_DATA(string RESULT_ID)
        {
            string strResults = "";
            string strSQL = " SELECT AVR_CONC FROM METER_CONSISTENCY_DATA where FK_LNG_SCHEME_ID=" + RESULT_ID + " and FK_LNG_METER_ID='" + str_pkId + "'";

            OperateData.PublicFunction MyDb = new OperateData.PublicFunction();
            strResults = MyDb.GetSingleData(strSQL, AccessLink);



            return strResults;
        }
         public  void UpdateToOracle(object o)
        {
            List<string > Lis_Id=(List<string>)o;
            UpdateToOracle(Lis_Id);
        }
    }
}
