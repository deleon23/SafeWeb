Imports CashForeCast.BusinessLayer.BLCountry
Imports CashForeCast.BusinessLayer.Contact
Imports CashForeCast.BusinessLayer.Customer
Imports CashForeCast.BusinessLayer.Language
Imports CashForeCast.BusinessLayer.Location
Imports CashForeCast.Model.Customer
Imports CashForeCast.Model.Language
Imports CashForeCast.Model.AccessRestriction
Imports CashForeCast.BusinessLayer.AccessRestriction
Imports FrameWork.BusinessLayer.Idioma
Imports FrameWork.BusinessLayer.Usuarios
Imports FrameWork.BusinessLayer.Utilitarios

Public Class CadCliente
    Inherits FWPageLista

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Fwservercontrol1 As FrameWork.WebControl.FWServerControl
    Protected WithEvents lblCadCliente As System.Web.UI.WebControls.Label
    Protected WithEvents lblNome As System.Web.UI.WebControls.Label
    Protected WithEvents lblCnpj As System.Web.UI.WebControls.Label
    Protected WithEvents txtNome As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvNome As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtCNPJ As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvCNPJ As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents cvCNPJ As System.Web.UI.WebControls.CustomValidator
    Protected WithEvents lblCep As System.Web.UI.WebControls.Label
    Protected WithEvents lblPais As System.Web.UI.WebControls.Label
    Protected WithEvents txtCEP As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvCEP As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlPais As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rfvPais As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblEstado As System.Web.UI.WebControls.Label
    Protected WithEvents lblCidade As System.Web.UI.WebControls.Label
    Protected WithEvents ddlEstado As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rfvEstado As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlCidade As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rfvCidade As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblBairro As System.Web.UI.WebControls.Label
    Protected WithEvents lblLogradouro As System.Web.UI.WebControls.Label
    Protected WithEvents txtBairro As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvBairro As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlLogradouro As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rfvLogradouro As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblEndereco As System.Web.UI.WebControls.Label
    Protected WithEvents lblNumero As System.Web.UI.WebControls.Label
    Protected WithEvents txtEndereco As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvEndereco As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtNumero As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvNumero As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblComplemento As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipoClie As System.Web.UI.WebControls.Label
    Protected WithEvents txtComplemento As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTipoCliente As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rfvTipoCliente As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblContato As System.Web.UI.WebControls.Label
    Protected WithEvents txtContato As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnLocalizar As System.Web.UI.WebControls.Button
    Protected WithEvents btnIncluir As System.Web.UI.WebControls.Button
    Protected WithEvents lblMensagem As System.Web.UI.WebControls.Label
    Protected WithEvents dtgDados As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnGravar As System.Web.UI.WebControls.Button
    Protected WithEvents btnVoltar As System.Web.UI.WebControls.Button
    Protected WithEvents Fwservercontrol4 As FrameWork.WebControl.FWServerControl
    Protected WithEvents trMensagem As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents ManutencaoGrid As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblInformaSaldo As System.Web.UI.WebControls.Label
    Protected WithEvents lblHoraCorte As System.Web.UI.WebControls.Label
    Protected WithEvents rblInformaSaldo As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents txtHoraCorte As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGravarSair As System.Web.UI.WebControls.Button


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private objBLAddressType As BLAddressType
    Private objBLCity As BLCity
    Private objBLContact As BLContact
    Private objBLCountry As BLCountry    
    Private objBLCustomer As BLCustomer
    Private objBLCustomerType As BLCustomerType
    Private objBLLanguage As BLLanguage
    Private objBLState As BLState
    Private objCustomer As Customer
    Private dttContact As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Carrega permissões da página...
        FrameWork.UI.Permissoes.CarregaPermissoes(Page.ToString, "ListaCliente")

        'Gerencia os componentes da tela...
        Me.InicializaComponentes()

        'Inicializa script para componente...
        Me.InicializaScripts()

        If Not Page.IsPostBack Then

            'Tradução...
            BLIdiomas.Traduzir(Me.Controls, Page.ToString, BLAcesso.ObterUsuario.CompanyGroup_Id())

            'Se o usuário não tiver permissão para incluir registros...
            If Not FrameWork.UI.Permissoes.Inclusão Then
                Me.btnGravar.Enabled = False
            End If

            If Not IsNothing(HttpContext.Current.Items("ParametrosConsulta")) Or Not IsNothing(HttpContext.Current.Items("Localizar")) Then
                If Not IsNothing(HttpContext.Current.Items("ParametrosConsulta")) Then
                    If HttpContext.Current.Items("ParametrosConsulta").GetType Is GetType(Hashtable) Then
                        Me.DadosFiltro = BLUtilitarios.Hash2String(CType(HttpContext.Current.Items("ParametrosConsulta"), Hashtable))
                    Else
                        Me.DadosFiltro = HttpContext.Current.Items("ParametrosConsulta").ToString
                    End If
                Else
                    If HttpContext.Current.Items("Localizar").GetType Is GetType(Hashtable) Then
                        Me.DadosFiltro = BLUtilitarios.Hash2String(CType(HttpContext.Current.Items("Localizar"), Hashtable))
                    Else
                        Me.DadosFiltro = HttpContext.Current.Items("Localizar").ToString
                    End If
                End If
            End If

            'Popula combos...
            Me.PopulaComboPais()
            Me.PopulaComboLogradouro()
            Me.PopulaComboTipoCliente()

            Me.Bind(TipoTransacao.Novo)

            'Recebe os parâmetros utilizados na consulta...
            If Not IsNothing(HttpContext.Current.Items("EstadoTelaCadCliente")) Then
                Me.ExibeDadosFiltro(CType(HttpContext.Current.Items("EstadoTelaCadCliente"), Hashtable))
                Me.PopulaComboEstado()
                If Me.CodEstado > 0 Then
                    BLUtilitarios.ConsultarValorCombo(Me.ddlEstado, Me.CodEstado.ToString)
                    Me.PopulaComboCidade()
                    If Me.CodCidade > 0 Then
                        BLUtilitarios.ConsultarValorCombo(Me.ddlCidade, Me.CodCidade.ToString)
                    End If
                End If
            End If

            If Not IsNothing(HttpContext.Current.Items("DataTableContact")) Then
                Me.DataTableContact = CType(HttpContext.Current.Items("DataTableContact"), DataTable)
                Me.Bind()
            End If

            'Se tiver código do Contato no Context...
            If Not IsNothing(HttpContext.Current.Items("ContactId")) AndAlso IsNumeric(HttpContext.Current.Items("ContactId")) Then

                'Se tiver código do customer no context obter...
                If Not IsNothing(HttpContext.Current.Items("CustomerId")) AndAlso IsNumeric(HttpContext.Current.Items("CustomerId")) Then
                    Me.CustomerId = Convert.ToInt32(HttpContext.Current.Items("CustomerId"))
                    If Me.DataTableContact.Rows.Count = 0 AndAlso Me.CustomerId > 0 Then
                        Me.Obter()
                    End If
                End If

                Me.ContactId = Convert.ToInt32(HttpContext.Current.Items("ContactId"))
                Me.ContactPhone = HttpContext.Current.Items("ContactPhone").ToString
                Me.ContactPosition = HttpContext.Current.Items("ContactPosition").ToString
                Me.txtContato.Text = HttpContext.Current.Items("ContactName").ToString

            Else
                If Not IsNothing(HttpContext.Current.Items("CustomerId")) AndAlso _
                        IsNumeric(HttpContext.Current.Items("CustomerId")) AndAlso _
                        Convert.ToInt32(HttpContext.Current.Items("CustomerId")) > 0 Then
                    Me.CustomerId = Convert.ToInt32(HttpContext.Current.Items("CustomerId"))
                    If Me.DataTableContact.Rows.Count = 0 AndAlso Me.CustomerId > 0 Then
                        Me.Obter()
                    End If
                End If
            End If
            Me.Bind()

        End If

    End Sub

