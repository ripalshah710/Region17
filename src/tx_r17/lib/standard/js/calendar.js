var objCalendar;

function clickhandler (d, m, y)
{
	var objFld = document.getElementById(objCalendar.bindToElement);
	objFld.value = (m + 1) + '/' + d + '/' + y;
	objCalendar.hide();
}

function toggleCalendar(i)
{
	objCalendar.toggleCalendar(i);
}

function toggleCurrent () 
{
	objCalendar.goToCurrent();
}

function calClose () 
{
	objCalendar.hide();
}

function produceCalendar (bindToFld)
{
	if (typeof(objCalendar ) != 'object') objCalendar = new Calendar();
	
	if (objCalendar.isIE4)
	{
		//objCalendar.posX = window.event.clientX - 195;
		objCalendar.posX = window.event.clientX + document.documentElement.scrollLeft + document.body.scrollLeft;
		objCalendar.posY = window.event.clientY + document.documentElement.scrollTop + document.body.scrollTop;
	}
	else
	{
		objCalendar.posX = 250;
		objCalendar.posY = 150;
	}

	for(var j = 0; j < objCalendar.ddlSelects.length; j++)
		objCalendar.ddlSelects[j].style.visibility = 'hidden';
		
	if(objCalendar.visible && bindToFld == objCalendar.bindToElement)
	{
		objCalendar.hide();
		return;
	}
	else
		objCalendar.bindToElement = bindToFld;

	objCalendar.BuildCalendar();
	return;
}

function Calendar(m, y)
{
    if (typeof(_calendar_prototype_called) == 'undefined')
    {
        _calendar_prototype_called = true ;
        
        // Object methods
        this.BuildCalendar	= _create;
        this.createCanvass	= _createCanvass;
        this.showCalendar	= _showCalendar;
        this.goToCurrent	= _goToCurrent;
        this.toggleCalendar = _toggleCalendar;
        this.moveTo			= _positionCanvass;
        this.hide			= _hide;
        this.show			= _show;
        this.init			= _init;
        this.renderNavBar	= _renderNavBar;
        this.GetDay			= _getDayString;
        this.ddlSelects		= document.getElementsByTagName('SELECT');
        
        g_calNavPanel = "<tr class=calNav>";
        g_calNavPanel += "<td colspan=2 class=calNav><a href=\"javascript:void(0);\" onClick='toggleCalendar(-1) ; return false ;' class=calNav title='Previous Month' /><<</a></td>";
        g_calNavPanel += "<td colspan=3 class=calNav align=center><a href=\"javascript:void(0);\" onClick='toggleCurrent() ; return false ;' class=calNav title='Current Month' />[ today ]</a></td>";
        g_calNavPanel += "<td colspan=2 class=calNav align=right><a href=\"javascript:void(0);\" onClick='toggleCalendar(1) ; return false ;' class=calNav title='Next Month' />>></a></td>";
        g_calNavPanel += "</tr>";
        
        // Object properties
        this.name			= 'default';
        this.currentDay		= 0;
        this.currentMonth	= 0;
        this.currentYear	= 0;
        this.visible		= false;
        this.posX			= 10;
        this.posY			= 10;
        this.isIE4			= new Boolean;
        this.isNav4			= new Boolean;
        this.NavAtTop		= true;
        
        this.hasEvents		= true ;		// If you set hasEvents fo FALSE then the calendar face is dumbed out.
        this.canvass		= new Object;	// The DIV || LAYER that we display the calendar on.
        this.bindToElement	= new Object;	// Bind to an ELEMENT on the page.
        
        // Date Array Constants
        this.days		= new Array("Sunday", "Monday", "Tuesday", "Wednesday","Thursday", "Friday", "Saturday") ;
        this.months		= new Array("January", "February", "March", "April", "May","June", "July", "August", "September", "October", "November", "December") ;
        this.totalDays	= new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
        
        // Call the Initialize() event.
        this.init(m, y);
    }
}

function _init( m, y )
{
    if (parseInt(navigator.appVersion.charAt(0) ) >= 4)
    {
		this.isNav4	= (navigator.appName == "Netscape") ? true : false;
		this.isIE4	= (navigator.appName.indexOf("Microsoft") != -1) ? true : false;
    }
    
    // Populate the current Day|Month|Year properties
    var objDate			= new Date();
    this.currentDay		= objDate.getDate();
    this.currentMonth	= objDate.getMonth();
    var curYear			= objDate.getYear();
    this.currentYear	= (curYear < 1000) ? curYear + 1900 : curYear ; 

	// Check constructor arguments    
    this.month = m || this.currentMonth;
    this.year = y || this.currentYear;

    this.createCanvass( ) ;
}

function _createCanvass( )
{
    if (this.isNav4) 
        this.canvass = new Layer(200);
    else if (this.isIE4)
    {
        var objDiv = document.createElement("<DIV STYLE='position: absolute'>");
        document.body.appendChild (objDiv);
        this.canvass = objDiv;
    }
    
    this.moveTo(this.posX, this.posY);
}

