import {Component, OnInit} from '@angular/core';
import {OidcSecurityService} from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(private oidcSecurityService: OidcSecurityService) {
  }

  async ngOnInit() {
    const isAuthed = await this.oidcSecurityService.checkAuth().toPromise();
    if (!isAuthed) {
      this.login();
    }
  }

  login() {
    this.oidcSecurityService.authorize();
  }
}
