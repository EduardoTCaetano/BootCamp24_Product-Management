import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ProductService } from './../../services/product.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edit-product',
  standalone: true,
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css'],
  imports: [CommonModule, ReactiveFormsModule]
})
export class EditProductComponent implements OnInit {
  productForm: FormGroup;
  productId: string;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.productForm = this.fb.group({
      id: [''],
      name: [''],
      description: [''],
      price: [''],
    });

    this.productId = this.route.snapshot.paramMap.get('id')!;
  }

  ngOnInit(): void {
    this.productService.getProductById(this.productId).subscribe({
      next: (product) => {
        this.productForm.patchValue(product);
      },
      error: (err) => {
        console.error('Erro ao carregar o produto:', err);
      },
    });
  }

  onSubmit(): void {
    this.productService.updateProduct(this.productForm.value).subscribe({
      next: () => {
        Swal.fire({
          title: 'Sucesso!',
          text: 'Produto atualizado com sucesso!',
          icon: 'success',
          confirmButtonText: 'OK'
        }).then(() => {
          this.router.navigate(['/admin/products']);
        });
      },
      error: (err) => {
        console.error('Erro ao atualizar produto:', err);
        Swal.fire({
          title: 'Erro!',
          text: 'Falha ao atualizar o produto.',
          icon: 'error',
          confirmButtonText: 'OK'
        });
      },
    });
  }
}
