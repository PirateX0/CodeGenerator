using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 大龙的代码生成器
{
    public partial class CodeGenerator : Form
    {
        public CodeGenerator()
        {
            InitializeComponent();
        }

        private void btnCon_Click(object sender, EventArgs e)
        {
            //重新连接的时候，先清除之前的记录。
            clbTabs.Items.Clear();
            string cmdText = "select * from INFORMATION_SCHEMA.tables";
            DataTable dt = SQLHelper.ExecuteDataTable(txtConStr.Text, cmdText);
            foreach (DataRow dr in dt.Rows)
            {
                clbTabs.Items.Add(dr["table_name"]);
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            DialogResult result = fbdPath.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderPath = fbdPath.SelectedPath;
                if (folderPath != "")
                {
                    txtPath.Text = folderPath;
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            ModelGenerator modelGenerator = new ModelGenerator();
            DALGenerator dalGenerator = new DALGenerator();
            BLLGenerator bllGenerator = new BLLGenerator();

            if (!IsValid())
            {
                return;
            }

            Generate(modelGenerator, dalGenerator, bllGenerator);

            ShowResult();
        }

        private void ShowResult()
        {
            MessageBox.Show("生成成功！");
        }

        private void Generate(ModelGenerator modelGenerator, DALGenerator dalGenerator, BLLGenerator bllGenerator)
        {
            foreach (string tableName in clbTabs.CheckedItems)
            {
                string processedTableName = GetProcessedTableName(tableName);
                InitializeGenerator(modelGenerator, dalGenerator, bllGenerator, tableName, processedTableName);

                modelGenerator.GenerateModel2010();
                dalGenerator.GenerateDAL2010();
                bllGenerator.GenerateBLL2010();
            }
        }

        private string GetProcessedTableName(string tableName)
        {
            string processedTableName = tableName;
            string prefix = txtPrefix.Text;
            string suffix = txtSuffix.Text;

            if (!string.IsNullOrEmpty(prefix))
            {
                processedTableName = processedTableName.Substring(prefix.Length);
            }
            if (!string.IsNullOrEmpty(suffix))
            {
                processedTableName = processedTableName.Substring(0, processedTableName.Length - suffix.Length);
            }

            return processedTableName;
        }

        private void InitializeGenerator(ModelGenerator modelGenerator, DALGenerator dalGenerator, BLLGenerator bllGenerator, string tableName, string processedTableName)
        {
            modelGenerator.ConStr = txtConStr.Text;
            modelGenerator.FolderPath = txtPath.Text;
            modelGenerator.NameSpace = txtNameSpace.Text;
            modelGenerator.TableName = tableName;
            modelGenerator.ProcessedTableName = processedTableName;
            bllGenerator.ConStr = txtConStr.Text;
            bllGenerator.FolderPath = txtPath.Text;
            bllGenerator.NameSpace = txtNameSpace.Text;
            bllGenerator.TableName = tableName;
            bllGenerator.ProcessedTableName = processedTableName;
            dalGenerator.ConStr = txtConStr.Text;
            dalGenerator.FolderPath = txtPath.Text;
            dalGenerator.NameSpace = txtNameSpace.Text;
            dalGenerator.TableName = tableName;
            dalGenerator.ProcessedTableName = processedTableName;
        }

        private bool IsValid()
        {
            if (txtConStr.Text == "")
            {
                MessageBox.Show("连接字符串不能为空！");
                return false;
            }
            if (txtPath.Text == "")
            {
                MessageBox.Show("生成路径不能为空！");
                return false;
            }
            if (txtNameSpace.Text == "")
            {
                MessageBox.Show("命名空间不能为空！");
                return false;
            }
            if (clbTabs.CheckedItems.Count <= 0)
            {
                MessageBox.Show("表不能为空！");
                return false;
            }
            if (!hasPrefix())
            {
                MessageBox.Show("表名不包含要去掉的前缀名");
                return false;
            }
            if (!hasSuffix())
            {
                MessageBox.Show("表名不包含要去掉的后缀名");
                return false;
            }
            return true;
        }

        private bool hasPrefix()
        {
            foreach (string tableName in clbTabs.CheckedItems)
            {
                string processedTableName = tableName;
                string prefix = txtPrefix.Text;

                if (!string.IsNullOrEmpty(prefix))
                {
                    string prefixTableName = tableName.Substring(0, prefix.Length);
                    if (!prefixTableName.Contains(prefix))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool hasSuffix()
        {
            foreach (string tableName in clbTabs.CheckedItems)
            {
                string processedTableName = tableName;
                string suffix = txtSuffix.Text;

                if (!string.IsNullOrEmpty(suffix))
                {
                    string suffixTableName = tableName.Substring(tableName.Length - suffix.Length, suffix.Length);
                    if (!suffixTableName.Contains(suffix))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}