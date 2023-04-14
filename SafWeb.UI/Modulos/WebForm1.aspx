<%@ Page language="C#" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.WebControls" Assembly="RadGrid.Net2" %>
<%@ Register TagPrefix="cc1" Namespace="FrameWork.WebControl" Assembly="FrameWork.WebControl" %>
<%@ Register TagPrefix="rada" Namespace="Telerik.WebControls" Assembly="RadAjax.Net2" %>

<form id="form1" runat="server">
                                        <table class="MarginTopConteudo" cellspacing="0" cellpadding="0" width="740" align="center"
                                            border="0">                                            
                                            <tr>
                                                <td class="cadlbl" style="width: 230px">
                                                    <asp:Label ID="blJornadaList" runat="server" Text="Jornada:"></asp:Label>
                                                </td>
                                                <td class="style1">
                                                    &nbsp;</td> 
                                                <td class="cadlbl">
                                                    &nbsp;</td> 
                                                <td class="cadlbl">
                                                    &nbsp;</td> 
                                            </tr>
                                            <tr>
                                                <td style="height: 19px; width: 230px;">
                                                    <asp:DropDownList ID="ddlJornadaCad" runat="server" CssClass="cadddl" 
                                                        AutoPostBack="true" Width="150px" Enabled="true">
                                                    </asp:DropDownList>                                                    
                                                </td>
                                                <td class="style2">
                                                    &nbsp;</td>  
                                                <td style="height: 19px">
                                                    &nbsp;</td>  
                                                <td style="height: 19px">
                                                    &nbsp;</td>  
                                            </tr>                                    
                                            <tr>
                                                <td class="cadlbl">
                                                    &nbsp;</td>
                                                <td class="style1">
                                                    &nbsp;</td>
                                                <td class="cadlbl">
                                                    &nbsp;</td>
                                                <td class="cadlbl">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblDescricaoList" runat="server" Text="Descrição:"></asp:Label>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblHorarioCad" runat="server" Text="Horário:"></asp:Label>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblHorarioCad0" runat="server" Text="Dur. Refeição:"></asp:Label>
                                                </td>
                                                <td class="cadlbl">
                                                    <asp:Label ID="lblCodigoEscalaCad" runat="server" Text="Código Escala:"></asp:Label>
                                                </td>
                                            </tr>                                     
                                            <tr>
                                                <td class="cadlbl">
                                                    <asp:TextBox ID="txtDescricaoCad" runat="server" MaxLength="50"
                                                         Width="217px" CssClass="cadtxt"/>                                                                                              
                                                </td>                            
                                                <td class="style1">
                                                                                                                                                <cc1:FWMascara ID="txtHorarioCad" runat="server" Width="99px" CssClass="cadtxt" MaxLength="10"
                                                                                                Mascara="HORA"></cc1:FWMascara><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                                                                    runat="server" ErrorMessage="- Campo Horário: Formato Inválido" ControlToValidate="txtHorarioCad"
                                                                                                    ValidationExpression="^([0-1][0-9]|[2][0-3]):[0-5][0-9]$">*</asp:RegularExpressionValidator>

                                                </td>
                                                </td>                            
                                                <td class="cadlbl">
                                                                                                                                                <cc1:FWMascara ID="txtDurRefeicaoCad" runat="server" Width="99px" 
                                                        CssClass="cadtxt" MaxLength="10"
                                                                                                Mascara="HORA"></cc1:FWMascara><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                                                                    runat="server" 
                                                        ErrorMessage="- Campo Dur. Refeição: Formato Inválido" ControlToValidate="txtDurRefeicaoCad"
                                                                                                    
                                                        ValidationExpression="^([0-1][0-9]|[2][0-3]):[0-5][0-9]$">*</asp:RegularExpressionValidator>

                                                </td>                            
                                                <td class="cadlbl">
                                                <cc1:fwmascara ID="txtCodEscalaCad" runat="server" CssClass="cadtxt" Mascara="NUMERO"
                                                    MaxLength="5" Width="75px"></cc1:fwmascara></td>                            
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center" style="height: 19px">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>

</form>
