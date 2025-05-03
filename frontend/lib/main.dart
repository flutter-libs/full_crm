import 'package:flutter/material.dart';
import 'package:frontend/screens/dashboard_screen.dart';
import 'package:frontend/screens/home_screen.dart';
import 'package:frontend/screens/login_screen.dart';
import 'package:frontend/screens/register_screen.dart';
import 'package:toastification/toastification.dart';

void main() {
  runApp(CRMFrontend());
}

class CRMFrontend extends StatelessWidget {
  const CRMFrontend({super.key});

  @override
  Widget build(BuildContext context) {
    return ToastificationWrapper(
        config: ToastificationConfig(
          maxTitleLines: 2,
          maxDescriptionLines: 6,
          marginBuilder: (context, alignment) => const EdgeInsets.fromLTRB(0, 16, 0, 110),
        ),
        child: MaterialApp(
          theme: ThemeData.dark(),
          initialRoute: HomeScreen.id,
          routes: {
            HomeScreen.id: (context) => HomeScreen(),
            LoginScreen.id: (context) => LoginScreen(),
            RegisterScreen.id: (context) => RegisterScreen(),
            DashboardScreen.id: (context) => DashboardScreen(),
          },
        )
    );
  }
}
