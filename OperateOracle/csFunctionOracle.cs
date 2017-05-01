using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data.OleDb;
using System.IO;
using System.Collections;
namespace OperateOracle
{
    public class csFunctionOracle
    {
        public readonly string OracleLink = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "OracleLink", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
        public static readonly string AccessLink = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "AccessLink", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
        public Dictionary<string, string> BaseInfoColumnName;
        public Dictionary<string, string> ErrorInfoColumnName;
        public Dictionary<string, string> RjsInfoColumnName;
        public Dictionary<string, string> JKXKInfoColumnName;
        public Dictionary<string, string> JKSDTQInfoColumnName;
        public Dictionary<string, string> JKSSInfoColumnName;
        public Dictionary<string, string> JKZZInfoColumnName;
        public Dictionary<string, string> JKFYInfoColumnName;
        public int ShowTheConditionTable(string FromName, string ConditionShow, string man, string Keyword, out List<DataTableMember> TempTableInfoList)
        {
            int result = 0;
            try
            {
                OracleConnection Conn = new OracleConnection(OracleLink);
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                List<DataTableMember> tempInfoMeter = new List<DataTableMember>();
                if (Keyword.Substring(0, 1) == "E" || Keyword.Substring(0, 1) == "F" || Keyword.Contains("ZP") || Keyword.Contains("ZF"))
                {
                    //string sql = "SELECT * FROM Vt_Sb_Jkzdjcjl WHERE to_char(JDRQ,'yyyy/MM/dd HH24:MI:SS') BETWEEN '" + ConditionShow + "' AND '" + endTime + " ' AND JDRYBH = '" + man + "' ORDER BY JDRQ,to_number(BW)";
                    string sql = "SELECT * FROM  Vt_Sb_Jkzdjcjl WHERE ZCBH ='" + ConditionShow + "' ORDER BY JDRQ DESC";

                    OracleCommand adp = new OracleCommand(sql, Conn);
                    OracleDataReader myReader = null;
                    myReader = adp.ExecuteReader();
                    int count = 0;

                    List<DataTableMember> tempInfoQF = new List<DataTableMember>();
                    while (myReader.Read())
                    {
                        count++;
                        if (count != 1)
                        {
                            tempInfoMeter.Add(new DataTableMember()
                            {
                                ID = count,

                                StrJlbh = myReader["ZCBH"].ToString(),
                                StrJdjl = myReader["JCJLDM"].ToString(),
                                StrGZDBH = myReader["GZDBH"].ToString(),
                                StrWD = myReader["WD"].ToString(),
                                StrSD = myReader["SD"].ToString(),
                                IntMeterNum = Convert.ToInt32(myReader["BW"]),
                                StrJdrq = myReader["JDRQ"].ToString(),
                                StrJyy = myReader["JDRYBH"].ToString(),
                                StrJddw = myReader["BZ"].ToString(),
                                StrHyy = myReader["HYRYBH"].ToString(),
                                StrBZZZZCBH = myReader["BZZZZCBH"].ToString(),
                                // StrTestType=myReader["chrTestType"].ToString(),
                                // StrBlx=myReader["chrblx"].ToString(),
                                // StrBmc=myReader["chrBmc"].ToString(),
                                // StrUb=myReader["chrUb"].ToString(),
                                // StrIb=myReader["chrIb"].ToString(),
                                // StrMeterLevel=myReader["chrBdj"].ToString(),
                                // StrManufacture=myReader["chrZzcj"].ToString(),
                                //StrWcResult=myReader["chrJbwc"].ToString(),
                                //StrShellSeal = myReader["chrQianFeng1"].ToString(),
                                //StrCodeSeal = myReader["chrQianFeng2"].ToString(),
                            });
                        }

                    }
                }
                else
                {
                    string sql = "SELECT * FROM " + FromName + "  WHERE ZCBH ='" + ConditionShow + "' ORDER BY JDRQ DESC ";
                    OracleCommand adp = new OracleCommand(sql, Conn);
                    OracleDataReader myReader = null;
                    myReader = adp.ExecuteReader();
                    int count = 0;

                    List<DataTableMember> tempInfoQF = new List<DataTableMember>();
                    while (myReader.Read())
                    {
                        count++;
                        if (count != 1)
                        {
                            tempInfoMeter.Add(new DataTableMember()
                            {
                                ID = count,

                                StrJlbh = myReader["ZCBH"].ToString(),
                                StrJdjl = myReader["JDJLDM"].ToString(),
                                StrGZDBH = myReader["GZDBH"].ToString(),
                                StrWD = myReader["WD"].ToString(),
                                StrSD = myReader["SD"].ToString(),
                                IntMeterNum = Convert.ToInt32(myReader["BW"]),
                                StrJdrq = myReader["JDRQ"].ToString(),
                                StrJyy = myReader["JDRYBH"].ToString(),
                                StrJddw = myReader["BZ"].ToString(),
                                StrHyy = myReader["HYRYBH"].ToString(),
                                StrBZZZZCBH = myReader["BZZZZCBH"].ToString(),
                                // StrTestType=myReader["chrTestType"].ToString(),
                                // StrBlx=myReader["chrblx"].ToString(),
                                // StrBmc=myReader["chrBmc"].ToString(),
                                // StrUb=myReader["chrUb"].ToString(),
                                // StrIb=myReader["chrIb"].ToString(),
                                // StrMeterLevel=myReader["chrBdj"].ToString(),
                                // StrManufacture=myReader["chrZzcj"].ToString(),
                                //StrWcResult=myReader["chrJbwc"].ToString(),
                                //StrShellSeal = myReader["chrQianFeng1"].ToString(),
                                //StrCodeSeal = myReader["chrQianFeng2"].ToString(),
                            });
                        }


                    }
                }



                TempTableInfoList = tempInfoMeter;
                return result;
            }
            catch (Exception e)
            {
                TempTableInfoList = null;
                return -1;
            }
        }

