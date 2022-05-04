<%@ Application Language="C#" Inherits="region4.escWeb.global_aspx" %>

<script runat="server">
       
    protected override void LoadObjectProvider()
    {
        Application["objectProvider"] = new ObjectProvider();
    }
   
       
</script>
