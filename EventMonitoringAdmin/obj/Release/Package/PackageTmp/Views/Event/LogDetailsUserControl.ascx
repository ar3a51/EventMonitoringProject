<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EventMonitoringAdmin.EventManagementService.LogDetails>" %>

    <fieldset>
        <legend>Log Details</legend>
        
        <div class="display-label"><b>Event Id:</b></div>
        <div class="display-field"><%= Html.Encode(Model.eventId) %></div>
        
        <div class="display-label"><b>Source:</b></div>
        <div class="display-field"><%= Html.Encode(Model.source) %></div>
        
        <div class="display-label"><b>Date and time:</b></div>
        <div class="display-field"><%= Html.Encode(String.Format("{0:g}", Model.dtGenerated)) %></div>
        
        <div class="display-label"><b>User Name:</b></div>
        <div class="display-field"><%= Html.Encode(Model.chUsername) %></div>
        
        <div class="display-label"><b>Type:</b></div>
        <div class="display-field"><%= Html.Encode(Model.type) %></div>
        
        <div class="display-label"><b>GUID:</b></div>
        <div class="display-field"><%= Html.Encode(Model.guid) %></div>
        
        <div class="display-label"><b>Error Message:</b></div>
        <div class="display-field"><%= Html.Encode(Model.errMessage) %></div>
        
    </fieldset>
   