#Region " Enum "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Enum das colunas do DataTable
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[pvbraga]	3/11/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Enum enmDataTable
        Contact_Id = 0
        Contact_Name = 1
        Contact_Phone = 2
        Contact_Cargo = 3
        PK = 4
    End Enum

#End Region

#Region " Eventos "

#Region " Botões "

    Private Sub btnGravar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGravar.Click

        Dim blnRetorno As Boolean

        Try
            If Page.IsValid Then
                If Not txtHoraCorte.Text.Equals("") Then
                    If IsDate(txtHoraCorte.Text) Then
                        Me.Bind(TipoTransacao.CarregarDados)
                        blnRetorno = Me.Gravar()
                    Else
                        Me.lblMensagem.Text = BLIdiomas.TraduzirMensagens(Mensagens.FATOR_RISCO_HORARIO_INVALIDO)
                        Me.lblMensagem.Visible = True
                        Me.lblMensagem.CssClass = "cadMsgErro"
                        Me.trMensagem.Visible = True
                    End If
                Else
                    Me.Bind(TipoTransacao.CarregarDados)
                    blnRetorno = Me.Gravar()
                End If

            End If

        Catch ex As Exception
            TrataErro(ex)

        End Try

        If blnRetorno Then
            If CustomerId = 0 Then
                Me.Bind(TipoTransacao.Novo)
                Me.DataTableContact = RecuperaDatatable(dtgDados)
                Me.Bind()
            Else
                HttpContext.Current.Items.Add("DadosFiltro", Me.DadosFiltro())
                Server.Transfer("ListaCliente.aspx")
            End If
        End If

    End Sub

    Private Sub btnGravarSair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGravarSair.Click

        Dim blnRetorno As Boolean

        Try
            If Page.IsValid Then
                If Not txtHoraCorte.Text.Equals("") Then
                    If IsDate(txtHoraCorte.Text) Then
                        Me.Bind(TipoTransacao.CarregarDados)
                        blnRetorno = Me.Gravar()
                    Else
                        Me.lblMensagem.Text = BLIdiomas.TraduzirMensagens(Mensagens.FATOR_RISCO_HORARIO_INVALIDO)
                        Me.lblMensagem.Visible = True
                        Me.lblMensagem.CssClass = "cadMsgErro"
                        Me.trMensagem.Visible = True
                    End If
                Else
                    Me.Bind(TipoTransacao.CarregarDados)
                    blnRetorno = Me.Gravar()
                End If

            End If

        Catch ex As Exception
            TrataErro(ex)

        End Try

        If blnRetorno Then
            If CustomerId = 0 Then
                Me.Bind(TipoTransacao.Novo)
                Me.DataTableContact = RecuperaDatatable(dtgDados)
                Me.Bind()
                HttpContext.Current.Items.Add("DadosFiltro", Me.DadosFiltro())
                Server.Transfer("ListaCliente.aspx")
            Else
                HttpContext.Current.Items.Add("DadosFiltro", Me.DadosFiltro())
                Server.Transfer("ListaCliente.aspx")
            End If
        End If



    End Sub

    Private Sub btnIncluir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIncluir.Click

        If Not Me.txtContato.Text.Equals(String.Empty) Then

            'Transforma o Grid que está na tela em um DataTable para Adicionar a nova linha...
            dttContact = RecuperaDatatable(Me.dtgDados)

            InsereLinha(dttContact)
            Me.Bind()

        End If

    End Sub

    Private Sub btnLocalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocalizar.Click

        'Armazena a Url da página para retorno...
        HttpContext.Current.Items.Add("UrlVoltar", "CadCliente.aspx")

        'Armazena o estado dos componentes no momento do post...
        HttpContext.Current.Items.Add("EstadoTelaCadCliente", Me.EnviaDadosConsulta)
        HttpContext.Current.Items.Add("Localizar", Me.DadosFiltro)
        HttpContext.Current.Items.Add("CustomerId", Me.CustomerId)
        HttpContext.Current.Items.Add("DataTableContact", RecuperaDatatable(dtgDados))

        Server.Transfer("ListaContato.aspx")

    End Sub

    Private Sub btnVoltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVoltar.Click
        HttpContext.Current.Items.Add("DadosFiltro", Me.DadosFiltro())
        Server.Transfer("ListaCliente.aspx")
    End Sub

