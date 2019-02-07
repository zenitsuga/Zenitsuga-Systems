<?php 
try
{
	//$dbcon = mysqli_connect("sql12.freemysqlhosting.net","sql12277814","w1KgYiwCmH","sql12277814");
	$conn = new PDO('mysql:host=sql12.freemysqlhosting.net;dbname=sql12277814;','sql12277814','w1KgYiwCmH');

}catch(PDOException $ex)
{	
	echo 'ERROR'.$e->getMessage();
}


 ?>