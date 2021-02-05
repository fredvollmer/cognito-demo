import { Component } from '@angular/core';
import {OidcSecurityService} from 'angular-auth-oidc-client';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private oidcSecurityService: OidcSecurityService) {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.oidcSecurityService.logoff(endSessionUrl => {
      location.href = `https://icarus.auth.us-east-1.amazoncognito.com/logout?client_id=2vmjrkudt1srocep4p3f7ug5c0&logout_uri=${location.origin}`;
    });
  }
}
