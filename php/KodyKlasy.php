<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$nauczycielPass = $_POST["nauczycielPass"];
	$aktywnaKlasa = $_POST["aktywnaKlasa"];
	$nrSzkoly = "";
	$licznik = 0;
	//
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";
	
	$sql = "SELECT szkola FROM `nauczyciel` WHERE `identyfikator` LIKE \"$nauczycielPass\" ";	
	
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0){
		while($row = $result->fetch_assoc()) {
			//echo $row["szkola"];
			//echo "<br>";
			$nrSzkoly = $row["szkola"];
		}
		
	
		$sql = "SELECT u1kod, u2kod, u3kod, u4kod, u5kod, u6kod, u7kod, u8kod, u9kod, u10kod, u11kod, u12kod, u13kod, u14kod, u15kod, u16kod, u17kod, u18kod, u19kod, u20kod, u21kod, u22kod, u23kod, u24kod, u25kod, u26kod, u27kod, u28kod, u29kod, u30kod, u31kod, u32kod, u33kod, u34kod, u35kod, u36kod, u37kod, u38kod, u39kod, u40kod  FROM `klasa` WHERE `szkola` LIKE \"$nrSzkoly\";";
		
		//$sql = "SELECT COUNT(*) as total FROM `szkola`";
		
		$result = $conn->query($sql);

		if ($result->num_rows > 0){
			while($row = $result->fetch_assoc()) {
				//echo $row["szkola"];
				//
				if ($aktywnaKlasa == $licznik) {
					echo $row["u1kod"];
					echo " ";//"<br>";
					echo $row["u2kod"];
					echo " ";//"<br>";
					echo $row["u3kod"];
					echo " ";//"<br>";
					echo $row["u4kod"];
					echo " ";//"<br>";
					echo $row["u5kod"];
					echo " ";//"<br>";
					echo $row["u6kod"];
					echo " ";//"<br>";
					echo $row["u7kod"];
					echo " ";//"<br>";
					echo $row["u8kod"];
					echo " ";//"<br>";
					echo $row["u9kod"];
					echo " ";//"<br>";
					echo $row["u10kod"];
					echo " ";//"<br>";
					echo $row["u11kod"];
					echo " ";//"<br>";
					echo $row["u12kod"];
					echo " ";//"<br>";
					echo $row["u13kod"];
					echo " ";//"<br>";
					echo $row["u14kod"];
					echo " ";//"<br>";
					echo $row["u15kod"];
					echo " ";//"<br>";
					echo $row["u16kod"];
					echo " ";//"<br>";
					echo $row["u17kod"];
					echo " ";//"<br>";
					echo $row["u18kod"];
					echo " ";//"<br>";
					echo $row["u19kod"];
					echo " ";//"<br>";
					echo $row["u20kod"];
					echo " ";//"<br>";
					echo $row["u21kod"];
					echo " ";//"<br>";
					echo $row["u22kod"];
					echo " ";//"<br>";
					echo $row["u23kod"];
					echo " ";//"<br>";
					echo $row["u24kod"];
					echo " ";//"<br>";
					echo $row["u25kod"];
					echo " ";//"<br>";
					echo $row["u26kod"];
					echo " ";//"<br>";
					echo $row["u27kod"];
					echo " ";//"<br>";
					echo $row["u28kod"];
					echo " ";//"<br>";
					echo $row["u29kod"];
					echo " ";//"<br>";
					echo $row["u30kod"];
					echo " ";//"<br>";
					echo $row["u31kod"];
					echo " ";//"<br>";
					echo $row["u32kod"];
					echo " ";//"<br>";
					echo $row["u33kod"];
					echo " ";//"<br>";
					echo $row["u34kod"];
					echo " ";//"<br>";
					echo $row["u35kod"];
					echo " ";//"<br>";
					echo $row["u36kod"];
					echo " ";//"<br>";
					echo $row["u37kod"];
					echo " ";//"<br>";
					echo $row["u38kod"];
					echo " ";//"<br>";
					echo $row["u39kod"];
					echo " ";//"<br>";
					echo $row["u40kod"];
					echo " ";//"<br>";
				}
				$licznik = $licznik + 1;
			}
		}
		else{
			echo "false";
		}
	}
	
	$conn->close();
	
?>