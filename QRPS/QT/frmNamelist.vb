Imports System.Data
Imports System.Windows.Forms


Public Class frmNamelist

    Private parentForm As frmQuotation_View_V3
    Public Sub New(ByVal callingForm As frmQuotation_View_V3)

        InitializeComponent()
        Me.parentForm = callingForm
        LoadMockupData()
    End Sub

    Public Sub New()
        InitializeComponent()
        LoadMockupData()
    End Sub


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then

            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            If DataGridView1.Columns.Contains("CustomerName") Then

                Dim customerName As String = selectedRow.Cells("CustomerName").Value.ToString()

                If Me.parentForm IsNot Nothing Then
                    Me.parentForm.SearchText = customerName
                    Me.Close()

                Else
                    MessageBox.Show("ข้อมูล: " & customerName, "ไม่ได้ระบุฟอร์มเป้าหมาย", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End If

        End If
    End Sub

    Private Sub LoadMockupData()
        Dim dt As New DataTable("CustomerData")

        dt.Columns.Add("CustomerCode", GetType(String))
        dt.Columns.Add("CustomerName", GetType(String))
        dt.Columns.Add("ContactInfo", GetType(String))
        dt.Columns.Add("CreditTerm", GetType(String))
        dt.Columns.Add("Phone", GetType(String))

        dt.Rows.Add("CT17061900001", "บริษัท ควอลิฟาย เมด โซลูชันส์ จำกัด", "อำเภอไม่ระบุ จังหวัดไม่ระบุ 00000", "-", "-")
        dt.Rows.Add("P775", "บริษัท ฤทธา จำกัด", "94 ซอยพหลโยธิน 32 ถนนพหลโยธิน จันทรเกษม เขตจตุจักร กรุงเทพมหานคร 10900", "120 วัน", "021111")
        dt.Rows.Add("G001", "กรมแพทย์ทหารเรือ", "504 ถนน สมเด็จพระเจ้าตากสิน บุคคโล เขตธนบุรี กรุงเทพมหานคร 00000", "-", "-")
        dt.Rows.Add("G002", "กรมแพทย์ทหารอากาศ", "ถนนพหลโยธิน บางเขน กรุงเทพฯ", "-", "-")
        dt.Rows.Add("G200", "กรมแพทย์ทหารอากาศ กองบัญชาการสนับสนุนทหารอากาศ", "แขวงคลองถนน เขตสายไหม กรุงเทพมหานคร 00000", "-", "-")
        dt.Rows.Add("G988", "คณะสัตวแพทยศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย", "ภาควิชาสรีรวิทยา . เขตปทุมวัน กรุงเทพมหานคร 10330", "60 วัน", "0994000158")
        dt.Rows.Add("3-D011", "บริษัท กรุงเทพอินเตอร์โปรดักส์ จำกัด (สำนักงานใหญ่)", "91/247-251 ถนนสุวินทวงศ์ แขวงมีนบุรี เขตมีนบุรี กรุงเทพมหานคร 10510", "60 วัน", "0105537110503")
        dt.Rows.Add("3-D001", "ทัสนัยแล็บ", "อ.เมือง จ.อุตรดิตถ์", "-", "-")
        dt.Rows.Add("D002", "โรงพยาบาลสมเด็จพยุพราชเชียงของ", "อ.เชียงของ จ.เชียงราย", "-", "-")
        dt.Rows.Add("D004", "โรงพยาบาลบรรพตพิสัย", "อ.บรรพตพิสัย จ.นครสวรรค์", "21 วัน", "0814742797")
        dt.Rows.Add("D007", "วรรณาแล็บ", "จ.พิจิตร", "-", "-")
        dt.Rows.Add("3-D400", "หจก. แล็บไลน์ (2000) (สำนักงานใหญ่)", "148/28 ม.14 ต. วัดไทรย์ อ.เมืองนครสวรรค์ จ.นครสวรรค์ 60000", "เงินสด", "081-8888805")
        dt.Rows.Add("2-G67176", "โรงพยาบาลบ้านนาสาร", "83/4 ถนนคลองหา ต.นาสาร อ.บ้านนาสาร จ.สุราษฎร์ธานี 84120", "60 วัน", "0994000566")

        DataGridView1.DataSource = dt

        If DataGridView1.Columns.Contains("CustomerCode") Then DataGridView1.Columns("CustomerCode").HeaderText = "รหัสลูกค้า"
        If DataGridView1.Columns.Contains("CustomerName") Then DataGridView1.Columns("CustomerName").HeaderText = "ชื่อลูกค้า"
        If DataGridView1.Columns.Contains("ContactInfo") Then DataGridView1.Columns("ContactInfo").HeaderText = "ที่อยู่/รายละเอียดติดต่อ"
        If DataGridView1.Columns.Contains("CreditTerm") Then DataGridView1.Columns("CreditTerm").HeaderText = "เครดิต"
        If DataGridView1.Columns.Contains("Phone") Then DataGridView1.Columns("Phone").HeaderText = "เบอร์โทร"

        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        If DataGridView1.Columns.Contains("CustomerCode") Then DataGridView1.Columns("CustomerCode").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        If DataGridView1.Columns.Contains("CreditTerm") Then DataGridView1.Columns("CreditTerm").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        If DataGridView1.Columns.Contains("Phone") Then DataGridView1.Columns("Phone").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        If DataGridView1.Columns.Contains("CustomerName") Then DataGridView1.Columns("CustomerName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        If DataGridView1.Columns.Contains("ContactInfo") Then DataGridView1.Columns("ContactInfo").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

    End Sub

    Private Sub frmNamelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class