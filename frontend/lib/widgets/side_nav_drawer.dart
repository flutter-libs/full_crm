import 'package:flutter/material.dart';
import 'package:frontend/screens/lead/lead_create_screen.dart';
import 'package:frontend/screens/lead/lead_list_screen.dart';
import 'package:frontend/screens/login_screen.dart';
import 'package:frontend/screens/meeting/meeting_create_screen.dart';
import 'package:frontend/screens/meeting/meeting_list_screen.dart';
import 'package:frontend/screens/register_screen.dart';

class SideNavDrawer extends StatelessWidget {
  const SideNavDrawer({super.key});

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        children: [
          // Drawer Header
          DrawerHeader(
            decoration: BoxDecoration(color: Colors.blue),
            child: Text(
              'CRM',
              style: TextStyle(color: Colors.white, fontSize: 24),
            ),
          ),
          ExpansionTile(
            leading: Icon(Icons.lock_clock_outlined),
            title: Text('Authentication'),
            children: [
              // Home link
              ListTile(
                leading: Icon(Icons.app_registration),
                title: Text('Register'),
                onTap: () {
                  Navigator.pushNamed(context, RegisterScreen.id);
                },
              ),
              ListTile(
                leading: Icon(Icons.app_registration),
                title: Text('Login'),
                onTap: () {
                  Navigator.pushNamed(context, LoginScreen.id);
                },
              ),
            ],
          ),
          // Dropdown Section for Leads
          ExpansionTile(
            leading: Icon(Icons.leaderboard),
            title: Text('Leads'),
            children: [
              ListTile(
                leading: Icon(Icons.list),
                title: Text('All Leads'),
                onTap: () => Navigator.pushNamed(context, LeadListScreen.id),
              ),
              ListTile(
                leading: Icon(Icons.add),
                title: Text('Add Lead'),
                onTap: () => Navigator.pushNamed(context, LeadCreateScreen.id),
              ),
            ],
          ),

          // Dropdown Section for Campaigns
          ExpansionTile(
            leading: Icon(Icons.campaign),
            title: Text('Meetings'),
            children: [
              ListTile(
                title: Text('All Meetings'),
                onTap: () => Navigator.pushNamed(context, MeetingListScreen.id),
              ),
              ListTile(
                title: Text('Add Meeting'),
                onTap:
                    () => Navigator.pushNamed(context, MeetingCreateScreen.id),
              ),
            ],
          ),

          // Settings
          ListTile(
            leading: Icon(Icons.settings),
            title: Text('Settings'),
            onTap: () => Navigator.pushNamed(context, '/settings'),
          ),
        ],
      ),
    );
  }
}
