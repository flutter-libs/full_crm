import 'package:flutter/material.dart';
import 'package:frontend/screens/campaign/campaign_create_screen.dart';
import 'package:frontend/screens/campaign/campaign_list_screen.dart';
import 'package:frontend/screens/contact/contact_create_screen.dart';
import 'package:frontend/screens/contact/contact_list_screen.dart';
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
      elevation: 8.0,
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
          ExpansionTile(
            leading: Icon(Icons.campaign),
            title: Text('Campaigns'),
            children: [
              ListTile(
                leading: Icon(Icons.list),
                title: Text('All Campaigns'),
                onTap: () => Navigator.pushNamed(context, CampaignListScreen.id),
              ),
              ListTile(
                leading: Icon(Icons.add),
                title: Text('Add Campaign'),
                onTap: () => Navigator.pushNamed(context, CampaignCreateScreen.id),
              ),
            ],
          ),
          // Dropdown Section for Campaigns
          ExpansionTile(
            leading: Icon(Icons.meeting_room),
            title: Text('Meetings'),
            children: [
              ListTile(
                leading: Icon(Icons.list),
                title: Text('All Meetings'),
                onTap: () => Navigator.pushNamed(context, MeetingListScreen.id),
              ),
              ListTile(
                leading: Icon(Icons.add),
                title: Text('Add Meeting'),
                onTap:
                    () => Navigator.pushNamed(context, MeetingCreateScreen.id),
              ),
            ],
          ),

          ExpansionTile(
            leading: Icon(Icons.contact_page_outlined),
            title: Text('Contacts'),
            children: [
              ListTile(
                leading: Icon(Icons.list),
                title: Text('All Contacts'),
                onTap: () => Navigator.pushNamed(context, ContactListScreen.id),
              ),
              ListTile(
                leading: Icon(Icons.add),
                title: Text('Add Contact'),
                onTap:
                    () => Navigator.pushNamed(context, ContactCreateScreen.id),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
