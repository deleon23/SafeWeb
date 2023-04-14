<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadEscalaHorarioColaborador.aspx.cs" Inherits="SafWeb.UI.Modulos.Escala.CadEscalaHorarioColaborador" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>

<html>
<head runat="server">
    <title>%%SITE%%</title> 
    <link href="../../Estilos/FrameWork.css" type="text/css" rel="stylesheet"/>
    <link href="../../Estilos/Brinks.css" rel="stylesheet" type="text/css" />
    <link href="%%PATH%%/Web20/Calendar.Web20.css" rel="stylesheet" type="text/css" />
   
    <script language="javascript">                
        function PreencherHiddenColaboradores(colaboradores)
		{
            document.getElementById("txtHiddenColaboradores").value = colaboradores;
			
			AjaxNS.AR('Colaboradores','', 'radAjaxHorarios');
		}
		
		javascript:history.forward(1);
		
		function RefleshPagina()
		{	
		    alert(1);
			
		}
			 
       		
		//desabilita o bot�o direito do mouse
	    function click()
	    {
            if (event.button == 2 || event.button == 3)
            {
                oncontextmenu='return false';
            }
        }
        document.onmousedown = click;
        document.oncontextmenu = new Function("return false;");
        
        //bloquea a teclas de voltar alt + setas
        function tecla()
        {
            var tecla=window.event.keyCode;
            if (tecla == 18)
            {
                 event.keyCode=0;
                 event.returnValue=false;  
            } 
        }
        document.onkeydown=tecla;

    </script>
    
