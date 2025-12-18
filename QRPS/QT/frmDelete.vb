Public Class frmDelete
    ' รับค่า List รายการที่จะลบ
    Public Property ItemsToDelete As New List(Of String)

    Private Sub frmDelete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' นำเลขที่เอกสารทั้งหมดใน List มาเชื่อมกันด้วยเครื่องหมายคอมม่า (,)
        Dim docNumbers As String = String.Join(", ", ItemsToDelete)

        ' แสดงข้อความใน Label
        Confirmmsg.Text = "คุณแน่ใจใช่ไหมที่จะลบเอกสารเลขที่:" & docNumbers
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        ' เมื่อกด OK ฟอร์มจะปิดและส่งค่า DialogResult.OK กลับไปหน้าหลัก
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ' เมื่อกด Cancel ฟอร์มจะปิดและส่งค่า DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click

    End Sub
End Class