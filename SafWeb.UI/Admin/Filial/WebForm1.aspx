<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SafWeb.UI.Admin.Filial.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 16px;
        }
    </style>
</head>
<body style="height: 334px">
    <form id="form1" runat="server">
    <div style="height: 91px">
    
                                                                  <table width="740" cellspacing="0" cellpadding="0" align="center">    

                                            <tr>
                                                <td class="cadmsg" colspan="2">
                                                    <asp:ValidationSummary id="ValidationSummary1" runat="server" DisplayMode="List">
                                                    </asp:ValidationSummary><asp:Label id="lblMensagem" runat="server" Visible="false" 
                                                        Text="Label" CssClass="cadlbl"></asp:Label>
                                                </td>
                                                <td class="cadmsg">
                                                    &nbsp;</td>
                                            </tr>

                                            
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblRegionalCad" runat="server" Text="Regional:"></asp:Label>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblFilialCad" runat="server" Text="Sigla Filial:"></asp:Label>
                                                </td> 
                                                <td class="cadlbl" width="250px">
                                                    <asp:Label ID="lblFilial" runat="server" Text="Filial:"></asp:Label>
                                                </td> 
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:DropDownList ID="ddlRegionalCad" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="150px" OnSelectedIndexChanged="ddlRegionalCad_SelectedIndexChanged" Enabled="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvRegional" runat="server" ControlToValidate="ddlRegionalCad"
                                                        ErrorMessage="Campo Obrigatório: Regional." InitialValue="0"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                                
                                                <td class="cadlbl">
                                                    <asp:TextBox ID="txtSiglaFilial" runat="server" MaxLength="50"
                                                         Width="150px" CssClass="cadtxt"/>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSiglaFilial"
                                                        ErrorMessage="Campo Obrigatório: Sigla Filial." SetFocusOnError="True">*</asp:RequiredFieldValidator>                                                                                    
                                                </td>  
                                                
                                                <td width="250px" style="height: 19px">
                                                    <asp:TextBox ID="txtFilial" runat="server" MaxLength="50"
                                                         Width="150px" CssClass="cadtxt"/>
                                                    <asp:RequiredFieldValidator ID="rfvFilial_" runat="server" ControlToValidate="txtFilial"
                                                        ErrorMessage="Campo Obrigatório: Filial." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>  
                                            </tr>                                       
                                            </tr>                                       
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblCidade" runat="server" Text="Cidade:"></asp:Label>
                                                </td> 
                                                <td class="cadlbl" width="200px">
                                                    <asp:Label ID="lblFusoHorario" runat="server" Text="Fuso horário:"></asp:Label>
                                                </td> 
                                            </tr>
                                            <tr>
                                                <td class="cadlblcadlbl">
                                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" Enabled="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEstado" runat="server" ControlToValidate="ddlEstado"
                                                        ErrorMessage="Campo Obrigatório: Estado." InitialValue="0"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:DropDownList ID="ddlCidade" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="150px" Enabled="false">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCidade" runat="server" ControlToValidate="ddlCidade"
                                                        ErrorMessage="Campo Obrigatório: Cidade." InitialValue="0"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td> 
                                                <td class="cadlbl" width="200px">
                                                    <asp:DropDownList ID="ddlFusoHorario" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="150px" Enabled="true" Height="16px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvFusoHorario" runat="server" ControlToValidate="ddlFusoHorario"
                                                        ErrorMessage="Campo Obrigatório: Fuso Horário." InitialValue="0"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td> 
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                                    &nbsp;</td>
                                                <td class="cadlbl">
                                                    &nbsp;</td>
                                                <td width="200px" style="height: 19px">
                                                    &nbsp;</td>
                                            </tr>






                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="Label2" runat="server" Text="Código Filial Corporate:"></asp:Label>
                                                </td>

                                                <td class="cadlbl">
                                                    <asp:Label ID="Label7" runat="server" Text="Tolerância no acesso:"></asp:Label>
                                                </td>


                                                <td class="cadlbl">
                                                    <asp:CheckBox ID="chkPortariaValAcesso" runat="server" Text="Portaria valida acesso?" Width="188px" CssClass="cadchk"/>
                                                    </td> 
                                            </tr>



                                            <tr>
                                                <td class="style1">
                                                    <asp:TextBox ID="txtCodigoFilialCorporate" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="149px" Enabled="true"/>
                                                    <asp:RequiredFieldValidator ID="rfvCodigoFilialCorporate" runat="server" ControlToValidate="txtCodigoFilialCorporate"
                                                        ErrorMessage="Campo Obrigatório: Código Filial Corporate." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                                
                                                <td class="style1">
                                                    <asp:TextBox ID="txtQtdToleranciaAcesso" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="false" Width="149px" Enabled="true"/>
                                                    <asp:RequiredFieldValidator ID="rfvToleranciaAcesso" runat="server" ControlToValidate="txtQtdToleranciaAcesso"
                                                        ErrorMessage="Campo Obrigatório: Tolerância no acesso." SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td width="250px" class="style1">
                                                    <asp:CheckBox ID="chkColetorOnline" runat="server" CssClass="cadchk" 
                                                        Text="Possui coletores de acesso online?" Width="231px" Enabled="true" 
                                                        Height="16px"/>
                                                </td>
                                            </tr>                                       



                                            <tr>
                                                <td class="cadlbl">
                                                </td>
                                                <td class="cadlbl">
                                                </td>
                                                <td class="cadlbl" width="250px">
                                                    &nbsp;</td>
                                            </tr>


                                            </table>





    </div>
    </form>
</body>
</html>