function _positionCanvass(x, y)
{
    if(x==null || y==null)
    {
        x	= this.posX;
        y	= this.posY;
    }
    
    if(isNaN(x) || isNaN(y))
    {
        alert('You can only enter numbers for the x/y co-ordinates');
        return;
    }
    
    if(this.isNav4)
    {
        this.canvass.left	= this.posX = x;
        this.canvass.top	= this.posY = y;
    }
    else if(this.isIE4)
    {
		this.canvass.style.left	= this.posX = x ;
		this.canvass.style.top	= this.posY = y ;
    }
}

function _show()
{
    if(this.isNav4)
        this.canvass.visibility = 'show' ;
    else if (this.isIE4)
        this.canvass.style.visibility = 'visible' ;
    
    this.moveTo(this.posX, this.posY);
    this.visible = true;
}

function _hide()
{
    if(this.isNav4)
        this.canvass.visibility = 'hide';
    else if(this.isIE4)
        this.canvass.style.visibility = 'hidden';

	for(var j = 0; j < this.ddlSelects.length; j++)
		this.ddlSelects[j].style.visibility = 'visible';
		
    this.visible = false;
}

function _showCalendar(s)
{
    if(this.isNav4)
    {
		this.canvass.document.open() ;
		this.canvass.document.writeln(s);
		this.canvass.document.close() ;
    }
    else if(this.isIE4)
        this.canvass.innerHTML = s;
    
    this.show();
}

function _goToCurrent()
{
	this.year	= this.currentYear;
	this.month	= this.currentMonth;
	this.BuildCalendar();
}

function _renderNavBar(pos)
{
    if(pos == 'top' && this.NavAtTop && this.hasEvents)
        return g_calNavPanel;
    else if(pos == 'bottom' && !(this.NavAtTop) && this.hasEvents)
        return g_calNavPanel;
    else
		return '';
}

function _toggleCalendar(n)
{
    if((this.month + n) < 0)
    {
        this.month = 11;
        -- this.year;
	}
    else if((this.month + n) >= 12) 
    { 
        this.month = 0;
        ++ this.year;
    }
    else
        this.month = this.month + n;
    
    this.BuildCalendar();
}

function _create()
{
    var rowCount = 0;
    var sOut = new String;
    
    if(this.year % 4 == 0 && (this.year % 100 != 0 || this.year % 400 == 0))
    	this.totalDays[1] = 29;
    
    var objDate = new Date(this.year, this.month, 1);
    var firstDayOfMonth = objDate.getDay();
    objDate.setDate(31);
    var lastDayOfMonth = objDate.getDay();
    objDate = null;
    
    sOut = "<table border=0 cellpadding=1 cellspacing=0 class=calendar>";
    sOut += "<tr class=calHeader>"
			+ "<td colspan=6 class=calHeader>" + this.months[this.month] + " " + this.year + "</td>"
			+ "<td align='right' class=calHeader><a href=\"javascript:void(0);\" onclick='calClose();return false;' title='Close'><img src='~/lib/standard/img/x.gif' border='0'></a></td>";
			+ "</tr>" ;
			
    sOut += this.renderNavBar('top');
   
    sOut += "<tr class='calDaysHeader'>" ;
    for (x=0; x<7; x++)
        sOut += this.GetDay(this.days[x].substring(0,3), false);
    sOut += "</tr>" ;

    sOut += "<tr>" ;
    for (x=1; x<=firstDayOfMonth; x++)
    {
        rowCount++ ;
        sOut += this.GetDay ("&nbsp;", false) ;
    }
    
    var dayCount = 1 ;
    while (dayCount <= this.totalDays[this.month])
    {
    	if (rowCount % 7 == 0)
    	    sOut += "</tr><tr>" ;
    	
    	sOut += this.GetDay (dayCount, true);
    	++rowCount;
    	++dayCount;
    }

    while (rowCount % 7 != 0)
	{
		++rowCount ;
		sOut += this.GetDay ("&nbsp;", false);
    }
    
    sOut += "</tr>";
    sOut += this.renderNavBar('bottom');
    sOut += "</table>";
    
    this.showCalendar(sOut);
}

function _getDayString (dayNum, isDay)
{
    var blnIsCurrentMonth	= (this.year == this.currentYear && this.month == this.currentMonth);
    var blnIsCurrentDay		= (blnIsCurrentMonth && (dayNum == this.currentDay));
    var lpszCssClass		= "calDay_UnSel";
    var CellEvents			= "";
    
    if (blnIsCurrentDay) 
		lpszCssClass = "calDay_Sel";
    	
    if(isDay)
    {
		CellEvents = " onMouseOver=\"this.className = '" + lpszCssClass + "Hover';\" onMouseOut=\"this.className='" + lpszCssClass + "';\"  onclick='clickhandler(" + dayNum + "," + this.month + "," + this.year + ");'";
		
		tmpCellInner = "<a href=\"javascript:void(0);\" onclick='clickhandler(" + dayNum + "," + this.month + "," + this.year + ");'>" + dayNum + "</a>";
    }
    else if(dayNum != '&nbsp;')
    {
		tmpCellInner = dayNum;
		lpszCssClass = "calDay_Header";
	}
	else
		tmpCellInner = dayNum;
	
    return "<td valign=top class=" + lpszCssClass + CellEvents + "><span>" + tmpCellInner + "</span></td>" ;
}