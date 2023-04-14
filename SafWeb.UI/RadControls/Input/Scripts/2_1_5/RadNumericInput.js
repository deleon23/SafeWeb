var NumberFormat={};
NumberFormat.Round=function(_1,_2){
if(!_2.NumberFormat.AllowRounding){
return _1;
}
var _3=Math.pow(10,_2.NumberFormat.DecimalDigits);
return Math.round(_1*_3)/_3;
};
NumberFormat.SplitGroups=function(_4,_5,_6){
var sb=_4.toString().split("");
for(var i=sb.length-_5;i>0;i-=_5){
sb.splice(i,0,_6);
}
return sb.join("");
};
NumberFormat.Parse=function(_9,_a,_b,_c){
return parseFloat(_9.toString().replace(_a,"").replace(_b,".").replace(_c,"-"));
};
NumberFormat.Pad=function(_d,_e,_f){
while(_d.toString().length<_e){
_d=_d.toString()+_f.toString();
}
return _d;
};
NumberFormat.Format=function(_10,_11){
var num=parseFloat(_10.toString().replace(_11.NumberFormat.DecimalSeparator,"."));
if(isNaN(num)){
return "";
}
var _13="";
num=this.Round(num,_11);
var _14=Math.abs(num).toString().split(".");
_13+=this.SplitGroups(_14[0],_11.NumberFormat.GroupSizes,_11.NumberFormat.GroupSeparator);
if((!_11.NumberFormat.AllowRounding)&&(_14[1])){
_13+=_11.NumberFormat.DecimalSeparator+_14[1];
}else{
if(!_11.NumberFormat.AllowRounding){
_13=_13;
}else{
if(_14[1]){
_13+=_11.NumberFormat.DecimalSeparator+this.Pad(_14[1],_11.NumberFormat.DecimalDigits,"0");
}else{
if(_11.NumberFormat.DecimalDigits>0){
_13+=_11.NumberFormat.DecimalSeparator+this.Pad("",_11.NumberFormat.DecimalDigits,"0");
}
}
}
}
var _15=num<0?_11.NumberFormat.NegativePattern:_11.NumberFormat.PositivePattern;
return _15.replace("n",_13);
};
NumberFormat.ReturnZeroIfEmpty=function(_16){
if((_16==null)||(_16=="")||isNaN(_16)){
return 0;
}
return _16;
};;function RadNumericTextBox(id,_2,_3){
RadTextBox.Extend(this);
this.CallBase("DisposeOldInstance",arguments);
this.Constructor(id);
this.Initialize(_2,_3);
}
RadNumericTextBox.prototype={Constructor:function(id){
this.CallBase("Constructor",arguments);
},Initialize:function(_5,_6){
this.CallBase("Initialize",arguments);
this.CompileRegEx();
this.Hold=false;
this.AttachDomEvent(this.TextBoxElement.form,"reset","OnReset");
if(this.InitialValue!=""){
this.HiddenElement.value=NumberFormat.Round(this.InitialValue,this);
}
},CompileRegEx:function(){
var _7=this.NumberFormat.DecimalSeparator=="."?"\\.":this.NumberFormat.DecimalSeparator;
this.AcceptRegExp=new RegExp("[0-9"+_7+this.NumberFormat.NegativeSign+"]{1}");
this.RejectRegExp=new RegExp("[^0-9"+_7+this.NumberFormat.NegativeSign+"]{1}","g");
this.DecimalReplaceRegExp=new RegExp(_7,"g");
},GetParsedTextBoxValue:function(){
var _8=NumberFormat.Parse(this.GetTextBoxValue(),this.RejectRegExp,this.NumberFormat.DecimalSeparator,this.NumberFormat.NegativeSign);
_8=Math.max(this.MinValue,_8);
_8=Math.min(this.MaxValue,_8);
return _8;
},TextBoxKeyDownHandler:function(e){
if(!this.IncrementSettings.InterceptArrowKeys){
return;
}
if(e.altKey||e.ctrlKey){
return true;
}
var _a=/MSIE/.test(navigator.userAgent);
var _b=_a?e.keyCode:e.which;
if(_b==38){
this.Move(this.IncrementSettings.Step,false);
}else{
if(_b==40){
this.Move(-this.IncrementSettings.Step,false);
}
}
},HandleWheel:function(_c){
if(!this.IncrementSettings.InterceptMouseWheel){
return;
}
this.CalculateSelection();
if(_c){
this.Move(-this.IncrementSettings.Step,false);
}else{
this.Move(this.IncrementSettings.Step,false);
}
this.ApplySelection();
},TextBoxKeyPressHandler:function(e){
if(e.altKey||e.ctrlKey){
return true;
}
var _e=/MSIE/.test(navigator.userAgent);
var _f=_e?e.keyCode:e.which;
if(!this.RaiseEvent("OnKeyPress",{"DomEvent":e,"KeyCode":_f,"KeyCharacter":String.fromCharCode(_f)})){
return RadControlsNamespace.DomEvent.PreventDefault(e);
}
if(_f==13){
this.UpdateHiddenValueOnKeyPress(e);
if(this.AutoPostBack){
this.RaisePostBackEvent();
}
return true;
}
if(!_f||_f==8||_f==9){
return true;
}
var _10=this.GetTextBoxValue();
var _11=String.fromCharCode(_f);
if(!_11.match(this.AcceptRegExp)){
var _12={Reason:RadInputErrorReason.ParseError,InputText:_10,KeyCode:_f,KeyCharacter:_11};
this.RaiseErrorEvent(_12);
RadControlsNamespace.DomEvent.PreventDefault(e);
return false;
}
if(_11==this.NumberFormat.NegativeSign){
this.CalculateSelection();
var _13=(this.SelectionStart!=0);
_13=_13||(_10.indexOf(this.NumberFormat.NegativeSign)==0&&(this.SelectionStart==0&&this.SelectionEnd==0));
if(_13){
var _12={Reason:RadInputErrorReason.ParseError,InputText:_10,KeyCode:_f,KeyCharacter:_11};
this.RaiseErrorEvent(_12);
return RadControlsNamespace.DomEvent.PreventDefault(e);
}
}
if(_11==this.NumberFormat.DecimalSeparator){
this.CalculateSelection();
var _14=_10.substr(0,this.SelectionStart);
var _15=_10.substr(this.SelectionStart,this.SelectionEnd-this.SelectionStart);
var _16=_10.substr(this.SelectionEnd,_10.length-1);
if(_14.match(this.DecimalReplaceRegExp)){
this.SelectionStart--;
this.SelectionEnd--;
}else{
if(_15.match(this.DecimalReplaceRegExp)){
this.SelectionEnd--;
}
}
if(_15.length==_10.length){
this.SetTextBoxValue("");
}else{
this.SetTextBoxValue((_14+this.NumberFormat.DecimalSeparator+_16).replace(this.DecimalReplaceRegExp,""));
}
}
},MoveUp:function(){
return this.Move(this.IncrementSettings.Step,true);
},MoveDown:function(){
return this.Move(-this.IncrementSettings.Step,true);
},Move:function(_17,_18){
if(this.IsReadOnly()){
return false;
}
var _19=this.GetValue();
_19=_19+_17;
if(!_18){
this._SetValue(_19);
}else{
this.SetValue(_19);
}
return true;
},InitializeButtons:function(){
this.SpinUpButton=null;
this.SpinDownButton=null;
this.Button=null;
var _1a=document.getElementById(this.WrapperElementID);
var _1b=_1a.getElementsByTagName("a");
var i;
for(i=0;i<_1b.length;i++){
if(_1b[i].className.indexOf("spinbutton down")!=(-1)){
this.SpinDownButton=_1b[i];
}else{
if(_1b[i].className.indexOf("spinbutton up")!=(-1)){
this.SpinUpButton=_1b[i];
}
}
}
if(this.ShowSpinButtons){
this.AttachDomEvent(this.SpinUpButton,"mousedown","ButtonUpMouseDownHandler");
this.AttachDomEvent(this.SpinDownButton,"mousedown","ButtonDownMouseDownHandler");
this.AttachDomEvent(this.SpinUpButton,"mouseup","ButtonUpMouseUpHandler");
this.AttachDomEvent(this.SpinDownButton,"mouseup","ButtonDownMouseUpHandler");
this.AttachDomEvent(this.SpinUpButton,"mouseout","SpinMouseOutHandler");
this.AttachDomEvent(this.SpinDownButton,"mouseout","SpinMouseOutHandler");
}
this.CallBase("InitializeButtons",arguments);
},SpinMouseOutHandler:function(e){
this.Hold=false;
},ButtonUpMouseDownHandler:function(e){
var _1f={"ButtonName":"MoveUpButton"};
if(!this.RaiseEvent("OnButtonClick",_1f)){
return;
}
this.Hold=true;
this._RepeatedMoveUp(300);
},ButtonDownMouseDownHandler:function(e){
var _21={"ButtonName":"MoveDownButton"};
if(!this.RaiseEvent("OnButtonClick",_21)){
return;
}
this.Hold=true;
this._RepeatedMoveDown(300);
},_RepeatedMoveUp:function(_22){
if(this.Hold==false){
return;
}
var _23=this;
var _24=_22;
if(_24>50){
_24-=40;
}
var _25=function(){
if(_23._RepeatedMoveUp){
_23._RepeatedMoveUp(_24);
}
};
_23.MoveUp();
window.setTimeout(_25,_22);
},_RepeatedMoveDown:function(_26){
if(this.Hold==false){
return;
}
var _27=this;
var _28=_26;
if(_28>50){
_28-=40;
}
var _29=function(){
if(_27._RepeatedMoveDown){
_27._RepeatedMoveDown(_28);
}
};
_27.MoveDown();
window.setTimeout(_29,_26);
},ButtonUpMouseUpHandler:function(e){
this.Hold=false;
this.SpinUpButton.blur();
},ButtonDownMouseUpHandler:function(e){
this.Hold=false;
this.SpinDownButton.blur();
},OnReset:function(){
this.SetValue(this.OriginalValue);
this.TextBoxElement.defaultValue=this.OriginalValue;
},SetValue:function(_2c){
if(typeof (_2c)=="string"&&_2c!=""){
_2c=_2c.replace(this.RejectRegExp,this.NumberFormat.DecimalSeparator);
_2c=NumberFormat.Parse(_2c,this.RejectRegExp,this.NumberFormat.DecimalSeparator,this.NumberFormat.NegativeSign);
}
if((_2c<this.MinValue||_2c>this.MaxValue)&&_2c!=""){
var _2d={Reason:RadInputErrorReason.OutOfRange,InputText:_2c};
this.RaiseErrorEvent(_2d);
}
this.CallBase("SetValue",arguments);
},_SetValue:function(_2e){
if(_2e<this.MinValue||_2e>this.MaxValue){
var _2f={Reason:RadInputErrorReason.OutOfRange,InputText:_2e};
this.RaiseErrorEvent(_2f);
}
this.CallBase("_SetValue",arguments);
},SetHiddenValue:function(_30){
if(typeof (_30)!="number"){
_30=NumberFormat.Parse(_30,this.RejectRegExp,this.NumberFormat.DecimalSeparator,this.NumberFormat.NegativeSign);
}
_30=Math.max(this.MinValue,_30);
_30=Math.min(this.MaxValue,_30);
_30=NumberFormat.Round(_30,this);
if(isNaN(_30)){
_30="";
}
this.CallBase("SetHiddenValue",arguments);
},GetDisplayValue:function(){
return NumberFormat.Format(this.GetParsedTextBoxValue(),this);
},GetEditValue:function(){
var _31=this.GetValue().toString().replace(".",this.NumberFormat.DecimalSeparator);
return _31.replace("-",this.NumberFormat.NegativeSign);
},GetValue:function(){
var _32=parseFloat(this.HiddenElement.value);
if(isNaN(_32)){
_32="";
}
return _32;
},InitializeHiddenElement:function(id){
this.HiddenElement=document.getElementById(id+"_Value");
},InitializeValidationField:function(id){
this.validationField=document.getElementById(id);
},SetValidationField:function(_35){
this.validationField.value=_35.toString().replace(".",this.NumberFormat.DecimalSeparator);
},GetValidationField:function(_36){
return this.validationField;
},IsNegative:function(){
return this.GetValue()<0;
}};;//BEGIN_ATLAS_NOTIFY
if (typeof(Sys) != "undefined"){if (Sys.Application != null && Sys.Application.notifyScriptLoaded != null){Sys.Application.notifyScriptLoaded();}}
//END_ATLAS_NOTIFY
