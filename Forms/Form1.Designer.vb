<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        btnSuscribe = New Button()
        btnDesuscrube = New Button()
        txtIP = New TextBox()
        txtPort = New TextBox()
        txtApp = New TextBox()
        txtTema = New TextBox()
        btnPulicar = New Button()
        btnObtener = New Button()
        txtCotenido = New TextBox()
        TableLayoutPanel1 = New TableLayoutPanel()
        Label7 = New Label()
        Label8 = New Label()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(52, 57)
        Label1.Name = "Label1"
        Label1.Size = New Size(80, 15)
        Label1.TabIndex = 0
        Label1.Text = "MQ Broker IP:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(52, 111)
        Label2.Name = "Label2"
        Label2.Size = New Size(92, 15)
        Label2.TabIndex = 1
        Label2.Text = "MQ Broker Port:"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(52, 170)
        Label3.Name = "Label3"
        Label3.Size = New Size(43, 15)
        Label3.TabIndex = 2
        Label3.Text = "AppID:"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(52, 229)
        Label4.Name = "Label4"
        Label4.Size = New Size(39, 15)
        Label4.TabIndex = 3
        Label4.Text = "Tema:"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(463, 57)
        Label5.Name = "Label5"
        Label5.Size = New Size(159, 15)
        Label5.TabIndex = 4
        Label5.Text = "Contenido de la publicacion:"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(752, 57)
        Label6.Name = "Label6"
        Label6.Size = New Size(110, 15)
        Label6.TabIndex = 5
        Label6.Text = "Mensajes recibidos:"
        ' 
        ' btnSuscribe
        ' 
        btnSuscribe.BackColor = SystemColors.ActiveCaption
        btnSuscribe.FlatStyle = FlatStyle.Popup
        btnSuscribe.Location = New Point(192, 286)
        btnSuscribe.Name = "btnSuscribe"
        btnSuscribe.Size = New Size(75, 23)
        btnSuscribe.TabIndex = 6
        btnSuscribe.Text = "Suscribirse"
        btnSuscribe.UseVisualStyleBackColor = False
        ' 
        ' btnDesuscrube
        ' 
        btnDesuscrube.BackColor = SystemColors.ActiveCaption
        btnDesuscrube.FlatStyle = FlatStyle.Popup
        btnDesuscrube.ForeColor = SystemColors.ControlText
        btnDesuscrube.Location = New Point(282, 286)
        btnDesuscrube.Name = "btnDesuscrube"
        btnDesuscrube.Size = New Size(75, 23)
        btnDesuscrube.TabIndex = 7
        btnDesuscrube.Text = "Desuscribirse"
        btnDesuscrube.UseVisualStyleBackColor = False
        ' 
        ' txtIP
        ' 
        txtIP.Location = New Point(192, 54)
        txtIP.Name = "txtIP"
        txtIP.Size = New Size(165, 23)
        txtIP.TabIndex = 8
        ' 
        ' txtPort
        ' 
        txtPort.Location = New Point(192, 108)
        txtPort.Name = "txtPort"
        txtPort.Size = New Size(165, 23)
        txtPort.TabIndex = 9
        ' 
        ' txtApp
        ' 
        txtApp.Location = New Point(192, 167)
        txtApp.Name = "txtApp"
        txtApp.Size = New Size(165, 23)
        txtApp.TabIndex = 10
        ' 
        ' txtTema
        ' 
        txtTema.Location = New Point(192, 229)
        txtTema.Name = "txtTema"
        txtTema.Size = New Size(165, 23)
        txtTema.TabIndex = 11
        ' 
        ' btnPulicar
        ' 
        btnPulicar.BackColor = SystemColors.ActiveCaption
        btnPulicar.FlatStyle = FlatStyle.Popup
        btnPulicar.Location = New Point(567, 286)
        btnPulicar.Name = "btnPulicar"
        btnPulicar.Size = New Size(75, 23)
        btnPulicar.TabIndex = 12
        btnPulicar.Text = "Publicar"
        btnPulicar.UseVisualStyleBackColor = False
        ' 
        ' btnObtener
        ' 
        btnObtener.BackColor = SystemColors.ActiveCaption
        btnObtener.FlatStyle = FlatStyle.Popup
        btnObtener.Location = New Point(801, 286)
        btnObtener.Name = "btnObtener"
        btnObtener.Size = New Size(139, 23)
        btnObtener.TabIndex = 13
        btnObtener.Text = "Obtener mensaje"
        btnObtener.UseVisualStyleBackColor = False
        ' 
        ' txtCotenido
        ' 
        txtCotenido.Location = New Point(444, 75)
        txtCotenido.Multiline = True
        txtCotenido.Name = "txtCotenido"
        txtCotenido.Size = New Size(198, 177)
        txtCotenido.TabIndex = 15
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 127F))
        TableLayoutPanel1.Controls.Add(Label8, 1, 0)
        TableLayoutPanel1.Controls.Add(Label7, 0, 0)
        TableLayoutPanel1.Location = New Point(752, 85)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 2
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 22F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(200, 167)
        TableLayoutPanel1.TabIndex = 16
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(3, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(39, 15)
        Label7.TabIndex = 0
        Label7.Text = "TEMA"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(76, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(75, 15)
        Label8.TabIndex = 1
        Label8.Text = "CONTENIDO"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(999, 357)
        Controls.Add(TableLayoutPanel1)
        Controls.Add(txtCotenido)
        Controls.Add(btnObtener)
        Controls.Add(btnPulicar)
        Controls.Add(txtTema)
        Controls.Add(txtApp)
        Controls.Add(txtPort)
        Controls.Add(txtIP)
        Controls.Add(btnDesuscrube)
        Controls.Add(btnSuscribe)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Name = "Form1"
        Text = "Form1"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents btnSuscribe As Button
    Friend WithEvents btnDesuscrube As Button
    Friend WithEvents txtIP As TextBox
    Friend WithEvents txtPort As TextBox
    Friend WithEvents txtApp As TextBox
    Friend WithEvents txtTema As TextBox
    Friend WithEvents btnPulicar As Button
    Friend WithEvents btnObtener As Button
    Friend WithEvents txtCotenido As TextBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label

End Class
