<?php
// connection db
$con = new mysqli('localhost', 'root', '', 'unityaccess');

// test connection
if ($con->connect_error) {
    die("Connection failed: " . $con->connect_error);
}

// Nhận dữ liệu từ POST
$username = $_POST['name'];
$password = $_POST['password'];
$salt = uniqid(mt_rand(), true);
$hash = crypt($password, $salt);





// Chuẩn bị câu lệnh SQL
$stmt = $con->prepare("INSERT INTO players (username, hash, salt) VALUES (?, ?, ?)");


// test câu lệnh
if ($stmt === false) {
    die("Error preparing statement: " . $con->error);
}

// Gắn tham số vào câu lệnh SQL
$stmt->bind_param("sss", $username, $hash, $salt);

// Thực thi câu lệnh
if ($stmt->execute()) {
    echo "User registered successfully!";
} else {
    echo "Error: " . $stmt->error;
}

// Đóng kết nối
$stmt->close();
$con->close();
?>
