<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp.aspx.cs" Inherits="PopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>jQuery Popup Dialog - Click</title>
<style type="text/css">
#overlay {
position: fixed;
top: 0;
left: 0;
width: 100%;
height: 100%;
background-color: #000;
filter:alpha(opacity=70);
-moz-opacity:0.7;
-khtml-opacity: 0.7;
opacity: 0.7;
z-index: 100;
display: none;
}
.content a{
text-decoration: none;
}
.popup{
width: 100%;
margin: 0 auto;
display: none;
position: fixed;
z-index: 101;
}
.content{
min-width: 600px;
width: 600px;
min-height: 150px;
margin: 100px auto;
background: #f3f3f3;
position: relative;
z-index: 103;
padding: 10px;
border-radius: 5px;
box-shadow: 0 2px 5px #000;
}
.content p{
clear: both;
color: #555555;
text-align: justify;
}
.content p a{
color: #d91900;
font-weight: bold;
}
.content .x{
float: right;
height: 35px;
left: 22px;
position: relative;
top: -25px;
width: 34px;
}
.content .x:hover{
cursor: pointer;
}
</style>
<script type="text/javascript" src="Scripts/jquery-1.9.1.js"></script>
<script type='text/javascript'>
    $(function () {
        var overlay = $('<div id="overlay"></div>');
        $('.close').click(function () {
            $('.popup').hide();
            overlay.appendTo(document.body).remove();
            return false;
        });

        $('.x').click(function () {
            $('.popup').hide();
            overlay.appendTo(document.body).remove();
            return false;
        });

        $('.click').click(function () {
            overlay.show();
            overlay.appendTo(document.body);
            $('.popup').show();
            return false;
        });
    });
</script>
</head>
<body>
<div class='popup'>
<div class='content'>
<img src='http://www.developertips.net/demos/popup-dialog/img/x.png' alt='quit' class='x' id='x' />
<p>
Welcome to Aspdotnet-Suresh.com. Please use above search option which was there in top right side to get help with many of articles.
<br/>
<br/>
<a href='' class='close'>Close</a>
</p>
</div>
</div>         
<div id='container'>
<a href='' class='click'><h2><b>Click Here to See Popup! </b></h2></a> <br/>
</div>
</body>
</html>