</head>
<body>
    <form id="form" runat="server" method="post">   
         <table cellspacing="0" cellpadding="0" width="778" align="center" border="0">
            <tr>
                <td style="width: 1010px">
                    <!-- ********************* START: CABE�ALHO ****************** -->
                    <cc1:FWServerControl ID="FWServerControlCabecalho" runat="server" Controle="Pagebanner"
                        Arquivo="/Modulos/Framework/CabecalhoBrinks.ascx"></cc1:FWServerControl>
                    <!-- ********************* END: CABE�ALHO ****************** -->
                </td>
            </tr>
            <tr>
                <!-- ********************* START: CONTE�DO ****************** -->
                <td class="backbox" valign="top" height="250" style="width: 1010px">                   
                        
                        <!-- ********************* PAINEL DE ERRO ****************** -->
                        <asp:Panel ID="pnlErro" runat="server"/>                     
                                                                        
                        <!-- ***************** START: BARRA DE TITUTO ******************* -->
                        <table class="MarginTopBarra" cellspacing="0" cellpadding="0" width="760" align="center"
                            border="0">
                            <tr>
                                <td class="cadBarraTitulo" align="left" style="height: 25px; width: 750px">
                                    <img src="../../Imagens/comum/bulletbarra.gif" align="absMiddle">
                                    <asp:Label ID="Label2" runat="server" Text="Lan�amento de Escalas - Sele��o de hor�rios de entrada"></asp:Label>
                                </td>
                                <td class="cadBarraTitulo" align="right" height="25" style="width: 9px">
                                    <asp:ImageButton ID="btnHelpCad" runat="server" AlternateText="Ajuda" ImageUrl="%%PATH%%/imagens/icones/ico_help.jpg"
                                        ImageAlign="AbsMiddle" Visible="False" />
                                </td>
                            </tr>
                            <!-- ***************** END: BARRA DE TITUTO ******************* -->                            
                            <tr>
                                <td class="backboxconteudo" valign="top" style="height: 250px" colspan="2">                                 
                                    
                                    <!-- ****************** START: FORMULARIO *********************** -->
                                    <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                        border="0">
                                        <tr>
                                            <td class="cadmsg">
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" />
                                                <asp:Label ID="lblMensagem" runat="server" Text="Label" Visible="false" CssClass="cadlbl"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top:10px; padding-bottom:20px;"> 
                                                 <rada:RadAjaxPanel ID="RadAjaxPanel1" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                                                        LoadingPanelID="lpaCadastro" EnableAJAX="false"> 
                                                                                                                                                                     
                                                    <!-- ****************** START: PAINEL DATA *********************** -->
                                                                                                                            
                                                    <asp:Panel ID="pnlData" runat="server">                                          
                                                                                                            
                                                        <table width="740" cellspacing="0" cellpadding="0" align="center" border="0">
                                                            <tr>
                                                                <td align="right" width="500">
                                                                    <telerik:RadCalendar ID="clrPeriodoEscala" runat="server" Font-Names="Arial,Verdana,Tahoma"
                                                                        ForeColor="Black" Style="border-color: #ececec" EnableViewSelector="true" 
                                                                        CultureInfo="Portuguese (Brazil)" FocusedDate="2009-11-11" 
                                                                        RangeMinDate="2009-11-11" SelectedDate="" Skin="Web20" 
                                                                        FastNavigationNextImage="%%PATH%%/Web20/Calendar/fastNavRight.gif" 
                                                                        FastNavigationPrevImage="%%PATH%%/Web20/Calendar/fastNavLeft.gif" 
                                                                        NavigationNextImage="%%PATH%%/Web20/Calendar/arrowRight.gif" 
                                                                        NavigationPrevImage="%%PATH%%/Web20/Calendar/arrowLeft.gif" EnableEmbeddedSkins="False">
                                                                        <DisabledDayStyle CssClass="calendarHolidays_" />
                                                                    </telerik:RadCalendar>
                                                                </td>
                                                                <td class="cadlbl" align="left" width="240" valign="bottom" style="padding-left:20px; padding-bottom:0px;">    
                                                                    <asp:Button ID="btnCriarEscala" runat="server" CssClass="cadbutton100" Text="Criar Escala" OnClick="btnCriarEscala_Click"/>                                                    
                                                                </td>                                                                                                                                                                                           
                                                            </tr>                                                                                                                                                                    
                                                        </table>                                                    
                                                    </asp:Panel>                                                        
                                                
                                                    <!-- ********************* END: PAINEL DATA ************************** --> 
                                                                                                                                                       
                                                        <!-- ****************** START: PAINEL HORA COLABORADOR *********************** -->
                                                        <asp:Panel ID="pnlHoraColaborador" runat="server"> 
                                                            <table width="740" cellspacing="0" cellpadding="0" align="center" style="border: 0px">                                                                
                                                                <tr>
                                                                    <td class="cadlbl"  width="190px" style="padding-top: 20px; padding-left:10px;">  
                                                                         <table width="735" cellspacing="0" cellpadding="0" align="center" style="border: 0px">
                                                                            <tr>
                                                                                <td class="cadlbl" width="190px" style="padding: 10px 0px 5px 0px;">
                                                                                    <asp:Label ID="lblDatasSelecionadas" runat="server" Text="Data(s) selecionada(s):"></asp:Label>                                                                        
                                                                                </td>                                                                                
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="cadlblBlack"  width="735" style="padding-bottom:20px;">
                                                                                    <asp:Label ID="lblDatSelResp" runat="server"></asp:Label>                                                                        
                                                                                </td>                                                                                                                                                                                            
                                                                            </tr> 
                                                                         </table>                                                                
                                                                    </td>
                                                                </tr>                                                                                                                               
                                                            </table>                                                                                               
                                                            
                                                             <rada:RadAjaxPanel ID="radAjaxHorarios" runat="server" ClientEvents-OnRequestStart="OnRequestStart"
                                                                LoadingPanelID="lpaCadastro">   
                                                                                                                                                         
                                                                <asp:Panel ID="pnlListHorarioColaboradores" runat="server"></asp:Panel>
                                                                <asp:HiddenField ID="txtHiddenColaboradores" runat="server" />
                                                        
                                                                <table width="740" cellspacing="0" cellpadding="0" align="center" style="border: 1px solid black">
                                                                    <tr>
                                                                        <td class="cadlbl"  width="190px" style="padding-top: 20px; padding-left:10px;">
                                                                            <asp:Label ID="lblHorarios" runat="server" Text="Hor�rios:"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="rfvHorarios" runat="server" ControlToValidate="lstHorarios"
                                                                                ErrorMessage="Campo Obrigat�rio: Horarios." InitialValue="0"
                                                                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td class="cadlbl"  width="350px"  style="padding-top: 20px;">
                                                                            <asp:Label ID="lblColaboradores" runat="server" Text="Colaboradores:"></asp:Label>
                                                                            <asp:RequiredFieldValidator ID="rfvColaboradores" runat="server" ControlToValidate="lstColaboradores"
                                                                                ErrorMessage="Campo Obrigat�rio: Colaboradores." InitialValue="0"
                                                                                SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                        </td>                                                                                                                                                                                            
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="190px" style="padding-left:10px; padding-bottom:10px;">                                                                     
                                                                            <asp:ListBox ID="lstHorarios" runat="server" Height="250px" Width="130px"
                                                                                CssClass="cadlstBox" SelectionMode="Single">
                                                                            </asp:ListBox>
                                                                        </td>
                                                                        <td width="350px" style="padding-bottom:10px;">
                                                                            <asp:ListBox ID="lstColaboradores" runat="server" Height="250px" Width="355px"
                                                                                SelectionMode="Multiple" CssClass="cadlstBox"></asp:ListBox>                                                                
                                                                        </td>
                                                                        <td class="cadlbl" align="left" width="170px" valign="bottom" style="padding-bottom:20px; padding-left:55px;">                                                                                  
                                                                              <asp:Button ID="btnInserir" runat="server" CssClass="cadbutton100" Text="Inserir" OnClick="btnInserir_Click"/>                                                                          
                                                                        </td>  
                                                                        
                                                                        
                                                                        
                                                                    </tr> 
                                                                        
                                                                    <tr>
                                                                        <td width="190px" style="padding-left:10px; padding-bottom:10px;">                                                                     

                                                                            <asp:CheckBox ID="chkHoraExtra" runat="server" Height="20px" Width="200px"
                                                                                SelectionMode="Single" CssClass="cadchk" Font-Bold="true" ForeColor="DarkRed" Visible="false" Text="Fora de escala (hora extra)"></asp:CheckBox>                                                                
                                                                        </td>
                                                                        <td width="350px" style="padding-bottom:10px;">
                                                                        </td>
                                                                        <td class="cadlbl" align="left" width="170px" valign="bottom" style="padding-bottom:20px; padding-left:55px;">                                                                                  
                                                                        </td>                                                                                                                                                                                           
                                                                    </tr>    
                                                                    
                                                                                                                                                                                                                                                                                                                                     
                                                                                                                                                                                                                                        
                                                                </table>                                           
                                                        
                                                                <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                                                        style="border: 1px solid black">
                                                                        <tr>
                                                                            <td align="center" colspan="3" style="padding-top:20px;">
                                                                                <!-- ********************* STAR: RADGRID ************************** -->
                                                                                <rad:RadGrid ID="radGridHorariosColaboradores" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                    AutoGenerateColumns="False" CssClass="dtg" GridLines="None" Skin="None" Width="100%" Height="250px"
                                                                                    OnItemCommand="radGridHorariosColaboradores_ItemCommand" OnItemDataBound="radGridHorariosColaboradores_ItemDataBound"
                                                                                    OnNeedDataSource="radGridHorariosColaboradores_NeedDataSource">
                                                                                    <GroupHeaderItemStyle CssClass="dtgGroupHeader"></GroupHeaderItemStyle>
                                                                                    <GroupPanel Text="">
                                                                                        <PanelStyle CssClass="dtgGroupPanel"></PanelStyle>
                                                                                        <PanelItemsStyle CssClass="dtgGroupPanelItem"></PanelItemsStyle>
                                                                                    </GroupPanel>
                                                                                    <AlternatingItemStyle CssClass="dtgItemStyle"></AlternatingItemStyle>
                                                                                    <ItemStyle CssClass="dtgItemStyleAlternate"></ItemStyle>
                                                                                    <HeaderStyle CssClass="dtgHeaderStyle"></HeaderStyle>
                                                                                    <MasterTableView>
                                                                                        <NoRecordsTemplate>
                                                                                            <div>
                                                                                                <table cellpadding="0" cellspacing="0" height="250" width="100%">
                                                                                                    <tr>
                                                                                                        <td class="cadlbl">
                                                                                                            <% = FrameWork.BusinessLayer.Idioma.BLIdiomas.TraduzirMensagens(FrameWork.Model.Idioma.Mensagens.CONSULTA_SEM_RETORNO) %>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </div>
                                                                                        </NoRecordsTemplate>
                                                                                        <PagerTemplate>
                                                                                            <table align="center" border="0" cellpadding="5" cellspacing="0" class="pag" height="15"
                                                                                                width="100%">
                                                                                                <tr>
                                                                                                    <td align="center" valign="center">
                                                                                                        <asp:Panel ID="pnlPaginaAtual" runat="server" DefaultButton="btnIr">
                                                                                                            <asp:ImageButton ID="imgPrimeira" runat="server" CausesValidation="false" CommandArgument="First"
                                                                                                                CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq.gif" />
                                                                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandArgument="First"
                                                                                                                CommandName="Page" CssClass="pagLink" Enabled="True">Primeira</asp:LinkButton>
                                                                                                            <asp:ImageButton ID="imgAnterior" runat="server" CausesValidation="false" CommandArgument="Prev"
                                                                                                                CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setaesq2.gif" />
                                                                                                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandArgument="Prev"
                                                                                                                CommandName="Page" CssClass="pagLink" Enabled="True">Anterior</asp:LinkButton>&#160;&#160;
                                                                                                            <asp:Label ID="Label3" runat="server" CssClass="paglbl">| P�gina:</asp:Label>

                                                                                                            <cc1:FWMascara ID="FWMascara1" runat="server" AutoPostBack="False" CssClass="pagtxtbox"
                                                                                                                Mascara="NUMERO" MaxLength="4" Text='<%# (int)(DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex")) + 1 %>'
                                                                                                                Width="50px"></cc1:FWMascara>


                                                                                                            <asp:Label ID="Label4" runat="server" CssClass="paglbl">de</asp:Label>
                                                                                                            <asp:Label ID="Label6" runat="server" CssClass="paglbl" Enabled="True"><%#DataBinder.Eval(Container, "Paging.PageCount").ToString()%></asp:Label>
                                                                                                            <asp:LinkButton ID="btnIr" runat="server" CommandName="IrPagina" CssClass="pagLink">Ir</asp:LinkButton>
                                                                                                            <span class="paglbl">&#160;|&#160;</span>
                                                                                                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandArgument="Next"
                                                                                                                CommandName="Page" CssClass="pagLink" Enabled="True">Pr�xima</asp:LinkButton>
                                                                                                            <asp:ImageButton ID="imgProxima" runat="server" CausesValidation="false" CommandArgument="Next"
                                                                                                                CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir2.gif" />
                                                                                                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandArgument="Last"
                                                                                                                CommandName="Page" CssClass="pagLink" Enabled="True">�ltima</asp:LinkButton>
                                                                                                            <asp:ImageButton ID="imgUltima" runat="server" CausesValidation="false" CommandArgument="Last"
                                                                                                                CommandName="Page" CssClass="pagImg" ImageUrl="~/imagens/icones/ico_setadir.gif" />
                                                                                                        </asp:Panel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </PagerTemplate>
                                                                                        <Columns>                                                                                    
                                                                                            <rad:GridBoundColumn DataField="HorarioColaborador" HeaderText="Hor�rios" SortExpression="HorarioColaborador"
                                                                                                UniqueName="HorarioColaborador">
                                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridBoundColumn DataField="NomesColaboradores" HeaderText="Colaboradores" SortExpression="Colaboradores"
                                                                                                UniqueName="Colaboradores">
                                                                                                <ItemStyle Width="25%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>                                                                      
                                                                                            <rad:GridBoundColumn DataField="CodigosColaboradores" SortExpression="CodigosColaboradores" UniqueName="CodigosColaboradores"
                                                                                                Visible="false">                                                                                
                                                                                            </rad:GridBoundColumn>
                                                                                            <rad:GridTemplateColumn UniqueName="TemplateColumn" Visible="False">
                                                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="Editar" runat="server" AlternateText="Editar" CausesValidation="False"
                                                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdEscalacao") %>' CommandName="Editar"
                                                                                                        ImageUrl="%%PATH%%/imagens/icones/ico_editar.gif" ToolTip="Editar"></asp:ImageButton>
                                                                                                </ItemTemplate>
                                                                                            </rad:GridTemplateColumn>  
                                                                                            <rad:GridBoundColumn DataField="Bloqueado" SortExpression="Bloqueado"
                                                                                                UniqueName="Bloqueado" Visible="false">
                                                                                                <ItemStyle Width="5%"></ItemStyle>
                                                                                            </rad:GridBoundColumn>                                                                               
                                                                                        </Columns>
                                                                                        <RowIndicatorColumn Visible="False">
                                                                                            <HeaderStyle Width="20px" />
                                                                                        </RowIndicatorColumn>
                                                                                        <ExpandCollapseColumn Resizable="False" Visible="False">
                                                                                            <HeaderStyle Width="20px" />
                                                                                        </ExpandCollapseColumn>
                                                                                    </MasterTableView>
                                                                                </rad:RadGrid>
                                                                                <!-- ********************* END: RADGRID ************************** -->
                                                                            </td>
                                                                        </tr>
                                                                    </table> 
                                                                </rada:RadAjaxPanel>                                                           
                                                            </asp:Panel>
                                                            <!-- ********************* END: PAINEL HORA COLABORADOR ************************** -->  
                                                 </rada:RadAjaxPanel>                                                                                                                               
                                                <rada:AjaxLoadingPanel ID="lpaCadastro" Width="75px" runat="server" HorizontalAlign="Center"
                                                    Transparency="30" Height="75px">
                                                    <asp:Image ID="Image1" Style="margin-top: 100px" runat="server" ImageUrl="%%PATH%%/RadControls/Ajax/Skins/Default/Loading.gif"
                                                        AlternateText="Aguarde ..."></asp:Image>
                                                </rada:AjaxLoadingPanel>                                                                                                                                                                                                   
                                            </td>
                                        </tr>
                                    </table>                                    
                                    
                                    <!-- ********************* START: BARRA DE BOT�ES INFERIOR ************************** -->
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="760">
                                        <caption>
                                        </caption>
                                        <tr>
                                            <td align="right" class="backBarraBotoes">                                          
                                                &nbsp;<asp:Button ID="btnVoltar" runat="server" CausesValidation="False" CssClass="cadbutton100"
                                                        Text="Voltar" Width="50px" OnClick="btnVoltar_Click" />
                                                &nbsp;
                                                <asp:Button ID="btnAvancar" runat="server" CssClass="cadbutton100"
                                                    Text="Avan�ar" OnClick="btnAvancar_Click"/>                                                
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- ********************* END: BARRA DE BOT�ES INFERIOR ************************** -->                                   
                                    
                                </td>
                            </tr>
                        </table>                                                                       
                    
                </td>
                <!-- ********************* END: CONTE�DO ****************** -->
            </tr>
            <tr>
                <td style="width: 1010px">
                    <!-- ********************* START: RODAP� ************************** -->
                    <cc1:FWServerControl ID="FWServerControlRodape" runat="server" Controle="Rodape"
                        Arquivo="/Modulos/Framework/RodapeBrinks.ascx"></cc1:FWServerControl>
                    <!-- ********************* END: RODAP� ************************** -->
                </td>
            </tr>
        </table>
        <asp:HtmlIframe id="IframeAgenda" runat="server" style="z-index: 101; left: -500px; visibility: visible;
            position: absolute; top: 0px" name="gToday:contrast:agenda.js" src="../../Agenda/calendario.htm"
            frameborder="0" width="132" scrolling="no" height="142"/>
    </form>
</body>
</html>