#End Region

#Region " Combos "

    Private Sub ddlPais_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPais.SelectedIndexChanged
        Me.PopulaComboEstado()
    End Sub

    Private Sub ddlEstado_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Me.PopulaComboCidade()
    End Sub

#End Region

#Region " DataGrid "

#Region " ItemCommand "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Executa o método do Item Selecionado.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[imorales]	25/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub dtgDados_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDados.ItemCommand

        If e.CommandName.Trim() = "Excluir" Then
            Try
                RemoverLinha(Convert.ToInt32(e.CommandArgument))
            Catch ex As Exception
                TrataErro(ex)
            End Try

        End If

    End Sub

#End Region

#Region " ItemDataBound "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Configura os botões de edição e exclusão no DataGrid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[imorales]	25/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub dtgDados_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDados.ItemDataBound

        Dim btnExcluir As ImageButton, btnEditar As ImageButton

        If e.Item.ItemType = ListItemType.Item Or _
           e.Item.ItemType = ListItemType.AlternatingItem Then

            Try
                'Excluir...
                btnExcluir = CType(e.Item.FindControl("imgExcluir"), ImageButton)
                If Not IsNothing(btnExcluir) Then
                    btnExcluir.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Contact_Id").ToString()
                    btnExcluir.Attributes.Add("OnClick", "return confirm('" & BLIdiomas.TraduzirMensagens(Mensagens.REGISTRO_CONFIRMA_EXCLUSAO) & "');")
                    btnExcluir.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EXCLUIR)
                    btnExcluir.Visible = True
                End If

                'Se o usuário não tiver permissão para excluir registros...
                If Not FrameWork.UI.Permissoes.Alteração AndAlso Not Me.CustomerId = 0 Then
                    btnExcluir.Enabled = False
                    btnExcluir.Attributes.Item("STYLE") = "filter:progid:DXImageTransform.Microsoft.Alpha(opacity = 40)"
                End If

            Catch ex As Exception
                TrataErro(ex)

            End Try

        End If

    End Sub

#End Region

#Region " SortCommand "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Ordena o DataGrid.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[imorales]	25/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub dtgDados_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDados.SortCommand

        Dim strSortExpression As String = String.Empty
        Dim strSortAsc As String = String.Empty

        Try
            If Not IsNothing(ViewState("SortExpression")) Then
                strSortExpression = ViewState("SortExpression").ToString
            End If

            If Not IsNothing(ViewState("SortAsc")) Then
                strSortAsc = ViewState("SortAsc").ToString
            End If

            ' Valoriza o valor SortExpression
            ViewState("SortExpression") = e.SortExpression.ToString

            'Verifica a se a SortExpression é a mesma 
            If (e.SortExpression = strSortExpression) Then
                'Se sim muda o tipo de para ASC ou DES
                If strSortAsc.Equals("YES") Then
                    ViewState("SortAsc") = "NO"
                Else
                    ViewState("SortAsc") = "YES"
                End If
            Else
                'Senão força o ASC
                ViewState("SortAsc") = "YES"
            End If

            'Vincula dados da base ao DataGrid...
            Me.Bind()

        Catch ex As Exception
            TrataErro(ex)

        End Try

    End Sub

#End Region

#End Region

#End Region

#Region " Métodos "

#Region "Get Parametros Restricao"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Carrega os parametros para fazer as validações.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mtzrodrigooliveira]	19/01/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function GetParametrosRestricao() As ParamAccessRestriction

        Dim objBLParamAccessRest As BLParamAccessRestriction
        Dim objParamAccessRest As ParamAccessRestriction

        Try
            objBLParamAccessRest = New BLParamAccessRestriction
            objParamAccessRest = New ParamAccessRestriction

            objParamAccessRest = objBLParamAccessRest.Obter(BLAcesso.ObterUsuario.CompanyGroup_Id, BLAcesso.ObterUsuario.Company_Id)

            Return objParamAccessRest

        Catch ex As Exception
            TrataErro(ex)
        End Try

    End Function

#End Region

#Region "Obter Primeiro horario"

    Private Sub ObterPrimeiroHorario()

        Me.PrimeiroHorario = Convert.ToDateTime(GetParametrosRestricao.HorarioInicial.ToShortTimeString)

    End Sub

