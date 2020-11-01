Imports System.Data
Imports System.Data.OleDb
Public Class Editar
    Dim conexion As New OleDbConnection
    Dim comando As New OleDbCommand
    Dim actualizar As String
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
        conexion.Open()

        Dim consulta As String
        Dim oda As OleDbDataAdapter
        Dim ods As DataSet
        Dim registro As Byte
        If txtId.Text <> "" Then
            consulta = "select Numpaciente,Nombre,Apellido,Edad,Fechanac,Direccion,Localidad 
                        from Registro where Numpaciente='" & txtId.Text & " '"
            oda = New OleDbDataAdapter(consulta, conexion)
            ods = New DataSet
            oda.Fill(ods, "Registro")
            registro = ods.Tables("Registro").Rows.Count

            If registro <> 0 Then
                DataGridView1.DataSource = ods
                DataGridView1.DataMember = "Registro"

                txtId.Text = ods.Tables("Registro").Rows(0).Item("Numpaciente")
                txtNombre.Text = ods.Tables("Registro").Rows(0).Item("Nombre")
                txtApellido.Text = ods.Tables("Registro").Rows(0).Item("Apellido")
                txtEdad.Text = ods.Tables("Registro").Rows(0).Item("Edad")
                DTPfecha.Value = ods.Tables("Registro").Rows(0).Item("Fechanac")
                txtDireccion.Text = ods.Tables("Registro").Rows(0).Item("Direccion")
                cbCiudad.SelectedItem = ods.Tables("Registro").Rows(0).Item("Localidad")

                conexion.Close()
            Else
                MsgBox("No existe el paciente", vbInformation, "Aviso")
                conexion.Close()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            conexion.Open()
            actualizar = "UPDATE Registro SET Nombre = '" & txtNombre.Text &
                "' ,Apellido='" & txtApellido.Text &
                "' ,Edad='" & txtEdad.Text &
                "' ,Fechanac='" & DTPfecha.Value &
                "' ,Direccion='" & txtDireccion.Text &
                "' ,Localidad='" & cbCiudad.Text &
                "' WHERE Numpaciente='" & txtId.Text & "'"

            comando = New OleDbCommand(actualizar, conexion)
            comando.ExecuteNonQuery()

            MsgBox("Registro Actualizado", vbInformation, "Aviso")
            conexion.Close()
        Catch ex As Exception
            MsgBox("El registro no se pudo actualizar", vbCritical, "Aviso")
            conexion.Close()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim eliminar As String
        Dim registro As Byte

        conexion.Open()
        registro = MsgBox("¿Esta seguro de que desea eliminar el registro?", vbYesNo, "Confimar")

        If registro = vbYes Then
            eliminar = "DELETE *FROM Registro WHERE Numpaciente='" & txtId.Text & "'"
            comando = New OleDbCommand(eliminar, conexion)
            comando.ExecuteNonQuery()

            MsgBox("El registro ah sido eliminado", vbInformation, "Aviso")

            conexion.Close()
        Else
            MsgBox("No se pudo eliminar el registro", vbCritical, "Aviso")
            conexion.Close()
        End If
    End Sub
    Private Sub cbCiudad_GotFocus(sender As Object, e As EventArgs) Handles cbCiudad.GotFocus
        llenarCB()

        conexion.Close()
    End Sub
End Class