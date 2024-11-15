Sys.Application.add_load(ApplicationLoadHandler);
function ApplicationLoadHandler()
{
	Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequest);
	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
}