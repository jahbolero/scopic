import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddProductComponent } from './components/add-product/add-product.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductComponent } from './components/product/product.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { AuthGuard } from './helpers/auth.guard';
import { Role } from './models/role';

const routes: Routes = [
    {
        path: '',
        component: ProductListComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.User] }
    },
    {
        path: 'products',
        component: ProductListComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'product/:productId',
        component: ProductComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.User] }
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'addProduct',
        component: AddProductComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.Admin] }
    },
    {
        path: 'editProduct/:productId',
        component: AddProductComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.Admin] }
    },
    {
        path: 'admin',
        component: AdminDashboardComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.Admin] }
    },
    {
        path: 'profile',
        component: UserProfileComponent,
        canActivate: [AuthGuard],
        data: { roles: [Role.User] }
    },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }