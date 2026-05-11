import { useState } from "react";
import { api } from "../../services/api";
import type { Order } from "../../types/order";

import "./styles.css";

export function SearchOrder() {
  const [id, setId] = useState("");
  const [order, setOrder] = useState<Order | null>(null);

  async function handleSearch() {
    const response = await api.get(`/orders/${id}`);

    setOrder(response.data);
  }

  return (
    <div className="search-order">
      <h2>Buscar Pedido</h2>

      <input
        type="text"
        placeholder="ID do Pedido"
        value={id}
        onChange={(e) => setId(e.target.value)}
      />

      <button onClick={handleSearch}>Buscar</button>

      {order && (
        <div className="result">
          <p>
            Cliente:
            <strong> {order.customer}</strong>
          </p>

          <p>
            Valor:
            <strong> R$ {order.value}</strong>
          </p>

          <p>
            Data:
            <strong> {new Date(order.orderDate).toLocaleString()}</strong>
          </p>
        </div>
      )}
    </div>
  );
}
