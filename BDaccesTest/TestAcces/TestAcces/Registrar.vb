Imports System.Data
Imports System.Data.OleDb
Public Class Registrar
    Dim comando As New OleDbCommand
    Dim conexion As New OleDbConnection
    Dim datos As String
    Public Sub llenarCB()
        conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data source=prueba.accdb"
        conexion.Open()

        Dim da As New OleDbDataAdapter("SELECT Localidad FROM Localidades", conexion)
        Dim ds As New DataSet
        da.Fill(ds)
        If ds.Tables(0).Rows.Count > 0 Then
            cbCiudad.DataSource = ds.Tables(0)
            cbCiudad.DisplayMember = "Localidad"
            cbCiudad.ValueMember = "Localidad"
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data source=prueba.accdb"

            datos = "INSERT INTO Registro(Numpaciente,Nombre,Apellido,Edad,Fechanac,Direccion,Localidad)" &
                                             "VALUES(txtId,txtNombre,txtApellido,txtEdad,DTPfecha,txtDireccion,cbCiudad)"

            comando = New OleDbCommand(datos, conexion)

            comando.Parameters.AddWithValue("@Numpaciente", txtId.Text)
            comando.Parameters.AddWithValue("@Nombre", txtNombre.Text)
            comando.Parameters.AddWithValue("@Apellido", txtApellido.Text)
            comando.Parameters.AddWithValue("@Edad", txtEdad.Text)
            comando.Parameters.AddWithValue("@Fechanac", DTPfecha)
            comando.Parameters.AddWithValue("@Direccion", txtDireccion.Text)
            comando.Parameters.AddWithValue("@Localidad", cbCiudad.SelectedValue.ToString)

            conexion.Open()
            comando.ExecuteNonQuery()

            MsgBox("Registro guardado correctamente", vbInformation, "Aviso")

            conexion.Close()
        Catch ex As Exception
            MsgBox("El registro no se pudo guardar", vbCritical, "Aviso")
            conexion.Close()
        End Try

    End Sub
    Private Sub cbCiudad_GotFocus(sender As Object, e As EventArgs) Handles cbCiudad.GotFocus
        llenarCB()

        conexion.Close()
    End Sub
End Class