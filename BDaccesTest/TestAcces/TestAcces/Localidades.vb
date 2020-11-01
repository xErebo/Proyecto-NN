Imports System.Data
Imports System.Data.OleDb
Public Class Localidades
    Dim comando As New OleDbCommand
    Dim actualizar As String
    Private Sub Localidades_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim oda As OleDbDataAdapter
        Dim ods As New DataSet
        Dim consulta As String

        conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data source=prueba.accdb"
        conexion.Open()

        consulta = "SELECT Localidad,CP FROM Localidades"
        oda = New OleDbDataAdapter(consulta, conexion)
        ods.Tables.Add("Localidades")
        oda.Fill(ods.Tables("Localidades"))
        DataGridView1.DataSource = ods.Tables("Localidades")
        conexion.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            conexion.Open()

            comando = New OleDbCommand("insert into Localidades(Localidad,CP)" &
                                       "values(txtLoc,txtCP)", conexion)
            comando.Parameters.AddWithValue("@Localidad", txtLoc.Text)
            comando.Parameters.AddWithValue("@CP", txtCP.Text)
            comando.ExecuteNonQuery()

            MsgBox("Localidad guardada correctamente", vbInformation, "Aviso")

            conexion.Close()
        Catch ex As Exception
            MsgBox("El registro no se guardo", vbCritical, "Aviso")

            conexion.Close()
        End Try

        conexion.Close()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim eliminar As String
        Dim registro As Byte

        conexion.Open()
        registro = MsgBox("¿Esta seguro de que desea eliminar el registro?", vbYesNo, "Confimar")

        If registro = vbYes Then
            eliminar = "DELETE *FROM Localidades WHERE Localidad='" & txtLoc.Text & "'"
            comando = New OleDbCommand(eliminar, conexion)
            comando.ExecuteNonQuery()

            MsgBox("El registro ah sido eliminado", vbInformation, "Aviso")

            conexion.Close()
        Else
            MsgBox("No se pudo eliminar el registro", vbCritical, "Aviso")

            conexion.Close()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conexion.Open()

        Dim consulta As String
        Dim oda As OleDbDataAdapter
        Dim ods As DataSet
        Dim registro As Byte
        If txtLoc.Text <> "" Then
            consulta = "SELECT Localidad From Localidades where Localidad='" & txtLoc.Text & " '"
            oda = New OleDbDataAdapter(consulta, conexion)
            ods = New DataSet
            oda.Fill(ods, "Localidades")
            registro = ods.Tables("Localidades").Rows.Count

            If registro <> 0 Then
                DataGridView1.DataSource = ods
                DataGridView1.DataMember = "Localidades"

                txtLoc.Text = ods.Tables("Localidades").Rows(0).Item("Localidad")
                conexion.Close()
            Else
                MsgBox("No existe la localidad", vbInformation, "Aviso")
                conexion.Close()
            End If
        End If
    End Sub
End Class