        public DataTable GetZcbhTable(string MeterZcbh,string Sql)
        {
            DataTable dt = new DataTable ();
            using (OracleConnection conn = new OracleConnection(OracleLink))
            {
                OracleCommand cmd = new OracleCommand(Sql, conn);
                OracleDataAdapter OracleDa = new OracleDataAdapter(Sql, conn);
                OracleDa.Fill(dt);
                dt.Dispose();
                return dt;
            }

        }

        public DataTable GetZcbhTableLocal(string MeterZcbh, string Sql)
        {
            DataTable dt = new DataTable();
            using (OleDbConnection conn = new OleDbConnection(AccessLink))
            {
                OleDbCommand cmd = new OleDbCommand(Sql, conn);
                OleDbDataAdapter OracleDa = new OleDbDataAdapter(Sql, conn);
                OracleDa.Fill(dt);
                dt.Dispose();
                return dt;
            }

        }

        public static void ExportEasy(DataTable dtSource, string strFileName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            //填充表头   
            IRow dataRow = sheet.CreateRow(0);
            foreach (DataColumn column in dtSource.Columns)
            {
                dataRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }


            //填充内容   
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                dataRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    dataRow.CreateCell(j).SetCellValue(dtSource.Rows[i][j].ToString());
                }
            }
            for (int i = 0; i <= dtSource.Columns.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }


