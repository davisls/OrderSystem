import { useEffect, useState } from "react";

import { Header } from "../../components/Header";
import { OrderForm } from "../../components/OrderForm";
import { OrderCard } from "../../components/OrderCard";
import { SearchOrder } from "../../components/SearchOrder";

import { api } from "../../services/api";
import type { Order } from "../../types/order";

import "./styles.css";

export function Home() {
  const [orders, setOrders] = useState<Order[]>([]);

  async function loadOrders() {
    const response = await api.get("/orders");

    setOrders(response.data);
  }

  useEffect(() => {
    async function fetchOrders() {
      const response = await api.get("/orders");

      setOrders(response.data);
    }

    fetchOrders();
  }, []);

  return (
    <>
      <Header />

      <main className="container">
        <div className="left-side">
          <OrderForm onCreated={loadOrders} />

          <SearchOrder />
        </div>

        <div className="right-side">
          <h2>Pedidos</h2>

          <div className="orders-grid">
            {orders.map((order) => (
              <OrderCard key={order.id} order={order} />
            ))}
          </div>
        </div>
      </main>
    </>
  );
}
