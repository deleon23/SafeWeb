if(typeof window.RadControlsNamespace=="undefined"){
window.RadControlsNamespace={};
}
if(typeof (window.RadControlsNamespace.DomEventMixin)=="undefined"||typeof (window.RadControlsNamespace.DomEventMixin.Version)==null||window.RadControlsNamespace.DomEventMixin.Version<3){
RadControlsNamespace.DomEventMixin={Version:3,Initialize:function(_1){
_1.CreateEventHandler=this.CreateEventHandler;
_1.AttachDomEvent=this.AttachDomEvent;
_1.DetachDomEvent=this.DetachDomEvent;
_1.DisposeDomEventHandlers=this.DisposeDomEventHandlers;
_1._domEventHandlingEnabled=true;
_1.EnableDomEventHandling=this.EnableDomEventHandling;
_1.DisableDomEventHandling=this.DisableDomEventHandling;
_1.RemoveHandlerRegister=this.RemoveHandlerRegister;
_1.GetHandlerRegister=this.GetHandlerRegister;
_1.AddHandlerRegister=this.AddHandlerRegister;
_1.handlerRegisters=[];
},EnableDomEventHandling:function(){
this._domEventHandlingEnabled=true;
},DisableDomEventHandling:function(){
this._domEventHandlingEnabled=false;
},CreateEventHandler:function(_2,_3){
var _4=this;
return function(e){
if(!_4._domEventHandlingEnabled&&!_3){
return;
}
return _4[_2](e||window.event);
};
},AttachDomEvent:function(_6,_7,_8,_9){
var _a=this.CreateEventHandler(_8,_9);
var _b=this.GetHandlerRegister(_6,_7,_8);
if(_b!=null){
this.DetachDomEvent(_b.Element,_b.EventName,_8);
}
var _c={"Element":_6,"EventName":_7,"HandlerName":_8,"Handler":_a};
this.AddHandlerRegister(_c);
if(_6.addEventListener){
_6.addEventListener(_7,_a,false);
}else{
if(_6.attachEvent){
_6.attachEvent("on"+_7,_a);
}
}
},DetachDomEvent:function(_d,_e,_f){
var _10=null;
var _11="";
if(typeof _f=="string"){
_11=_f;
_10=this.GetHandlerRegister(_d,_e,_11);
if(_10==null){
return;
}
_f=_10.Handler;
}
if(!_d){
return;
}
if(_d.removeEventListener){
_d.removeEventListener(_e,_f,false);
}else{
if(_d.detachEvent){
_d.detachEvent("on"+_e,_f);
}
}
if(_10!=null&&_11!=""){
this.RemoveHandlerRegister(_10);
_10=null;
}
},DisposeDomEventHandlers:function(){
for(var i=0;i<this.handlerRegisters.length;i++){
var _13=this.handlerRegisters[i];
if(_13!=null){
this.DetachDomEvent(_13.Element,_13.EventName,_13.Handler);
}
}
this.handlerRegisters=[];
},RemoveHandlerRegister:function(_14){
try{
var _15=_14.index;
for(var i in _14){
_14[i]=null;
}
this.handlerRegisters[_15]=null;
}
catch(e){
}
},GetHandlerRegister:function(_17,_18,_19){
for(var i=0;i<this.handlerRegisters.length;i++){
var _1b=this.handlerRegisters[i];
if(_1b!=null&&_1b.Element==_17&&_1b.EventName==_18&&_1b.HandlerName==_19){
return this.handlerRegisters[i];
}
}
return null;
},AddHandlerRegister:function(_1c){
_1c.index=this.handlerRegisters.length;
this.handlerRegisters[this.handlerRegisters.length]=_1c;
}};
RadControlsNamespace.DomEvent={};
RadControlsNamespace.DomEvent.PreventDefault=function(e){
if(!e){
return true;
}
if(e.preventDefault){
e.preventDefault();
}
e.returnValue=false;
return false;
};
RadControlsNamespace.DomEvent.StopPropagation=function(e){
if(!e){
return;
}
if(e.stopPropagation){
e.stopPropagation();
}else{
e.cancelBubble=true;
}
};
RadControlsNamespace.DomEvent.GetTarget=function(e){
if(!e){
return null;
}
return e.target||e.srcElement;
};
RadControlsNamespace.DomEvent.GetRelatedTarget=function(e){
if(!e){
return null;
}
return e.relatedTarget||(e.type=="mouseout"?e.toElement:e.fromElement);
};
RadControlsNamespace.DomEvent.GetKeyCode=function(e){
if(!e){
return 0;
}
return e.which||e.keyCode;
};
};if(typeof window.RadControlsNamespace=="undefined"){
window.RadControlsNamespace={};
}
if(typeof (window.RadControlsNamespace.EventMixin)=="undefined"||typeof (window.RadControlsNamespace.EventMixin.Version)==null||window.RadControlsNamespace.EventMixin.Version<2){
RadControlsNamespace.EventMixin={Version:2,Initialize:function(_1){
_1._listeners={};
_1._eventsEnabled=true;
_1.AttachEvent=this.AttachEvent;
_1.DetachEvent=this.DetachEvent;
_1.RaiseEvent=this.RaiseEvent;
_1.EnableEvents=this.EnableEvents;
_1.DisableEvents=this.DisableEvents;
_1.DisposeEventHandlers=this.DisposeEventHandlers;
},DisableEvents:function(){
this._eventsEnabled=false;
},EnableEvents:function(){
this._eventsEnabled=true;
},AttachEvent:function(_2,_3){
if(!this._listeners[_2]){
this._listeners[_2]=[];
}
this._listeners[_2][this._listeners[_2].length]=(RadControlsNamespace.EventMixin.ResolveFunction(_3));
},DetachEvent:function(_4,_5){
var _6=this._listeners[_4];
if(!_6){
return false;
}
var _7=RadControlsNamespace.EventMixin.ResolveFunction(_5);
for(var i=0;i<_6.length;i++){
if(_7==_6[i]){
_6.splice(i,1);
return true;
}
}
return false;
},DisposeEventHandlers:function(){
for(var _9 in this._listeners){
var _a=null;
if(this._listeners.hasOwnProperty(_9)){
_a=this._listeners[_9];
for(var i=0;i<_a.length;i++){
_a[i]=null;
}
_a=null;
}
}
},ResolveFunction:function(_c){
if(typeof (_c)=="function"){
return _c;
}else{
if(typeof (window[_c])=="function"){
return window[_c];
}else{
return new Function("var Sender = arguments[0]; var Arguments = arguments[1];"+_c);
}
}
},RaiseEvent:function(_d,_e){
if(!this._eventsEnabled){
return true;
}
var _f=true;
if(this[_d]){
var _10=RadControlsNamespace.EventMixin.ResolveFunction(this[_d])(this,_e);
if(typeof (_10)=="undefined"){
_10=true;
}
_f=_f&&_10;
}
if(!this._listeners[_d]){
return _f;
}
for(var i=0;i<this._listeners[_d].length;i++){
var _12=this._listeners[_d][i];
var _10=_12(this,_e);
if(typeof (_10)=="undefined"){
_10=true;
}
_f=_f&&_10;
}
return _f;
}};
};function RadTextBox(id,_2,_3){
this.DisposeOldInstance(id);
this.Constructor(id);
this.Initialize(_2,_3);
}
RadTextBox.Extend=function(_4){
for(var i in this.prototype){
if(_4[i]){
continue;
}
_4[i]=this.prototype[i];
}
};
RadInputErrorReason={ParseError:0,OutOfRange:1};
RadTextBox.prototype={DisposeOldInstance:function(id){
try{
var _7=window[id];
if(_7!=null){
_7.Dispose();
window[id]=null;
}
}
catch(e){
}
},Constructor:function(id){
this.ClientID=id;
this.WrapperElementID=id+"_wrapper";
this.TextBoxElement=document.getElementById(id+"_text");
this.OriginalTextBoxCssText=this.TextBoxElement.style.cssText;
if(this.OriginalTextBoxCssText.indexOf(";")!=this.OriginalTextBoxCssText.length-1){
this.OriginalTextBoxCssText+=";";
}
this.UpdatePercentageHeight();
this.OriginalMaxLength=this.TextBoxElement.maxLength;
if(this.OriginalMaxLength==-1){
this.OriginalMaxLength=2147483647;
}
this.InitializeHiddenElement(id);
this.InitializeValidationField(id);
this.SelectionEnd=0;
this.SelectionStart=0;
this.Focused=false;
this.Enabled=true;
this.Hovered=false;
this.Invalid=false;
this.IsEmptyMessage=false;
RadControlsNamespace.EventMixin.Initialize(this);
RadControlsNamespace.DomEventMixin.Initialize(this);
},UpdatePercentageHeight:function(){
var _9=document.getElementById(this.WrapperElementID);
if(_9.style.height.indexOf("%")>-1){
if(_9.offsetHeight!=0){
this.TextBoxElement.style.height=_9.offsetHeight+"px";
this.OriginalTextBoxCssText+="height:"+this.TextBoxElement.style.height+";";
}else{
var _a=this;
window.setTimeout(function(){
_a.TextBoxElement.style.height=_9.offsetHeight+"px";
_a.OriginalTextBoxCssText+="height:"+_a.TextBoxElement.style.height+";";
},0);
}
}
},Dispose:function(){
this.DisposeDomEventHandlers();
this.DisposeEventHandlers();
if(this._restoreTimeoutId!=null){
clearTimeout(this._restoreTimeoutId);
this._restoreTimeoutId=null;
}
var _b;
for(_b in this){
this[_b]=null;
}
},CallBase:function(_c,_d){
return RadTextBox.prototype[_c].apply(this,_d);
},Initialize:function(_e,_f){
if((navigator.userAgent.indexOf("Firefox")==(-1))&&(this.TextBoxElement.type=="password")){
var obj=this;
setTimeout(function(){
obj._SetValue("");
obj.UpdateDisplayValue();
},0);
}
this.Styles=_f;
this.LoadCongfiguration(_e);
this.LoadClientEvents(_e);
this.AttachEventHandlers();
this.UpdateDisplayValue();
this.UpdateCssClass();
this.InitializeButtons();
var _11=this;
this.AttachDomEvent(window,"unload","Dispose");
this.InitialValue=this.GetValue();
this.RaiseEvent("OnLoad",null);
},LoadCongfiguration:function(_12){
for(var i in _12){
if(i!="ClientEvents"){
this[i]=_12[i];
}
}
},LoadClientEvents:function(_14){
var _15=null;
for(var _16 in _14.ClientEvents){
var _15=eval(_14.ClientEvents[_16]);
if(typeof (_15)=="function"){
this.AttachEvent(_16,_15);
}
}
},AttachEventHandlers:function(){
this.AttachToTextBoxEvent("keyup","TextBoxKeyUpHandler");
this.AttachToTextBoxEvent("keypress","TextBoxKeyPressHandler");
this.AttachToTextBoxEvent("blur","TextBoxBlurHandler");
this.AttachToTextBoxEvent("focus","TextBoxFocusHandler");
this.AttachToTextBoxEvent("mouseout","TextBoxMouseOutHandler");
this.AttachToTextBoxEvent("mouseover","TextBoxMouseOverHandler");
this.AttachToTextBoxEvent("keydown","TextBoxKeyDownHandler");
if(window.addEventListener){
this.AttachToTextBoxEvent("DOMMouseScroll","TextBoxMouseWheelHandler");
this.AttachToTextBoxEvent("dragdrop","TextBoxDragDropHandler");
}else{
this.AttachToTextBoxEvent("mousewheel","TextBoxMouseWheelHandler");
this.AttachToTextBoxEvent("drop","TextBoxDropHandler");
}
},InitializeHiddenElement:function(id){
this.HiddenElement=document.getElementById(id);
},InitializeValidationField:function(id){
},IsMultiLine:function(){
return this.TextBoxElement.tagName.toUpperCase()=="TEXTAREA";
},TextBoxKeyPressHandler:function(e){
var _1a=/MSIE/.test(navigator.userAgent);
var _1b=_1a?e.keyCode:e.which;
if(!this.RaiseEvent("OnKeyPress",{"DomEvent":e,"KeyCode":_1b,"KeyCharacter":String.fromCharCode(_1b)})){
return RadControlsNamespace.DomEvent.PreventDefault(e);
}
if((_1b==13)&&!this.IsMultiLine()){
this.UpdateHiddenValueOnKeyPress(e);
if(this.AutoPostBack){
this.RaisePostBackEvent();
}
return true;
}
},TextBoxKeyUpHandler:function(e){
this.UpdateHiddenValueOnKeyPress(e);
},UpdateHiddenValueOnKeyPress:function(){
this.UpdateHiddenValue();
},AttachToTextBoxEvent:function(_1d,_1e){
this.AttachDomEvent(this.TextBoxElement,_1d,_1e);
},TextBoxBlurHandler:function(e){
this.Focused=false;
var _20=this.GetTextBoxValue();
if(this.InitialValue!=_20){
this.SetValue(_20);
}else{
this.UpdateDisplayValue();
this.UpdateCssClass();
}
this.RaiseEvent("OnBlur",{"DomEvent":e});
},TextBoxFocusHandler:function(e){
this.Focused=true;
this.UpdateDisplayValue();
this.UpdateCssClass();
this.UpdateSelectionOnFocus();
this.RaiseEvent("OnFocus",{"DomEvent":e});
},TextBoxMouseOutHandler:function(e){
this.Hovered=false;
this.UpdateCssClass();
this.RaiseEvent("OnMouseOut",{"DomEvent":e});
},TextBoxMouseOverHandler:function(e){
this.Hovered=true;
this.UpdateCssClass();
this.RaiseEvent("OnMouseOver",{"DomEvent":e});
},TextBoxDropHandler:function(e){
this.SetValue(e.dataTransfer.getData("text"));
},TextBoxDragDropHandler:function(e){
this.SetValue(this.GetTextBoxValue());
},TextBoxMouseWheelHandler:function(e){
var _27;
if(this.Focused){
if(e.wheelDelta){
_27=e.wheelDelta/120;
if(window.opera){
_27=-_27;
}
}else{
if(e.detail){
_27=-e.detail/3;
}
}
if(_27>0){
this.HandleWheel(false);
}else{
this.HandleWheel(true);
}
return false;
}
return true;
},HandleWheel:function(_28){
},TextBoxKeyDownHandler:function(e){
},Disable:function(){
this.Enabled=false;
this.TextBoxElement.disabled="disabled";
this.UpdateCssClass();
this.RaiseEvent("OnDisable",null);
},Enable:function(){
this.Enabled=true;
this.TextBoxElement.disabled="";
this.UpdateCssClass();
this.RaiseEvent("OnEnable",null);
},Focus:function(){
this.TextBoxElement.focus();
},Blur:function(){
this.TextBoxElement.blur();
},SetValue:function(_2a){
var _2b=this.SetHiddenValue(_2a);
if(_2b==false){
_2a="";
}
this.TriggerDOMChangeEvent(this.GetValidationField());
this.RaiseValueChangedEvent(_2a,this.InitialValue);
if(typeof (_2b)=="undefined"||_2b==true){
this.SetTextBoxValue(this.GetEditValue());
this.UpdateDisplayValue();
this.UpdateCssClass();
}
},TriggerDOMChangeEvent:function(_2c){
if(_2c.fireEvent&&document.createEventObject){
var _2d=document.createEventObject();
_2c.fireEvent("onchange",_2d);
}else{
if(_2c.dispatchEvent){
var _2e=true;
var _2d=document.createEvent("HTMLEvents");
_2d.initEvent("change",_2e,true);
_2c.dispatchEvent(_2d);
}
}
},_SetValue:function(_2f){
var _30=this.SetHiddenValue(_2f);
if(typeof (_30)=="undefined"||_30==true){
this.SetTextBoxValue(this.GetEditValue());
}
},RaiseValueChangedEvent:function(_31,_32){
if(_31.toString()==_32.toString()){
return false;
}
this.InitialValue=this.GetValue();
var _33=this.RaiseEvent("OnValueChanged",this.ValueChangedEventArgs(_31,_32));
if(this.AutoPostBack&&_33){
this.RaisePostBackEvent();
}
return _33;
},Clear:function(){
this.SetValue("");
},SetTextBoxValue:function(_34){
if(this.TextBoxElement.value!=_34){
this.TextBoxElement.value=_34;
}
},GetTextBoxValue:function(_35){
return this.TextBoxElement.value;
},GetWrapperElement:function(){
return document.getElementById(this.WrapperElementID);
},UpdateDisplayValue:function(){
if(this.Focused){
this.SetTextBoxValue(this.GetEditValue());
}else{
if(this.IsEmpty()&&this.EmptyMessage){
this.TextBoxElement.maxLength=2147483647;
this.IsEmptyMessage=true;
this.SetTextBoxValue(this.EmptyMessage);
this.TextBoxElement.maxLength=this.OriginalMaxLength;
}else{
this.IsEmptyMessage=false;
this.SetTextBoxValue(this.GetDisplayValue());
}
}
},UpdateSelectionOnFocus:function(){
switch(this.SelectionOnFocus){
case 0:
break;
case 1:
this.SetCaretPosition(0);
break;
case 2:
if(this.TextBoxElement.value.length>0){
this.SetCaretPosition(this.TextBoxElement.value.length);
}
break;
case 3:
this.SelectAllText();
break;
default:
this.SetCaretPosition(0);
break;
}
},RaiseErrorEvent:function(_36){
if(this.InEventRaise){
return;
}
this.InEventRaise=true;
var _37=this.RaiseEvent("OnError",_36);
if(_37!=false){
this.Invalid=true;
this.ErrorHandlingCanceled=false;
this.UpdateCssClass();
var _38=this;
var _39=function(){
_38.Invalid=false;
_38.UpdateCssClass();
};
this._restoreTimeoutId=setTimeout(_39,100);
}else{
this.ErrorHandlingCanceled=true;
}
this.InEventRaise=false;
},RaisePostBackEvent:function(){
eval(this.PostBackEventReferenceScript);
},UpdateCssClass:function(){
if(this.Enabled&&(!this.IsEmptyMessage)&&(!this.IsNegative())){
this.TextBoxElement.style.cssText=this.OriginalTextBoxCssText+this.UpdateCssText(this.Styles["EnabledStyle"][0]);
this.TextBoxElement.className=this.Styles["EnabledStyle"][1];
}
if(this.Enabled&&(!this.IsEmptyMessage)&&this.IsNegative()){
this.TextBoxElement.style.cssText=this.OriginalTextBoxCssText+this.UpdateCssText(this.Styles["NegativeStyle"][0]);
this.TextBoxElement.className=this.Styles["NegativeStyle"][1];
}
if(this.Enabled&&this.IsEmptyMessage){
this.TextBoxElement.style.cssText=this.OriginalTextBoxCssText+this.UpdateCssText(this.Styles["EmptyMessageStyle"][0]);
this.TextBoxElement.className=this.Styles["EmptyMessageStyle"][1];
}
if(this.Hovered){
this.TextBoxElement.style.cssText=this.OriginalTextBoxCssText+this.UpdateCssText(this.Styles["HoveredStyle"][0]);
this.TextBoxElement.className=this.Styles["HoveredStyle"][1];
}
if(this.Focused){
this.TextBoxElement.style.cssText=this.OriginalTextBoxCssText+this.UpdateCssText(this.Styles["FocusedStyle"][0]);
this.TextBoxElement.className=this.Styles["FocusedStyle"][1];
}
if(this.Invalid){
this.TextBoxElement.style.cssText=this.OriginalTextBoxCssText+this.UpdateCssText(this.Styles["InvalidStyle"][0]);
this.TextBoxElement.className=this.Styles["InvalidStyle"][1];
}
if(!this.Enabled){
this.TextBoxElement.style.cssText=this.OriginalTextBoxCssText+this.UpdateCssText(this.Styles["DisabledStyle"][0]);
this.TextBoxElement.className=this.Styles["DisabledStyle"][1];
}
},UpdateCssText:function(_3a){
var _3b=_3a.split(";");
var i;
var _3d="";
for(i=0;i<_3b.length;i++){
var _3e=_3b[i].split(":");
if(_3e.length==2){
var _3f=""+_3e[0].toLowerCase();
if(_3f!="width"&&_3f!="height"){
_3d+=_3b[i]+";";
}
}
}
return _3d;
},CalculateSelection:function(){
if(window.opera||!document.selection){
this.SelectionEnd=this.TextBoxElement.selectionEnd;
this.SelectionStart=this.TextBoxElement.selectionStart;
return;
}
var s1=document.selection.createRange();
if(s1.parentElement()!=this.TextBoxElement){
return;
}
var s=s1.duplicate();
s.move("character",-this.TextBoxElement.value.length);
s.setEndPoint("EndToStart",s1);
var _42=s.text.length;
var _43=s.text.length+s1.text.length;
this.SelectionEnd=Math.max(_42,_43);
this.SelectionStart=Math.min(_42,_43);
},ApplySelection:function(){
if(window.opera||!document.selection){
this.TextBoxElement.selectionStart=this.SelectionStart;
this.TextBoxElement.selectionEnd=this.SelectionEnd;
return;
}
this.TextBoxElement.select();
sel=document.selection.createRange();
sel.collapse();
sel.moveStart("character",this.SelectionStart);
sel.collapse();
sel.moveEnd("character",this.SelectionEnd-this.SelectionStart);
sel.select();
},SelectText:function(_44,end){
this.SelectionStart=_44;
this.SelectionEnd=end;
this.ApplySelection();
},SelectAllText:function(){
if(this.TextBoxElement.value.length>0){
this.SelectText(0,this.TextBoxElement.value.length);
return true;
}
return false;
},SetCaretPosition:function(_46){
this.SelectionStart=_46;
this.SelectionEnd=_46;
this.ApplySelection();
},UpdateHiddenValue:function(){
return this.SetHiddenValue(this.TextBoxElement.value);
},InitializeButtons:function(){
this.Button=null;
var _47=document.getElementById(this.WrapperElementID);
var _48=_47.getElementsByTagName("a");
for(i=0;i<_48.length;i++){
if(_48[i].className.indexOf("gobutton")!=(-1)){
this.Button=_48[i];
this.AttachDomEvent(this.Button,"click","ButtonClickHandler");
}
}
},ButtonClickHandler:function(e){
var _4a={"ButtonName":"Button"};
this.RaiseEvent("OnButtonClick",_4a);
},SetHiddenValue:function(_4b){
if(this.HiddenElement.value!=_4b.toString()){
this.HiddenElement.value=_4b;
}
this.SetValidationField(_4b);
return true;
},SetValidationField:function(_4c){
},GetValidationField:function(){
return this.HiddenElement;
},ClearHiddenValue:function(){
this.HiddenElement.value="";
},ValueChangedEventArgs:function(_4d,_4e){
if(_4e==null){
_4e=this.HiddenElement.value;
}
return {"NewValue":_4d,"OldValue":_4e};
},GetValue:function(){
return this.HiddenElement.value;
},GetDisplayValue:function(){
return this.HiddenElement.value;
},GetEditValue:function(){
return this.HiddenElement.value;
},IsEmpty:function(){
return this.HiddenElement.value=="";
},IsNegative:function(){
return false;
},IsReadOnly:function(){
return this.TextBoxElement.readOnly||!this.Enabled;
}};
if(typeof (window.RadControlsNamespace)=="undefined"){
window.RadControlsNamespace=new Object();
}
RadControlsNamespace.AppendStyleSheet=function(_4f,_50,_51){
if(!_51){
return;
}
var _52=window.netscape&&!window.opera;
if(!_4f&&_52){
document.write("<"+"link"+" rel='stylesheet' type='text/css' href='"+_51+"' />");
}else{
var _53=document.createElement("link");
_53.rel="stylesheet";
_53.type="text/css";
_53.href=_51;
document.getElementsByTagName("head")[0].appendChild(_53);
}
};
if(typeof (WebForm_AutoFocus)=="function"){
WebForm_AutoFocus=function(_54){
var _55=document.getElementById(_54);
var _56=false;
if((_55.style)&&(typeof (_55.style.visibility)!="undefined")&&(_55.style.visibility=="hidden")&&(typeof (_55.style.width)!="undefined")&&((_55.style.width=="0px")||(_55.style.width=="0pt"))&&(_55.tagName.toLowerCase()=="input")){
_56=true;
}
if(_56){
_54=_54+"_text";
}
var _57;
if(__nonMSDOMBrowser){
_57=document.getElementById(_54);
}else{
_57=document.all[_54];
}
var _58=_57;
if(_57&&(!WebForm_CanFocus(_57))){
_58=WebForm_FindFirstFocusableChild(_57);
}
if(_58){
try{
_58.focus();
if(__nonMSDOMBrowser){
_58.scrollIntoView(false);
}
if(window.__smartNav){
window.__smartNav.ae=_58.id;
}
}
catch(e){
}
}
};
};if(typeof (Telerik)=="undefined"){
Telerik={};
}
if(Telerik.TextInputEvents==null){
Telerik.TextInputEvents={};
}
Telerik.TextInputEvents.ValueListener=function(_1){
this.Owner=_1;
this.EventRequest=null;
};
Telerik.TextInputEvents.ValueListener.prototype={AddChangeEventRequest:function(_2,_3){
if(this.EventRequest==null){
this.EventRequest={New:_2,Old:_3};
}else{
this.EventRequest.New=_2;
}
},QueueChangeEventRequest:function(_4,_5){
this.CancelPreviousRequest();
this.AddChangeEventRequest(_4,_5);
var _6=this;
var _7=function(){
_6.ValueChangedAction=null;
_6.ProcessEvents();
};
if(this.Owner.DelayValueChangedEvent()){
this.ValueChangedAction=window.setTimeout(_7,300);
}else{
_7();
}
},CancelPreviousRequest:function(){
if(this.ValueChangedAction!=null){
window.clearTimeout(this.ValueChangedAction);
this.ValueChangedAction=null;
}
},Dispose:function(){
this.CancelPreviousRequest();
},ProcessEvents:function(){
if(this.EventRequest!=null){
this.Owner.RaiseValueChangedEvent(this.EventRequest.New,this.EventRequest.Old);
this.EventRequest=null;
}
}};;//BEGIN_ATLAS_NOTIFY
if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
//END_ATLAS_NOTIFY
