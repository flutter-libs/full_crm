import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:frontend/constants/form_fields.dart';
import 'package:frontend/models/user.dart';
import 'package:frontend/screens/dashboard_screen.dart';
import 'package:frontend/screens/login_screen.dart';
import 'package:frontend/services/user_api_service.dart';
import 'package:frontend/widgets/custom_app_bar.dart';
import 'package:frontend/widgets/side_nav_drawer.dart';
import 'package:frontend/widgets/toast_alerts.dart' as alert;
import 'package:frontend/widgets/us_state_selector.dart';
import 'package:intl/intl.dart';

class RegisterScreen extends StatefulWidget {
  const RegisterScreen({super.key});

  static const String id = "register_screen";

  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  final _formKey = GlobalKey<FormState>();
  final _userService = UserApiService();
  var _obscurePassword = true;
  var _obscureConfirmPassword = true;
  DateTime? _selectedDateOfBirth;

  final TextEditingController _email = TextEditingController();
  final TextEditingController _password = TextEditingController();
  final TextEditingController _confirmPassword = TextEditingController();
  final TextEditingController _userName = TextEditingController();
  final TextEditingController _name = TextEditingController();
  final TextEditingController _address = TextEditingController();
  final TextEditingController _city = TextEditingController();
  final TextEditingController _state = TextEditingController();
  final TextEditingController _zipCode = TextEditingController();

  void _pickDate() async {
    DateTime? picked = await showDatePicker(
      context: context,
      barrierColor: Color.fromRGBO(22, 22, 22, 0.5),
      helpText: 'Select Your Date of Birth',
      cancelText: 'Cancel',
      confirmText: 'Submit',
      fieldHintText: 'MM/DD/YYYY',
      fieldLabelText: 'Enter date manually',
      errorFormatText: 'Invalid format',
      errorInvalidText: 'Out of range',
      initialDate: DateTime.now(),
      firstDate: DateTime(1900, 1, 1),
      lastDate: DateTime(2200, 12, 31),
      builder: (BuildContext context, Widget? child) {
        return Theme(
          data: ThemeData.dark().copyWith(
            colorScheme: ColorScheme.dark(
              primary: Colors.black38,
              onPrimary: Colors.indigoAccent,
              onSurface: Colors.indigoAccent,
            ),
            textButtonTheme: TextButtonThemeData(
              style: TextButton.styleFrom(
                foregroundColor: Colors.white,
                backgroundColor: Color.fromRGBO(22, 22, 22, 1),
              ),
            ),
            datePickerTheme: DatePickerThemeData(
              dayStyle: TextStyle(
                fontWeight: FontWeight.bold,
                color: Colors.white,
              ),
              yearStyle: TextStyle(
                fontWeight: FontWeight.bold,
                color: Colors.white,
              ),
              weekdayStyle: TextStyle(
                fontWeight: FontWeight.bold,
                color: Colors.indigoAccent,
              ),
            ),
            canvasColor: Theme.of(context).primaryColorDark,
          ),
          child: child!,
        );
      },
    );
    if (picked != null) {
      setState(() => _selectedDateOfBirth = picked);
    }
  }

  bool _isLoading = false;

  Future<void> submitRegisterForm() async {
    if (!_formKey.currentState!.validate()) return;

    setState(() => _isLoading = true);

    try {
      final response = await _userService.register({
        'userName': _userName.text.trim(),
        'email': _email.text.trim(),
        'password': _password.text.trim(),
        'confirmPassword': _confirmPassword.text.trim(),
        'name': _name.text.trim(),
        'dateOfBirth': _selectedDateOfBirth,
        'address': _address.text.trim(),
        'city': _city.text.trim(),
        'state': _state.text.trim(),
        'zipCode': _zipCode.text.trim()
      });

      if (response.statusCode == 200 || response.statusCode == 201) {
        // Registration success
        alert.showSuccessToast(context, 'Registration was successful, redirecting to the Dashboard', 'Registration Successful');

        // Navigate to login screen or home
        Navigator.pushReplacementNamed(context, LoginScreen.id);
      } else {
        // Handle unexpected status code
        alert.showErrorToast(context, 'Registration failed, please try again later', 'Registration Failed');
      }
    } on DioException catch (e) {
      String errorMessage = 'Registration failed';
      if (e.response?.data != null && e.response?.data is Map<String, dynamic>) {
        errorMessage = e.response?.data['message'] ?? errorMessage;
      }
      alert.showErrorToast(context, errorMessage, 'Registration Failed');
    } finally {
      setState(() => _isLoading = false);
    }
  }

@override
  void dispose() {
    // TODO: implement dispose
    super.dispose();
    _name.dispose();
    _userName.dispose();
    _email.dispose();
    _password.dispose();
    _confirmPassword.dispose();
    _address.dispose();
    _city.dispose();
    _state.dispose();
    _zipCode.dispose();
  }


