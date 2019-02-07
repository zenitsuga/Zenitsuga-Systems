
<?php
$connect=mysql_connect("sql12.freemysqlhosting.net","sql12277814");
if(!$connect)
{
	echo "Error".mysql_error();
	}
	
	
	$db=mysql_select_db("attendance_db");
	if(!$db)
	{
		echo "Error".mysql_error();
		}
		



?>