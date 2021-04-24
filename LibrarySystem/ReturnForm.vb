﻿Imports ZXing
Imports System.Data.SqlClient

Public Class ReturnForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ReturnScanner.ShowDialog()
        idlbl.Visible = True
        namelbl.Visible = True
        bookidlbl.Visible = True
        booklbl.Visible = True
        fineslbl.Visible = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'if fines
        If Integer.Parse(fineslbl.Text) > 0 Then
            FinesConfirmationForm.ShowDialog()
        Else
            Dim query As String = "UPDATE [dbo].[BookBorrow] SET [date_returned] = @datetime, [Remarks] = @remark WHERE Book_BookId=@bookID and date_returned IS NULL"
            Using con As SqlConnection = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True")
                Using cmd As SqlCommand = New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@datetime", DateTime.Now)
                    cmd.Parameters.AddWithValue("@remark", TextBox1.Text)
                    cmd.Parameters.AddWithValue("@bookID", Integer.Parse(bookidlbl.Text))
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                    MessageBox.Show("Sucessfully logged.")
                    Me.Dispose()
                End Using
            End Using

        End If

        Module1.LoadBorrow(BooksBorrowedForm.DataGridView1)
        Me.Dispose()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Dispose()
    End Sub

    Private Sub ReturnForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub

    Private Sub ReturnForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        idlbl.Visible = False
        namelbl.Visible = False
        bookidlbl.Visible = False
        booklbl.Visible = False
        fineslbl.Visible = False
    End Sub
End Class