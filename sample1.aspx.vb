Imports Oracle.ManagedDataAccess.Client

Public Class sample1
    Inherits System.Web.UI.Page

    Public detail As String = String.Empty
    Public hidCheck As String = String.Empty
    Public strName As String = String.Empty
    Public strPrice As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '初期表示時のみこのタイミングで処理を行います
        If Not IsPostBack Then
            Dim dt As DataTable = GetData() 'データ取得
            DispData(dt) 'データ表示
        End If
    End Sub


    ''' <summary>
    ''' データ取得
    ''' </summary>
    Private Function GetData() As DataTable 'Page_Loadで実行
        'データテーブルを宣言
        Dim dt As DataTable = New DataTable()

        Dim Sql As String = "SELECT name,price FROM SYOUHIN"
        Try
            Using Conn As OracleConnection = New OracleConnection()
                Conn.ConnectionString =
                     "User Id=system;Password=Oracle18c;Data Source=localhost/XE;"
                Conn.Open()
                Using cmd As OracleCommand = New OracleCommand(Sql)
                    cmd.Connection = Conn
                    cmd.CommandType = CommandType.Text
                    Using reader As OracleDataReader = cmd.ExecuteReader()
                        'テーブルデータに代入
                        dt.Load(reader)
                    End Using
                End Using
            End Using
            Return dt
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' データ表示
    ''' </summary>
    Private Sub DispData(ByVal dt As DataTable)
        grvDetail.DataSource = dt
        grvDetail.DataBind()
    End Sub

    ''' <summary>
    ''' 登録データをチェック
    ''' </summary>
    Private Function CheckData() As Boolean
        'データが存在するかどうかチェックする処理は省略します
        'すでにデータが登録されていたらtrueを返します
    End Function

    ''' <summary>
    ''' データ登録
    ''' </summary>
    Private Sub InsertData()
        Dim Sql As String = "insert into syouhin (name,price) VALUES (:name,:price)"
        Dim Name As String
        Name = Request.Form("txtName")
        Dim Price As String
        Price = Request.Form("txtPrice")

        Using Conn As OracleConnection = New OracleConnection()

            Conn.ConnectionString =
          "User Id=system;Password=Oracle18c;Data Source=localhost/XE;"
            Try
                Conn.Open()

                Using transaction As OracleTransaction = Conn.BeginTransaction()

                    Try
                        Using cmd As OracleCommand = New OracleCommand(Sql)

                            cmd.Connection = Conn
                            cmd.CommandType = CommandType.Text
                            cmd.BindByName = True

                            cmd.Parameters.Add(New OracleParameter("name", OracleDbType.Varchar2, Name, ParameterDirection.Input))
                            cmd.Parameters.Add(New OracleParameter("price", OracleDbType.Varchar2, Price, ParameterDirection.Input))
                            'cmd.Parameters.Add(New OracleParameter("price", OracleDbType.Varchar2)).Value = "2000"

                            cmd.ExecuteNonQuery()
                            transaction.Commit()
                        End Using
                    Catch ex As Exception
                        transaction.Rollback()
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btnTouroku_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.ServerClick

        InsertData()

        Dim dt As DataTable = GetData() 'データ取得
        DispData(dt) 'データ表示
    End Sub


End Class