#End Region

#Region " Bind "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Liga o DataGrid aos dados retornados da consulta.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	29/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub Bind()

        Me.dtgDados.PageSize = BLUtilitarios.ObterPageSize()
        Dim dttGrid As DataTable

        Try
            If Not IsNothing(BLAcesso.ObterUsuario) Then

                objBLContact = New BLContact

                dttGrid = Me.DataTableContact

                Me.dtgDados.DataSource = Me.BuscaDataViewOrdenado(dttGrid)

                Me.dtgDados.CurrentPageIndex = BLUtilitarios.ObterPageIndiceGrid(Me.dtgDados.CurrentPageIndex, _
                                                                                 Me.dtgDados.PageCount, _
                                                                                 dttGrid.Rows.Count, _
                                                                                 Me.dtgDados.PageSize)

                Me.dtgDados.DataBind()

                If dttGrid.Rows.Count = 0 Then
                    Me.dtgDados.Visible = False
                    Me.ManutencaoGrid.Visible = False
                Else
                    Me.dtgDados.Visible = True
                    Me.ManutencaoGrid.Visible = True
                End If

            End If

        Catch ex As Exception
            TrataErro(ex)

        Finally
            objBLContact = Nothing

        End Try

    End Sub

#End Region

#Region " Bind Model "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Trafega informações com a model.
    ''' </summary>
    ''' <param name="penmTipoTransacao"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	28/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub Bind(ByVal penmTipoTransacao As TipoTransacao)

        Try
            If IsNothing(objCustomer) Then
                objCustomer = New Customer
            End If

            Select Case penmTipoTransacao
                Case TipoTransacao.CarregarDados
                    'Carrega model...
                    With objCustomer
                        If Not IsNothing(BLAcesso.ObterUsuario) Then
                            .CompanyGroupId = BLAcesso.ObterUsuario.CompanyGroup_Id
                            .CompanyId = BLAcesso.ObterUsuario.Company_Id
                            .BranchGroupID = BLAcesso.ObterUsuario.BranchGroupId
                            .BranchID = BLAcesso.ObterUsuario.BranchId
                        End If
                        .CustomerCod = Me.txtCNPJ.Text
                        .CustomerName = Me.txtNome.Text
                        If IsNumeric(Me.ddlTipoCliente.SelectedValue) Then
                            .CustomerTypeId = Convert.ToInt32(Me.ddlTipoCliente.SelectedValue)
                        End If
                        If IsNumeric(Me.ddlLogradouro.SelectedValue) Then
                            .AddressTypeId = Convert.ToInt32(Me.ddlLogradouro.SelectedValue)
                        End If
                        .Address = Me.txtEndereco.Text
                        .AddressNumber = Me.txtNumero.Text
                        .AddressQuarter = Me.txtBairro.Text
                        .AddressPostalCode = Me.txtCEP.Text
                        .AddressComplement = Me.txtComplemento.Text
                        If IsNumeric(Me.ddlPais.SelectedValue) Then
                            .CountryId = Convert.ToInt32(Me.ddlPais.SelectedValue)
                        End If
                        If IsNumeric(Me.ddlEstado.SelectedValue) Then
                            .StateId = Convert.ToInt32(Me.ddlEstado.SelectedValue)
                        End If
                        If IsNumeric(Me.ddlCidade.SelectedValue) Then
                            .CityId = Convert.ToInt32(Me.ddlCidade.SelectedValue)
                        End If

                        If Not txtHoraCorte.Text.Equals("") Then
                            .CutHour = Convert.ToDateTime(txtHoraCorte.Text)
                        End If
                        .InformBalance = Convert.ToInt32(rblInformaSaldo.SelectedItem.Value)
                    End With
                Case TipoTransacao.DescarregarDados
                    'Carrega Form...
                    With objCustomer
                        Me.txtCNPJ.Text = .CustomerCod
                        Me.txtNome.Text = .CustomerName

                        If Not IsNothing(Me.ddlTipoCliente.Items.FindByValue(.CustomerTypeId.ToString())) Then
                            BLUtilitarios.ConsultarValorCombo(Me.ddlTipoCliente, .CustomerTypeId.ToString())
                        Else
                            BLUtilitarios.ConsultarValorCombo(Me.ddlTipoCliente, "-1")
                        End If
                        If Not IsNothing(Me.ddlLogradouro.Items.FindByValue(.AddressTypeId.ToString())) Then
                            BLUtilitarios.ConsultarValorCombo(Me.ddlLogradouro, .AddressTypeId.ToString())
                        Else
                            BLUtilitarios.ConsultarValorCombo(Me.ddlLogradouro, "-1")
                        End If

                        Me.txtEndereco.Text = .Address
                        Me.txtNumero.Text = .AddressNumber
                        Me.txtBairro.Text = .AddressQuarter
                        Me.txtCEP.Text = .AddressPostalCode
                        Me.txtComplemento.Text = .AddressComplement

                        If Not IsNothing(Me.ddlPais.Items.FindByValue(.CountryId.ToString())) Then
                            BLUtilitarios.ConsultarValorCombo(Me.ddlPais, .CountryId.ToString())
                        Else
                            BLUtilitarios.ConsultarValorCombo(Me.ddlLogradouro, "-1")
                        End If

                        Me.PopulaComboEstado()

                        If Not IsNothing(Me.ddlEstado.Items.FindByValue(.StateId.ToString())) Then
                            BLUtilitarios.ConsultarValorCombo(Me.ddlEstado, .StateId.ToString())
                        Else
                            BLUtilitarios.ConsultarValorCombo(Me.ddlEstado, "-1")
                        End If

                        Me.PopulaComboCidade()

                        If Not IsNothing(Me.ddlCidade.Items.FindByValue(.CityId.ToString())) Then
                            BLUtilitarios.ConsultarValorCombo(Me.ddlCidade, .CityId.ToString())
                        Else
                            BLUtilitarios.ConsultarValorCombo(Me.ddlCidade, "-1")
                        End If
                        If Convert.ToDateTime(.CutHour) <> #12:00:00 AM# Then
                            txtHoraCorte.Text = Convert.ToDateTime(.CutHour).ToString("HH:mm")
                        End If
                        BLUtilitarios.ConsultarValorRadioButtonList(rblInformaSaldo, .InformBalance.ToString)
                    End With
                Case TipoTransacao.Novo
                    'Novo Form...
                    Me.txtBairro.Text = String.Empty
                    Me.txtCEP.Text = String.Empty
                    Me.txtCNPJ.Text = String.Empty
                    Me.txtComplemento.Text = String.Empty
                    Me.txtContato.Text = String.Empty
                    Me.txtEndereco.Text = String.Empty
                    Me.txtNome.Text = String.Empty
                    Me.txtNumero.Text = String.Empty
                    Me.ddlCidade.SelectedIndex = -1
                    Me.ddlEstado.SelectedIndex = -1
                    Me.ddlLogradouro.SelectedIndex = -1
                    Me.ddlPais.SelectedIndex = -1
                    Me.ddlTipoCliente.SelectedIndex = -1
                    Me.txtHoraCorte.Text = ""
                    Me.dtgDados.Controls.Clear()
                    'Remove itens do DataGrid...
                    Dim dttDataTable As DataTable = Me.RecuperaDatatable(Me.dtgDados)
                    dttDataTable.Clear()
                    Me.dtgDados.DataSource = dttDataTable
                    Me.dtgDados.DataBind()
            End Select

        Catch ex As Exception
            TrataErro(ex)

        End Try

    End Sub

