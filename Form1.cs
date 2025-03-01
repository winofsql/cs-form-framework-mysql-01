﻿using System;
using System.Data.Odbc;
using System.Diagnostics;
using System.Windows.Forms;

namespace cs_form_mysql_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OdbcConnection myCon = CreateConnection();

            // MySQL の処理

            string target = this.scode.Text;

            // SQL
            string myQuery =
                $@"SELECT
                    社員マスタ.*,
                    DATE_FORMAT(生年月日,'%Y-%m-%d') as 誕生日
                    from 社員マスタ
                    where 社員コード = '{target}'";

            // SQL実行用のオブジェクトを作成
            OdbcCommand myCommand = new OdbcCommand();

            // 実行用オブジェクトに必要な情報を与える
            myCommand.CommandText = myQuery;    // SQL
            myCommand.Connection = myCon;       // 接続

            // 次でする、データベースの値をもらう為のオブジェクトの変数の定義
            OdbcDataReader myReader;

            // SELECT を実行した結果を取得
            myReader = myCommand.ExecuteReader();

            // myReader からデータが読みだされたら画面にセット
            if (myReader.Read())
            {
                // 列名より列番号を取得
                int index = myReader.GetOrdinal("氏名");
                // 列番号で、値を取得して文字列化
                string text = myReader.GetValue(index).ToString();
                // 出力ウインドウに出力
                Debug.WriteLine($"Debug:{text}");

                this.sname.Text = text;
                this.button1.Text = "送信";

            }

            myReader.Close();

            myCon.Close();

        }

        private OdbcConnection CreateConnection()
        {
            // 接続文字列の作成
            OdbcConnectionStringBuilder builder = new OdbcConnectionStringBuilder();
            builder.Driver = "MySQL ODBC 8.0 Unicode Driver";
            // 接続用のパラメータを追加
            builder.Add("server", "localhost");
            builder.Add("database", "lightbox");
            builder.Add("uid", "root");
            builder.Add("pwd", "");

            string work = builder.ConnectionString;

            Console.WriteLine(builder.ConnectionString);

            // 接続の作成
            OdbcConnection myCon = new OdbcConnection();

            // MySQL の接続準備完了
            myCon.ConnectionString = builder.ConnectionString;

            // MySQL に接続
            myCon.Open();

            return myCon;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("こんにちは");
        }
    }
}
