
class User {
  String? id;
  String? userName;
  String? email;
  String? name;
  String? address;
  String? city;
  String? state;
  String? zipCode;
  String? description;
  int? messageUserId;
  String? dateOfBirth;
  String? dateCreated;
  String? password;

  User({
    this.id,
    this.userName,
    this.email,
    this.name,
    this.address,
    this.city,
    this.state,
    this.zipCode,
    this.description,
    this.messageUserId,
    this.dateOfBirth,
    this.dateCreated,
    this.password,
  });

  factory User.fromJson(Map<String, dynamic> json) => User(
    id: json['id'],
    userName: json['userName'],
    email: json['email'],
    name: json['name'],
    address: json['address'],
    city: json['city'],
    state: json['state'],
    zipCode: json['zipCode'],
    description: json['description'],
    messageUserId: json['messageUserId'],
    dateOfBirth: json['dateOfBirth'],
    dateCreated: json['dateCreated'],
  );

  Map<String, dynamic> toJson() => {
    'id': id,
    'userName': userName,
    'email': email,
    'name': name,
    'address': address,
    'city': city,
    'state': state,
    'zipCode': zipCode,
    'description': description,
    'messageUserId': messageUserId,
    'dateOfBirth': dateOfBirth,
    'dateCreated': dateCreated,
  };
}