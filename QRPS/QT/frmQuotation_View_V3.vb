Imports System.Data
Imports System.Windows.Forms

Public Class frmQuotation_View_V3

    ' ===================================================================================================
    ' 1. Global Variables (ตัวแปรระดับ Class)
    ' ===================================================================================================
    Private dtOriginal As DataTable
    Private dv As DataView
    Private searchConditions As New System.Collections.Generic.Dictionary(Of String, String)()
    Private dtTransactionLog As DataTable
    Private dvTransactionLog As DataView ' DataView สำหรับ DataGridView2
    Private dvLogInitialFilter As String = "" ' สำหรับเก็บ filter เริ่มต้นของ Log (ถ้ามี)

    ' ตัวแปรสถานะ:
    Private isSearchExecuted As Boolean = False  ' สถานะว่าเคย Search ข้อมูลหลักสำเร็จแล้วหรือไม่
    Private searchInitialState As String         ' สถานะของ Controls ส่วน Search ที่ถูก Search ไปล่าสุด


    ' ===================================================================================================
    ' 2. State Management Helpers (ตัวช่วยในการจัดการสถานะปุ่มและ Controls)
    ' ***************************************************************************************************
    ' ** ส่วนนี้คือโค้ดที่คุณสามารถปรับแต่งเงื่อนไขการเปิด/ปิด ปุ่มได้อย่างสะดวก **
    ' ***************************************************************************************************
#Region "State Management"

    ' Sub สำหรับตั้งค่าสถานะของ Controls ในส่วน Filter (ส่วนที่ 2)
    Private Sub SetFilterControlState(ByVal enabled As Boolean)
        TextBox_Keyword.Enabled = enabled
        ComboBox_Status.Enabled = enabled
        ComboBox_Catagories.Enabled = enabled
        ComboBox_Group.Enabled = enabled
        ComboBox_Type.Enabled = enabled
        Button_ResetFilter.Enabled = enabled
        ' CheckBox_CreatedByMe.Enabled = True ' โดยปกติ CheckBox ตัวนี้ควรเปิดตลอด
    End Sub

    ' Function สำหรับรวมค่าจาก Search Controls (ส่วนที่ 1) เพื่อใช้เปรียบเทียบสถานะ
    Private Function GetCurrentSearchState() As String
        Dim stateBuilder As New System.Text.StringBuilder()

        stateBuilder.Append(TextBox_Search.Text.Trim().ToUpper())
        stateBuilder.Append("|")
        If ComboBox_CreateBy.SelectedIndex > -1 Then stateBuilder.Append(ComboBox_CreateBy.SelectedItem.ToString())
        stateBuilder.Append("|")
        If ComboBox_Sale.SelectedIndex > -1 Then stateBuilder.Append(ComboBox_Sale.SelectedItem.ToString())

        Return stateBuilder.ToString()
    End Function

    ' Sub สำหรับตั้งค่าสถานะของ Button_Search (เงื่อนไขหลักในการเปิด/ปิดปุ่มค้นหา)
    Private Sub SetSearchButtonState()
        Dim currentState As String = GetCurrentSearchState()

        If isSearchExecuted Then
            ' ถ้าเคย Search แล้ว:
            ' ปุ่ม Search จะเปิดใช้งานได้ต่อเมื่อสถานะปัจจุบันไม่ตรงกับสถานะที่ Search ไปแล้วเท่านั้น
            Button_Search.Enabled = (currentState <> searchInitialState)
        Else
            ' ถ้ายังไม่เคย Search (สถานะเริ่มต้น):
            ' ปุ่ม Search จะเปิดใช้งานเมื่อมีค่าในช่อง Search หลัก หรือมีการเลือก ComboBox อย่างน้อยหนึ่งช่อง
            Button_Search.Enabled = (Not String.IsNullOrWhiteSpace(TextBox_Search.Text) OrElse ComboBox_CreateBy.SelectedIndex > -1 OrElse ComboBox_Sale.SelectedIndex > -1)
        End If
    End Sub