  @override
  Widget build(BuildContext context) {
    double screenWidth = MediaQuery.of(context).size.width;
    double heightForm = screenWidth > 600 ? 650 : 300;
    double widthForm = screenWidth > 600 ? 600 : 300;
    double sepWidth = widthForm == 350 ? 200 : 100;
    return Scaffold(
      appBar: CustomAppBar(title: 'CRM: Sign Up'),
      drawer: SideNavDrawer(),
      body: Center(
        child: SizedBox(
          width: widthForm,
          height: heightForm,
          child: SingleChildScrollView(
            child: Card(
              elevation: 10.0,
              shadowColor: Color.fromRGBO(33, 33, 33, 1),
          child: Column(

          children: [
            Row(
              crossAxisAlignment: CrossAxisAlignment.center,
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                SizedBox(height: 30.0),
                Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Text(
                    'Register',
                    textAlign: TextAlign.right,
                    style: TextStyle(
                      color: Colors.indigoAccent,
                      fontSize: 35.0,
                      fontFamily: 'Ubuntu',
                      fontWeight: FontWeight.w700,
                    ),
                  ),
                ),
                SizedBox(width: sepWidth),
                Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: TextButton(
                    onPressed: () {
                      Navigator.pushNamed(context, LoginScreen.id);
                    },
                    child: Text(
                      'Login instead?',
                      textAlign: TextAlign.center,
                      style: TextStyle(
                        color: Colors.green,
                        fontWeight: FontWeight.normal,
                        fontFamily: 'Ubuntu',
                        fontSize: 16.0,
                      ),
                    ),
                  ),
                ),
              ],
            ),
            Form(
            key: _formKey,
                child: Padding(
                  padding: EdgeInsets.all(20.0),
                  child: Column(
                    children: [
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: _userName,
                          decoration: kTextInputUserNameStyle,
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: _email,
                          decoration: kTextInputEmailStyle,
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: _name,
                          decoration: kTextInputNameStyle,
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: _address,
                          decoration: kTextInputAddressStyle,
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: _city,
                          decoration: const InputDecoration(
                            label: Text(
                              'City',
                              style: TextStyle(
                                  color: Colors.white,
                                  fontFamily: 'Ubuntu',
                                  fontWeight: FontWeight.bold,
                                  fontSize: 14.0
                              ),
                            ),
                          ),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: USStateSelector(),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: _zipCode,
                          decoration:kTextInputZipCodeStyle,
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: TextField(
                          onTap: _pickDate,
                          decoration: InputDecoration(
                            label: Text(
                              _selectedDateOfBirth == null
                                  ? "Select Date of Birth"
                                  : DateFormat(
                                    'MM-dd-yyyy',
                                  ).format(_selectedDateOfBirth!),
                              style: const TextStyle(
                                fontFamily: 'Ubuntu',
                                fontSize: 16.0,
                                color: Colors.deepOrange,
                                fontWeight: FontWeight.w700,
                              ),
                            ),
                          ),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: _password,
                          decoration: InputDecoration(
                            label: Text(
                              'Password',
                              style: TextStyle(
                                  color: Colors.white,
                                  fontFamily: 'Ubuntu',
                                  fontWeight: FontWeight.bold,
                                  fontSize: 14.0
                              ),
                            ),
                            enabledBorder: UnderlineInputBorder(
                              borderSide: BorderSide(color: Colors.cyan),
                            ),
                            focusedBorder: UnderlineInputBorder(
                              borderSide: BorderSide(
                                color: Colors.blue,
                                width: 2,
                              ),
                            ),
                            errorBorder: UnderlineInputBorder(
                              borderSide: BorderSide(color: Colors.red),
                            ),
                            suffixIcon: IconButton(
                              icon:
                                  _obscurePassword
                                      ? Icon(
                                        Icons.visibility_off,
                                        color: Colors.green,
                                        size: 16.0,
                                      )
                                      : Icon(
                                        Icons.visibility,
                                        color: Colors.red,
                                        size: 16.0,
                                      ),
                              onPressed: () {
                                setState(() {
                                  _obscurePassword = !_obscurePassword;
                                });
                              },
                            ),
                          ),
                          obscureText: _obscurePassword,
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: _confirmPassword,
                          decoration: InputDecoration(
                            label: Text(
                              'Confirm Password',
                              style: TextStyle(
                                  color: Colors.white,
                                  fontFamily: 'Ubuntu',
                                  fontWeight: FontWeight.bold,
                                  fontSize: 14.0
                              ),
                            ),
                            enabledBorder: UnderlineInputBorder(
                              borderSide: BorderSide(color: Colors.cyan),
                            ),
                            focusedBorder: UnderlineInputBorder(
                              borderSide: BorderSide(
                                color: Colors.blue,
                                width: 2,
                              ),
                            ),
                            errorBorder: UnderlineInputBorder(
                              borderSide: BorderSide(color: Colors.red),
                            ),
                            suffixIcon: IconButton(
                              icon:
                                  _obscureConfirmPassword
                                      ? Icon(
                                        Icons.visibility_off,
                                        color: Colors.green,
                                        size: 16.0,
                                      )
                                      : Icon(
                                        Icons.visibility,
                                        color: Colors.red,
                                        size: 16.0,
                                      ),
                              onPressed: () {
                                setState(() {
                                  _obscureConfirmPassword =
                                      !_obscureConfirmPassword;
                                });
                              },
                            ),
                          ),
                          obscureText: _obscureConfirmPassword,
                        ),
                      ),
                      const SizedBox(height: 20),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: ElevatedButton(
                          onPressed: submitRegisterForm,
                          style: ElevatedButton.styleFrom(
                            foregroundColor: Colors.white,
                            backgroundColor: Colors.indigo,
                            shadowColor: Color.fromRGBO(77, 77, 77, 1),
                            elevation: 5.0,
                            padding: EdgeInsets.symmetric(
                              vertical: 10.0,
                              horizontal: 20.0,
                            ),
                          ),
                          child: Padding(
                              padding: const EdgeInsets.all(8.0),
                              child: Text(
                                "Register",
                                textAlign: TextAlign.center,
                                style: TextStyle(
                                  color: Colors.white,
                                  fontFamily: "Ubuntu",
                                  fontWeight: FontWeight.w700,
                                  fontSize: 22.0,
                                ),
                              ),
                            ),
                          ),
                        ),
                    ],
                  ),
                ),
              ),
              ]
            ),
            ),
          ),
        ),
      ),
    );
  }
}
