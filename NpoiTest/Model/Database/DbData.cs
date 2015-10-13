﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace NpoiTest.Model.Database
{
    /// <summary>
    ///     用于存储当前打开的数据库中的一些数据
    /// </summary>
    internal class DbData
    {
        //唯一的数据库连接
        private SQLiteConnection connection;

        /// <summary>
        ///     传入数据库路径的构造方法
        /// </summary>
        /// <param name="dbPath"></param>
        public DbData(string dbPath)
        {
            //首先判断数据库文件是否存在
            if (File.Exists(dbPath))
            {
                connection = new SQLiteConnection("Data Source="+dbPath);
                connection.Open();
            }
            else
            {
                MessageBox.Show("数据库文件不存在");
            }
        }

        /// <summary>
        /// 获取Project的List<String>
        /// </summary>
        public List<string> GetProjectStrList()
        {
            //首先读取project表数据
            SQLiteCommand readCmd = connection.CreateCommand();
            readCmd.CommandText = "SELECT prjName FROM Projects";
            SQLiteDataReader reader = readCmd.ExecuteReader();
            //将reader中的数据填充到list中去
            List<string> list = new List<string>();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }
            return list;
        }

        /// <summary>
        /// 获取数据库中---所有的---的核心数据
        /// </summary>
        /// <returns></returns>
        public List<DbBean> GetDbData()
        {
            //读取所有的Markers表中的数据
            SQLiteCommand readCmd = connection.CreateCommand();
            readCmd.CommandText = "SELECT * FROM Markers";
            SQLiteDataReader reader = readCmd.ExecuteReader();
            //将数据填充到List中
            List<DbBean> list = new List<DbBean>();
            while (reader.Read())
            {
                list.Add(new DbBean(reader.GetString(0), reader.GetDouble(1), reader.GetDouble(2)));
            }
            return list;
        }

        /// <summary>
        /// 获取指定prjName的核心数据
        /// </summary>
        /// <param name="prjName"></param>
        /// <returns></returns>
        public List<DbBean> GetSpecificDbData(string prjName)
        {
            //读取所有的Markers表中的数据
            SQLiteCommand readCmd = connection.CreateCommand();
            readCmd.CommandText = "SELECT * FROM Markers WHERE prjName="+"'"+prjName+"'";
            SQLiteDataReader reader = readCmd.ExecuteReader();
            //将数据填充到List中
            List<DbBean> list = new List<DbBean>();
            while (reader.Read())
            {
                list.Add(new DbBean(reader.GetString(0), reader.GetDouble(1), reader.GetDouble(2)));
            }
            return list;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseDb()
        {
            connection.Close();
        }
    }
}