<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="BajaActivos.aspx.cs" Inherits="App.Web.Logistica.RptBajaActivos" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphTitle" Runat="Server">
    Baja de activos
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <h5 class="alignCenter">
        <b>
            BAJA DE ACTIVOS
        </b>
    </h5>
        <table style="width: 950px;" class="tb">
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" CssClass="btn btn-primary" />
                    &nbsp;
                    <asp:Button ID="btnBajaActivo" runat="server" OnClick="btnBajaActivo_Click" Text="Dar de Baja Activo(s)" Enabled="False" CssClass="btn btn-primary" />
                    &nbsp;
                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" Enabled="False" OnClick="btnImprimir_Click" CssClass="btn btn-primary" />
                    <asp:TextBox ID="txtCondicion" runat="server" Visible="False" Width="16px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 200px;">
                    Motivo Baja Activo:
                </th>
                <td>
                    <asp:DropDownList ID="ddlMotivo" runat="server" CssClass="cajaTexto">
                    </asp:DropDownList>
                </td>
                <th style="width: 120px;">
                    Fecha de baja:
                </th>
                <td>
                    <asp:TextBox ID="txtFechaBaja" runat="server" TextMode="Date" CssClass="cajaTexto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    Disposición Final del Activo:
                </th>
                <td>
                    <asp:DropDownList ID="ddlDisposicion" runat="server" CssClass="cajaTexto">
                    </asp:DropDownList>
                </td>
                <th>
                    Código Activo:
                </th>
                <td>
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="cajaTexto"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="alignCenter" style="padding: 10px 0px;">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar y Agregar Activo" OnClick="btnBuscar_Click" CssClass="btn btn-primary" />
                </td>
            </tr>
        </table>

    <asp:GridView ID="gvActivo" DataKeyNames="IdLogistica" runat="server" BackColor="White" BorderColor="#999999" style="margin: 0 auto;"
                        BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="90%" 
        AutoGenerateColumns="False" OnRowCommand="gvActivo_RowCommand">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField HeaderText="Nª">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text="<%# (gvActivo.PageSize * gvActivo.PageIndex) + Container.DisplayIndex + 1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IdLogistica" HeaderText="IdLogistica" Visible="false" />
                            <asp:BoundField DataField="CODIGO" HeaderText="Código" />
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                            <asp:BoundField DataField="Origen" HeaderText="Origen" />
                            <asp:BoundField DataField="Detalle" HeaderText="Detalle" />
                            <asp:ButtonField ButtonType="Image" CommandName="quitar" ImageUrl="~/Iconos/x.png" HeaderImageUrl="~/Iconos/tachito.png">
                                <HeaderStyle Width="10px" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" />
                            </asp:ButtonField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                    </asp:GridView>
</asp:Content>
