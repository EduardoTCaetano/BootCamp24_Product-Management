import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { ProductService } from './../../services/product.service';
import { Product } from './../../models/product.model';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-product-list',
  standalone: true,
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
  imports: [CommonModule, RouterModule]
})
export class ProductListComponent implements OnInit {
  products?: Product[];

  constructor(private productService: ProductService, private router: Router) {}

  ngOnInit(): void {
    this.productService.getProducts().subscribe({
      next: (response) => {
        this.products = response;
      },
      error: (err) => {
        console.error('Erro ao carregar o produto:', err);
      },
    });
  }

  editProduct(product: Product): void {
    this.router.navigate(['/admin/products/edit', product.id]);
  }

  deactivateProduct(productId: string): void {
    Swal.fire({
      title: 'Tem certeza de que deseja desativar este produto?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sim, desativar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.productService.deactivateProduct(productId).subscribe({
          next: () => {
            const productIndex = this.products?.findIndex((product) => product.id === productId);
            if (productIndex !== undefined && productIndex > -1) {
              this.products![productIndex].isActive = false;
            }
            Swal.fire('Desativado!', 'Produto desativado com sucesso!', 'success');
          },
          error: (err) => {
            console.error('Erro ao desativar o produto:', err);
            Swal.fire('Erro!', 'Falha ao desativar o produto.', 'error');
          },
        });
      }
    });
  }

  activateProduct(productId: string): void {
    Swal.fire({
      title: 'Tem certeza de que deseja ativar este produto?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#28a745',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Sim, ativar!',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.productService.activateProduct(productId).subscribe({
          next: () => {
            const productIndex = this.products?.findIndex((product) => product.id === productId);
            if (productIndex !== undefined && productIndex > -1) {
              this.products![productIndex].isActive = true;
            }
            Swal.fire('Ativado!', 'Produto ativado com sucesso!', 'success');
          },
          error: (err) => {
            console.error('Erro ao ativar o produto:', err);
            Swal.fire('Erro!', 'Falha ao ativar o produto.', 'error');
          },
        });
      }
    });
  }
}