#End Region

#Region " Configura DataTable "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Adiciona as colunas do que serão usadas no Datagrid do Datatable
    ''' </summary>
    ''' <param name="pdttTabela"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[pvbraga]	30/10/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub ConfiguraDataTable(ByRef pdttTabela As DataTable)
        pdttTabela.Columns.Add(New DataColumn("Contact_id"))
        pdttTabela.Columns.Add(New DataColumn("Contact_Name"))
        pdttTabela.Columns.Add(New DataColumn("Contact_Phone"))
        pdttTabela.Columns.Add(New DataColumn("ExternalPosition_Name"))
        pdttTabela.Columns.Add(New DataColumn("Pk"))
    End Sub

#End Region

#Region " Gravar "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Grava o Cliente no Banco de Dados.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	28/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function Gravar() As Boolean

        Dim blnResultado As Boolean = False

        Try
            objBLCustomer = New BLCustomer

            Me.Bind(TipoTransacao.CarregarDados)

            'Se 0 Inserir, se não Alterar...
            If CustomerId = 0 Then
                blnResultado = objBLCustomer.Inserir(objCustomer, RecuperaDatatable(dtgDados))
            Else
                objCustomer.CustomerId = CustomerId
                'Page.RegisterStartupScript("Validacao", "<script language='javascript'> window.confirm('Demoro'); </script>")
                blnResultado = objBLCustomer.Alterar(objCustomer, RecuperaDatatable(dtgDados))
            End If

        Catch ex As Exception
            TrataErro(ex)

        End Try

        Me.trMensagem.Visible = True
        Me.lblMensagem.Visible = True

        If Not blnResultado Then
            Me.lblMensagem.Text = BLIdiomas.TraduzirMensagens(Mensagens.REGISTRO_GRAVAR_ERRO)
            Me.lblMensagem.CssClass = "cadMsgErro"
            Return False
        Else
            Me.lblMensagem.Text = BLIdiomas.TraduzirMensagens(Mensagens.REGISTRO_GRAVAR)
            Me.lblMensagem.CssClass = "cadMsgOk"
        End If

        HttpContext.Current.Items.Add("Mensagem", Convert.ToString(BLIdiomas.TraduzirMensagens(Mensagens.REGISTRO_GRAVAR)))

        Return blnResultado

    End Function

#End Region

#Region " Inicializa Componentes "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Inicializa os componentes da pagina atribuindo algum valor ou executando alguma função.
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[ftinelo]	3/7/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub InicializaComponentes()

        Me.lblMensagem.Text = String.Empty
        Me.trMensagem.Visible = False
        If Me.dtgDados.Items.Count = 0 Then
            Me.ManutencaoGrid.Visible = False
        End If

        If Not Me.ObterIdioma.Acronym.Equals("pt-BR") Then
            Me.cvCNPJ.Enabled = False
        Else
            Me.txtCNPJ.Attributes.Add("onKeyPress", "mascara_CNPJ(this, event)")
        End If

    End Sub

#End Region

#Region " Inicializa Scripts "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Inicializa os scripts dos controles.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Makesys]	27/10/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Sub InicializaScripts()
        'Atribui script para formatação de conteúdo digitado pelo usuário...
        If Me.ObterIdioma.Acronym.Equals("pt-BR") Then
            Me.txtCEP.Attributes.Add("OnKeyPress", "return mascara_CEP(this,event.keyCode);")
        End If
        Me.txtHoraCorte.Attributes.Add("onKeyPress", "return mascara_Hora(this, event.keyCode);")
    End Sub

