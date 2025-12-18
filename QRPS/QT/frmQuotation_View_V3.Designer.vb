<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmQuotation_View_V3
    Inherits System.Windows.Forms.Form

    'TESTING  ONLY'


    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button_Browse = New System.Windows.Forms.Button()
        Me.GroupBox_Filter = New System.Windows.Forms.GroupBox()
        Me.Button_ResetFilter = New System.Windows.Forms.Button()
        Me.ComboBox_Status = New System.Windows.Forms.ComboBox()
        Me.Label_Status = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ComboBox_Type = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox_Group = New System.Windows.Forms.ComboBox()
        Me.Label_Group = New System.Windows.Forms.Label()
        Me.ComboBox_Catagories = New System.Windows.Forms.ComboBox()
        Me.Label_Catagories = New System.Windows.Forms.Label()
        Me.TextBox_Keyword = New System.Windows.Forms.TextBox()
        Me.Label_Keyword = New System.Windows.Forms.Label()
        Me.Button_Template = New System.Windows.Forms.Button()
        Me.Button_Reset = New System.Windows.Forms.Button()
        Me.Button_Search = New System.Windows.Forms.Button()
        Me.ComboBox_Sale = New System.Windows.Forms.ComboBox()
        Me.Label_Sale = New System.Windows.Forms.Label()
        Me.ComboBox_CreateBy = New System.Windows.Forms.ComboBox()
        Me.Label_CreateBy = New System.Windows.Forms.Label()
        Me.TextBox_Search = New System.Windows.Forms.TextBox()
        Me.Label_NoName = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.CheckBox_CreatedByMe = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox_Filter.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Button_Browse)
        Me.GroupBox1.Controls.Add(Me.GroupBox_Filter)
        Me.GroupBox1.Controls.Add(Me.Button_Template)
        Me.GroupBox1.Controls.Add(Me.Button_Reset)
        Me.GroupBox1.Controls.Add(Me.Button_Search)
        Me.GroupBox1.Controls.Add(Me.ComboBox_Sale)
        Me.GroupBox1.Controls.Add(Me.Label_Sale)
        Me.GroupBox1.Controls.Add(Me.ComboBox_CreateBy)
        Me.GroupBox1.Controls.Add(Me.Label_CreateBy)
        Me.GroupBox1.Controls.Add(Me.TextBox_Search)
        Me.GroupBox1.Controls.Add(Me.Label_NoName)
        Me.GroupBox1.Font = New System.Drawing.Font("TH Sarabun New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(998, 333)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "เงื่อนไขการค้นหา"
        '
        'Button_Browse
        '
        Me.Button_Browse.Font = New System.Drawing.Font("TH Sarabun New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button_Browse.Location = New System.Drawing.Point(74, 23)
        Me.Button_Browse.Name = "Button_Browse"
        Me.Button_Browse.Size = New System.Drawing.Size(27, 27)
        Me.Button_Browse.TabIndex = 10
        Me.Button_Browse.Text = "..." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Button_Browse.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button_Browse.UseVisualStyleBackColor = True
        '
        'GroupBox_Filter
        '
        Me.GroupBox_Filter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox_Filter.Controls.Add(Me.Button_ResetFilter)
        Me.GroupBox_Filter.Controls.Add(Me.ComboBox_Status)
        Me.GroupBox_Filter.Controls.Add(Me.Label_Status)
        Me.GroupBox_Filter.Controls.Add(Me.DataGridView1)
        Me.GroupBox_Filter.Controls.Add(Me.ComboBox_Type)
        Me.GroupBox_Filter.Controls.Add(Me.Label1)
        Me.GroupBox_Filter.Controls.Add(Me.ComboBox_Group)
        Me.GroupBox_Filter.Controls.Add(Me.Label_Group)
        Me.GroupBox_Filter.Controls.Add(Me.ComboBox_Catagories)
        Me.GroupBox_Filter.Controls.Add(Me.Label_Catagories)
        Me.GroupBox_Filter.Controls.Add(Me.TextBox_Keyword)
        Me.GroupBox_Filter.Controls.Add(Me.Label_Keyword)
        Me.GroupBox_Filter.Location = New System.Drawing.Point(4, 51)
        Me.GroupBox_Filter.Name = "GroupBox_Filter"
        Me.GroupBox_Filter.Size = New System.Drawing.Size(983, 279)
        Me.GroupBox_Filter.TabIndex = 9
        Me.GroupBox_Filter.TabStop = False
        Me.GroupBox_Filter.Text = "กรองข้อมูล"
        '
        'Button_ResetFilter
        '
        Me.Button_ResetFilter.Location = New System.Drawing.Point(873, 16)
        Me.Button_ResetFilter.Name = "Button_ResetFilter"
        Me.Button_ResetFilter.Size = New System.Drawing.Size(104, 35)
        Me.Button_ResetFilter.TabIndex = 10
        Me.Button_ResetFilter.Text = "Reset Filter"
        Me.Button_ResetFilter.UseVisualStyleBackColor = True
        '
        'ComboBox_Status
        '
        Me.ComboBox_Status.Font = New System.Drawing.Font("TH Sarabun New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox_Status.FormattingEnabled = True
        Me.ComboBox_Status.Items.AddRange(New Object() {"Approved", "Cancel", "Completed", "Draft", "Invalid", "Manager Review", "Reject", "Request Approve"})
        Me.ComboBox_Status.Location = New System.Drawing.Point(726, 19)
        Me.ComboBox_Status.Name = "ComboBox_Status"
        Me.ComboBox_Status.Size = New System.Drawing.Size(105, 30)
        Me.ComboBox_Status.TabIndex = 17
        '
        'Label_Status
        '
        Me.Label_Status.AutoSize = True
        Me.Label_Status.Location = New System.Drawing.Point(680, 22)
        Me.Label_Status.Name = "Label_Status"
        Me.Label_Status.Size = New System.Drawing.Size(50, 28)
        Me.Label_Status.TabIndex = 16
        Me.Label_Status.Text = "Status"
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(5, 52)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(971, 221)
        Me.DataGridView1.TabIndex = 2
        '
        'ComboBox_Type
        '
        Me.ComboBox_Type.Font = New System.Drawing.Font("TH Sarabun New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox_Type.FormattingEnabled = True
        Me.ComboBox_Type.Items.AddRange(New Object() {"One-time", "Price agreement", "Supporting Doc", "Template"})
        Me.ComboBox_Type.Location = New System.Drawing.Point(574, 19)
        Me.ComboBox_Type.Name = "ComboBox_Type"
        Me.ComboBox_Type.Size = New System.Drawing.Size(103, 30)
        Me.ComboBox_Type.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(518, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 28)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "ประเภท"
        '
        'ComboBox_Group
        '
        Me.ComboBox_Group.Font = New System.Drawing.Font("TH Sarabun New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox_Group.FormattingEnabled = True
        Me.ComboBox_Group.Items.AddRange(New Object() {"-----------", "A1C Chek Express", "A1C Chek Pro", "AC9600", "AC9603", "Analyzer", "AS-280", "AS-480", "Cal & Control"})
        Me.ComboBox_Group.Location = New System.Drawing.Point(416, 19)
        Me.ComboBox_Group.Name = "ComboBox_Group"
        Me.ComboBox_Group.Size = New System.Drawing.Size(96, 30)
        Me.ComboBox_Group.TabIndex = 13
        '
        'Label_Group
        '
        Me.Label_Group.AutoSize = True
        Me.Label_Group.Location = New System.Drawing.Point(368, 23)
        Me.Label_Group.Name = "Label_Group"
        Me.Label_Group.Size = New System.Drawing.Size(50, 28)
        Me.Label_Group.TabIndex = 12
        Me.Label_Group.Text = "Group"
        '
        'ComboBox_Catagories
        '
        Me.ComboBox_Catagories.Font = New System.Drawing.Font("TH Sarabun New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox_Catagories.FormattingEnabled = True
        Me.ComboBox_Catagories.Items.AddRange(New Object() {"Accessories & Spare Part", "Blood Collection Tube", "Chemistry", "Diagnostic Kit", "Electrolyte", "HbA1C", "Hematology"})
        Me.ComboBox_Catagories.Location = New System.Drawing.Point(262, 19)
        Me.ComboBox_Catagories.Name = "ComboBox_Catagories"
        Me.ComboBox_Catagories.Size = New System.Drawing.Size(100, 30)
        Me.ComboBox_Catagories.TabIndex = 11
        '
        'Label_Catagories
        '
        Me.Label_Catagories.AutoSize = True
        Me.Label_Catagories.Location = New System.Drawing.Point(190, 22)
        Me.Label_Catagories.Name = "Label_Catagories"
        Me.Label_Catagories.Size = New System.Drawing.Size(77, 28)
        Me.Label_Catagories.TabIndex = 10
        Me.Label_Catagories.Text = "Catagories"
        '
        'TextBox_Keyword
        '
        Me.TextBox_Keyword.Font = New System.Drawing.Font("TH Sarabun New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_Keyword.Location = New System.Drawing.Point(68, 20)
        Me.TextBox_Keyword.Name = "TextBox_Keyword"
        Me.TextBox_Keyword.Size = New System.Drawing.Size(120, 29)
        Me.TextBox_Keyword.TabIndex = 11
        '
        'Label_Keyword
        '
        Me.Label_Keyword.AutoSize = True
        Me.Label_Keyword.Location = New System.Drawing.Point(4, 23)
        Me.Label_Keyword.Name = "Label_Keyword"
        Me.Label_Keyword.Size = New System.Drawing.Size(67, 28)
        Me.Label_Keyword.TabIndex = 10
        Me.Label_Keyword.Text = "Keyword"
        '
        'Button_Template
        '
        Me.Button_Template.Location = New System.Drawing.Point(715, 16)
        Me.Button_Template.Name = "Button_Template"
        Me.Button_Template.Size = New System.Drawing.Size(83, 36)
        Me.Button_Template.TabIndex = 8
        Me.Button_Template.Text = "Template"
        Me.Button_Template.UseVisualStyleBackColor = True
        '
        'Button_Reset
        '
        Me.Button_Reset.Location = New System.Drawing.Point(884, 16)
        Me.Button_Reset.Name = "Button_Reset"
        Me.Button_Reset.Size = New System.Drawing.Size(75, 36)
        Me.Button_Reset.TabIndex = 7
        Me.Button_Reset.Text = "Reset"
        Me.Button_Reset.UseVisualStyleBackColor = True
        '
        'Button_Search
        '
        Me.Button_Search.Location = New System.Drawing.Point(802, 16)
        Me.Button_Search.Name = "Button_Search"
        Me.Button_Search.Size = New System.Drawing.Size(75, 36)
        Me.Button_Search.TabIndex = 6
        Me.Button_Search.Text = "Search"
        Me.Button_Search.UseVisualStyleBackColor = True
        '
        'ComboBox_Sale
        '
        Me.ComboBox_Sale.Font = New System.Drawing.Font("TH Sarabun New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox_Sale.FormattingEnabled = True
        Me.ComboBox_Sale.Items.AddRange(New Object() {"1-C&Y", "1-CO", "1-SER", "1-TPT", "ไม่ ระบุ", "กชกร แสงไกร"})
        Me.ComboBox_Sale.Location = New System.Drawing.Point(580, 21)
        Me.ComboBox_Sale.Name = "ComboBox_Sale"
        Me.ComboBox_Sale.Size = New System.Drawing.Size(121, 30)
        Me.ComboBox_Sale.TabIndex = 5
        '
        'Label_Sale
        '
        Me.Label_Sale.AutoSize = True
        Me.Label_Sale.Location = New System.Drawing.Point(516, 25)
        Me.Label_Sale.Name = "Label_Sale"
        Me.Label_Sale.Size = New System.Drawing.Size(69, 28)
        Me.Label_Sale.TabIndex = 4
        Me.Label_Sale.Text = "ผู้แทนขาย"
        '
        'ComboBox_CreateBy
        '
        Me.ComboBox_CreateBy.Font = New System.Drawing.Font("TH Sarabun New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox_CreateBy.FormattingEnabled = True
        Me.ComboBox_CreateBy.IntegralHeight = False
        Me.ComboBox_CreateBy.ItemHeight = 22
        Me.ComboBox_CreateBy.Items.AddRange(New Object() {"4594", "adisorn", "admin", "APICHART", "chalee", "chanon", "jantratip"})
        Me.ComboBox_CreateBy.Location = New System.Drawing.Point(395, 21)
        Me.ComboBox_CreateBy.Name = "ComboBox_CreateBy"
        Me.ComboBox_CreateBy.Size = New System.Drawing.Size(121, 30)
        Me.ComboBox_CreateBy.TabIndex = 3
        '
        'Label_CreateBy
        '
        Me.Label_CreateBy.AutoSize = True
        Me.Label_CreateBy.Location = New System.Drawing.Point(322, 25)
        Me.Label_CreateBy.Name = "Label_CreateBy"
        Me.Label_CreateBy.Size = New System.Drawing.Size(74, 28)
        Me.Label_CreateBy.TabIndex = 2
        Me.Label_CreateBy.Text = "Create By"
        '
        'TextBox_Search
        '
        Me.TextBox_Search.Font = New System.Drawing.Font("TH Sarabun New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox_Search.Location = New System.Drawing.Point(102, 22)
        Me.TextBox_Search.Name = "TextBox_Search"
        Me.TextBox_Search.Size = New System.Drawing.Size(209, 29)
        Me.TextBox_Search.TabIndex = 1
        '
        'Label_NoName
        '
        Me.Label_NoName.AutoSize = True
        Me.Label_NoName.Location = New System.Drawing.Point(6, 25)
        Me.Label_NoName.Name = "Label_NoName"
        Me.Label_NoName.Size = New System.Drawing.Size(72, 28)
        Me.Label_NoName.TabIndex = 0
        Me.Label_NoName.Text = "No/Name"
        '
        'DataGridView2
        '
        Me.DataGridView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(4, 21)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(854, 368)
        Me.DataGridView2.TabIndex = 3
        '
        'CheckBox_CreatedByMe
        '
        Me.CheckBox_CreatedByMe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox_CreatedByMe.AutoSize = True
        Me.CheckBox_CreatedByMe.Checked = True
        Me.CheckBox_CreatedByMe.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox_CreatedByMe.Font = New System.Drawing.Font("TH Sarabun New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox_CreatedByMe.Location = New System.Drawing.Point(876, 362)
        Me.CheckBox_CreatedByMe.Name = "CheckBox_CreatedByMe"
        Me.CheckBox_CreatedByMe.Size = New System.Drawing.Size(126, 32)
        Me.CheckBox_CreatedByMe.TabIndex = 19
        Me.CheckBox_CreatedByMe.Text = "Created By Me"
        Me.CheckBox_CreatedByMe.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.DataGridView2)
        Me.GroupBox2.Font = New System.Drawing.Font("TH Sarabun New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(5, 334)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(864, 395)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Latest Edit"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(883, 400)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(104, 36)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "New"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(883, 458)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(104, 36)
        Me.Button2.TabIndex = 22
        Me.Button2.Text = "Edit"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(883, 516)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(104, 36)
        Me.Button3.TabIndex = 23
        Me.Button3.Text = "Delete"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Location = New System.Drawing.Point(883, 607)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(104, 36)
        Me.Button4.TabIndex = 24
        Me.Button4.Text = "Preview"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.UseVisualStyleBackColor = True
        '
        'frmQuotation_View_V3
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.CheckBox_CreatedByMe)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimizeBox = False
        Me.Name = "frmQuotation_View_V3"
        Me.Text = "   "
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox_Filter.ResumeLayout(False)
        Me.GroupBox_Filter.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label_NoName As Label
    Friend WithEvents ComboBox_Sale As ComboBox
    Friend WithEvents Label_Sale As Label
    Friend WithEvents ComboBox_CreateBy As ComboBox
    Friend WithEvents Label_CreateBy As Label
    Friend WithEvents TextBox_Search As TextBox
    Friend WithEvents Button_Reset As Button
    Friend WithEvents Button_Search As Button
    Friend WithEvents Button_Template As Button
    Friend WithEvents GroupBox_Filter As GroupBox
    Friend WithEvents TextBox_Keyword As TextBox
    Friend WithEvents Label_Keyword As Label
    Friend WithEvents ComboBox_Catagories As ComboBox
    Friend WithEvents Label_Catagories As Label
    Friend WithEvents ComboBox_Group As ComboBox
    Friend WithEvents Label_Group As Label
    Friend WithEvents ComboBox_Status As ComboBox
    Friend WithEvents Label_Status As Label
    Friend WithEvents ComboBox_Type As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button_ResetFilter As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents CheckBox_CreatedByMe As CheckBox
    Friend WithEvents Button_Browse As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
End Class
