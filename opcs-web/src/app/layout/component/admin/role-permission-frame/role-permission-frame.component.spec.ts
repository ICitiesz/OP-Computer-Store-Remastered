import {ComponentFixture, TestBed} from '@angular/core/testing';

import {RolePermissionFrameComponent} from './role-permission-frame.component';

describe('RolePermissionFrameComponent', () => {
	let component: RolePermissionFrameComponent;
	let fixture: ComponentFixture<RolePermissionFrameComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [RolePermissionFrameComponent]
		})
			.compileComponents();

		fixture = TestBed.createComponent(RolePermissionFrameComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
