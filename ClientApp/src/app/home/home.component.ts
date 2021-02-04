import { Component, OnDestroy, OnInit } from '@angular/core';
import { OidcClientNotification, OidcSecurityService, PublicConfiguration } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent  implements OnInit {
  constructor(public oidcSecurityService: OidcSecurityService) {}

  ngOnInit() {
      this.oidcSecurityService.checkAuth().subscribe((auth) => console.log('is authenticated', auth));
  }

  login() {
      this.oidcSecurityService.authorize();
  }
  
  logout() {
      this.oidcSecurityService.logoff();
  }
}