#End Region

#Region " Insere Linha "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Insere linha no DataTable
    ''' </summary>
    ''' <param name="pdttTable"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[pvbraga]	30/10/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub InsereLinha(ByRef pdttTable As DataTable)

        If InserirRegistro(pdttTable) Then
            Dim dtrRow As DataRow

            dtrRow = pdttTable.NewRow

            dtrRow.Item(enmDataTable.PK) = pdttTable.Rows.Count + 1
            dtrRow.Item(enmDataTable.Contact_Id) = Me.ContactId
            dtrRow.Item(enmDataTable.Contact_Name) = Me.txtContato.Text
            dtrRow.Item(enmDataTable.Contact_Cargo) = Me.ContactPosition
            dtrRow.Item(enmDataTable.Contact_Phone) = Me.ContactPhone

            pdttTable.Rows.Add(dtrRow)

            Me.DataTableContact = pdttTable

            Me.lblMensagem.Text = String.Empty
            Me.lblMensagem.Visible = False
            Me.trMensagem.Visible = False
        Else
            Me.lblMensagem.Text = BLIdiomas.TraduzirMensagens(Mensagens.REGISTRO_JA_EXISTE)
            Me.lblMensagem.Visible = True
            Me.lblMensagem.CssClass = "cadMsgErro"
            Me.trMensagem.Visible = True
        End If

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica se o registro pode ser inserido
    ''' </summary>
    ''' <param name="pdttTable"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[rcoelho]	22/01/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function InserirRegistro(ByVal pdttDataTable As DataTable) As Boolean

        Dim drLinhas() As DataRow

        drLinhas = pdttDataTable.Select("Contact_id = " & Me.ContactId.ToString())

        If drLinhas.Length > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

#End Region

#Region " Obter "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Carrega os dados do Cliente de acordo com o código.    
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	28/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub Obter()

        Try
            If Not IsNothing(BLAcesso.ObterUsuario) Then
                objBLCustomer = New BLCustomer

                Me.DataTableContact = objBLCustomer.ListarCustomerxContact(BLAcesso.ObterUsuario.CompanyGroup_Id, _
                                                                    BLAcesso.ObterUsuario.Company_Id, Me.CustomerId)

                objCustomer = objBLCustomer.Obter(BLAcesso.ObterUsuario.CompanyGroup_Id, BLAcesso.ObterUsuario.Company_Id, _
                                                    Me.CustomerId)

                ' Descarrega na Model...
                Me.Bind(TipoTransacao.DescarregarDados)

                'Se o usuário não tiver permissão para alterar registros...
                If Not FrameWork.UI.Permissoes.Alteração Then
                    Me.btnGravar.Enabled = False
                    Me.btnLocalizar.Enabled = False
                    Me.btnIncluir.Enabled = False
                Else
                    Me.btnGravar.Enabled = True
                    Me.btnLocalizar.Enabled = True
                    Me.btnIncluir.Enabled = True
                End If

            End If

        Catch ex As Exception
            TrataErro(ex)

        Finally
            objBLCustomer = Nothing

        End Try

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtem o idioma do usuário da empresa do usuário logado
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[rmoreira]	18/12/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function ObterIdioma() As Language

        Try
            If Not IsNothing(BLAcesso.ObterUsuario) Then
                objBLLanguage = New BLLanguage

                Return objBLLanguage.Obter(BLAcesso.ObterUsuario.CompanyGroup_Id, BLAcesso.ObterUsuario.Company_Id)
            End If

        Catch ex As Exception
            TrataErro(ex)

        Finally
            objBLLanguage = Nothing

        End Try

    End Function

#End Region

