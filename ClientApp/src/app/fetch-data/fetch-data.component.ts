import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  private users: User[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private oidcSecurityService: OidcSecurityService) {
    http.get<User[]>(baseUrl + 'user', {
      headers: {
        Authorization: `Bearer ${this.oidcSecurityService.getToken()}`,
      },
    }).subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }

  ngOnInit(): void {
    //TODO: shoudn't the constructor logic really happen here?
  }
}

interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  territoryId: string;
  role: string;
  isEnabled: boolean;
}
