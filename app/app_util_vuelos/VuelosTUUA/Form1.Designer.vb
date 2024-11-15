<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCodigoAerolinea = New System.Windows.Forms.TextBox
        Me.txtNumeroVuelo = New System.Windows.Forms.TextBox
        Me.dtFechaVuelo = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbTipoVuelo = New System.Windows.Forms.ComboBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.lblMensaje = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(37, 121)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Código Aerolinea:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(37, 160)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Número Vuelo:"
        '
        'txtCodigoAerolinea
        '
        Me.txtCodigoAerolinea.Location = New System.Drawing.Point(133, 118)
        Me.txtCodigoAerolinea.MaxLength = 3
        Me.txtCodigoAerolinea.Name = "txtCodigoAerolinea"
        Me.txtCodigoAerolinea.Size = New System.Drawing.Size(49, 20)
        Me.txtCodigoAerolinea.TabIndex = 2
        '
        'txtNumeroVuelo
        '
        Me.txtNumeroVuelo.Location = New System.Drawing.Point(133, 157)
        Me.txtNumeroVuelo.MaxLength = 5
        Me.txtNumeroVuelo.Name = "txtNumeroVuelo"
        Me.txtNumeroVuelo.Size = New System.Drawing.Size(100, 20)
        Me.txtNumeroVuelo.TabIndex = 3
        '
        'dtFechaVuelo
        '
        Me.dtFechaVuelo.Location = New System.Drawing.Point(133, 233)
        Me.dtFechaVuelo.Name = "dtFechaVuelo"
        Me.dtFechaVuelo.Size = New System.Drawing.Size(200, 20)
        Me.dtFechaVuelo.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(37, 237)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Fecha de Vuelo:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(37, 195)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Tipo de vuelo:"
        '
        'cmbTipoVuelo
        '
        Me.cmbTipoVuelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoVuelo.FormattingEnabled = True
        Me.cmbTipoVuelo.Location = New System.Drawing.Point(133, 192)
        Me.cmbTipoVuelo.Name = "cmbTipoVuelo"
        Me.cmbTipoVuelo.Size = New System.Drawing.Size(121, 21)
        Me.cmbTipoVuelo.TabIndex = 7
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(42, 293)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(161, 34)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Ingresar Vuelo"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lblMensaje
        '
        Me.lblMensaje.AutoSize = True
        Me.lblMensaje.BackColor = System.Drawing.Color.Transparent
        Me.lblMensaje.ForeColor = System.Drawing.Color.Blue
        Me.lblMensaje.Location = New System.Drawing.Point(224, 304)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(47, 13)
        Me.lblMensaje.TabIndex = 11
        Me.lblMensaje.Text = "Mensaje"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.BackgroundImage = Global.VuelosTUUA.My.Resources.Resources.avion1
        Me.ClientSize = New System.Drawing.Size(675, 360)
        Me.Controls.Add(Me.lblMensaje)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmbTipoVuelo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtFechaVuelo)
        Me.Controls.Add(Me.txtNumeroVuelo)
        Me.Controls.Add(Me.txtCodigoAerolinea)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "TUUA-Vuelos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCodigoAerolinea As System.Windows.Forms.TextBox
    Friend WithEvents txtNumeroVuelo As System.Windows.Forms.TextBox
    Friend WithEvents dtFechaVuelo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbTipoVuelo As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lblMensaje As System.Windows.Forms.Label

End Class