#Region " Parâmetros Consulta "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Carrega um array com os parametros de consulta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[ftinelo]	27/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function EnviaDadosConsulta() As Hashtable

        Dim hstTemp As Hashtable

        Try
            'Cria um novo HashTable...
            hstTemp = New Hashtable

            'Adiciona os controles no HashTable...
            hstTemp.Add(Me.txtNome.ID.ToString(), Me.txtNome.Text)
            hstTemp.Add(Me.txtCNPJ.ID.ToString(), Me.txtCNPJ.Text)
            hstTemp.Add(Me.txtCEP.ID.ToString(), Me.txtCEP.Text)
            hstTemp.Add(Me.txtBairro.ID.ToString(), Me.txtBairro.Text)
            hstTemp.Add(Me.ddlLogradouro.ID.ToString(), Me.ddlLogradouro.SelectedValue)
            hstTemp.Add(Me.txtEndereco.ID.ToString(), Me.txtEndereco.Text)
            hstTemp.Add(Me.txtNumero.ID.ToString(), Me.txtNumero.Text)
            hstTemp.Add(Me.txtComplemento.ID.ToString(), Me.txtComplemento.Text)
            hstTemp.Add(Me.ddlTipoCliente.ID.ToString(), Me.ddlTipoCliente.SelectedValue)
            hstTemp.Add(Me.ddlPais.ID.ToString, Me.ddlPais.SelectedValue)
            hstTemp.Add(Me.ddlEstado.ID.ToString, Me.ddlEstado.SelectedValue)
            hstTemp.Add(Me.ddlCidade.ID.ToString, Me.ddlCidade.SelectedValue)
            hstTemp.Add(Me.txtHoraCorte.ID.ToString, Me.txtHoraCorte.Text)
            hstTemp.Add(Me.rblInformaSaldo.ID.ToString, Me.rblInformaSaldo.SelectedItem.Value)

            Return hstTemp

        Catch ex As Exception
            TrataErro(ex)

        End Try

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[ftinelo]	27/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub ExibeDadosFiltro(ByVal hstTemp As Hashtable)

        Dim objCampo As String

        Try
            If hstTemp.Keys.Count > 0 Then

                For Each objCampo In hstTemp.Keys
                    If TypeOf (FindControl(Convert.ToString(objCampo))) Is TextBox Then
                        CType(FindControl(Convert.ToString(objCampo)), TextBox).Text = hstTemp(objCampo).ToString()
                    ElseIf TypeOf (FindControl(Convert.ToString(objCampo))) Is DropDownList Then
                        If objCampo.ToString.Equals("ddlEstado") And Not hstTemp(objCampo).ToString.Equals(String.Empty) Then
                            Me.CodEstado = Convert.ToInt32(hstTemp(objCampo).ToString)
                        End If
                        If objCampo.ToString.Equals("ddlCidade") And Not hstTemp(objCampo).ToString.Equals(String.Empty) Then
                            Me.CodCidade = Convert.ToInt32(hstTemp(objCampo).ToString)
                        End If
                        If Not hstTemp(objCampo).ToString.Equals(String.Empty) Then
                            BLUtilitarios.ConsultarValorCombo(CType(FindControl(Convert.ToString(objCampo)), DropDownList), hstTemp(objCampo).ToString())
                        End If
                    ElseIf TypeOf (FindControl(Convert.ToString(objCampo))) Is ListBox Then
                        CType(FindControl(Convert.ToString(objCampo)), ListBox).SelectedValue = hstTemp(objCampo).ToString()
                    ElseIf TypeOf (FindControl(Convert.ToString(objCampo))) Is CheckBox Then
                        CType(FindControl(Convert.ToString(objCampo)), CheckBox).Checked = Convert.ToBoolean(hstTemp(objCampo))
                    End If
                Next

            End If

        Catch ex As Exception
            TrataErro(ex)

        End Try

    End Sub

#End Region

#Region " Popula Combos "

#Region " Popula Combo Cidade "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Carrega o combo de Cidade.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	28/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub PopulaComboCidade()

        Try
            If Not IsNothing(BLAcesso.ObterUsuario) Then
                objBLCity = New BLCity

                If IsNumeric(Me.ddlEstado.SelectedValue) Then
                    objBLCity.PopularCidades(Me.ddlCidade, BLAcesso.ObterUsuario.CompanyGroup_Id, Convert.ToInt32(Me.ddlEstado.SelectedValue))
                End If
            End If

        Catch ex As Exception
            TrataErro(ex)

        Finally
            objBLCity = Nothing

        End Try

    End Sub

#End Region

#Region " Popula Combo Estado "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Carrega o combo de Estado.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	26/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub PopulaComboEstado()

        Try
            If Not IsNothing(BLAcesso.ObterUsuario) AndAlso IsNumeric(Me.ddlPais.SelectedValue) Then
                objBLState = New BLState

                objBLState.PopularEstado(Me.ddlEstado, BLAcesso.ObterUsuario.CompanyGroup_Id, Convert.ToInt32(Me.ddlPais.SelectedValue))
            End If

        Catch ex As Exception
            TrataErro(ex)

        Finally
            objBLState = Nothing

        End Try

    End Sub

#End Region

#Region " Popula Combo Logradouro "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Carrega o combo de Logradouro.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	28/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub PopulaComboLogradouro()

        Try
            If Not IsNothing(BLAcesso.ObterUsuario) Then
                objBLAddressType = New BLAddressType

                objBLAddressType.PopularLogradouro(Me.ddlLogradouro, BLAcesso.ObterUsuario.CompanyGroup_Id)
            End If

        Catch ex As Exception
            TrataErro(ex)

        Finally
            objBLAddressType = Nothing

        End Try

    End Sub

#End Region

#Region " Popula Combo País "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Carrega o combo de Paises.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	26/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub PopulaComboPais()

        Try
            If Not IsNothing(BLAcesso.ObterUsuario) Then
                objBLCountry = New BLCountry

                objBLCountry.PopularComboPais(Me.ddlPais, BLAcesso.ObterUsuario.CompanyGroup_Id, Situacao.Ativo)
            End If

        Catch ex As Exception
            TrataErro(ex)

        Finally
            objBLCountry = Nothing

        End Try

    End Sub

#End Region

#Region " Popula Combo Tipo Cliente "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Carrega o combo de Tipo de Cliente.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	28/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub PopulaComboTipoCliente()

        Try

            If Not IsNothing(BLAcesso.ObterUsuario) Then
                objBLCustomerType = New BLCustomerType

                objBLCustomerType.PopularTipoCliente(Me.ddlTipoCliente, BLAcesso.ObterUsuario.CompanyGroup_Id, BLAcesso.ObterUsuario.Company_Id)
            End If

        Catch ex As Exception
            TrataErro(ex)

        Finally
            objBLCustomerType = Nothing

        End Try

    End Sub

#End Region

#End Region