#End Region
    ' ***************************************************************************************************


    ' ===================================================================================================
    '  Form Events & Properties
    ' ===================================================================================================

    ' Event: Form Load (เริ่มต้น) 
    Private Sub frmQuotation_View_V3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadMockupData()

        If dv IsNot Nothing Then
            dv.RowFilter = "1=0"
        End If

        isSearchExecuted = False
        SetSearchButtonState()
        SetFilterControlState(False)
        ApplyTransactionLogFilter()

        searchInitialState = GetCurrentSearchState()
    End Sub

    Public Property SearchText As String
        Get
            Return TextBox_Search.Text
        End Get
        Set(value As String)
            TextBox_Search.Text = value
            SetSearchButtonState()
        End Set
    End Property


    ' ===================================================================================================
    '  Event Handlers: การจัดการ Search/Filter
    ' ===================================================================================================

    ' ********************
    ' ** Event สำหรับ Section 1 (Search) **
    ' ********************
    Private Sub TextBox_Search_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Search.TextChanged
        SetSearchButtonState()
    End Sub

    Private Sub ComboBox_CreateBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_CreateBy.SelectedIndexChanged
        SetSearchButtonState()
    End Sub

    Private Sub ComboBox_Sale_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Sale.SelectedIndexChanged
        SetSearchButtonState()
    End Sub

    ' เมื่อกดปุ่ม Search
    Private Sub Button_Search_Click(sender As Object, e As EventArgs) Handles Button_Search.Click

        PrepareAndExecuteSearch()
        isSearchExecuted = True
        searchInitialState = GetCurrentSearchState()

        SetSearchButtonState()
        SetFilterControlState(True)
    End Sub

    ' Reset หลัก - เคลียร์ Search และ Filter ทั้งหมด
    Private Sub Button_Reset_Click(sender As Object, e As EventArgs) Handles Button_Reset.Click

        TextBox_Search.Clear()
        ComboBox_CreateBy.SelectedIndex = -1
        ComboBox_Sale.SelectedIndex = -1
        Button_ResetFilter_Click(Nothing, Nothing)
        searchConditions.Clear()

        If dv IsNot Nothing Then
            dv.RowFilter = "1=0"
        End If

        isSearchExecuted = False
        SetSearchButtonState()
        SetFilterControlState(False)
        searchInitialState = GetCurrentSearchState()
    End Sub


    ' ********************
    ' ** Event สำหรับ Section 2 (Filter) **
    ' ********************

    Private Sub CheckBox_CreatedByMe_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_CreatedByMe.CheckedChanged
        ApplyTransactionLogFilter()
    End Sub

    Private Sub ComboBox_Status_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Status.SelectedIndexChanged
        If isSearchExecuted Then PerformSearchOrFilter()
    End Sub

    Private Sub TextBox_Keyword_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Keyword.TextChanged
        If isSearchExecuted Then PerformSearchOrFilter()
    End Sub

    Private Sub ComboBox_Catagories_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Catagories.SelectedIndexChanged
        If isSearchExecuted Then PerformSearchOrFilter()
    End Sub

    Private Sub ComboBox_Group_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Group.SelectedIndexChanged
        If isSearchExecuted Then PerformSearchOrFilter()
    End Sub

    Private Sub ComboBox_Type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_Type.SelectedIndexChanged
        If isSearchExecuted Then PerformSearchOrFilter()
    End Sub

    ' Reset Filter (เฉพาะส่วนล่าง)
    Private Sub Button_ResetFilter_Click(sender As Object, e As EventArgs) Handles Button_ResetFilter.Click
        ComboBox_Catagories.SelectedIndex = -1
        ComboBox_Group.SelectedIndex = -1
        ComboBox_Type.SelectedIndex = -1
        ComboBox_Status.SelectedIndex = -1
        TextBox_Keyword.Clear()

        ' กรองโดยใช้เฉพาะเงื่อนไข Search (บน) ที่ถูก Search ไปล่าสุด
        If isSearchExecuted Then PerformSearchOrFilter()
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
    End Sub

    ' ===================================================================================================
    ' 5. Core Logic Subs (ฟังก์ชันตรรกะหลัก: Search, Filter, Load Data)
    ' ===================================================================================================

    Private Sub ApplyTransactionLogFilter()
        If dvTransactionLog IsNot Nothing Then
            If CheckBox_CreatedByMe.Checked Then
                dvTransactionLog.RowFilter = "สร้างเอกสารโดย = 'admin'"
            Else
                dvTransactionLog.RowFilter = String.Empty
            End If
        End If
    End Sub

    Private Sub PrepareAndExecuteSearch()
        searchConditions.Clear()
        Dim customerSearch As String = TextBox_Search.Text.Trim()
        If Not String.IsNullOrEmpty(customerSearch) Then
            searchConditions.Add("CustomerSearch", String.Format("ลูกค้า LIKE '%{0}%'", customerSearch.Replace("'", "''")))
        End If

        If ComboBox_CreateBy.SelectedIndex > -1 AndAlso Not String.IsNullOrEmpty(ComboBox_CreateBy.SelectedItem.ToString()) Then
            Dim createBy As String = ComboBox_CreateBy.SelectedItem.ToString()
            searchConditions.Add("CreateBySearch", String.Format("สร้างเอกสารโดย = '{0}'", createBy.Replace("'", "''")))
        End If

        If ComboBox_Sale.SelectedIndex > -1 AndAlso Not String.IsNullOrEmpty(ComboBox_Sale.SelectedItem.ToString()) Then
            Dim selectedSale As String = ComboBox_Sale.SelectedItem.ToString()
            searchConditions.Add("SaleSearch", String.Format("พนักงานขาย = '{0}'", selectedSale.Replace("'", "''")))
        End If

        PerformSearchOrFilter()
    End Sub


    Private Sub PerformSearchOrFilter()
        If dv IsNot Nothing Then

            If Not isSearchExecuted AndAlso searchConditions.Count = 0 Then
                dv.RowFilter = "1=0"
                Exit Sub
            End If

            Dim filterString As New System.Text.StringBuilder()
            Dim conditions As New System.Collections.Generic.List(Of String)()

            ' ************************************
            ' ** Search (ส่วนบน)          **
            ' ************************************
            For Each condition In searchConditions.Values
                conditions.Add(condition)
            Next

            If isTemplateActive Then
                conditions.Add("ประเภทเอกสาร = 'Template'")
            End If
            ' ************************************
            ' ** Filter (ส่วนล่าง)        **
            ' ************************************

            ' ComboBox_Status (สถานะเอกสาร)
            If ComboBox_Status.SelectedIndex > -1 AndAlso Not String.IsNullOrEmpty(ComboBox_Status.SelectedItem.ToString()) Then
                Dim selectedStatus As String = ComboBox_Status.SelectedItem.ToString()
                conditions.Add(String.Format("สภานะเอกสาร = '{0}'", selectedStatus.Replace("'", "''")))
            End If

            ' TextBox_Keyword (ค้นหาใน เลขที่เอกสาร OR ลูกค้า)
            Dim keyword As String = TextBox_Keyword.Text.Trim()
            If Not String.IsNullOrEmpty(keyword) Then
                Dim keywordCondition As String
                Dim safeKeyword = keyword.Replace("'", "''")

                ' เงื่อนไข OR สำหรับ Keyword
                keywordCondition = String.Format("(เลขที่เอกสาร LIKE '%{0}%' OR ลูกค้า LIKE '%{0}%')", safeKeyword)

                conditions.Add(keywordCondition)
            End If

            ' ComboBox_Catagories (Category)
            If ComboBox_Catagories.SelectedIndex > -1 AndAlso Not String.IsNullOrEmpty(ComboBox_Catagories.SelectedItem.ToString()) Then
                Dim category As String = ComboBox_Catagories.SelectedItem.ToString()
                conditions.Add(String.Format("ประเภทเอกสาร = '{0}'", category.Replace("'", "''")))
            End If

            ' ComboBox_Group 
            If ComboBox_Group.SelectedIndex > -1 AndAlso Not String.IsNullOrEmpty(ComboBox_Group.SelectedItem.ToString()) Then
                Dim groupValue As String = ComboBox_Group.SelectedItem.ToString()
                conditions.Add(String.Format("[ประเภทเอกสาร] = '{0}'", groupValue.Replace("'", "''")))
            End If

            ' ComboBox_Type
            If ComboBox_Type.SelectedIndex > -1 AndAlso Not String.IsNullOrEmpty(ComboBox_Type.SelectedItem.ToString()) Then
                Dim typeValue As String = ComboBox_Type.SelectedItem.ToString()
                conditions.Add(String.Format("[ประเภทเอกสาร] = '{0}'", typeValue.Replace("'", "''")))
            End If

            If conditions.Count > 0 Then
                filterString.Append(String.Join(" AND ", conditions.ToArray()))
            End If

            dv.RowFilter = filterString.ToString()
        End If


    End Sub

    Private Sub LoadMockupData()
        ' *****************************************************************
        ' ** 6.1 DataGridView1 (dtOriginal) Initialization - 20 Rows **
        ' *****************************************************************
        If dtOriginal Is Nothing Then
            Dim dt As New DataTable("MainTable")

            ' กำหนด Columns
            dt.Columns.Add("เลขที่เอกสาร", GetType(String))
            dt.Columns.Add("วันที่สร้างเอกสาร", GetType(String))
            dt.Columns.Add("ประเภทเอกสาร", GetType(String))
            dt.Columns.Add("ลูกค้า", GetType(String))
            dt.Columns.Add("พนักงานขาย", GetType(String))
            dt.Columns.Add("จำนวนเงิน", GetType(Decimal))
            dt.Columns.Add("สภานะเอกสาร", GetType(String))
            dt.Columns.Add("ยืนราคา", GetType(String))
            dt.Columns.Add("ส่งของ", GetType(String))
            dt.Columns.Add("สร้างเอกสารโดย", GetType(String))
            dt.Columns.Add("วันที่อนุมัติ", GetType(String))
            dt.Columns.Add("ผู้มีอำนาจลงนาม", GetType(String))
            dt.Rows.Add("QMS2511013", "2025/11/10 10:00", "One-time", "บริษัท ควอลิฟาย เมด โซลูชันส์ จำกัด", "1-CO", 185000.0, "Approved", "60", "2025/12/30", "admin", "2025/11/10", "ADMIN")
            dt.Rows.Add("QMS2511012", "2025/11/09 14:30", "Price agreement", "บริษัท ฤทธา จำกัด", "1-SER", 850000.5, "Draft", "90", "2026/01/15", "adisorn", "-", "ADMIN")
            dt.Rows.Add("QMS2511011", "2025/11/08 09:15", "One-time", "กรมแพทย์ทหารเรือ", "1-C&Y", 45000.0, "Approved", "30", "2025/12/01", "4594", "2025/11/08", "ADMIN")
            dt.Rows.Add("QMS2511010", "2025/11/07 16:00", "Template", "กรมแพทย์ทหารอากาศ", "1-C&Y", 12345.0, "Manager Review", "60", "2025/12/07", "chalee", "-", "MANAGER")
            dt.Rows.Add("QMS2510009", "2025/10/25 11:45", "Price agreement", "กรมแพทย์ทหารอากาศ กองบัญชาการสนับสนุนทหารอากาศ", "1-TPT", 250000.75, "Reject", "365", "2026/10/25", "APICHART", "-", "MANAGER")
            dt.Rows.Add("QMS2510008", "2025/10/20 13:00", "One-time", "คณะสัตวแพทยศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย", "1-CO", 7900.0, "Completed", "30", "2025/11/20", "admin", "2025/10/20", "ADMIN")
            dt.Rows.Add("QMS2510007", "2025/10/16 11:40", "Supporting Doc", "บริษัท กรุงเทพอินเตอร์โปรดักส์ จำกัด (สำนักงานใหญ่)", "1-C&Y", 324500.85, "Request Approve", "60", "2025/12/16", "chanon", "-", "ADMIN")
            dt.Rows.Add("QMS2510006", "2025/10/15 11:00", "Price agreement", "ทัสนัยแล็บ", "ไม่ ระบุ", 320500.85, "Draft", "30", "2025/11/15", "jantratip", "-", "ADMIN")
            dt.Rows.Add("QMS2510005", "2025/10/05 10:09", "One-time", "โรงพยาบาลสมเด็จพยุพราชเชียงของ", "1-CO", 12000.0, "Approved", "90", "2026/01/05", "admin", "2025/10/06", "ADMIN")
            dt.Rows.Add("QMS2509004", "2025/09/25 09:09", "Template", "โรงพยาบาลบรรพตพิสัย", "1-CO", 15000.0, "Invalid", "120", "2026/01/25", "adisorn", "-", "ADMIN")
            dt.Rows.Add("QMS2509003", "2025/09/03 09:45", "Price agreement", "วรรณาแล็บ", "1-C&Y", 7900.0, "Cancel", "30", "2025/10/03", "4594", "2025/09/03", "ADMIN")
            dt.Rows.Add("QMS2508002", "2025/08/02 11:02", "Supporting Doc", "หจก. แล็บไลน์ (2000) (สำนักงานใหญ่)", "1-TPT", 45000.5, "Draft", "365", "2026/08/02", "chalee", "-", "ADMIN")
            dt.Rows.Add("QMS2508001", "2025/08/01 11:12", "One-time", "โรงพยาบาลบ้านนาสาร", "กชกร แสงไกร", 155000.0, "Approved", "60", "2025/09/30", "chanon", "2025/08/01", "ADMIN")
            dt.Rows.Add("QMS2507001", "2025/07/01 09:00", "One-time", "บริษัท สบายใจ โซลูชั่น", "1-C&Y", 100000.0, "Approved", "30", "2025/08/01", "chanon", "2025/07/01", "ADMIN")
            dt.Rows.Add("QMS2507002", "2025/07/05 14:00", "Price agreement", "โรงเรียนเทพศิรินทร์", "1-TPT", 5000.0, "Draft", "90", "2025/10/05", "admin", "-", "ADMIN")
            dt.Rows.Add("QMS2506003", "2025/06/10 10:00", "Template", "กองทัพเรือ", "1-C&Y", 450000.0, "Approved", "60", "2025/08/10", "4594", "2025/06/10", "ADMIN")
            dt.Rows.Add("QMS2506004", "2025/06/15 15:00", "One-time", "บริษัท เอ็กซ์วายแซด จำกัด", "1-CO", 15000.0, "Manager Review", "30", "2025/07/15", "chalee", "-", "MANAGER")
            dt.Rows.Add("QMS2505005", "2025/05/01 11:00", "Price agreement", "หอการค้าไทย", "1-TPT", 75000.0, "Reject", "365", "2026/05/01", "APICHART", "-", "MANAGER")
            dt.Rows.Add("QMS2504006", "2025/04/01 10:00", "Supporting Doc", "กระทรวงเกษตรฯ", "1-CO", 50000.0, "Completed", "30", "2025/05/01", "admin", "2025/04/01", "ADMIN")
            dt.Rows.Add("QMS2503007", "2025/03/15 12:00", "One-time", "องค์การบริหารส่วนจังหวัด", "1-SER", 29900.0, "Request Approve", "120", "2025/07/15", "adisorn", "-", "ADMIN")


            dtOriginal = dt.Copy()

            dv = New DataView(dtOriginal)
            DataGridView1.DataSource = dv

            If DataGridView1.Columns.Count > 0 Then
                If DataGridView1.Columns.Contains("เลขที่เอกสาร") Then DataGridView1.Columns("เลขที่เอกสาร").HeaderText = "เลขที่เอกสาร"
                If DataGridView1.Columns.Contains("วันที่สร้างเอกสาร") Then DataGridView1.Columns("วันที่สร้างเอกสาร").HeaderText = "วันที่สร้าง"
                If DataGridView1.Columns.Contains("ประเภทเอกสาร") Then DataGridView1.Columns("ประเภทเอกสาร").HeaderText = "ประเภท"
                If DataGridView1.Columns.Contains("ลูกค้า") Then DataGridView1.Columns("ลูกค้า").HeaderText = "ลูกค้า"
                If DataGridView1.Columns.Contains("พนักงานขาย") Then DataGridView1.Columns("พนักงานขาย").HeaderText = "เซลล์"
                If DataGridView1.Columns.Contains("จำนวนเงิน") Then
                    DataGridView1.Columns("จำนวนเงิน").HeaderText = "ยอดรวม"
                    DataGridView1.Columns("จำนวนเงิน").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    DataGridView1.Columns("จำนวนเงิน").DefaultCellStyle.Format = "N2"
                End If
                If DataGridView1.Columns.Contains("สภานะเอกสาร") Then DataGridView1.Columns("สภานะเอกสาร").HeaderText = "สถานะ"
                If DataGridView1.Columns.Contains("ยืนราคา") Then DataGridView1.Columns("ยืนราคา").HeaderText = "ยืนราคา"
                If DataGridView1.Columns.Contains("ส่งของ") Then DataGridView1.Columns("ส่งของ").HeaderText = "กำหนดส่ง"
                If DataGridView1.Columns.Contains("สร้างเอกสารโดย") Then DataGridView1.Columns("สร้างเอกสารโดย").HeaderText = "ผู้สร้าง"
                If DataGridView1.Columns.Contains("วันที่อนุมัติ") Then DataGridView1.Columns("วันที่อนุมัติ").HeaderText = "วันอนุมัติ"
                If DataGridView1.Columns.Contains("ผู้มีอำนาจลงนาม") Then DataGridView1.Columns("ผู้มีอำนาจลงนาม").HeaderText = "ผู้อนุมัติ"
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                If DataGridView1.Columns.Contains("เลขที่เอกสาร") Then DataGridView1.Columns("เลขที่เอกสาร").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                If DataGridView1.Columns.Contains("วันที่สร้างเอกสาร") Then DataGridView1.Columns("วันที่สร้างเอกสาร").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                If DataGridView1.Columns.Contains("ประเภทเอกสาร") Then DataGridView1.Columns("ประเภทเอกสาร").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                If DataGridView1.Columns.Contains("พนักงานขาย") Then DataGridView1.Columns("พนักงานขาย").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                'If DataGridView1.Columns.Contains("จำนวนเงิน") Then DataGridView1.Columns("จำนวนเงิน").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                If DataGridView1.Columns.Contains("สภานะเอกสาร") Then DataGridView1.Columns("สภานะเอกสาร").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                If DataGridView1.Columns.Contains("ลูกค้า") Then DataGridView1.Columns("ลูกค้า").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                'If DataGridView1.Columns.Contains("ยืนราคา") Then DataGridView1.Columns("ยืนราคา").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                'If DataGridView1.Columns.Contains("ส่งของ") Then DataGridView1.Columns("ส่งของ").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                'If DataGridView1.Columns.Contains("สร้างเอกสารโดย") Then DataGridView1.Columns("สร้างเอกสารโดย").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                'If DataGridView1.Columns.Contains("วันที่อนุมัติ") Then DataGridView1.Columns("วันที่อนุมัติ").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                'If DataGridView1.Columns.Contains("ผู้มีอำนาจลงนาม") Then DataGridView1.Columns("ผู้มีอำนาจลงนาม").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            End If
        End If

        ' *****************************************************************
        ' ** 6.2 DataGridView2 (Transaction Log) Initialization - 20 Rows **
        ' *****************************************************************
        If dtTransactionLog Is Nothing Then
            Dim dtLog As New DataTable("TransactionLog")

            dtLog.Columns.Add("เลขที่เอกสาร", GetType(String))
            dtLog.Columns.Add("วันที่สร้างเอกสาร", GetType(String))
            dtLog.Columns.Add("ประเภทเอกสาร", GetType(String))
            dtLog.Columns.Add("ลูกค้า", GetType(String))
            dtLog.Columns.Add("พนักงานขาย", GetType(String))
            dtLog.Columns.Add("จำนวนเงิน", GetType(Decimal))
            dtLog.Columns.Add("สภานะเอกสาร", GetType(String))
            dtLog.Columns.Add("ยืนราคา", GetType(String))
            dtLog.Columns.Add("ส่งของ", GetType(String))
            dtLog.Columns.Add("สร้างเอกสารโดย", GetType(String))
            dtLog.Columns.Add("วันที่อนุมัติ", GetType(String))
            dtLog.Columns.Add("ผู้มีอำนาจลงนาม", GetType(String))
            dtLog.Columns.Add("วันที่อัปเดต", GetType(DateTime))

            ' ข้อมูล Mockup 20 แถวสำหรับ Log (เรียงลำดับตาม Update Date ล่าสุด)
            dtLog.Rows.Add("QMS2511014", "2025/11/10 13:00", "One-time", "บริษัท ใหม่ล่าสุดจำกัด", "1-CO", 99000.0, "Draft", "60", "2025/12/30", "admin", "-", "ADMIN", #11/10/2025 01:30:00 PM#)
            dtLog.Rows.Add("QMS2510009", "2025/10/25 11:45", "Price agreement", "กรมแพทย์ทหารอากาศ กองบัญชาการสนับสนุนทหารอากาศ", "1-TPT", 250000.75, "Reject", "365", "2026/10/25", "APICHART", "-", "MANAGER", #11/10/2025 12:30:00 PM#)
            dtLog.Rows.Add("QMS2510005", "2025/10/05 10:09", "One-time", "โรงพยาบาลสมเด็จพยุพราชเชียงของ", "1-CO", 12000.0, "Approved", "90", "2026/01/05", "admin", "2025/10/06", "ADMIN", #11/10/2025 11:30:00 AM#)
            dtLog.Rows.Add("QMS2511013", "2025/11/10 10:00", "One-time", "บริษัท ควอลิฟาย เมด โซลูชันส์ จำกัด", "1-CO", 185000.0, "Approved", "60", "2025/12/30", "admin", "2025/11/10", "ADMIN", #11/10/2025 10:05:00 AM#)
            dtLog.Rows.Add("QMS2508001", "2025/08/01 11:12", "One-time", "โรงพยาบาลบ้านนาสาร", "กชกร แสงไกร", 155000.0, "Reject", "60", "2025/09/30", "chanon", "2025/08/01", "ADMIN", #11/10/2025 09:00:00 AM#)
            dtLog.Rows.Add("QMS2509003", "2025/09/03 09:45", "Price agreement", "วรรณาแล็บ", "1-C&Y", 7900.0, "Cancel", "30", "2025/10/03", "4594", "2025/09/03", "ADMIN", #11/09/2025 03:00:00 PM#)
            dtLog.Rows.Add("QMS2511012", "2025/11/09 14:30", "Price agreement", "บริษัท ฤทธา จำกัด", "1-SER", 850000.5, "Draft", "90", "2026/01/15", "adisorn", "-", "ADMIN", #11/09/2025 02:30:00 PM#)
            dtLog.Rows.Add("QMS2510008", "2025/10/20 13:00", "One-time", "คณะสัตวแพทยศาสตร์ จุฬาลงกรณ์มหาวิทยาลัย", "1-CO", 7900.0, "Completed", "30", "2025/11/20", "admin", "2025/10/20", "ADMIN", #11/09/2025 01:00:00 PM#)
            dtLog.Rows.Add("QMS2510007", "2025/10/16 11:40", "Supporting Doc", "บริษัท กรุงเทพอินเตอร์โปรดักส์ จำกัด (สำนักงานใหญ่)", "1-C&Y", 324500.85, "Request Approve", "60", "2025/12/16", "chanon", "-", "ADMIN", #11/09/2025 10:00:00 AM#)
            dtLog.Rows.Add("QMS2511011", "2025/11/08 09:15", "One-time", "กรมแพทย์ทหารเรือ", "1-C&Y", 45000.0, "Approved", "30", "2025/12/01", "4594", "2025/11/08", "ADMIN", #11/08/2025 04:00:00 PM#)
            dtLog.Rows.Add("QMS2508002", "2025/08/02 11:02", "Supporting Doc", "หจก. แล็บไลน์ (2000) (สำนักงานใหญ่)", "1-TPT", 45000.5, "Draft", "365", "2026/08/02", "chalee", "-", "ADMIN", #11/08/2025 02:00:00 PM#)
            dtLog.Rows.Add("QMS2510006", "2025/10/15 11:00", "Price agreement", "ทัสนัยแล็บ", "ไม่ ระบุ", 320500.85, "Draft", "30", "2025/11/15", "jantratip", "-", "ADMIN", #11/08/2025 11:00:00 AM#)
            dtLog.Rows.Add("QMS2511010", "2025/11/07 16:00", "Template", "กรมแพทย์ทหารอากาศ", "1-C&Y", 12345.0, "Manager Review", "60", "2025/12/07", "chalee", "-", "MANAGER", #11/07/2025 05:00:00 PM#)
            dtLog.Rows.Add("QMS2509004", "2025/09/25 09:09", "Template", "โรงพยาบาลบรรพตพิสัย", "1-CO", 15000.0, "Invalid", "120", "2026/01/25", "adisorn", "-", "ADMIN", #11/07/2025 03:00:00 PM#)
            dtLog.Rows.Add("QMS2507001", "2025/07/01 09:00", "One-time", "บริษัท สบายใจ โซลูชั่น", "1-C&Y", 100000.0, "Approved", "30", "2025/08/01", "chanon", "2025/07/01", "ADMIN", #11/06/2025 11:00:00 AM#)
            dtLog.Rows.Add("QMS2507002", "2025/07/05 14:00", "Price agreement", "โรงเรียนเทพศิรินทร์", "1-TPT", 5000.0, "Draft", "90", "2025/10/05", "admin", "-", "ADMIN", #11/06/2025 10:00:00 AM#)
            dtLog.Rows.Add("QMS2506003", "2025/06/10 10:00", "Template", "กองทัพเรือ", "1-C&Y", 450000.0, "Approved", "60", "2025/08/10", "4594", "2025/06/10", "ADMIN", #11/05/2025 01:00:00 PM#)
            dtLog.Rows.Add("QMS2506004", "2025/06/15 15:00", "One-time", "บริษัท เอ็กซ์วายแซด จำกัด", "1-CO", 15000.0, "Manager Review", "30", "2025/07/15", "chalee", "-", "MANAGER", #11/05/2025 12:00:00 PM#)
            dtLog.Rows.Add("QMS2505005", "2025/05/01 11:00", "Price agreement", "หอการค้าไทย", "1-TPT", 75000.0, "Reject", "365", "2026/05/01", "APICHART", "-", "MANAGER", #11/05/2025 10:00:00 AM#)
            dtLog.Rows.Add("QMS2504006", "2025/04/01 10:00", "Supporting Doc", "กระทรวงเกษตรฯ", "1-CO", 50000.0, "Completed", "30", "2025/05/01", "admin", "2025/04/01", "ADMIN", #11/04/2025 09:00:00 AM#)

            dtTransactionLog = dtLog.Copy()
            dvTransactionLog = New DataView(dtTransactionLog)
            dvTransactionLog.Sort = "วันที่อัปเดต DESC"
            DataGridView2.DataSource = dvTransactionLog

            If DataGridView2.Columns.Count > 0 Then
                If DataGridView2.Columns.Contains("เลขที่เอกสาร") Then DataGridView2.Columns("เลขที่เอกสาร").HeaderText = "เลขที่เอกสาร"
                If DataGridView2.Columns.Contains("วันที่สร้างเอกสาร") Then DataGridView2.Columns("วันที่สร้างเอกสาร").HeaderText = "วันที่สร้าง"
                If DataGridView2.Columns.Contains("ประเภทเอกสาร") Then DataGridView2.Columns("ประเภทเอกสาร").HeaderText = "ประเภท"
                If DataGridView2.Columns.Contains("ลูกค้า") Then DataGridView2.Columns("ลูกค้า").HeaderText = "ลูกค้า"
                If DataGridView2.Columns.Contains("พนักงานขาย") Then DataGridView2.Columns("พนักงานขาย").HeaderText = "เซลล์"
                If DataGridView2.Columns.Contains("จำนวนเงิน") Then
                    DataGridView2.Columns("จำนวนเงิน").HeaderText = "ยอดรวม"
                    DataGridView2.Columns("จำนวนเงิน").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    DataGridView2.Columns("จำนวนเงิน").DefaultCellStyle.Format = "N2"
                End If
                If DataGridView2.Columns.Contains("สภานะเอกสาร") Then DataGridView2.Columns("สภานะเอกสาร").HeaderText = "สถานะ"
                If DataGridView2.Columns.Contains("สร้างเอกสารโดย") Then DataGridView2.Columns("สร้างเอกสารโดย").HeaderText = "ผู้สร้าง"

                ' ซ่อนคอลัมน์ที่ไม่จำเป็นต้องแสดงใน Log 
                If DataGridView2.Columns.Contains("ยืนราคา") Then DataGridView2.Columns("ยืนราคา").Visible = False
                If DataGridView2.Columns.Contains("ส่งของ") Then DataGridView2.Columns("ส่งของ").Visible = False
                If DataGridView2.Columns.Contains("วันที่อนุมัติ") Then DataGridView2.Columns("วันที่อนุมัติ").Visible = False
                If DataGridView2.Columns.Contains("ผู้มีอำนาจลงนาม") Then DataGridView2.Columns("ผู้มีอำนาจลงนาม").Visible = False
                If DataGridView2.Columns.Contains("วันที่อัปเดต") Then DataGridView2.Columns("วันที่อัปเดต").Visible = False

                DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
                If DataGridView2.Columns.Contains("เลขที่เอกสาร") Then DataGridView2.Columns("เลขที่เอกสาร").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                If DataGridView2.Columns.Contains("วันที่สร้างเอกสาร") Then DataGridView2.Columns("วันที่สร้างเอกสาร").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                If DataGridView2.Columns.Contains("ประเภทเอกสาร") Then DataGridView2.Columns("ประเภทเอกสาร").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                If DataGridView2.Columns.Contains("พนักงานขาย") Then DataGridView2.Columns("พนักงานขาย").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                If DataGridView2.Columns.Contains("จำนวนเงิน") Then DataGridView2.Columns("จำนวนเงิน").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                If DataGridView2.Columns.Contains("สภานะเอกสาร") Then DataGridView2.Columns("สภานะเอกสาร").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                If DataGridView2.Columns.Contains("ลูกค้า") Then DataGridView2.Columns("ลูกค้า").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                'If DataGridView2.Columns.Contains("สร้างเอกสารโดย") Then DataGridView2.Columns("สร้างเอกสารโดย").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

                DataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                DataGridView2.MultiSelect = False
            End If
        End If
    End Sub

    ' ===================================================================================================
    ' 6. Unused Handlers 
    ' ===================================================================================================

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    ' ประกาศตัวแปรไว้ด้านบนสุดของ Class
    Dim isTemplateActive As Boolean = False

    Private Sub Button_Template_Click(sender As Object, e As EventArgs) Handles Button_Template.Click
        ' สลับค่า True/False
        isTemplateActive = Not isTemplateActive

        If isTemplateActive Then
            Button_Template.BackColor = Color.RoyalBlue
            Button_Template.ForeColor = Color.White
        Else
            Button_Template.BackColor = SystemColors.Control
            Button_Template.ForeColor = SystemColors.ControlText
            Button_Template.UseVisualStyleBackColor = True
        End If

        PerformSearchOrFilter()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frmAddnew As New frmAddnew()
        frmAddnew.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frmEdit As New frmEdit()
        frmEdit.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim frmPreview As New frmSelectReport()
        frmPreview.ShowDialog()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim frm As New frmDelete()

        ' วนลูปหาแถวที่ถูกเลือก (SelectedRows) ซึ่งตอนนี้คลิกตรงไหนในแถวก็ได้แล้ว
        For Each row As DataGridViewRow In DataGridView2.SelectedRows
            If Not row.IsNewRow Then
                ' ดึงข้อมูลจากคอลัมน์ "เลขที่เอกสาร"
                ' ใช้ชื่อคอลัมน์ตรงๆ เพื่อป้องกันความผิดพลาดถ้ามีการสลับที่ Index
                If DataGridView2.Columns.Contains("เลขที่เอกสาร") Then
                    frm.ItemsToDelete.Add(row.Cells("เลขที่เอกสาร").Value.ToString())
                End If
            End If
        Next

        ' ตรวจสอบและแสดง Popup
        If frm.ItemsToDelete.Count > 0 Then
            ' แสดง Popup ยืนยัน
            If frm.ShowDialog() = DialogResult.OK Then
                ' --- คำสั่งลบข้อมูลออกจาก dtTransactionLog ---
                ' ตัวอย่างการลบแถวออกจากตารางที่คุณใช้โชว์ (DataTable)
                For Each docNo As String In frm.ItemsToDelete
                    ' ค้นหาแถวใน dtTransactionLog ที่มีเลขที่เอกสารตรงกันแล้วลบทิ้ง
                    Dim rowsToDelete = dtTransactionLog.Select("[เลขที่เอกสาร] = '" & docNo & "'")
                    For Each r In rowsToDelete
                        dtTransactionLog.Rows.Remove(r)
                    Next
                Next

                MessageBox.Show("ลบเอกสารสำเร็จเรียบร้อยแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("กรุณาเลือกแถวข้อมูลที่ต้องการลบก่อน โดยการคลิกที่แถวในตาราง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Class