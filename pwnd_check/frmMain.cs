using pwnd_check.common;
namespace pwnd_check.client
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private async void btnCheckPassword_Click(object sender, EventArgs e)
        {
            btnCheckPassword.Enabled = false;
            try
            {
                var (found, count) = await HibpService.CheckPasswordAsync(txtPassword.Text);
                txtResult.Text = found
                    ? $"Password is part of a leak and found {count:n0} times."
                    : "Congratulations. Your password was not found in the database.";
            }
            catch (Exception ex)
            {
                txtResult.Text = ex.Message;
            }
            finally
            {
                btnCheckPassword.Enabled = true;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtSha1.Text = HibpService.ComputeSha1(txtPassword.Text);
        }
    }
}
