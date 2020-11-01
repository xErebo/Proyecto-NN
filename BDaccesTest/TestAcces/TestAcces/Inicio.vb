Imports System.Data
Imports System.Data.OleDb
Public Class Inicio
    Dim conexion As New OleDbConnection
    Private Sub mostrar()
        Dim oda As OleDbDataAdapter
        Dim ods As New DataSet
        Dim consulta As String

        consulta = "SELECT Numpaciente,Nombre,Apellido,Edad,Fechanac,Direccion,Localidad FROM Registro"
        oda = New OleDbDataAdapter(consulta, conexion)
        ods.Tables.Add("Registro")
        oda.Fill(ods.Tables("Registro"))
        DataGridView1.DataSource = ods.Tables("Registro")
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data source=prueba.accdb"
            conexion.Open()

            MsgBox("Conectado a la Base de Datos con exito", vbInformation, "Aviso")
        Catch ex As Exception
            MsgBox("No se pudo conectar a la Base de Datos", vbCritical, "Aviso")

            conexion.Close()
        End Try

        mostrar()

        conexion.Close()
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        conexion.Close()
    End Sub
    Private Sub RegistrarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegistrarToolStripMenuItem.Click
        Registrar.Show()
    End Sub
    Private Sub EditarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditarToolStripMenuItem.Click
        Editar.Show()
    End Sub

    Private Sub LocalidadesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LocalidadesToolStripMenuItem.Click
        Localidades.show()
    End Sub
End Class
