﻿using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get
            {
                return loginAccount;
            }

            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount);
            }
        }


        public fAccountProfile(Account acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }

        void ChangeAccount(Account acc)
        {
            txtbUserName.Text = acc.UserName;
            txbDisplayName.Text = acc.DisplayName;
        }

        void UpdateAccountInfo()
        {
            string displayname = txbDisplayName.Text;
            string password = txbPassWord.Text;
            string newpass = txbNewPassWord.Text;
            string renewpass = txbReEnterPass.Text;
            string username = txtbUserName.Text;

            if (!newpass.Equals(renewpass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới.");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(username, displayname, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành công.");
                    if (updateAccount != null)
                    {
                        updateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(username)));
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu!!!");
                }
            }
        }

        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }

        private void txbNewPassWord_TextChanged(object sender, EventArgs e)
        {

        }



        public class AccountEvent : EventArgs
        {
            private Account acc;

            public Account Acc
            {
                get
                {
                    return acc;
                }

                set
                {
                    acc = value;
                }
            }

            public AccountEvent(Account acc)
            {
                this.Acc = acc;
            }
        }
    }
}