#Region " Recupera DataTable "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Transforma o Datagrid em um DataTable
    ''' </summary>
    ''' <param name="dtgGrid"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[pvbraga]	30/10/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function RecuperaDatatable(ByVal pdtgGrid As DataGrid) As DataTable

        Dim dttGrid As New DataTable
        Me.ConfiguraDataTable(dttGrid)

        Dim dtrRow As DataRow

        For intDataGrid As Integer = 0 To pdtgGrid.Items.Count - 1
            dtrRow = dttGrid.NewRow
            For intDataTable As Integer = 0 To dttGrid.Columns.Count - 1
                dtrRow.Item(intDataTable) = pdtgGrid.Items(intDataGrid).Cells(intDataTable).Text
            Next
            dttGrid.Rows.Add(dtrRow)
        Next

        Return dttGrid

    End Function

#End Region

#Region " Remover Linha "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Remove linha do DataTable
    ''' </summary>
    ''' <param name="pintContactId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[pvbraga]	30/10/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub RemoverLinha(ByVal pintContactId As Integer)

        Dim dttTemp As DataTable
        Dim drLinha() As DataRow

        dttTemp = RecuperaDatatable(dtgDados)
        drLinha = dttTemp.Select("Contact_ID = " & Convert.ToString(pintContactId))

        If Not IsNothing(drLinha) AndAlso Not IsNothing(drLinha(0)) Then
            dttTemp.Rows.Remove(drLinha(0))
        End If

        Me.DataTableContact = dttTemp
        Me.Bind()

    End Sub

#End Region

#End Region

#Region " Propriedades "

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Propriedade armazena o código da cidade em uma ViewState.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	29/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Property CodCidade() As Int32
        Get
            If Not IsNothing(Me.ViewState("vsCodCidade")) Then
                Return Convert.ToInt32(Me.ViewState("vsCodCidade"))
            Else
                Return 0
            End If
        End Get
        Set(ByVal Value As Int32)
            Me.ViewState.Add("vsCodCidade", Value)
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Propriedade armazena o código do estado em uma ViewState.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	29/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Property CodEstado() As Int32
        Get
            If Not IsNothing(Me.ViewState("vsCodEstado")) Then
                Return Convert.ToInt32(Me.ViewState("vsCodEstado"))
            Else
                Return 0
            End If
        End Get
        Set(ByVal Value As Int32)
            Me.ViewState.Add("vsCodEstado", Value)
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Propriedade armazena o código do contato em uma ViewState.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	29/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Property ContactId() As Int32
        Get
            If Not IsNothing(Me.ViewState("vsCodContact")) Then
                Return Convert.ToInt32(Me.ViewState("vsCodContact"))
            Else
                Return 0
            End If
        End Get
        Set(ByVal Value As Int32)
            Me.ViewState.Add("vsCodContact", Value)
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Propriedade armazena o telefone do contato em uma ViewState.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	29/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Property ContactPhone() As String
        Get
            If Not IsNothing(Me.ViewState("vsContactPhone")) Then
                Return Me.ViewState("vsContactPhone").ToString
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal Value As String)
            Me.ViewState.Add("vsContactPhone", Value)
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Propriedade armazena a posição do contato em uma ViewState.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	29/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Property ContactPosition() As String
        Get
            If Not IsNothing(Me.ViewState("vsContactCargo")) Then
                Return Me.ViewState("vsContactCargo").ToString
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal Value As String)
            Me.ViewState.Add("vsContactCargo", Value)
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Propriedade armazena o código do cliente em uma ViewState.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[roliveira]	29/9/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Property CustomerId() As Integer
        Get
            If Not IsNothing(Me.ViewState("vsCustomerId")) Then
                Return Convert.ToInt32(Me.ViewState("vsCustomerId"))
            Else
                Return 0
            End If
        End Get
        Set(ByVal Value As Integer)
            Me.ViewState.Add("vsCustomerId", Value)
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Propriedade que guarda os parametros utilizados na listagem de clientes
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[rcoelho]	02/11/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property DadosFiltro() As String
        Get
            If Not IsNothing(Me.ViewState.Item("vsDadosFiltro")) Then
                Return Convert.ToString(Me.ViewState.Item("vsDadosFiltro"))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal Value As String)
            Me.ViewState.Add("vsDadosFiltro", Value)
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Propriedade que guarda os parametros utilizados na listagem de clientes
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[rcoelho]	02/11/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Property DadosFiltroLocalizar() As String
        Get
            If Not IsNothing(Me.ViewState.Item("vsDadosFiltroLocalizar")) Then
                Return Convert.ToString(Me.ViewState.Item("vsDadosFiltroLocalizar"))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal Value As String)
            Me.ViewState.Add("vsDadosFiltroLocalizar", Value)
        End Set
    End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Propriedade que guarda os registros do grid de contatos.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[rcoelho]	02/11/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Property DataTableContact() As DataTable
        Get
            If Not IsNothing(Me.ViewState.Item("vsDataTableContact")) Then
                Return CType(Me.ViewState.Item("vsDataTableContact"), DataTable)
            Else
                Return New DataTable
            End If
        End Get
        Set(ByVal Value As DataTable)
            Me.ViewState.Add("vsDataTableContact", Value)
        End Set
    End Property


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''     Primeiro horario da Empresa
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MtzRodrigoOliveira]	30/6/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Property PrimeiroHorario() As DateTime
        Get
            If Not IsNothing(Me.ViewState.Item("vsPrimeiroHorario")) Then
                Return Convert.ToDateTime((Me.ViewState.Item("vsPrimeiroHorario")))
            Else
                Return #12:00:00 AM#
            End If
        End Get
        Set(ByVal Value As DateTime)
            Me.ViewState.Add("vsPrimeiroHorario", Value)
        End Set
    End Property

#End Region

    

End Class
