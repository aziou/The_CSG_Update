using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DataCore;
using System.Data;
using System.Data.OleDb;

namespace SoftType_G
{
    public class csFunction
    {
        public string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
        public string Sql_word_2 = ";Persist Security Info=False";
        public readonly string BaseConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml";
        public readonly string datapath = OperateData.FunctionXml.ReadElement("NewUser/CloumMIS/Item", "Name", "txt_DataPath", "Value", "", System.AppDomain.CurrentDomain.BaseDirectory + @"\config\NewBaseInfo.xml");
           

        public  ObservableCollection<MeterBaseInfoFactor> GetBaseInfo(string CheckTime, string SQL)
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
    }
}
