<?php
// Kết nối database
$con = new mysqli('localhost', 'root', '', 'unityaccess');

if ($con->connect_error) {
    die("Connection failed: " . $con->connect_error);
}




// Truy vấn lấy tất cả username từ bảng players
$sql = "SELECT username FROM players";
$result = $con->query($sql);

$users = array();

if ($result->num_rows > 0) {
    while ($row = $result->fetch_assoc()) {
        $users[] = $row['username'];
    }

    // Trả về dạng JSON
    echo json_encode($users);
} else {
    echo "No users found";
}





$con->close();
?>
