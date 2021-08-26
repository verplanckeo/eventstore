import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './features/home';
import { AuthGuard } from './decorator'
import { UsersModule } from './features/users/users.module';
import { VideoModule } from './features/video/video.module';
import { TicketsModule } from './features/tickets/tickets.module';

// lazy loading modules
const accountModule = () => import('./features/account/account.module').then(x => x.AccountModule);
const usersModule = () => import('./features/users/users.module').then(x => UsersModule);
const videoModule = () => import('./features/video/video.module').then(x => VideoModule);
const ticketsModule = () => import('./features/tickets/tickets.module').then(x => TicketsModule);

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'users', loadChildren: usersModule, canActivate: [AuthGuard] },
  { path: 'account', loadChildren: accountModule },
  { path: 'video', loadChildren: videoModule },
  { path: 'tickets', loadChildren: ticketsModule, canActivate: [AuthGuard] },

  // any other path redirects to home
  { path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
