using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Collections.ObjectModel;
using DataCore;
namespace OperateData
{
    public class PublicFunction
    {
        public string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
        public string Sql_word_2 = ";Persist Security Info=False";
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";
        public readonly string datapath = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_DataPath", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
           
        /// <summary>
        /// 执行access  _SQL
        /// </summary>
        /// <param name="SQL"></param>
        private  void ExcuteAccess(string SQL)
        {
            using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + datapath + Sql_word_2))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                OleDbCommand cmd = new OleDbCommand(SQL, conn);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 执行access  _SQL
        /// </summary>
        /// <param name="SQL"></param>
        public  List<string> ExcuteAccess(string SQL,string Message_out)
        {
            
            List<string> OutMessage = new List<string>();
            using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + datapath + Sql_word_2))
            {
                
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    OleDbCommand cmd = new OleDbCommand(SQL, conn);
                    OleDbDataReader myreader = null;
                    myreader = cmd.ExecuteReader();
                    while (myreader.Read())
                    {
                        OutMessage.Add(myreader[Message_out].ToString().Trim());
                    }
                    return OutMessage;
                    conn.Close();
                }
                catch (Exception e)
                {
                    conn.Close();
                    return OutMessage;
                }
               

            }
        }



        public ObservableCollection<MeterBaseInfoFactor> GetBaseInfo(string CheckTime,string SQL,string Soft_Type)
        {
            ObservableCollection<MeterBaseInfoFactor> Temp_Base = new ObservableCollection<MeterBaseInfoFactor>();
            if (Soft_Type == "CL3000S")
            {
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
            else
            {
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
                            PK_LNG_METER_ID = Myreader["intMyId"].ToString(),
                            LNG_BENCH_POINT_NO = Myreader["intBno"].ToString(),
                            AVR_ASSET_NO = Myreader["chrJlbh"].ToString(),
                            AVR_UB = Myreader["chrUb"].ToString(),
                            AVR_IB = Myreader["chrIb"].ToString(),
                            AVR_TEST_PERSON = Myreader["chrJyy"].ToString(),
                            AVR_TOTAL_CONCLUSION = Myreader["chrJdjl"].ToString(),
                            CHR_UPLOAD_FLAG = Myreader["chrSetNetState"].ToString(),
                            AVR_SEAL_1 = Myreader["chrQianFeng1"].ToString(),
                            AVR_SEAL_2 = Myreader["chrQianFeng2"].ToString(),
                            AVR_SEAL_3 = Myreader["chrQianFeng3"].ToString(),
                        });
                    }
                    conn.Close();
                    return Temp_Base;
                }
            }
            
        }
 
    }
}