            //保存   
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
            }
         
        }

        public csFunctionOracle()
        {
            #region baseInfoCol
            BaseInfoColumnName.Add("GZDBH", "工作单编号");
            BaseInfoColumnName.Add("ZCBH", "资产编号");
            BaseInfoColumnName.Add("SJBZ", "首检标志");
            BaseInfoColumnName.Add("BW", "表位");
            BaseInfoColumnName.Add("WD", "温度");
            BaseInfoColumnName.Add("SD", "湿度");
            BaseInfoColumnName.Add("JDYJDM", "检定依据代码");
            BaseInfoColumnName.Add("JDJLDM", "检定结论代码");
            BaseInfoColumnName.Add("WGJCJLDM", "外观检查结论代码");
            BaseInfoColumnName.Add("WGBZJCJLDM", "外观标志检查结论代码");
            BaseInfoColumnName.Add("YQJJCJLDM", "元器件检查结论代码");
            BaseInfoColumnName.Add("JYXNSYJLDM", "绝缘性能试验结论代码");
            BaseInfoColumnName.Add("MCDYSYJLDM", "脉冲电压试验结论代码");
            BaseInfoColumnName.Add("JLDYSYJLDM", "交流电压试验结论代码");
            BaseInfoColumnName.Add("ZQDYQSYJLDM", "准确度要求试验结论代码");
            BaseInfoColumnName.Add("CSSYJLDM", "常数试验结论代码");
            BaseInfoColumnName.Add("QDDL", "起动电流");
            BaseInfoColumnName.Add("QDSYJLDM", "起动试验结论代码");
            BaseInfoColumnName.Add("QDSYDYZ", "潜动试验电压值");
            BaseInfoColumnName.Add("FQDLZ", "防潜电流值");
            BaseInfoColumnName.Add("QISYJLDM", "潜动试验结论代码");
            BaseInfoColumnName.Add("JBWCSYJLDM", "基本误差试验结论代码");
            BaseInfoColumnName.Add("RJSWCSYJLDM", "日计时误差试验结论代码");
            BaseInfoColumnName.Add("RJSWCZ", "日计时误差值");
            BaseInfoColumnName.Add("JDQZDNSZWCSYJLDM", "计度器总电能示值误差试验结论代码");
            BaseInfoColumnName.Add("FLSDDNSSWCSYJLDM", "费率时段电能示数误差试验结论代码");
            BaseInfoColumnName.Add("ZDXLWCSYJLDM", "最大需量误差试验结论代码");
            BaseInfoColumnName.Add("DQYQSYJLDM", "电气要求试验结论代码");
            BaseInfoColumnName.Add("GLXHSYJLDM", "功率消耗试验结论代码");
            BaseInfoColumnName.Add("DYDYYXSYJLDM", "电源电压影响试验结论代码");
            BaseInfoColumnName.Add("DYFWSYJLDM", "电压范围试验结论代码");
            BaseInfoColumnName.Add("DYZJHDSZDYXSYJLDM", "电压暂降或短时中断影响试验结论代码");
            BaseInfoColumnName.Add("DYDSZDDSZDYXSYJLDM", "电压短时中断对时钟的影响试验结论代码");
            BaseInfoColumnName.Add("DYCSJZDDSZYXSYJLDM", "电压长时间中断对时钟影响试验结论代码");
            BaseInfoColumnName.Add("DYCSJZDDDNBYXSYJLDM", "电压长时间中断对电能表影响试验结论代码");
            BaseInfoColumnName.Add("DYHZLDYTSZDYXSYJLDM", "电压和直流电源同时中断对电能表程序和存贮数据的影响试验结论代码");
            BaseInfoColumnName.Add("GNJCJLDM", "功能检查结论代码");
            BaseInfoColumnName.Add("DNJLGNJCJLDM", "电能计量功能检查结论代码");
            BaseInfoColumnName.Add("DLDJGNJCJLDM", "电量冻结功能检查结论代码");
            BaseInfoColumnName.Add("ZDXLGNJCJLDM", "最大需量功能检查结论代码");
            BaseInfoColumnName.Add("FLHSDGNJCJLDM", "费率和时段功能检查结论代码");
            BaseInfoColumnName.Add("SJJLGNJCJLDM", "事件记录功能检查结论代码");
            BaseInfoColumnName.Add("MCSCGNJCJLDM", "脉冲输出功能检查结论代码");
            BaseInfoColumnName.Add("XSGNJCJLDM", "显示功能检查结论代码");
            BaseInfoColumnName.Add("YZNRJCJLDM", "预置内容检查结论代码");
            BaseInfoColumnName.Add("AQFHGNJCJLDM", "安全防护功能检查结论代码");
            BaseInfoColumnName.Add("TDDGNJCJLDM", "通断电功能检查结论代码")            BaseInfoColumnName.Add("TDDGNJCJLDM", "通断电功能检查结论代码");
            BaseInfoColumnName.Add("TXGNJCJLDM", "通信功能检查结论代码");
            BaseInfoColumnName.Add("TXGYYZXJCJLDM", "通信规约一致性检查结论代码");
            BaseInfoColumnName.Add("SJCSXKGRSYJLDM", "数据传输线抗干扰试验结论代码");
            BaseInfoColumnName.Add("YZXSYJLDM", "一致性试验结论代码");
            BaseInfoColumnName.Add("WCBCSYJLDM", "误差变差试验结论代码");
            BaseInfoColumnName.Add("WCYZXSYJLDM", "误差一致性试验结论代码");
            BaseInfoColumnName.Add("FZDLSYBCSYJLDM", "负载电流升降变差试验结论代码");
            BaseInfoColumnName.Add("SDTQWCSYJLDM", "时段投切误差试验结论代码");
            BaseInfoColumnName.Add("XLZQWCSYJLDM", "需量周期误差试验结论代码");
            BaseInfoColumnName.Add("DNCLBZPCGJZ", "电能测量标准偏差估计值");
            BaseInfoColumnName.Add("GPSDSCZ", "GPS对时差值");
            BaseInfoColumnName.Add("GPSDSJLDM", "GPS对时结论代码");
            BaseInfoColumnName.Add("JDRYBH", "检定员编号");
            BaseInfoColumnName.Add("JDRQ", "检定日期");
            BaseInfoColumnName.Add("HYRYBH", "核验员编号");
            BaseInfoColumnName.Add("HYRQ", "核验日期");
            BaseInfoColumnName.Add("DQBM", "地区编码");
            BaseInfoColumnName.Add("HYRQ", "核验日期");
            BaseInfoColumnName.Add("BZ", "检定情况说明");
            BaseInfoColumnName.Add("BZZZZCBH", "电能计量标准设备资产编号");
            
            #endregion

            #region Error
            ErrorInfoColumnName.Add("GZDBH","工作单编号");
            ErrorInfoColumnName.Add("ZCBH","资产编号");
            ErrorInfoColumnName.Add("GLFXDM","功率方向代码");
            ErrorInfoColumnName.Add("GLYSDM","功率因数代码");
            ErrorInfoColumnName.Add("FZDLDM","负载电流代码");
            ErrorInfoColumnName.Add("XBDM","相别代码");
            ErrorInfoColumnName.Add("FZLXDM","负载类型代码");
            ErrorInfoColumnName.Add("FYDM","分元代码");
            ErrorInfoColumnName.Add("WCZ1","误差1");
            ErrorInfoColumnName.Add("WCZ2","误差2");
            ErrorInfoColumnName.Add("WCZ3","误差3");
            ErrorInfoColumnName.Add("WCZ4","误差4");
            ErrorInfoColumnName.Add("WCZ5","误差5");
            ErrorInfoColumnName.Add("WCPJZ","误差平均值");
            ErrorInfoColumnName.Add("WCXYZ","不平衡负载与平衡负载的误差差值");
            ErrorInfoColumnName.Add("JLDM","结论代码");
            ErrorInfoColumnName.Add("WCCZ","不平衡负载与平衡负载的误差差值");
            ErrorInfoColumnName.Add("WCCZXYZ","误差差值修约值");
            ErrorInfoColumnName.Add("DQBM","地区编码");

        #endregion 
            

        }
    }
}
