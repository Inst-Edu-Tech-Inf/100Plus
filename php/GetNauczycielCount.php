<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$nauczycielPass = $_POST["nauczycielPass"];
	//
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";
	
	//$sql = "SELECT name, score FROM scores";
	$sql = "SELECT COUNT(*) as total FROM `nauczyciel` WHERE `identyfikator` LIKE \"$nauczycielPass\" ";
	
	$result = $conn->query($sql);
	//$data = mysql_query("SELECT COUNT(*) as total FROM `uczen`");
	//$info = mysql_fetch_assoc($result);
	//echo "Rows found :" . $info["total"];
	$row = $result->fetch_assoc();
	if ($row["total"] == 1)
		echo "true";
	else
		echo "false";
	//echo $row["total"];
	//echo "OK";
	//$data = array();
	//$data = $row;
	//echo $data["total"];

	
	//$data = array();
	//while(($row = mysql_fetch_array($result))) {
	//	$data[] = $row['id'];
	//	echo $row['id'];
	//}
	
	//if ($result->num_rows > 0){
		//output data of each row
	//	while($row = $result->fetch_assoc()) {
	//		echo  $row['identyfikator'] ;
			//" - Name: "$row["klasa"]. " " .$row["identyfikator"]. "<br>";
	//	}
	//} 
	$conn->close();
	
?>