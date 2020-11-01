Imports System.Data
Imports System.Data.OleDb
Module ConexionBD
    Public conexion As New OleDbConnection
    Sub connectionStr()
        conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data source=prueba.accdb"
    End Sub
End Module
